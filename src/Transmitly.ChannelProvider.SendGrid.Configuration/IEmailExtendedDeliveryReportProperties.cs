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
using System.Collections.Generic;
using Transmitly.Channel.Configuration.Email;
using Transmitly.ChannelProvider.SendGrid.Configuration;

namespace Transmitly
{
	public interface IEmailExtendedDeliveryReportProperties
	{
		IEmailExtendedChannelProperties Adapt(IEmailChannelConfiguration email);
		int? AsmGroupId { get; set; }
		string? BounceClassification { get; set; }
		IReadOnlyCollection<string>? Categories { get; set; }
		string? Domain { get; set; }
		string? Email { get; set; }
		string? Event { get; set; }
		string? EventId { get; set; }
		string? From { get; set; }
		bool? MachineOpen { get; set; }
		string? MessageId { get; set; }
		string? Reason { get; set; }
		string? Response { get; set; }
		string? SmtpId { get; set; }
		string? Status { get; set; }
		DateTimeOffset? Timestamp { get; set; }
		string? Type { get; set; }
		string? Url { get; set; }
		string? UserAgent { get; set; }
		int? Attempt { get; set; }
	}
}