// Copyright (c) 2022 Quetzal Rivera.
// Licensed under the MIT License, See LICENCE in the project root for license information.

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

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
    }
}
