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
    }
}
