// Copyright (c) 2021 Quetzal Rivera.
// Licensed under the MIT License, See LICENCE in the project root for license information.

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;

namespace Ngrok.AgentAPI
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public sealed class RequestListResource : BaseResource
    {
        /// <summary>List of captured requests. See the Captured Request Detail resource for docs on the request objects</summary>
        [JsonPropertyName(PropertyNames.Requests)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public RequestDetail[] Requests { get; set; }
    }
}
