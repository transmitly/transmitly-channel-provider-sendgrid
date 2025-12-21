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
var communicationClient =
	new CommunicationsClientBuilder()
	.AddSendGridSupport(options =>
	{
		options.ApiKey = "12354";
	})
	.AddPipeline("first-pipeline", options =>
	{
		options.AddEmail("from@transmit.ly", email =>
		{
			email.Subject.AddStringTemplate("My first pipeline!");
			email.HtmlBody.AddStringTemplate("My <strong>first</strong> pipeline is great!");
			email.TextBody.AddStringTemplate("My *first* pipeline is great!");
		});
	});
```
* See the [Transmitly](https://github.com/transmitly/transmitly) project for more details on what a channel provider is and how it can be configured.

### Using Templates

```csharp
using Transmitly;
...
var communicationClient =
	new CommunicationsClientBuilder()
	.AddSendGridSupport(options =>
	{
		options.ApiKey = "12354";
	})
	.AddPipeline("first-pipeline", options =>
	{
		options.AddEmail("from@transmit.ly", email =>
		{
			//It's recommended to define 'backup' templates to keep
			//you prepared for using a different channel provider
			email.Subject.AddStringTemplate("My first pipeline!");
			email.HtmlBody.AddStringTemplate("My <strong>first</strong> pipeline is great!");
			email.TextBody.AddStringTemplate("My *first* pipeline is great!");

			email.SendGrid().TemplateId = "d-36ed2a324e76475f9c68462505899384";
		});
	});
```


### Delivery Reports

SendGrid only allows one webhook per account and it's unable to be specified dynamically on a per communication basis. By using the [MS MVC Core](https://github.com/transmitly/transmitly-microsoft-aspnetcore-mvc) package we can take advantage of Transmitly's provider agnostic approach for handling delivery updates from the supported channel providers.
#### Setup
* SendGrid Dashboard
* Settings
* Mail Settings
* Webhook Settings
* Event Webhooks
* Create a new webhook
* Post Url: https://my.domain.com/Communications/channel/provider/update?tlyc=Email&tlycp=SendGrid
  * The path will depend upon your app. In this example, we're using the path to the [Kitchen sink sample controller](https://github.com/transmitly/transmitly/tree/main/samples/Transmitly.KitchenSink.AspNetCoreWebApi). The important bits are the `?tlyc=Email&tlycp=SendGrid` to help out Transmitly.

* Your App
* ```bash
  dotnet add package Transmitly.Microsoft.AspnetCore.Mvc
  ```

* Register

```csharp
  builder.Services
	.AddControllers(options =>
	{
		//Adds the necessary model binders to handle channel provider specific webhooks (Twilio, Infobip, etc)
		//and convert them to delivery reports (Added with package: Transmitly.Microsoft.AspnetCore.Mvc)
		options.AddTransmitlyDeliveryReportModelBinders();
	})
  	.AddTransmitly(tly=>
	{
		tly..AddDeliveryReportHandler((report) =>
		{
			logger?.LogInformation("[{channelId}:{channelProviderId}:StatusChanged] Id={id}; Status={status}", report.ChannelId, report.ChannelProviderId, report.ResourceId, report.Status.Type);
			return Task.CompletedTask;
		}, [DeliveryReport.Event.StatusChanged()])
  	   	//...rest of config
  	});
  ```
  * Add a new controller route to handle the SendGrid webhooks
```csharp
[HttpPost("channel/provider/update", Name = "DeliveryReport")]
public IActionResult ChannelProviderDeliveryReport(ChannelProviderDeliveryReportRequest providerReport)
{
	_communicationsClient.DispatchAsync(providerReport.DeliveryReports);
	return Ok();
}
 ```

### Copyright and Trademark 

Copyright © 2024–2025 Code Impressions, LLC.

Transmitly™ is a trademark of Code Impressions, LLC.

This open-source project is sponsored and maintained by Code Impressions
and is licensed under the [Apache License, Version 2.0](http://apache.org/licenses/LICENSE-2.0.html).

The Apache License applies to the software code only and does not grant
permission to use the Transmitly name or logo, except as required to
describe the origin of the software.
