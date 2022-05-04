// Copyright (c) 2022 Quetzal Rivera.
// Licensed under the MIT License, See LICENCE in the project root for license information.

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Net;
using System.Text.Json.Serialization;

namespace Ngrok.AgentAPI
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public sealed class Response
    {
        [JsonPropertyName(PropertyNames.Status)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Status { get; set; }
        [JsonPropertyName(PropertyNames.StatusCode)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public HttpStatusCode StatusCode { get; set; }
        [JsonPropertyName(PropertyNames.Proto)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Proto { get; set; }
        [JsonPropertyName(PropertyNames.Headers)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IDictionary<string, string[]> Headers { get; set; }
        [JsonPropertyName(PropertyNames.Raw)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Raw { get; set; }
    }
}
