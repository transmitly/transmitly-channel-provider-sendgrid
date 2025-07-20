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

using Transmitly.Channel.Configuration;
using Transmitly.ChannelProvider.SendGrid.Configuration;
using Transmitly.Util;

namespace Transmitly.ChannelProvider.SendGrid.Sdk.Configuration.Email
{
	/// <inheritdoc cref="IEmailExtendedChannelProperties"/>
	public sealed class ExtendedEmailChannelProperties : IEmailExtendedChannelProperties
	{
		private readonly IExtendedProperties? _extendedProperties;
		
		private const string ProviderKey = SendGridConstant.EmailPropertiesKey;

		public ExtendedEmailChannelProperties()
		{

		}

		internal ExtendedEmailChannelProperties(IExtendedProperties properties)
		{
			_extendedProperties = properties;
		}

		private ExtendedEmailChannelProperties(IChannel<IEmail> emailChannel)
		{
			Guard.AgainstNull(emailChannel);
			_extendedProperties = Guard.AgainstNull(emailChannel.ExtendedProperties);

		}

		public string? MessageId
		{
			get
			{
				return _extendedProperties?.GetValue<string>(ProviderKey, nameof(MessageId));
			}
			set
			{
				_extendedProperties?.AddOrUpdate(ProviderKey, nameof(MessageId), value);
			}
		}

		public string? TemplateId
		{
			get
			{
				return _extendedProperties?.GetValue<string>(ProviderKey, nameof(TemplateId));
			}
			set
			{
				_extendedProperties?.AddOrUpdate(ProviderKey, nameof(TemplateId), value);
			}
		}

		public IEmailExtendedChannelProperties Adapt(IChannel<IEmail> email)
		{
			return new ExtendedEmailChannelProperties(email);
		}
	}
}