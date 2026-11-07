// Copyright (c) Code Impressions, LLC. All Rights Reserved.
//  
//  Licensed under the Apache License, Version 2.0 (the "License")
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//  
//      http://www.apache.org/licenses/LICENSE-2.0
//  
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.

using System;
using Transmitly.ChannelProvider.SendGrid.Configuration;
using Transmitly.ChannelProvider.SendGrid.Sdk.Email;
using Transmitly.Exceptions;

namespace Transmitly
{
	public static class SendGridChannelProviderExtensions
	{
		/// <summary>
		/// Adds SendGrid support to the channel provider configuration.
		/// </summary>
		/// <param name="channelProviderConfiguration">Channel provider configuration.</param>
		/// <param name="options">Configuration options for the SendGrid dispatcher.</param>
		/// <param name="providerId">Optional provider identifier.</param>
		/// <returns><paramref name="channelProviderConfiguration"/></returns>
		/// <exception cref="CommunicationsException">Signature validation is on but no <see cref="SendGridOptions.EventWebhookVerificationKey"/> is set.</exception>
		public static CommunicationsClientBuilder AddSendGridSupport(this CommunicationsClientBuilder channelProviderConfiguration, Action<SendGridOptions> options, string? providerId = null)
		{
			var opts = new SendGridOptions();
			options(opts);
			if (opts.ValidateDeliveryReportSignature && string.IsNullOrWhiteSpace(opts.EventWebhookVerificationKey))
				throw new CommunicationsException($"SendGrid {nameof(SendGridOptions.EventWebhookVerificationKey)} is required when {nameof(SendGridOptions.ValidateDeliveryReportSignature)} is true. Set it to your Signed Event Webhook verification key, or set {nameof(SendGridOptions.ValidateDeliveryReportSignature)} = false if you don't receive SendGrid event webhooks.");
			channelProviderConfiguration
				.ChannelProvider
				.Build(Id.ChannelProvider.SendGrid(providerId), opts)
				.AddDispatcher<EmailChannelProviderDispatcher, IEmail>(Id.Channel.Email())
				.AddDeliveryReportRequestAdaptor<SendGridChannelProviderDeliveryReportRequestAdaptor>()
				.Register();

			return channelProviderConfiguration;
		}
	}
}
