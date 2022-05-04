// Copyright (c) 2022 Quetzal Rivera.
// Licensed under the MIT License, See LICENCE in the project root for license information.

using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Ngrok.AgentAPI
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public sealed class OAuth
    {
        /// <summary>Allow only OAuth2 users with these email domains.</summary>
        [JsonPropertyName(PropertyNames.AllowDomains)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<string> AllowDomains { get; set; }
        /// <summary>Allow only OAuth users with these emails.</summary>
        [JsonPropertyName(PropertyNames.AllowEmails)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<string> AllowEmails { get; set; }
        /// <summary>Request these OAuth2 scopes when a user authenticates.</summary>
        [JsonPropertyName(PropertyNames.OAuthScopes)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<string> OAuthScopes { get; set; }
        /// <summary>Enforce authentication OAuth2 provider on the endpoint, e.g. 'google'. For a lit of available providers, see <a href="https://ngrok.com/docs/cloud-edge#oauth-providers">OAuth2 providers</a>.</summary>
        [JsonPropertyName(PropertyNames.Provider)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Provider { get; set; }
    }
}
