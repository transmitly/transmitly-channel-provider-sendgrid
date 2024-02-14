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

using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Transmitly.ChannelProvider;

namespace Transmitly.SendGrid
{
	internal class SendGridChannelProviderClient : ChannelProviderClient<IEmail, SendGridMessage>
	{
		private readonly string _apiKey;
		public SendGridChannelProviderClient(SendGridClientOptions sendGridClientOptions)
		{
			if (sendGridClientOptions is null)
			{
				throw new ArgumentNullException(nameof(sendGridClientOptions));
			}

			_apiKey = sendGridClientOptions.ApiKey;
		}

		public override async Task<IReadOnlyCollection<IDispatchResult?>> DispatchAsync(SendGridMessage communication, IDispatchCommunicationContext communicationContext, CancellationToken cancellationToken)
		{
			var client = new SendGridClient(_apiKey);

			var res = await client.SendEmailAsync(communication, cancellationToken).ConfigureAwait(false);
			return [new SendGridResult { IsDelivered = res.IsSuccessStatusCode, DispatchStatus = res.IsSuccessStatusCode ? DispatchStatus.Dispatched : DispatchStatus.Error }];
		}

		public override async Task<IReadOnlyCollection<IDispatchResult?>> DispatchAsync(IEmail communication, IDispatchCommunicationContext communicationContext, CancellationToken cancellationToken)
		{
			Guard.AgainstNull(communication);
			var client = new SendGridClient(_apiKey);

			var email = communication;
			var msg = MailHelper.CreateSingleEmailToMultipleRecipients(
				new EmailAddress(communication.From.Value, communication.From.Display),
				communication.To!.Select(m => new EmailAddress(m.Value, m.Display)).ToList(),
				email.Subject,
				email.TextBody,
				email.HtmlBody);

			var res = await client.SendEmailAsync(msg, cancellationToken).ConfigureAwait(false);
			return [new SendGridResult { IsDelivered = res.IsSuccessStatusCode, DispatchStatus = res.IsSuccessStatusCode ? DispatchStatus.Dispatched : DispatchStatus.Error }];
		}
	}
}