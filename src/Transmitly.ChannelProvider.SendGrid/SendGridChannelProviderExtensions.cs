﻿// ﻿﻿Copyright (c) Code Impressions, LLC. All Rights Reserved.
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

using SendGrid.Helpers.Mail;
using System;
using Transmitly.ChannelProvider.SendGrid.Configuration;
using Transmitly.ChannelProvider.SendGrid.Sdk;

namespace Transmitly
{
	public static class SendGridChannelProviderExtensions
	{
		private const string SendGridId = "SendGrid";

		public static SendGridExtendedEmailProperties SendGrid(this IEmailChannel email)
		{
			return new SendGridExtendedEmailProperties(email);
		}

		public static string SendGrid(this ChannelProviders channelProviders, string? providerId = null)
		{
			Guard.AgainstNull(channelProviders);
			return channelProviders.GetId(SendGridId, providerId);
		}

		//public static IPipelineChannelConfiguration AddSendGridTemplateEmail(this IPipelineChannelConfiguration pipelineChannelConfiguration, IAudienceAddress fromAddress, string templateId, Action<ISendGridEmailChannel> emailChannelConfiguration, params string[]? allowedChannelProviders)
		//{
		//	Guard.AgainstNull(templateId);

		//	var allowedProviders = new List<string>(allowedChannelProviders ?? []) { Id.ChannelProvider.SendGrid() }.ToArray();
		//	var emailOptions = new SendGridEmailChannel(fromAddress, allowedProviders)
		//	{
		//		TemplateId = templateId
		//	};
		//	emailChannelConfiguration(emailOptions);
		//	pipelineChannelConfiguration.AddChannel(emailOptions);
		//	return pipelineChannelConfiguration;
		//}

		//public static IPipelineChannelConfiguration AddSendGridEmail(this IPipelineChannelConfiguration pipelineChannelConfiguration, IAudienceAddress fromAddress, Action<ISendGridEmailChannel> emailChannelConfiguration, params string[]? allowedChannelProviders)
		//{
		//	var allowedProviders = new List<string>(allowedChannelProviders ?? []) { Id.ChannelProvider.SendGrid() }.ToArray();
		//	var emailOptions = new SendGridEmailChannel(fromAddress, allowedProviders);
		//	emailChannelConfiguration(emailOptions);
		//	pipelineChannelConfiguration.AddChannel(emailOptions);
		//	return pipelineChannelConfiguration;
		//}

		public static CommunicationsClientBuilder AddSendGridSupport(this CommunicationsClientBuilder channelProviderConfiguration, Action<SendGridOptions> options, string? providerId = null)
		{
			var opts = new SendGridOptions();
			options(opts);
			channelProviderConfiguration
				.ChannelProvider
				.Build(Id.ChannelProvider.SendGrid(providerId), opts)
				.AddDispatcher<SendGridMessageChannelProviderClient, SendGridMessage>(Id.Channel.Email())
				.AddDispatcher<EmailChannelProviderClient, IEmail>(Id.Channel.Email())
				.Register();
			return channelProviderConfiguration;
		}
	}
}