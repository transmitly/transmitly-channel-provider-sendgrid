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
using Transmitly.ChannelProvider.Configuration;
using Transmitly.Util;

namespace Transmitly.ChannelProvider.SendGrid.Configuration
{
	/// <summary>
	/// Extensions for configuring SendGrid extended properties for email channels.
	/// </summary>
	public static class SendGridChannelProviderExtendedPropertiesBuilderExtensions
	{
		private static Type? _emailAdaptorType;

		/// <summary>
		/// Gets the email extended channel properties adaptor.
		/// </summary>
		internal static IEmailExtendedChannelProperties Email => Create<IEmailExtendedChannelProperties>(Guard.AgainstNull(_emailAdaptorType));

		/// <summary>
		/// Creates an instance of the specified type.
		/// </summary>
		/// <typeparam name="T">Type to create a new instance of</typeparam>
		/// <param name="t"></param>
		/// <returns></returns>
		private static T Create<T>(Type t)
		{
			return (T)Guard.AgainstNull(Activator.CreateInstance(t));
		}

		/// <summary>
		/// Adds an email extended properties adaptor to the channel provider registration builder.
		/// </summary>
		/// <typeparam name="T">The concrete <see cref="IEmailExtendedChannelProperties"/> to register for email extended properties.</typeparam>
		/// <param name="builder">Builder object.</param>
		/// <returns>The provided builder object.</returns>
		public static ChannelProviderRegistrationBuilder AddEmailExtendedPropertiesAdaptor<T>(this ChannelProviderRegistrationBuilder builder)
			where T : class, IEmailExtendedChannelProperties, new()
		{
			_emailAdaptorType = typeof(T);
			return builder;
		}

	}
}