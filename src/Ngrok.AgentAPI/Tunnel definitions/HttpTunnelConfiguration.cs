// Copyright (c) 2022 Quetzal Rivera.
// Licensed under the MIT License, See LICENCE in the project root for license information.

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;
using Ngrok.AgentAPI.Abstractions;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Ngrok.AgentAPI
{
    /// <summary>This object represents the HTTP tunnel definitions.</summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public sealed class HttpTunnelConfiguration : TunnelConfiguration
    {
        private const string _PROTO_ = "http";
        /// <summary>
        /// Initialize a new instance of <see cref="HttpTunnelConfiguration"/>.
        /// </summary>
        /// <param name="name">Tunnel name.</param>
        /// <param name="addr">Forward traffic to this local port number or network address. This can be just a port (</param>
        /// <param name="metadata">Optional. Arbitrary user-defined metadata that will appear in the ngrok service API when listing tunnel sessions.</param>
        public HttpTunnelConfiguration(string name, string addr, [Optional] string metadata) : base(name, addr, metadata)
        {
        }

        /// <summary>This is a list of username:password combinations to use for basic authenticate. Passwords must be at least 8 characters long.</summary>
        [JsonPropertyName(PropertyNames.BasicAuth)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<string> BasicAuth { get; set; }
        /// <summary>Reject requests when 5XX responses exceed this ratio.</summary>
        [JsonPropertyName(PropertyNames.CircuitBreaker)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public float? CircuitBreaker { get; set; }
        /// <summary>Gzip compress HTTP responses from your web service.</summary>
        [JsonPropertyName(PropertyNames.Compression)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? Compression { get; set; }
        /// <summary>Rewrite the HTTP Host header to this value, or <i>preserve</i> to leave it unchanged. The <i>rewrite</i> option will rewrite the host header to match the hostname of the upstream service you are sending traffic to.</summary>
        [JsonPropertyName(PropertyNames.HostHeader)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string HostHeader { get; set; }
        /// <summary>The hostname to request. If using a custom domain, this requires registering in the ngrok dashboard and setting a DNS CNAME value. When using wildcard domains you will need to surround the value with single quotes (hostname: '*.example.com').</summary>
        [JsonPropertyName(PropertyNames.Hostname)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Hostname { get; set; }
        /// <summary>Enable/disable the http request inspection in the web and agent API (default: true)</summary>
        [JsonPropertyName(PropertyNames.Inspect)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? Inspect { get; set; }
        /// <summary>
        /// Ip restriction configuration.
        /// </summary>
        [JsonPropertyName(PropertyNames.IpRestriction)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IpRestriction IpRestriction { get; set; }
        /// <summary>
        /// The path to the TLS certificate authority to verify client certs in mutual TLS.
        /// </summary>
        [JsonPropertyName(PropertyNames.MutualTlsCas)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string MutualTlsCas { get; set; }
        /// <summary>
        /// OAuth configuration.
        /// </summary>
        [JsonPropertyName(PropertyNames.OAuth)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public OAuth OAuth { get; set; }
        /// <summary>The tunnel protocol name. This defines the type of tunnel you would like to start.</summary>
        [JsonPropertyName(PropertyNames.Proto)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Proto => _PROTO_;
        /// <summary>The version of PROXY protocol to use with this tunnel, empty if not using. Example values are 1 or 2.</summary>
        [JsonPropertyName(PropertyNames.ProxyProto)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ProxyProto { get; set; }
        /// <summary>
        /// Request header.
        /// </summary>
        [JsonPropertyName(PropertyNames.RequestHeader)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public RequestHeader RequestHeader { get; set; }
        /// <summary>
        /// Response header.
        /// </summary>
        [JsonPropertyName(PropertyNames.ResponseHeader)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ResponseHeader ResponseHeader { get; set; }
        /// <summary>
        /// (http, https). Bind to an HTTPS endpoint (
        /// </summary>
        [JsonPropertyName(PropertyNames.Schemes)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<string> Schemes { get; set; }
        /// <summary>Subdomain name to request. If unspecified, ngrok provides a unique subdomain based on your account type.</summary>
        [JsonPropertyName(PropertyNames.Subdomain)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Subdomain { get; set; }
        /// <summary>
        /// Verify webhook.
        /// </summary>
        [JsonPropertyName(PropertyNames.VerifyWebhook)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public VerifyWebhook VerifyWebhook { get; set; }
        /// <summary>
        /// Convert ingress websocket connections to TCP upstream.
        /// </summary>
        [JsonPropertyName(PropertyNames.WebsocketTcpConverter)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? WebsocketTcpConverter { get; set; }
    }
}
