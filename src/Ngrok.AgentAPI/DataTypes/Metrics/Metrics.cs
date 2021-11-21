using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;

namespace Ngrok.AgentAPI
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public sealed class Metrics
    {
        [JsonPropertyName(PropertyNames.Conns)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Conns Conns { get; set; }
        [JsonPropertyName(PropertyNames.Http)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public HTTP Http { get; set; }
    }
}
