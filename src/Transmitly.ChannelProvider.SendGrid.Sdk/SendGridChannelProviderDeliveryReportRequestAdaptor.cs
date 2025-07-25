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
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Threading.Tasks;
using Transmitly.Delivery;
using System.Linq;

namespace Transmitly.ChannelProvider.SendGrid.Sdk.Email
{
	public sealed class SendGridChannelProviderDeliveryReportRequestAdaptor : IChannelProviderDeliveryReportRequestAdaptor
	{
		public Task<IReadOnlyCollection<DeliveryReport>?> AdaptAsync(IRequestAdaptorContext adaptorContext)
		{
			if (!ShouldAdapt(adaptorContext))
				return Task.FromResult<IReadOnlyCollection<DeliveryReport>?>(null);

			var statuses = JsonSerializer.Deserialize<List<SendGridEmailWebhookEvent>>(adaptorContext.Content!);

			if (statuses == null)
				return Task.FromResult<IReadOnlyCollection<DeliveryReport>?>(null);
			statuses = statuses.OrderByDescending(o=>o.Timestamp).ToList();
			var ret = new List<DeliveryReport>(statuses.Count);
			foreach (var emailReport in statuses)
			{
				var report = new SendGridEmailDeliveryReport(
						DeliveryReport.Event.StatusChanged(),
						Id.Channel.Email(),
						Id.ChannelProvider.SendGrid(),
						//SG uses a static webhook URL
						null,
						null,
						emailReport.MessageId,
						Util.ToDispatchStatus(emailReport.Event),
						null,
						null,
						null
					).ApplyExtendedProperties(emailReport);

				ret.Add(report);
			}

			return Task.FromResult<IReadOnlyCollection<DeliveryReport>?>(ret);
		}

		private static bool ShouldAdapt(IRequestAdaptorContext adaptorContext)
		{
			if (string.IsNullOrWhiteSpace(adaptorContext.Content))
				return false;
			return
				(adaptorContext.GetQueryValue(DeliveryUtil.ChannelIdKey)?.Equals(Id.Channel.Email(), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
				(adaptorContext.GetQueryValue(DeliveryUtil.ChannelProviderIdKey)?.StartsWith(Id.ChannelProvider.SendGrid(), StringComparison.InvariantCultureIgnoreCase) ?? false);


		}
	}
}