// Copyright (c) 2022 Quetzal Rivera.
// Licensed under the MIT License, See LICENCE in the project root for license information.

using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Ngrok.AgentAPI
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public sealed class VerifyWebhook
    {
        /// <summary>Verify webhooks are signed by this provider, e.g. 'slack'. For a full list of providers, see Webhook Verification Providers.</summary>
        [JsonPropertyName(PropertyNames.Provider)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Provider { get; set; }
        /// <summary>The secret used by provider to sign webhooks, if there is one.</summary>
        [JsonPropertyName(PropertyNames.Secret)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Secret { get; set; }
    }
}
