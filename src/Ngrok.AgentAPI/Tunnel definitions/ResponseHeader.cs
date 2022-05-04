// Copyright (c) 2022 Quetzal Rivera.
// Licensed under the MIT License, See LICENCE in the project root for license information.

using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Ngrok.AgentAPI
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public sealed class ResponseHeader
    {
        /// <summary>The headers to add to the response in the key:value format.</summary>
        [JsonPropertyName(PropertyNames.Add)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<string> Add { get; set; }
        /// <summary>The header keys to remove from the response.</summary>
        [JsonPropertyName(PropertyNames.Remove)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<string> Remove { get; set; }
    }
}
