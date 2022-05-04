// Copyright (c) 2022 Quetzal Rivera.
// Licensed under the MIT License, See LICENCE in the project root for license information.

using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Ngrok.AgentAPI.Abstractions;

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
        /// <summary>Dynamically starts a new tunnel on the ngrok client. The request body parameters are the same as those you would use to define the tunnel in the configuration file.</summary>
        /// <param name="configuration">
        /// Tunnel configurtion. Can be one of: <br />
        /// <list type="bullet">
        /// <item><see cref="HttpTunnelConfiguration"/></item>
        /// <item><see cref="TcpTunnelConfiguration"/></item>
        /// <item><see cref="TlsTunnelConfiguration"/></item>
        /// <item><see cref="LabeledTunnelConfiguration"/></item>
        /// </list>
        /// </param>
        /// <returns>A response body describing the started tunnel.</returns>
        public TunnelDetail StartTunnel<TConfiguration>(TConfiguration configuration)
            where TConfiguration : TunnelConfiguration
        {
            return PostRequest<TunnelDetail>(PathNames.Tunnels, configuration);
        }

        /// <summary>Dynamically starts a new tunnel on the ngrok client. The request body parameters are the same as those you would use to define the tunnel in the configuration file.</summary>
        /// <param name="configuration">
        /// Tunnel configurtion. Can be one of: <br />
        /// <list type="bullet">
        /// <item><see cref="HttpTunnelConfiguration"/></item>
        /// <item><see cref="TcpTunnelConfiguration"/></item>
        /// <item><see cref="TlsTunnelConfiguration"/></item>
        /// <item><see cref="LabeledTunnelConfiguration"/></item>
        /// </list>
        /// </param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>A response body describing the started tunnel.</returns>
        public async Task<TunnelDetail> StartTunnelAsync<TConfiguration>(TConfiguration configuration, [Optional] CancellationToken cancellationToken)
            where TConfiguration : TunnelConfiguration
        {
            return await PostRequestAsync<TunnelDetail>(PathNames.Tunnels, configuration, cancellationToken: cancellationToken);
        }
    }
}
