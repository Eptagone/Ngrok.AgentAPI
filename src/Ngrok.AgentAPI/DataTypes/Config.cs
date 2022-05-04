﻿// Copyright (c) 2022 Quetzal Rivera.
// Licensed under the MIT License, See LICENCE in the project root for license information.

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;

namespace Ngrok.AgentAPI
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public sealed class Config
    {
        [JsonPropertyName(PropertyNames.Addr)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Addr { get; set; }
        [JsonPropertyName(PropertyNames.Inspect)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool Inspect { get; set; }
    }
}
