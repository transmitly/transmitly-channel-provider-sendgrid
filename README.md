# Transmitly.ChannelProvider.SendGrid

`Transmitly.ChannelProvider.SendGrid` is the convenience package for sending email with [Transmitly](https://github.com/transmitly/transmitly) through [Twilio SendGrid](https://sendgrid.com/).

This is the package most applications should install. It wires together:

- `Transmitly.ChannelProvider.SendGrid.Configuration`
- `Transmitly.ChannelProvider.SendGrid.Sdk`

Supported channels:

- `Email`

## Install

```shell
dotnet add package Transmitly.ChannelProvider.SendGrid
```

## Quick Start

```csharp
using Transmitly;

ICommunicationsClient client = new CommunicationsClientBuilder()
	.AddSendGridSupport(options =>
	{
		options.ApiKey = "your-sendgrid-api-key";
		// Required unless ValidateDeliveryReportSignature = false. See Delivery Reports.
		options.EventWebhookVerificationKey = "your-event-webhook-verification-key";
	})
	.AddPipeline("welcome-email", pipeline =>
	{
		pipeline.AddEmail("welcome@example.com".AsIdentityAddress("Example App"), email =>
		{
			email.Subject.AddStringTemplate("Welcome to Example App");
			email.HtmlBody.AddStringTemplate("<strong>Welcome</strong> to Example App.");
			email.TextBody.AddStringTemplate("Welcome to Example App.");
		});
	})
	.BuildClient();

var result = await client.DispatchAsync(
	"welcome-email",
	"customer@example.com",
	new { });
```

## Configuration

`AddSendGridSupport(options => ...)` accepts `SendGridOptions`.

Common settings:

- `ApiKey`: your Twilio SendGrid API key.
- `Host`: defaults to `https://api.sendgrid.com`.
- `Version`: defaults to `v3`.
- `HttpErrorAsException`: forward HTTP failures as exceptions from the underlying client.
- `RequestHeaders`, `UrlPath`, `Auth`, and `ReliabilitySettings` for advanced client configuration.

## SendGrid-Specific Email Features

This package registers SendGrid email extensions through `email.SendGrid()`.

The primary provider-specific setting is `TemplateId`, which lets you send with a SendGrid dynamic template instead of the channel subject/body content.

```csharp
using Transmitly;

pipeline.AddEmail("welcome@example.com".AsIdentityAddress("Example App"), email =>
{
	email.Subject.AddStringTemplate("Welcome to Example App");
	email.TextBody.AddStringTemplate("Welcome to Example App.");
	email.SendGrid().TemplateId = "d-0123456789abcdef0123456789abcdef";
});
```

## Delivery Reports

This package registers a SendGrid webhook adaptor that converts SendGrid event webhook payloads into Transmitly `DeliveryReport` instances.

SendGrid posts every event to one static webhook URL per account and can't add Transmitly's per-message context to it. So the URL itself has to identify the channel and provider with `tlyc=Email` and `tlycp=SendGrid`. Without them the adaptor ignores the request:

```text
https://your-app.example.com/communications/channel/provider/update?tlyc=Email&tlycp=SendGrid
```

- `tlycp=SendGrid` matches every SendGrid registration, including ones added with a `providerId`.
- Because the URL is the same for every message, reports don't carry a pipeline intent or pipeline id. To match a report to the message you sent, compare resource ids. The dispatch result's resource id is SendGrid's `X-Message-ID`. The report's resource id is the event's `sg_message_id`, which starts with that `X-Message-ID`, followed by `.` and a suffix SendGrid adds.

If you are using the MVC integration packages, point the webhook at your Transmitly delivery-report endpoint and include the same query-string context.

Event webhooks are checked against SendGrid's Signed Event Webhook headers. Turn on **Signed Event Webhook** in SendGrid (Settings → Mail Settings → Event Webhook) and copy the verification key. Requests that fail the check are rejected with `403 Forbidden` by the MVC integration packages.

- `EventWebhookVerificationKey`: the verification key. `AddSendGridSupport` throws at startup if it is missing while `ValidateDeliveryReportSignature` is `true`.
- `ValidateDeliveryReportSignature`: defaults to `true`. Set it to `false` if you don't receive SendGrid event webhooks; then no key is needed.

## Related Packages

- [Transmitly](https://github.com/transmitly/transmitly)
- [Transmitly.ChannelProvider.SendGrid.Configuration](https://github.com/transmitly/transmitly-channel-provider-sendgrid-configuration)
- [Transmitly.ChannelProvider.SendGrid.Sdk](https://github.com/transmitly/transmitly-channel-provider-sendgrid-sdk)

---
_Copyright (c) Code Impressions, LLC. This open-source project is sponsored and maintained by Code Impressions and is licensed under the [Apache License, Version 2.0](http://apache.org/licenses/LICENSE-2.0.html)._
