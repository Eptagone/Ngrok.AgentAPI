// Copyright (c) 2022 Quetzal Rivera.
// Licensed under the MIT License, See LICENCE in the project root for license information.

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;
using Ngrok.AgentAPI.Abstractions;
using System.Runtime.InteropServices;

namespace Ngrok.AgentAPI
{
    /// <summary>This object represents the TLS tunnel definitions.</summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public sealed class TlsTunnelConfiguration : TunnelConfiguration
    {
        private const string _PROTO_ = "tls";

        /// <summary>
        /// Initialize a new instance of <see cref="TlsTunnelConfiguration"/>.
        /// </summary>
        /// <param name="name">Tunnel name.</param>
        /// <param name="addr">Forward traffic to this local port number or network address. This can be just a port (</param>
        /// <param name="metadata">Optional. Arbitrary user-defined metadata that will appear in the ngrok service API when listing tunnel sessions.</param>
        public TlsTunnelConfiguration(string name, string addr, [Optional] string metadata) : base(name, addr, metadata)
        {
        }

        /// <summary>
        /// The path to the TLS certificate authority to verify client certs for mutual TLS. You will also need to specify <see cref="Key"/> and <see cref="Crt"/> to enable mutual TLS.
        /// </summary>
        [JsonPropertyName(PropertyNames.MutualTlsCas)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string MutualTlsCas { get; set; }
        /// <summary>
        /// PEM TLS certificate at this path to terminate TLS traffic before forwarding locally. Requires <see cref="Key"/> to also be specified.
        /// </summary>
        [JsonPropertyName(PropertyNames.Crt)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Crt { get; set; }
        /// <summary>The hostname to request. If using a custom domain, this requires registering in the ngrok dashboard and setting a DNS CNAME value. When using wildcard domains you will need to surround the value with single quotes (hostname: '*.example.com').</summary>
        [JsonPropertyName(PropertyNames.Hostname)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Hostname { get; set; }
        /// <summary>
        /// Ip restriction configuration.
        /// </summary>
        [JsonPropertyName(PropertyNames.IpRestriction)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IpRestriction IpRestriction { get; set; }
        /// <summary>
        /// PEM TLS private key at this path to terminate TLS traffic before forwarding locally. Requires <see cref="Crt"/> to also be specified.
        /// </summary>
        [JsonPropertyName(PropertyNames.Key)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Key { get; set; }
        /// <summary>The tunnel protocol name. This defines the type of tunnel you would like to start.</summary>
        [JsonPropertyName(PropertyNames.Proto)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Proto => _PROTO_;
        /// <summary>The version of PROXY protocol to use with this tunnel, empty if not using. Example values are 1 or 2.</summary>
        [JsonPropertyName(PropertyNames.ProxyProto)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ProxyProto { get; set; }
        /// <summary>Subdomain name to request. If unspecified, ngrok provides a unique subdomain based on your account type.</summary>
        [JsonPropertyName(PropertyNames.Subdomain)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Subdomain { get; set; }
        /// <summary>
        /// Terminate at the ngrok "edge" or "agent". defaults to no termination or "edge" if <see cref="Crt"/> or <see cref="Key"/> are present.
        /// </summary>
        [JsonPropertyName(PropertyNames.TerminateAt)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string TerminateAt { get; set; }
    }
}
