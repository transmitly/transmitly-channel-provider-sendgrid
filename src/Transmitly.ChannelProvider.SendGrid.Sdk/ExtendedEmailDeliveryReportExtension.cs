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

namespace Transmitly.ChannelProvider.SendGrid.Sdk.Email
{
	static class ExtendedEmailDeliveryReportExtension
	{
		public static SendGridEmailDeliveryReport ApplyExtendedProperties(this SendGridEmailDeliveryReport emailDeliveryReport, SendGridEmailWebhookEvent report)
		{
			_ = new ExtendedEmailDeliveryReportProperties(emailDeliveryReport)
			{
				AsmGroupId = report.AsmGroupId,
				MessageId = report.MessageId,
				From = report.From,
				EventId = report.EventId,
				Event = report.Event,
				Email = report.Email,
				Domain = report.Domain,
				Categories = report.Categories,
				BounceClassification = report.BounceClassification,
				Reason = report.Reason,
				Response = report.Response,
				MachineOpen = report.MachineOpen,
				SmtpId = report.SmtpId,
				Status = report.Status,
				Timestamp = report.Timestamp,
				Type = report.Type,
				Url = report.Url,
				UserAgent = report.UserAgent
			};

			return emailDeliveryReport;
		}
	}
}