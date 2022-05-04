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
        /// <summary>Deletes all captured requests.</summary>
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
    }
}
