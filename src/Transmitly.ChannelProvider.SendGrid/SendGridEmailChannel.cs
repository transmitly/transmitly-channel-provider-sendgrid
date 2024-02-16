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

using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Transmitly.Template.Configuration;

namespace Transmitly.SendGrid
{
	internal class SendGridEmailChannel(IAudienceAddress fromAddress, string[]? allowedChannelProviders) : ISendGridEmailChannel
	{
		private static readonly Regex Rfc2822Regex = new("(?:(?<name>[^\\<]*)\\<(?<email>.*@.*)\\>|(?<name>)(?<email>[^\\<]*@.*[^\\>]))", RegexOptions.ECMAScript | RegexOptions.Compiled, TimeSpan.FromMilliseconds(250));
		public IContentTemplateConfiguration Subject => new ContentTemplateConfiguration();

		public IContentTemplateConfiguration HtmlBody => new ContentTemplateConfiguration();

		public IContentTemplateConfiguration TextBody => new ContentTemplateConfiguration();

		public IAudienceAddress FromAddress { get; set; } = Guard.AgainstNull(fromAddress);

		public string Id { get; } = Transmitly.Id.Channel.Email(nameof(SendGridMessage));

		public string? TemplateId { get; set; }

		public IEnumerable<string> AllowedChannelProviderIds => allowedChannelProviders ?? [];

		public async Task<object> GenerateCommunicationAsync(IDispatchCommunicationContext communicationContext)
		{
			var subject = await Subject.RenderAsync(communicationContext, false);
			var htmlBody = await HtmlBody.RenderAsync(communicationContext, false);
			var textBody = await TextBody.RenderAsync(communicationContext, false);

			//todo: attachments
			var to = communicationContext.RecipientAudiences.SelectMany(m => m.Addresses).Select(x => new EmailAddress(x.Value, x.Display)).ToList();

			if (string.IsNullOrEmpty(TemplateId))
				return MailHelper.CreateSingleEmailToMultipleRecipients(new EmailAddress(FromAddress.Value, FromAddress.Display), to, subject, textBody, htmlBody);
			return MailHelper.CreateSingleTemplateEmailToMultipleRecipients(new EmailAddress(FromAddress.Value, FromAddress.Display), to, TemplateId, communicationContext.ContentModel?.Model);
		}

		public bool SupportsAudienceAddress(IAudienceAddress audienceAddress)
		{
			return Rfc2822Regex.IsMatch(audienceAddress.Value);
		}
	}
}
