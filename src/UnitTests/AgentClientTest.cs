// Copyright (c) 2021 Quetzal Rivera.
// Licensed under the MIT License, See LICENCE in the project root for license information.

using Ngrok.AgentAPI;
using System.Threading.Tasks;
using Xunit;
using System;
using System.Linq;
using System.Net.Http;

namespace UnitTests
{
    public class AgentClientTest
    {
        private const string sampleTunnelName = "Test01";
        private const string sampleTunnelUrl = "https://localhost:80";
        private const string sampleHeader = "localhost:4040";
        private const string sampleTunnelName2 = "Test02";
        private const string sampleTunnelUrl2 = "https://localhost:4040";
        private const string sampleHeader2 = "localhost:80";

        private readonly NgrokAgentClient api;
        public AgentClientTest()
        {
            api = new NgrokAgentClient();
        }

        [Fact]
        public void ListTunnels()
        {
            var list = api.ListTunnels();
            Assert.NotNull(list.Tunnels);
            Assert.NotNull(list.Uri);
            foreach (var tunnel in list.Tunnels)
            {
                Assert.NotNull(tunnel);
                Assert.NotNull(tunnel.Name);
                Assert.NotNull(tunnel.Metrics);
                Assert.NotNull(tunnel.Uri);
                Assert.NotNull(tunnel.PublicUrl);
                Assert.NotNull(tunnel.Proto);
                Assert.NotNull(tunnel.Config);
                Assert.NotNull(tunnel.Config?.Addr);
                Assert.NotNull(tunnel.Config?.Inspect);
            }
        }

        [Fact]
        public async Task ListTunnelsAsync()
        {
            var list = await api.ListTunnelsAsync();
            Assert.NotNull(list.Tunnels);
            Assert.NotNull(list.Uri);
            foreach (var tunnel in list.Tunnels)
            {
                Assert.NotNull(tunnel);
                Assert.NotNull(tunnel.Name);
                Assert.NotNull(tunnel.Metrics);
                Assert.NotNull(tunnel.Uri);
                Assert.NotNull(tunnel.PublicUrl);
                Assert.NotNull(tunnel.Proto);
                Assert.NotNull(tunnel.Config);
                Assert.NotNull(tunnel.Config?.Addr);
                Assert.NotNull(tunnel.Config?.Inspect);
            }
        }

        [Fact]
        public void StartTunnelWithHttpProto()
        {
            var configuration = new TunnelConfiguration(sampleTunnelName, "http", sampleTunnelUrl2)
            {
                BindTls = "true",
                HostHeader = sampleHeader2
            };
            try
            {
                var tunnel = api.StartTunnel(configuration);
                Assert.NotNull(tunnel);
                Assert.Equal(sampleTunnelName, tunnel.Name);
                Assert.NotNull(tunnel.Name);
                Assert.NotNull(tunnel.Metrics);
                Assert.NotNull(tunnel.Uri);
                Assert.NotNull(tunnel.PublicUrl);
                Assert.NotNull(tunnel.Proto);
                Assert.NotNull(tunnel.Config);
                Assert.NotNull(tunnel.Config?.Addr);
                Assert.NotNull(tunnel.Config?.Inspect);
            }
            catch (Exception e)
            {
                Assert.IsType<HttpRequestException>(e);
            }
        }

        [Fact]
        public async Task StartTunnelWithHttpProtoAsync()
        {
            var configuration = new TunnelConfiguration(sampleTunnelName2, "http", sampleTunnelUrl)
            {
                BindTls = "true",
                HostHeader = sampleHeader
            };
            try
            {
                var tunnel = await api.StartTunnelAsync(configuration);
                Assert.NotNull(tunnel);
                Assert.Equal(sampleTunnelName2, tunnel.Name);
                Assert.NotNull(tunnel.Name);
                Assert.NotNull(tunnel.Metrics);
                Assert.NotNull(tunnel.Uri);
                Assert.NotNull(tunnel.PublicUrl);
                Assert.NotNull(tunnel.Proto);
                Assert.NotNull(tunnel.Config);
                Assert.NotNull(tunnel.Config?.Addr);
                Assert.NotNull(tunnel.Config?.Inspect);
            }
            catch (Exception e)
            {
                Assert.IsType<HttpRequestException>(e);
            }
        }

        [Fact]
        public void StopTunnel()
        {
            try
            {
                api.StopTunnel(sampleTunnelName);
            }
            catch (Exception e)
            {
                Assert.IsType<HttpRequestException>(e);
            }
        }

        [Fact]
        public async Task StopTunnelAsync()
        {
            try
            {
                await api.StopTunnelAsync(sampleTunnelName2);
            }
            catch (Exception e)
            {
                Assert.IsType<HttpRequestException>(e);
            }
        }

        [Fact]
        public void ListCapturedRequests()
        {
            var list = api.ListCapturedRequests(5, sampleTunnelName);
            Assert.NotNull(list.Requests);
            Assert.NotNull(list.Uri);
            Assert.True(list.Requests.Length <= 5);
            foreach (var request in list.Requests)
            {
                Assert.NotNull(request.Id);
                Assert.NotNull(request.TunnelName);
                Assert.Equal(sampleTunnelName, request.TunnelName);
                Assert.NotNull(request);
                Assert.NotNull(request.Uri);
                Assert.NotEqual(default, request.Start);
                Assert.NotNull(request.Request);
                Assert.NotNull(request.Response);
                Assert.NotEqual(default, request.Duration);
            }
        }

        [Fact]
        public async Task ListCapturedRequestsAsync()
        {
            var list = await api.ListCapturedRequestsAsync(5, sampleTunnelName2);
            Assert.NotNull(list.Requests);
            Assert.NotNull(list.Uri);
            Assert.True(list.Requests.Length <= 5);
            foreach (var request in list.Requests)
            {
                Assert.NotNull(request.Id);
                Assert.NotNull(request.TunnelName);
                Assert.Equal(sampleTunnelName2, request.TunnelName);
                Assert.NotNull(request);
                Assert.NotNull(request.Uri);
                Assert.NotEqual(default, request.Start);
                Assert.NotNull(request.Request);
                Assert.NotNull(request.Response);
                Assert.NotEqual(default, request.Duration);
            }
        }

        [Fact]
        public void ReplayCapturedRequest()
        {
            var request = api.ListCapturedRequests(1, sampleTunnelName).Requests.SingleOrDefault();
            if (request != default)
            {
                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        api.ReplayCapturedRequest(request.Id, request.TunnelName);
                    }
                    catch (Exception e)
                    {
                        Assert.IsType<HttpRequestException>(e);
                    }
                }
            }
        }

        [Fact]
        public async Task ReplayCapturedRequestAsync()
        {
            var list = await api.ListCapturedRequestsAsync(1, sampleTunnelName).ConfigureAwait(false);
            var request = list.Requests.SingleOrDefault();
            if (request != default)
            {
                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        api.ReplayCapturedRequest(request.Id, request.TunnelName);
                    }
                    catch (Exception e)
                    {
                        Assert.IsType<HttpRequestException>(e);
                    }
                }
            }
        }

        [Fact]
        public void DeleteCapturedRequests()
        {
            try
            {
                api.DeleteCapturedRequests();
            }
            catch (Exception e)
            {
                Assert.IsType<HttpRequestException>(e);
            }
        }

        [Fact]
        public async Task DeleteCapturedRequestsAsync()
        {
            try
            {
                await api.DeleteCapturedRequestsAsync();
            }
            catch (Exception e)
            {
                Assert.IsType<HttpRequestException>(e);
            }
        }
    }
}
