// Copyright (c) 2021 Quetzal Rivera.
// Licensed under the MIT License, See LICENCE in the project root for license information.

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;

namespace Ngrok.AgentAPI
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public abstract class BaseResource
    {
        [JsonPropertyName(PropertyNames.Uri)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Uri { get; set; }
    }
}
