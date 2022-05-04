// Copyright (c) 2022 Quetzal Rivera.
// Licensed under the MIT License, See LICENCE in the project root for license information.

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;
using Ngrok.AgentAPI.Abstractions;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Ngrok.AgentAPI
{
    /// <summary>This object represents the Labeled tunnel definitions.</summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public sealed class LabeledTunnelConfiguration : TunnelConfiguration
    {
        /// <summary>
        /// Initialize a new instance of <see cref="LabeledTunnelConfiguration"/>.
        /// </summary>
        /// <param name="name">Tunnel name.</param>
        /// <param name="addr">Forward traffic to this local port number or network address. This can be just a port (</param>
        /// <param name="metadata">Optional. Arbitrary user-defined metadata that will appear in the ngrok service API when listing tunnel sessions.</param>
        public LabeledTunnelConfiguration(string name, string addr, [Optional] string metadata) : base(name, addr, metadata)
        {
        }

        /// <summary>
        /// The path to a TLS certificate when doing TLS termination at the agent.
        /// </summary>
        [JsonPropertyName(PropertyNames.Crt)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Crt { get; set; }
        /// <summary>
        /// Enable/disable the http request inspection in the web and agent API (default: true)
        /// </summary>
        [JsonPropertyName(PropertyNames.Inspect)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? Inspect { get; set; }
        /// <summary>
        /// The path to a TLS key when doing TLS termination at the agent.
        /// </summary>
        [JsonPropertyName(PropertyNames.Key)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Key { get; set; }
        /// <summary>
        /// The labels for this tunnel in the format name=value.
        /// </summary>
        [JsonPropertyName(PropertyNames.Labels)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<string> Labels { get; set; }
    }
}
