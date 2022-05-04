# Ngrok.AgentAPI

[![NuGet version](https://img.shields.io/nuget/v/Ngrok.AgentAPI.svg?style=flat-square)](https://www.nuget.org/packages/Ngrok.AgentAPI/)
[![ngrok Agent version](https://img.shields.io/badge/ngrok%20Agent-v3-brightgreen?style=flat-square)](https://ngrok.com/)

Simple library to use the ngrok Agent API. Start and stop tunnels programmatically.

## How to use

### Step 1: Run ngrok via external console or background service

Before you start using ngrok in your NET project, you need to make sure that ngrok is running. If not, run ngrok using the console, a script, or a background service.

```pwsh
ngrok start --none;
```

### Step 2: Start a new instance of the NgrokAgentClient

In your NET project, create a new instance of `NgrokAgentClient` as follows.

```CSharp
using Ngrok.AgentAPI

api = new NgrokAgentClient();
```

### Step 3: Call any method

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
// Start a new HTTP tunnel on port 5000 using http and https schemes
var configuration = new HttpTunnelConfiguration("MyLocalWebsite", "https://localhost:5000")
{
    Schemes = new string[] { "http", "https" },
    HostHeader = "localhost:5000"
};
var myTunnel = api.StartTunnel(configuration);
```

## See also

To see the list with all the methods, see the official [webpage](https://ngrok.com/docs/ngrok-agent/api) of the ngrok agent API documentation.

## License

[MIT](https://github.com/Eptagone/Ngrok.AgentAPI/blob/main/LICENSE)
