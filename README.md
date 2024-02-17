# Transmitly.ChannelProvider.SendGrid

A [Transmitly](https://github.com/transmitly/transmitly) channel provider that enables sending Email communications with [SendGrid](https://sendgrid.com)

### Getting started

To use the SendGrid channel provider, first install the [NuGet package](https://nuget.org/packages/transmitly.channel-provider.sendgrid):

```shell
dotnet add package Transmitly.ChannelProvider.SendGrid
```

Then add the channel provider using `AddSendGridSupport()`:

```csharp
using Transmitly;
...
var communicationClient = new CommunicationsClientBuilder()
	.AddSendGridSupport(options =>
	{
		options.ApiKey = "12354";
	})
```
* See the [Transmitly](https://github.com/transmitly/transmitly) project for more details on what a channel provider is and how it can be configured.


<picture>
  <source media="(prefers-color-scheme: dark)" srcset="https://github.com/transmitly/transmitly/assets/3877248/524f26c8-f670-4dfa-be78-badda0f48bfb">
  <img alt="an open-source project sponsored by CiLabs of Code Impressions, LLC" src="https://github.com/transmitly/transmitly/assets/3877248/34239edd-234d-4bee-9352-49d781716364" width="350" align="right">
</picture> 

---------------------------------------------------

_Copyright &copy; Code Impressions, LLC - Provided under the [Apache License, Version 2.0](http://apache.org/licenses/LICENSE-2.0.html)._
