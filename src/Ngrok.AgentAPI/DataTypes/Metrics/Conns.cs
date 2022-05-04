// Copyright (c) 2022 Quetzal Rivera.
// Licensed under the MIT License, See LICENCE in the project root for license information.

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;

namespace Ngrok.AgentAPI
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public sealed class Conns : MetricBase
    {
        [JsonPropertyName(PropertyNames.Gauge)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ushort Gauge { get; set; }
    }
}
