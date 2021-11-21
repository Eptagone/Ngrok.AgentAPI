// Copyright (c) 2021 Quetzal Rivera.
// Licensed under the MIT License, See LICENCE in the project root for license information.

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;

namespace Ngrok.AgentAPI
{
    /// <summary>Get status and metrics about the named running tunnel</summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public sealed class TunnelDetail
    {
        [JsonPropertyName(PropertyNames.Name)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonPropertyName(PropertyNames.Uri)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Uri { get; set; }
        [JsonPropertyName(PropertyNames.PublicUrl)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string PublicUrl { get; set; }
        [JsonPropertyName(PropertyNames.Proto)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Proto { get; set; }
        [JsonPropertyName(PropertyNames.Config)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Config Config { get; set; }
        [JsonPropertyName(PropertyNames.Metrics)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Metrics Metrics { get; set; }
    }
}
