// ﻿﻿Copyright (c) Code Impressions, LLC. All Rights Reserved.
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
using Transmitly.ChannelProvider.SendGrid.Sdk.Configuration.Email;
using Transmitly.Util;

namespace Transmitly
{
	public static class SendGridChannelProviderExtensions
	{
		/// <summary>
		/// Returns the SendGrid channel provider identifier.
		/// </summary>
		/// <param name="channelProviders">Channel providers.</param>
		/// <param name="providerId">Optional provider identifier.</param>
		/// <returns>SendGrid channel identifier.</returns>
		public static string SendGrid(this ChannelProviders channelProviders, string? providerId = null)
		{
			Guard.AgainstNull(channelProviders);
			return channelProviders.GetId(SendGridConstant.Id, providerId);
		}

		/// <summary>
		/// Adds SendGrid support to the channel provider configuration.
		/// </summary>
		/// <param name="channelProviderConfiguration">Channel provider configuration.</param>
		/// <param name="options">Configuration options for the SendGrid dispatcher.</param>
		/// <param name="providerId">Optional provider identifier.</param>
		/// <returns><paramref name="channelProviderConfiguration"/></returns>
		public static CommunicationsClientBuilder AddSendGridSupport(this CommunicationsClientBuilder channelProviderConfiguration, Action<SendGridOptions> options, string? providerId = null)
		{
			var opts = new SendGridOptions();
			options(opts);
			channelProviderConfiguration
				.ChannelProvider
				.Build(Id.ChannelProvider.SendGrid(providerId), opts)
				.AddDispatcher<EmailChannelProviderDispatcher, IEmail>(Id.Channel.Email())
				.AddEmailExtendedPropertiesAdaptor<ExtendedEmailChannelProperties>()
				.Register();

			return channelProviderConfiguration;
		}

		public static void Foo()
		{
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
		}
	}
}