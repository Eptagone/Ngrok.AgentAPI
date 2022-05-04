// Copyright (c) 2022 Quetzal Rivera.
// Licensed under the MIT License, See LICENCE in the project root for license information.

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;

namespace Ngrok.AgentAPI
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public abstract class MetricBase
    {
        [JsonPropertyName(PropertyNames.Count)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ushort Count { get; set; }
        [JsonPropertyName(PropertyNames.Rate1)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double Rate1 { get; set; }
        [JsonPropertyName(PropertyNames.Rate5)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double Rate5 { get; set; }
        [JsonPropertyName(PropertyNames.Rate15)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double Rate15 { get; set; }
        [JsonPropertyName(PropertyNames.P50)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public float P50 { get; set; }
        [JsonPropertyName(PropertyNames.P90)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public float P90 { get; set; }
        [JsonPropertyName(PropertyNames.P95)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public float P95 { get; set; }
        [JsonPropertyName(PropertyNames.P99)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public float P99 { get; set; }
    }
}
