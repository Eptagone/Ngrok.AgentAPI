// Copyright (c) 2022 Quetzal Rivera.
// Licensed under the MIT License, See LICENCE in the project root for license information.

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Text.Json.Serialization;

namespace Ngrok.AgentAPI
{
    /// <summary>Returns metadata and raw bytes of a captured request. The raw data is base64-encoded in the JSON response. The response value maybe null if the local server has not yet responded to a request.</summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public sealed class RequestDetail
    {
        [JsonPropertyName(PropertyNames.Uri)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Uri { get; set; }
        [JsonPropertyName(PropertyNames.Id)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Id { get; set; }
        [JsonPropertyName(PropertyNames.TunnelName)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string TunnelName { get; set; }
        [JsonPropertyName(PropertyNames.RemoteAddr)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string RemoteAddr { get; set; }
        [JsonPropertyName(PropertyNames.Start)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime Start { get; set; }
        [JsonPropertyName(PropertyNames.Duration)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public uint Duration { get; set; }
        [JsonPropertyName(PropertyNames.Request)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Request Request { get; set; }
        [JsonPropertyName(PropertyNames.Response)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Response Response { get; set; }
    }
}
