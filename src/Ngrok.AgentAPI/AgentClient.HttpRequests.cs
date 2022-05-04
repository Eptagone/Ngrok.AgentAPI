// Copyright (c) 2022 Quetzal Rivera.
// Licensed under the MIT License, See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Ngrok.AgentAPI
{
    /// <summary>This object represents the connection to the ngrok agent API that grants access to:
    /// <list type="bullet">
    /// <item>Collect status and metrics information</item>
    /// <item>Collect and replay captured requests</item>
    /// <item>Start and stop tunnels dynamically</item>
    /// </list>
    /// </summary>
    public sealed partial class NgrokAgentClient
    {
        internal static readonly JsonSerializerOptions DefaultSerializerOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        private const string applicationJson = "application/json";

        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        /// <summary>Default HttpClient for api requets.</summary>
        public static HttpClient DefaultHttpClient { get; private set; }

        /// <summary>Set the default HttpClient for api requets.</summary>
        /// <param name="client"><see cref="HttpClient"/> for http requets.</param>
        /// <exception cref="ArgumentNullException">Thrown when client is null.</exception>
        public static void SetDefaultHttpClient(HttpClient client)
        {
            DefaultHttpClient = client ?? new HttpClient();
            AddJson(DefaultHttpClient);
        }

        /// <summary>Makes a api request using HTTP GET and returns the response.</summary>
        /// <typeparam name="T">Response type.</typeparam>
        /// <param name="path">path name. See <see cref="PathNames"/></param>
        /// <returns><see cref="TResult"/></returns>
        /// <exception cref="HttpRequestException"/>
        internal TResult GetRequest<TResult>(string path, [Optional] Tuple<string, object>[] args)
        {
            try
            {
                return GetRequestAsync<TResult>(path, args).Result;
            }
            catch (AggregateException e)
            {
                throw e.InnerException;
            }
        }

        /// <summary>Makes a api request using HTTP POST and returns the response.</summary>
        /// <typeparam name="T">Response type.</typeparam>
        /// <param name="path">path name. See <see cref="PathNames"/></param>
        /// <param name="args">json parameters</param>
        /// <param name="serializeOptions">Options to control serialization behavior.</param>
        /// <returns><see cref="TResult"/></returns>
        internal TResult PostRequest<TResult>(string path, object args, [Optional] JsonSerializerOptions serializeOptions)
        {
            try
            {
                return PostRequestAsync<TResult>(path, args, serializeOptions).Result;
            }
            catch (AggregateException e)
            {
                throw e.InnerException;
            }
        }

        /// <summary>Makes a api request using HTTP GET and returns the response.</summary>
        /// <typeparam name="T">Response type.</typeparam>
        /// <param name="path">path name. See <see cref="PathNames"/></param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns><see cref="TResult"/></returns>
        /// <exception cref="HttpRequestException"/>
        internal async Task<TResult> GetRequestAsync<TResult>(string path, [Optional] Tuple<string, object>[] args, [Optional] CancellationToken cancellationToken)
        {
            var _path = $"{_baseUrl}{path}";
            if (args != default && args.Any())
            {
                var @params = new List<string>();
                foreach (var arg in args)
                {
                    var value = arg.Item2 is string ? HttpUtility.UrlEncode(arg.Item2 as string) : arg.Item2.ToString();
                    var param = $"{arg.Item1}={value}";
                    @params.Add(param);
                }
                _path += $"?{string.Join('&', @params)}";
            }
            using var request = new HttpRequestMessage(HttpMethod.Get, _path);
            return await SendRequestAsync<TResult>(request, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Makes a api request using HTTP POST and returns the response.</summary>
        /// <typeparam name="T">Response type.</typeparam>
        /// <param name="path">path name. See <see cref="PathNames"/></param>
        /// <param name="args">json parameters</param>
        /// <param name="serializeOptions">Options to control serialization behavior.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns><see cref="TResult"/></returns>
        internal async Task<TResult> PostRequestAsync<TResult>(string path, object args, [Optional] JsonSerializerOptions serializeOptions, [Optional] CancellationToken cancellationToken)
        {
            if (args == default)
            {
                throw new ArgumentException(null, nameof(args));
            }
            if (serializeOptions == default)
            {
                serializeOptions = DefaultSerializerOptions;
            }
            var stream = await SerializeAsStreamAsync(args, serializeOptions, cancellationToken)
                .ConfigureAwait(false);
            return await PostRequestAsync<TResult>(path, stream, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Makes a api request using HTTP POST and returns the response.</summary>
        /// <typeparam name="T">Response type.</typeparam>
        /// <param name="path">path name.</param>
        /// <param name="args">json parameters</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns><see cref="TResult"/></returns>
        internal async Task<TResult> PostRequestAsync<TResult>(string path, Stream args, [Optional] CancellationToken cancellationToken)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}{path}")
            {
                Content = new StreamContent(args)
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(applicationJson);
            return await SendRequestAsync<TResult>(request, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Makes a api request using HTTP POST and returns the response.</summary>
        /// <typeparam name="T">Response type.</typeparam>
        /// <param name="path">path name.</param>
        /// <param name="args">json parameters</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <exception cref="HttpRequestException"/>
        internal async Task PostRequestAsync(string path, Stream args, [Optional] CancellationToken cancellationToken)
        {
            var _path = $"{_baseUrl}{path}";
            using var request = new HttpRequestMessage(HttpMethod.Post, _path)
            {
                Content = new StreamContent(args)
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(applicationJson);
            await SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
        }

        internal static HttpClient AddJson(HttpClient client)
        {
            if (!client.DefaultRequestHeaders.Accept.Any(u => u.MediaType == applicationJson))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(applicationJson));
            }
            return client;
        }

        internal static async Task<Stream> SerializeAsStreamAsync(object args, [Optional] JsonSerializerOptions options, [Optional] CancellationToken cancellationToken)
        {
            if (options == default)
            {
                options = DefaultSerializerOptions;
            }
            var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, args, args.GetType(), options, cancellationToken).ConfigureAwait(false);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        internal async Task<TResult> SendRequestAsync<TResult>(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
            var res = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            var streamResponse = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            return await JsonSerializer.DeserializeAsync<TResult>(streamResponse, cancellationToken: cancellationToken);
        }

        internal async Task SendRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }
    }
}
