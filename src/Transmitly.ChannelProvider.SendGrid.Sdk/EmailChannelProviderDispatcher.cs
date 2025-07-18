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

using SendGrid;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Transmitly.ChannelProvider.SendGrid.Configuration;
using Transmitly.Util;

namespace Transmitly.ChannelProvider.SendGrid.Sdk.Configuration.Email
{
	/// <summary>
	/// Dispatches email communications using the SendGrid SDK.
	/// </summary>
	public sealed class EmailChannelProviderDispatcher : ChannelProviderRestDispatcher<IEmail>
	{
		private readonly string _userAgent;
		private readonly SendGridClientOptions _options;

		/// <summary>
		/// Initializes a new instance of the <see cref="EmailChannelProviderDispatcher"/> class with the specified SendGrid options.
		/// </summary>
		/// <param name="sendGridClientOptions"></param>
		public EmailChannelProviderDispatcher(SendGridOptions sendGridClientOptions)
		{
			Guard.AgainstNull(sendGridClientOptions);
			Guard.AgainstNullOrWhiteSpace(sendGridClientOptions.ApiKey, nameof(SendGridOptions.ApiKey));

			var reliabilitySettings = sendGridClientOptions.ReliabilitySettings;
			var authSettings = sendGridClientOptions.Auth;
			_userAgent = sendGridClientOptions.UserAgent;
			_options = new SendGridClientOptions
			{
				ApiKey = sendGridClientOptions.ApiKey,
				Host = sendGridClientOptions.Host,
				HttpErrorAsException = sendGridClientOptions.HttpErrorAsException,
				RequestHeaders = sendGridClientOptions.RequestHeaders != null ? new(
					sendGridClientOptions.RequestHeaders
				) : null,
				UrlPath = sendGridClientOptions.UrlPath,
				Version = sendGridClientOptions.Version,
				ReliabilitySettings = reliabilitySettings != null ? new(
					reliabilitySettings.MaximumNumberOfRetries,
					reliabilitySettings.MinimumBackOff,
					reliabilitySettings.MaximumBackOff,
					reliabilitySettings.DeltaBackOff
				) : new(),
				Auth = authSettings != null ? new(
					authSettings.Scheme,
					authSettings.Parameter
				) : null
			};
		}

		protected override async Task<IReadOnlyCollection<IDispatchResult?>> DispatchAsync(HttpClient restClient, IEmail email, IDispatchCommunicationContext communicationContext, CancellationToken cancellationToken)
		{
			Guard.AgainstNull(email);

			SendGridMessage? msg = null;

			var client = CreateClient(restClient);
			var emailProprties = new ExtendedEmailChannelProperties(email.ExtendedProperties);
			var from = new EmailAddress(email.From.Value, email.From.Display);
			var tos = email.To!.Select(m => new EmailAddress(m.Value, m.Display)).ToList();

			if (emailProprties.TemplateId is not null)
			{
				// Set the template ID if provided
				msg = MailHelper.CreateSingleTemplateEmailToMultipleRecipients(from,
					tos,
					emailProprties.TemplateId,
					communicationContext.ContentModel?.Model ?? new { }
				);
			}
			else
			{
				msg = MailHelper.CreateSingleEmailToMultipleRecipients(
					from,
					tos,
					email.Subject,
					email.TextBody,
					email.HtmlBody
			   );
			}

			if (msg == null)
			{
				throw new SendGridSdkDispatcherException("Unable to create a message to dispatch");
			}

			var sendResponse = await client.SendEmailAsync(msg, cancellationToken).ConfigureAwait(false);

			return [new SendGridDispatchResult(sendResponse)];
		}

		protected override void ConfigureHttpClient(HttpClient httpClient)
		{
			httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(_userAgent);
			base.ConfigureHttpClient(httpClient);
		}

		private SendGridClient CreateClient(HttpClient restClient)
		{
			return new SendGridClient(
				restClient,
				_options.ApiKey,
				_options.Host,
				_options.RequestHeaders,
				_options.Version,
				_options.UrlPath,
				_options.HttpErrorAsException
			);
		}
	}
}