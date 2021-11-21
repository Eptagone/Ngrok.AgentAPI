// Copyright (c) 2021 Quetzal Rivera.
// Licensed under the MIT License, See LICENCE in the project root for license information.

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Ngrok.AgentAPI
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public sealed class Request
    {
        [JsonPropertyName(PropertyNames.Method)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Method { get; set; }
        [JsonPropertyName(PropertyNames.Proto)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Proto { get; set; }
        [JsonPropertyName(PropertyNames.Headers)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IDictionary<string, string[]> Headers { get; set; }
        [JsonPropertyName(PropertyNames.Uri)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Uri { get; set; }
        [JsonPropertyName(PropertyNames.Raw)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Raw { get; set; }
    }
}
