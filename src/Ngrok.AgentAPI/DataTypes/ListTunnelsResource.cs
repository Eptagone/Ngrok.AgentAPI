// Copyright (c) 2022 Quetzal Rivera.
// Licensed under the MIT License, See LICENCE in the project root for license information.

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;

namespace Ngrok.AgentAPI
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public sealed class TunnelListResource : BaseResource
    {
        /// <summary>List of all running tunnels. See the Tunnel detail resource for docs on the parameters of each tunnel object</summary>
        [JsonPropertyName(PropertyNames.Tunnels)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public TunnelDetail[] Tunnels { get; set; }
    }
}
