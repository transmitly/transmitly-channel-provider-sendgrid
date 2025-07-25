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
using Transmitly.Delivery;
using Transmitly.Util;

namespace Transmitly.ChannelProvider.SendGrid.Sdk.Email
{
	public sealed class ExtendedEmailDeliveryReportProperties : IEmailExtendedDeliveryReportProperties
	{
		private readonly IExtendedProperties _extendedProperties;
		private const string ProviderKey = SendGridConstant.EmailPropertiesKey;

		internal ExtendedEmailDeliveryReportProperties(DeliveryReport deliveryReport)
		{
			_extendedProperties = Guard.AgainstNull(deliveryReport).ExtendedProperties;
		}

		internal ExtendedEmailDeliveryReportProperties(IExtendedProperties properties)
		{
			_extendedProperties = Guard.AgainstNull(properties);
		}

		internal void Apply(SendGridEmailWebhookEvent report)
		{
			Email = report.Email;
			Domain = report.Domain;
			From = report.From;
			Timestamp = report.Timestamp;
			SmtpId = report.SmtpId;
			BounceClassification = report.BounceClassification;
			Event = report.Event;
			Categories = report.Categories;
			EventId = report.EventId;
			MessageId = report.MessageId;
			Reason = report.Reason;
			Status = report.Status;
			Type = report.Type;
			Response = report.Response;
			UserAgent = report.UserAgent;
			Url = report.Url;
			MachineOpen = report.MachineOpen;
			AsmGroupId = report.AsmGroupId;
			if (int.TryParse(report.Attempt, out var attemptInt))
				Attempt = attemptInt;
		}

		public IEmailExtendedChannelProperties Adapt(IEmailChannelConfiguration email)
		{
			return new ExtendedEmailChannelProperties(email);
		}

		public string? Email
		{
			get => _extendedProperties.GetValue<string?>(ProviderKey, nameof(Email));
			set => _extendedProperties.AddOrUpdate(ProviderKey, nameof(Email), value);
		}

		public string? Domain
		{
			get => _extendedProperties.GetValue<string?>(ProviderKey, nameof(Domain));
			set => _extendedProperties.AddOrUpdate(ProviderKey, nameof(Domain), value);
		}

		public string? From
		{
			get => _extendedProperties.GetValue<string?>(ProviderKey, nameof(From));
			set => _extendedProperties.AddOrUpdate(ProviderKey, nameof(From), value);
		}

		public DateTimeOffset? Timestamp
		{
			get => _extendedProperties.GetValue<DateTimeOffset?>(ProviderKey, nameof(Timestamp));
			set => _extendedProperties.AddOrUpdate(ProviderKey, nameof(Timestamp), value);
		}

		public string? SmtpId
		{
			get => _extendedProperties.GetValue<string?>(ProviderKey, nameof(SmtpId));
			set => _extendedProperties.AddOrUpdate(ProviderKey, nameof(SmtpId), value);
		}

		public string? BounceClassification
		{
			get => _extendedProperties.GetValue<string?>(ProviderKey, nameof(BounceClassification));
			set => _extendedProperties.AddOrUpdate(ProviderKey, nameof(BounceClassification), value);
		}

		public string? Event
		{
			get => _extendedProperties.GetValue<string?>(ProviderKey, nameof(Event));
			set => _extendedProperties.AddOrUpdate(ProviderKey, nameof(Event), value);
		}

		public IReadOnlyCollection<string>? Categories
		{
			get => _extendedProperties.GetValue<List<string>?>(ProviderKey, nameof(Categories));
			set => _extendedProperties.AddOrUpdate(ProviderKey, nameof(Categories), value);
		}

		public string? EventId
		{
			get => _extendedProperties.GetValue<string?>(ProviderKey, nameof(EventId));
			set => _extendedProperties.AddOrUpdate(ProviderKey, nameof(EventId), value);
		}

		public string? MessageId
		{
			get => _extendedProperties.GetValue<string?>(ProviderKey, nameof(MessageId));
			set => _extendedProperties.AddOrUpdate(ProviderKey, nameof(MessageId), value);
		}

		public string? Reason
		{
			get => _extendedProperties.GetValue<string?>(ProviderKey, nameof(Reason));
			set => _extendedProperties.AddOrUpdate(ProviderKey, nameof(Reason), value);
		}

		public string? Status
		{
			get => _extendedProperties.GetValue<string?>(ProviderKey, nameof(Status));
			set => _extendedProperties.AddOrUpdate(ProviderKey, nameof(Status), value);
		}

		public string? Type
		{
			get => _extendedProperties.GetValue<string?>(ProviderKey, nameof(Type));
			set => _extendedProperties.AddOrUpdate(ProviderKey, nameof(Type), value);
		}

		public string? Response
		{
			get => _extendedProperties.GetValue<string?>(ProviderKey, nameof(Response));
			set => _extendedProperties.AddOrUpdate(ProviderKey, nameof(Response), value);
		}

		public string? UserAgent
		{
			get => _extendedProperties.GetValue<string?>(ProviderKey, nameof(UserAgent));
			set => _extendedProperties.AddOrUpdate(ProviderKey, nameof(UserAgent), value);
		}

		public string? Url
		{
			get => _extendedProperties.GetValue<string?>(ProviderKey, nameof(Url));
			set => _extendedProperties.AddOrUpdate(ProviderKey, nameof(Url), value);
		}

		public bool? MachineOpen
		{
			get => _extendedProperties.GetValue<bool?>(ProviderKey, nameof(MachineOpen));
			set => _extendedProperties.AddOrUpdate(ProviderKey, nameof(MachineOpen), value);
		}

		public int? AsmGroupId
		{
			get => _extendedProperties.GetValue<int?>(ProviderKey, nameof(AsmGroupId));
			set => _extendedProperties.AddOrUpdate(ProviderKey, nameof(AsmGroupId), value);
		}

		public int? Attempt
		{
			get => _extendedProperties.GetValue<int?>(ProviderKey, nameof(AsmGroupId));
			set => _extendedProperties.AddOrUpdate(ProviderKey, nameof(AsmGroupId), value);
		}
	}
}