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

using System.Collections.Generic;

namespace Transmitly.ChannelProvider.SendGrid.Configuration
{
	/// <summary>
	/// Represents the configuration options for the SendGrid channel provider.
	/// </summary>
	public sealed class SendGridOptions
	{
		/// <summary>
		/// Your Twilio SendGrid API key.
		/// </summary>
		public string? ApiKey { get; set; }
		/// <summary>
		/// Base url (e.g. https://api.sendgrid.com).
		/// </summary>
		public string Host { get; set; } = "https://api.sendgrid.com";
		/// <summary>
		/// API version
		/// </summary>
		public string Version { get; set; } = "v3";
		/// <summary>
		///  Indicates whether HTTP error responses should be raised as exceptions.
		/// </summary>
		public bool HttpErrorAsException { get; set; }
		/// <summary>
		/// A dictionary of request headers.
		/// </summary>
		public IDictionary<string, string>? RequestHeaders { get; set; }
		///<summary>
		/// Path to endpoint (e.g. /path/to/endpoint).
		///</summary>
		public string? UrlPath { get; set; }
		/// <summary>
		/// The Auth header value.
		/// </summary>
		public AuthenticationHeaderValue? Auth { get; set; }
		/// <summary>
		/// The reliability settings to use on HTTP Requests.
		/// </summary>
		public ReliabilitySettings? ReliabilitySettings { get; set; }
		/// <summary>
		/// Web proxy uri.
		/// </summary>
		public string? ProxyUri { get; set; }
		/// <summary>
		/// Gets the user agent string used in HTTP requests.
		/// </summary>
		public string UserAgent => _userAgent;

		private readonly string _userAgent = GetUserAgent();

		private static string GetUserAgent()
		{
			string? version = null;
			try
			{
				version = typeof(SendGridOptions).Assembly.GetName().Version?.ToString();
			}
			catch
			{
				//eat error
			}

			if (string.IsNullOrWhiteSpace(version))
				version = "0.1.0";

			return $"transmitly-sendgrid/{version}";
		}
	}
}