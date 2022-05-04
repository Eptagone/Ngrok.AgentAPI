// Copyright (c) 2022 Quetzal Rivera.
// Licensed under the MIT License, See LICENCE in the project root for license information.

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
    }
}
