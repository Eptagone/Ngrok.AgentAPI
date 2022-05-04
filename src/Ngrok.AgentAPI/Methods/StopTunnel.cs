// Copyright (c) 2022 Quetzal Rivera.
// Licensed under the MIT License, See LICENCE in the project root for license information.

using System;
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
    }
}
