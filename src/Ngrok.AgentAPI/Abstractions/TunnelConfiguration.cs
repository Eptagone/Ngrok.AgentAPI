// Copyright (c) 2022 Quetzal Rivera.
// Licensed under the MIT License, See LICENCE in the project root for license information.

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace Ngrok.AgentAPI.Abstractions
{
    /// <summary>This object represents the tunnel definitions. Every tunnel must define proto and addr. Other properties are available and many are protocol-specific.</summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public abstract class TunnelConfiguration
    {
        /// <summary>
        /// Initialize a new instance of <see cref="TunnelConfiguration"/>.
        /// </summary>
        /// <param name="name">Tunnel name.</param>
        /// <param name="addr">Forward traffic to this local port number or network address. This can be just a port (</param>
        /// <param name="metadata">Optional. Arbitrary user-defined metadata that will appear in the ngrok service API when listing tunnel sessions.</param>
        public TunnelConfiguration(string name, string addr, [Optional] string metadata)
        {
            Name = name;
            Addr = addr;
            Metadata = metadata;
        }

        /// <summary>Tunnel name.</summary>
        [JsonPropertyName(PropertyNames.Name)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Name { get; }
        /// <summary>Forward traffic to this local port number or network address. This can be just a port (</summary>
        [JsonPropertyName(PropertyNames.Addr)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Addr { get; set; }
        /// <summary>Arbitrary user-defined metadata that will appear in the ngrok service API when listing tunnel sessions.</summary>
        [JsonPropertyName(PropertyNames.Metadata)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Metadata { get; }
    }
}
