// Copyright (c) 2021 Quetzal Rivera.
// Licensed under the MIT License, See LICENCE in the project root for license information.

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;

namespace Ngrok.AgentAPI
{
    /// <summary>This object represents the tunnel definitions. Every tunnel must define proto and addr. Other properties are available and many are protocol-specific.</summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public sealed class TunnelConfiguration
    {
        public TunnelConfiguration()
        {

        }
        public TunnelConfiguration(string name, string proto, string addr)
        {
            Name = name;
            Proto = proto;
            Addr = addr;
        }
        /// <summary>Required all. Tunnel name</summary>
        [JsonPropertyName(PropertyNames.Name)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>Required all. Tunnel protocol name, one of http, tcp, tls</summary>
        [JsonPropertyName(PropertyNames.Proto)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Proto { get; set; }
        /// <summary>Required all. Forward traffic to this local port number or network address</summary>
        [JsonPropertyName(PropertyNames.Addr)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Addr { get; set; }
        /// <summary>Optional http. Enable http request inspection</summary>
        [JsonPropertyName(PropertyNames.Inspect)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Inspect { get; set; }
        /// <summary>Optional http. HTTP basic authentication credentials to enforce on tunneled requests</summary>
        [JsonPropertyName(PropertyNames.Auth)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Auth { get; set; }
        /// <summary>Optional http. Rewrite the HTTP Host header to this value, or preserve to leave it unchanged</summary>
        [JsonPropertyName(PropertyNames.HostHeader)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string HostHeader { get; set; }
        /// <summary>Optional http. Bind an HTTPS or HTTP endpoint or both true, false, or both</summary>
        [JsonPropertyName(PropertyNames.BindTls)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string BindTls { get; set; }
        /// <summary>Optional http, tls. Subdomain name to request. If unspecified, uses the tunnel name</summary>
        [JsonPropertyName(PropertyNames.Subdomain)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Subdomain { get; set; }
        /// <summary>Optional http, tls. Hostname to request (requires reserved name and DNS CNAME)</summary>
        [JsonPropertyName(PropertyNames.Hostname)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Hostname { get; set; }
        /// <summary>Optional tls. PEM TLS certificate at this path to terminate TLS traffic before forwarding locally</summary>
        [JsonPropertyName(PropertyNames.Crt)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Crt { get; set; }
        /// <summary>Optional tls. PEM TLS private key at this path to terminate TLS traffic before forwarding locally</summary>
        [JsonPropertyName(PropertyNames.Key)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Key { get; set; }
        /// <summary>Optional tls. PEM TLS certificate authority at this path will verify incoming TLS client connection certificates</summary>
        [JsonPropertyName(PropertyNames.ClientCas)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ClientCas { get; set; }
        /// <summary>Optional tls. Bind the remote TCP port on the given address</summary>
        [JsonPropertyName(PropertyNames.RemoteAddr)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string RemoteAddr { get; set; }
        /// <summary>Optional all. Arbitrary user-defined metadata that will appear in the ngrok service API when listing tunnels.</summary>
        [JsonPropertyName(PropertyNames.Metadata)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Metadata { get; set; }
    }
}
