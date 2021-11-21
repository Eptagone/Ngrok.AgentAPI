// Copyright (c) 2021 Quetzal Rivera.
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
    public sealed class NgrokAgentClient
    {
        internal static readonly JsonSerializerOptions DefaultSerializerOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        private const string applicationJson = "application/json";

        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        /// <summary>Initialize a new instance of Agent Client client</summary>
        /// <param name="baseUrl">Optional. The base URL for API requests. If you override <b>web_addr</b> in your configuration file, specify the new URL. Otherwise, ignore this parameter.</param>
        /// <param name="httpClient">Optional. Custom HttpClient for api requests.</param>
        public NgrokAgentClient(string baseUrl = "http://127.0.0.1:4040/api", HttpClient httpClient = default)
        {
            _baseUrl = baseUrl;
            if (httpClient == default)
            {
                if (DefaultHttpClient == default)
                {
                    SetDefaultHttpClient(new HttpClient());
                }
                _httpClient = DefaultHttpClient;
            }
            else
            {
                _httpClient = AddJson(httpClient);
            }
        }

        /// <summary>Default HttpClient for api requets.</summary>
        public static HttpClient DefaultHttpClient { get; private set; }


        #region API Methods
        /// <summary>Returns a list of running tunnels with status and metrics information.</summary>
        /// <returns>List of all running tunnels. See the Tunnel detail resource for docs on the parameters of each tunnel object.</returns>
        public TunnelListResource ListTunnels()
        {
            return GetRequest<TunnelListResource>(PathNames.Tunnels);
        }

        /// <summary>Returns a list of running tunnels with status and metrics information.</summary>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>List of all running tunnels. See the Tunnel detail resource for docs on the parameters of each tunnel object.</returns>
        public async Task<TunnelListResource> ListTunnelsAsync([Optional] CancellationToken cancellationToken)
        {
            return await GetRequestAsync<TunnelListResource>(PathNames.Tunnels, cancellationToken: cancellationToken);
        }

        /// <summary>Dynamically starts a new tunnel on the ngrok client. The request body parameters are the same as those you would use to define the tunnel in the configuration file.</summary>
        /// <param name="configuration">Parameter names and behaviors are identical to those those defined in the configuration file. Use the tunnel definitions section as a reference for configuration parameters and their behaviors.</param>
        /// <returns>A response body describing the started tunnel.</returns>
        public TunnelDetail StartTunnel(TunnelConfiguration configuration)
        {
            return PostRequest<TunnelDetail>(PathNames.Tunnels, configuration);
        }

        /// <summary>Dynamically starts a new tunnel on the ngrok client. The request body parameters are the same as those you would use to define the tunnel in the configuration file.</summary>
        /// <param name="configuration">Parameter names and behaviors are identical to those those defined in the configuration file. Use the tunnel definitions section as a reference for configuration parameters and their behaviors.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>A response body describing the started tunnel.</returns>
        public async Task<TunnelDetail> StartTunnelAsync(TunnelConfiguration configuration, [Optional] CancellationToken cancellationToken)
        {
            return await PostRequestAsync<TunnelDetail>(PathNames.Tunnels, configuration, cancellationToken: cancellationToken);
        }

        /// <summary>Get status and metrics about the named running tunnel.</summary>
        /// <param name="name">Tunnel name</param>
        /// <returns>List of all running tunnels. See the Tunnel detail resource for docs on the parameters of each tunnel object.</returns>
        public TunnelDetail TunnelDetail(string name)
        {
            return GetRequest<TunnelDetail>($"{PathNames.Tunnels}/{name}");
        }

        /// <summary>Get status and metrics about the named running tunnel.</summary>
        /// <param name="name">Tunnel name</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>List of all running tunnels. See the Tunnel detail resource for docs on the parameters of each tunnel object.</returns>
        public async Task<TunnelDetail> TunnelDetailAsync(string name, [Optional] CancellationToken cancellationToken)
        {
            return await GetRequestAsync<TunnelDetail>($"{PathNames.Tunnels}/{name}", cancellationToken: cancellationToken);
        }

        /// <summary>Stop a running tunnel.</summary>
        /// <param name="name">Tunnel name</param>
        public void StopTunnel(string name)
        {
            try
            {
                _httpClient.DeleteAsync($"{_baseUrl}{PathNames.Tunnels}/{name}").Wait();
            }
            catch (AggregateException e)
            {
                throw e.InnerException;
            }
        }

        /// <summary>Stop a running tunnel.</summary>
        /// <param name="name">Tunnel name</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        public async Task StopTunnelAsync(string name, [Optional] CancellationToken cancellationToken)
        {
            await _httpClient.DeleteAsync($"{_baseUrl}{PathNames.Tunnels}/{name}", cancellationToken);
        }

        /// <summary>Returns a list of all HTTP requests captured for inspection. This will only return requests that are still in memory (ngrok evicts captured requests when their memory usage exceeds <b>inspect_db_size</b>).</summary>
        /// <param name="limit">maximum number of requests to return</param>
        /// <param name="tunnelName">filter requests only for the given tunnel name</param>
        /// <returns>A list of captured requests. See the Captured Request Detail resource for docs on the request objects.</returns>
        public RequestListResource ListCapturedRequests([Optional] uint? limit, [Optional] string tunnelName)
        {
            return GetRequest<RequestListResource>($"{PathNames.Requests}{PathNames.Http}", new Tuple<string, object>[] { new Tuple<string, object>(PropertyNames.Limit, limit), new Tuple<string, object>(PropertyNames.TunnelName, tunnelName) });
        }

        /// <summary>Returns a list of all HTTP requests captured for inspection. This will only return requests that are still in memory (ngrok evicts captured requests when their memory usage exceeds <b>inspect_db_size</b>).</summary>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>A list of captured requests. See the Captured Request Detail resource for docs on the request objects.</returns>
        public async Task<RequestListResource> ListCapturedRequestsAsync([Optional] uint? limit, [Optional] string tunnelName, [Optional] CancellationToken cancellationToken)
        {
            return await GetRequestAsync<RequestListResource>($"{PathNames.Requests}{PathNames.Http}", new Tuple<string, object>[] { new Tuple<string, object>(PropertyNames.Limit, limit), new Tuple<string, object>(PropertyNames.TunnelName, tunnelName) }, cancellationToken);
        }

        /// <summary>Replays a request against the local endpoint of a tunnel.</summary>
        /// <param name="id">Id of request to replay</param>
        /// <param name="tunnelName">Name of the tunnel to play the request against. If unspecified, the request is played against the same tunnel it was recorded on.</param>
        public void ReplayCapturedRequest(string id, string tunnelName)
        {
            try
            {
                ReplayCapturedRequestAsync(id, tunnelName).Wait();
            }
            catch (AggregateException e)
            {
                throw e.InnerException;
            }
        }

        /// <summary>Replays a request against the local endpoint of a tunnel.</summary>
        /// <param name="id">Id of request to replay</param>
        /// <param name="tunnelName">Name of the tunnel to play the request against. If unspecified, the request is played against the same tunnel it was recorded on.</returns>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        public async Task ReplayCapturedRequestAsync(string id, string tunnelName, [Optional] CancellationToken cancellationToken)
        {
            var stream = new MemoryStream();
            using var json = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true });
            json.WriteStartObject();
            json.WriteString(PropertyNames.Id, id);
            json.WriteString(PropertyNames.TunnelName, tunnelName);
            json.WriteEndObject();
            json.Flush(); json.Dispose();
            stream.Seek(0, SeekOrigin.Begin);
            await PostRequestAsync($"{PathNames.Requests}{PathNames.Http}", stream, cancellationToken);
            // Response: 204 status code with an empty body
        }

        /// <summary>Stop a running tunnel.</summary>
        public void DeleteCapturedRequests()
        {
            try
            {

                _httpClient.DeleteAsync($"{_baseUrl}{PathNames.Requests}{PathNames.Http}").Wait();
            }
            catch (AggregateException e)
            {
                throw e.InnerException;
            }
        }

        /// <summary>Deletes all captured requests.</summary>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        public async Task DeleteCapturedRequestsAsync([Optional] CancellationToken cancellationToken)
        {
            await _httpClient.DeleteAsync($"{_baseUrl}{PathNames.Requests}{PathNames.Http}", cancellationToken);
            // Response: 204 status code with an empty body
        }

        /// <summary>Returns metadata and raw bytes of a captured request. The raw data is base64-encoded in the JSON response. The response value maybe null if the local server has not yet responded to a request.</summary>
        /// <returns>List of all running tunnels. See the Tunnel detail resource for docs on the parameters of each tunnel object.</returns>
        public TunnelDetail CapturedRequestDetail(string requestId)
        {
            return GetRequest<TunnelDetail>($"{PathNames.Requests}{PathNames.Http}/{requestId}");
        }

        /// <summary>Returns metadata and raw bytes of a captured request. The raw data is base64-encoded in the JSON response. The response value maybe null if the local server has not yet responded to a request.</summary>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>List of all running tunnels. See the Tunnel detail resource for docs on the parameters of each tunnel object.</returns>
        public async Task<TunnelDetail> CapturedRequestDetailAsync(string requestId, [Optional] CancellationToken cancellationToken)
        {
            return await GetRequestAsync<TunnelDetail>($"{PathNames.Requests}{PathNames.Http}/{requestId}", cancellationToken: cancellationToken);
        }

        #endregion

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
        private TResult GetRequest<TResult>(string path, [Optional] Tuple<string, object>[] args)
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
        private TResult PostRequest<TResult>(string path, object args, [Optional] JsonSerializerOptions serializeOptions)
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
        private async Task<TResult> GetRequestAsync<TResult>(string path, [Optional] Tuple<string, object>[] args, [Optional] CancellationToken cancellationToken)
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
        private async Task<TResult> PostRequestAsync<TResult>(string path, object args, [Optional] JsonSerializerOptions serializeOptions, [Optional] CancellationToken cancellationToken)
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
        private async Task<TResult> PostRequestAsync<TResult>(string path, Stream args, [Optional] CancellationToken cancellationToken)
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
        private async Task PostRequestAsync(string path, Stream args, [Optional] CancellationToken cancellationToken)
        {
            var _path = $"{_baseUrl}{path}";
            using var request = new HttpRequestMessage(HttpMethod.Post, _path)
            {
                Content = new StreamContent(args)
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(applicationJson);
            await SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
        }

        private static HttpClient AddJson(HttpClient client)
        {
            if (!client.DefaultRequestHeaders.Accept.Any(u => u.MediaType == applicationJson))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(applicationJson));
            }
            return client;
        }

        private static async Task<Stream> SerializeAsStreamAsync(object args, [Optional] JsonSerializerOptions options, [Optional] CancellationToken cancellationToken)
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

        private async Task<TResult> SendRequestAsync<TResult>(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var streamResponse = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            return await JsonSerializer.DeserializeAsync<TResult>(streamResponse, cancellationToken: cancellationToken);
        }

        private async Task SendRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }
    }
}
