// Copyright (c) 2022 Quetzal Rivera.
// Licensed under the MIT License, See LICENCE in the project root for license information.

using System;
using System.Runtime.InteropServices;
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
    }
}
