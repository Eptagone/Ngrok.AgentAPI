// Copyright (c) 2022 Quetzal Rivera.
// Licensed under the MIT License, See LICENCE in the project root for license information.

using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;
using System.Runtime.InteropServices;

namespace Ngrok.AgentAPI
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public sealed class IpRestriction
    {
        public IpRestriction([Optional] IEnumerable<string> allowCidrs, [Optional] IEnumerable<string> denyCidrs)
        {
            AllowCidrs = allowCidrs;
            DenyCidrs = denyCidrs;
        }

        /// <summary>Rejects connections that do not match the given CIDRs.</summary>
        [JsonPropertyName(PropertyNames.AllowCidrs)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<string> AllowCidrs { get; set; }
        /// <summary>Rejects connections that match the given CIDRs and allows all other CIDRs.</summary>
        [JsonPropertyName(PropertyNames.DenyCidrs)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<string> DenyCidrs { get; set; }
    }
}
