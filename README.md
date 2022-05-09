<div align="center">

  <p><img src="https://i.ibb.co/ydk7KVc/banner.png" alt="Banner"></p>
  <p>FortniteDotNet is a simple and easy-to-use library used for interacting with Fortnite's HTTP and XMPP services. Features include interactions with parties and friends, general API data, and more.</p>
  
  [![NuGet Release](https://img.shields.io/nuget/v/FortniteDotNet?logo=nuget)](https://www.nuget.org/packages/FortniteDotNet)
  [![NuGet Downloads](https://img.shields.io/nuget/dt/FortniteDotNet?logo=nuget)](https://www.nuget.org/packages/FortniteDotNet)
  [![GitHub Issues](https://img.shields.io/github/issues/cyclonefreeze/FortniteDotNet?logo=github)](https://github.com/cyclonefreeze/FortniteDotNet/issues)
  [![GitHub Forks](https://img.shields.io/github/forks/cyclonefreeze/FortniteDotNet?logo=github)](https://github.com/cyclonefreeze/FortniteDotNet/forks)
  [![GitHub Stars](https://img.shields.io/github/stars/cyclonefreeze/FortniteDotNet?logo=github)](https://github.com/cyclonefreeze/FortniteDotNet/stargazers)
  [![GitHub License](https://img.shields.io/github/license/cyclonefreeze/FortniteDotNet)](https://github.com/cyclonefreeze/FortniteDotNet/blob/main/LICENSE)

</div>

## Installation
You can find FortniteDotNet on any NuGet package manager. Alternatively, you can use the dotnet CLI.
```
dotnet add package FortniteDotNet
```

## Features
Below is a list of some of the features FortniteDotNet includes.
- Party and XMPP services
- Friends services, including legacy endpoints
- Fortnite services
  - Cloudstorage (system/user)
  - Storefront requests (shop)
  - Profile-related operations (QueryProfile, etc.)
- Dependency injection
- Asynchronous codebase

## Usage
FortniteDotNet uses dependency injection. There are two ways to access different services within FortniteDotNet. Please ensure the package `Microsoft.Extensions.DependencyInjection` is installed.

### MAUI app example (MauiProgram.cs):
First:
```csharp
// Add the services upon creation
public static MauiApp CreateMauiApp()
{
    var builder = MauiApp.CreateBuilder().UseMauiApp<...>();    
    builder.Services.AddFortniteApi();
    
    ...
    
    return builder.Build();
}
```
then...
```csharp
// Get a service through a constructor
private readonly IAccountService _accountService;

public ExampleClass(IAccountService accountService)
{
    _accountService = accountService;
}
```
or...
```csharp
// Inject a service into a component
@inject IAccountService _accountService;
```

### MVC app example (Startup.cs):
First:
```csharp
public void ConfigureServices(IServiceCollection services)
{
    ...
    
    services.AddFortniteApi();
}
```
then...
```csharp
// Get a service through a constructor
private readonly IAccountService _accountService;

public ExampleClass(IAccountService accountService)
{
    _accountService = accountService;
}
```

### .NET app example:
```csharp
var services = new ServiceCollection()
    .AddFortniteDotNet()
    .BuildServiceProvider();

...

// Get a service
var accountService = services.GetService<IAccountService>();
```

## Example
The rewrite is yet to be completed, so I have not provided an example yet. You can see tips on usage above.

I've commented the example code so you can have some sort of understanding of what it's doing. Please also note that this is just an example, and there's plenty more that you can do with FortniteDotNet.

```cs
// TODO
```

## Contributions
If you wish to contribute to FortniteDotNet, you can do so by cloning this repository, making your changes, and submitting a pull request. If I deem the pull request inappropriate, I will deny it. 

FortniteDotNet is not **entirely** finished, especially due to the lack of MCP commands, so any contributions are greatly appreciated. The code structure is organised so should be easy to understand, however if you struggle, please don't hesitate to get in touch.

## Contact
For any queries regarding FortniteDotNet, you can open an issue or a pull request, and I will happily reply to the thread.