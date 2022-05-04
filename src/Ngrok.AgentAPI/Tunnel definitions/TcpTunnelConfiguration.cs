// Copyright (c) 2022 Quetzal Rivera.
// Licensed under the MIT License, See LICENCE in the project root for license information.

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;
using Ngrok.AgentAPI.Abstractions;
using System.Runtime.InteropServices;

namespace Ngrok.AgentAPI
{
    /// <summary>This object represents the TCP tunnel definitions.</summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public sealed class TcpTunnelConfiguration : TunnelConfiguration
    {
        private const string _PROTO_ = "tcp";
        /// <summary>
        /// Initialize a new instance of <see cref="TcpTunnelConfiguration"/>.
        /// </summary>
        /// <param name="name">Tunnel name.</param>
        /// <param name="addr">Forward traffic to this local port number or network address. This can be just a port (</param>
        /// <param name="metadata">Optional. Arbitrary user-defined metadata that will appear in the ngrok service API when listing tunnel sessions.</param>
        public TcpTunnelConfiguration(string name, string addr, [Optional] string metadata) : base(name, addr, metadata)
        {
        }

        /// <summary>
        /// Ip restriction configuration.
        /// </summary>
        [JsonPropertyName(PropertyNames.IpRestriction)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IpRestriction IpRestriction { get; set; }
        /// <summary>The tunnel protocol name. This defines the type of tunnel you would like to start.</summary>
        [JsonPropertyName(PropertyNames.Proto)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Proto => _PROTO_;
        /// <summary>The version of PROXY protocol to use with this tunnel, empty if not using. Example values are 1 or 2.</summary>
        [JsonPropertyName(PropertyNames.ProxyProto)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ProxyProto { get; set; }
        /// <summary>bind the remote TCP address and port. These addresses can be reserved in the ngrok dashboard to use across sessions. For example: <br />
        /// <code>2.tcp.ngrok.io:21746</code></summary>
        [JsonPropertyName(PropertyNames.RemoteAddr)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string RemoteAddr { get; set; }     
    }
}
