// Copyright (c) 2022 Quetzal Rivera.
// Licensed under the MIT License, See LICENCE in the project root for license information.

using System;
using System.IO;
using System.Net.Http;
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
    }
}
