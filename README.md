# Ngrok.AgentAPI

[![NuGet version (Telegram.BotAPI)](https://img.shields.io/nuget/v/Ngrok.AgentAPI.svg?style=flat-square)](https://www.nuget.org/packages/Ngrok.AgentAPI/)

Simple library to use the ngrok Agent API. Start and stop tunnels programmatically.

## How to use

### Step 1: Start a new instance of the NgrokAgentClient.

```CSharp
using Ngrok.AgentAPI

api = new NgrokAgentClient();
```

### Step 2: Call any method

Example:

```CSharp
// List all tunnels and do something
var resource = api.ListTunnels();
if(resource.Tunnels != null){
    foreach (var tunnel in list.Tunnels)
    {
        var tunnelName = tunnel.Name;
        // ...
    }
}
```

Example:

```CSharp
// Start a new tunnel on port 5000 using SSL
var configuration = new TunnelConfiguration("MyLocalWebsite", "http", "https://localhost:5000")
{
    BindTls = "true",
    HostHeader = "localhost:5000"
};
var myTunnel = api.StartTunnel(configuration);
```

## See also

To see the list with all the methods, see the official [webpage](https://ngrok.com/docs#client-api) of the ngrok agent API documentation.

## License

[MIT](https://github.com/Eptagone/Ngrok.AgentAPI/blob/main/LICENSE)
