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

namespace Transmitly.ChannelProvider.SendGrid.Configuration
{
	/// <summary>
	/// Extended properties for SendGrid emails.
	/// </summary>
	public interface IEmailExtendedChannelProperties
	{
		/// <summary>
		/// Adapts the provided email channel to SendGrid extended properties.
		/// </summary>
		/// <param name="email">Channel to adapt.</param>
		/// <returns>Extended email proprties.</returns>
		IEmailExtendedChannelProperties Adapt(IChannel<IEmail> email);
		/// <summary>
		/// Gets or sets the SendGrid message ID for the email.
		/// </summary>
		public string? TemplateId { get; set; }
		/// <summary>
		/// Gets or sets the SendGrid template ID for the email.
		/// </summary>
		public string? MessageId { get; set; }
	}
}