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

namespace Transmitly.ChannelProvider.SendGrid.Configuration
{
	/// <summary>
	/// A slightly modified version of SendGrid's <c>ReliabilitySettings</c> class.
	/// </summary>
	public sealed class ReliabilitySettings
	{
		/// <summary>
		/// Gets the maximum number of retries to execute against when sending an HTTP request
		/// before throwing an exception. Defaults to 0 (no retries; must be explicitly enabled).
		/// </summary>
		public int MaximumNumberOfRetries { get; }

		/// <summary>
		/// Gets the minimum amount of time to wait between HTTP retries. Defaults to 1 second.
		/// </summary>
		public TimeSpan MinimumBackOff { get; }

		/// <summary>
		/// Gets the maximum amount of time to wait between HTTP retries. Defaults to 10 seconds.
		/// </summary>
		public TimeSpan MaximumBackOff { get; }

		/// <summary>
		/// Gets the value used to calculate a random delta in the exponential delay between retries.
		/// Defaults to 1 second.
		/// </summary>
		public TimeSpan DeltaBackOff { get; }

		/// <summary>
		/// Gets the list of HTTP status codes for which a request will be retried.
		/// </summary>
		public ICollection<int> RetriableServerErrorStatusCodes { get; } = [.. DefaultRetriableServerErrorStatusCodes];

		/// <summary>
		/// Gets the default list of HTTP status codes for which a request will be retried.
		/// </summary>
		public static ICollection<int> DefaultRetriableServerErrorStatusCodes { get; } =
			[
				500, //InternalServerErrorr,
				502, //BadGateway,
				503, //ServiceUnavailable,
				504, //GatewayTimeout
			];

		/// <summary>
		/// Initializes a new instance of the <see cref="ReliabilitySettings"/> class with default settings.
		/// </summary>
		public ReliabilitySettings()
			: this(0, TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ReliabilitySettings"/> class.
		/// </summary>
		/// <param name="maximumNumberOfRetries">
		/// The maximum number of retries to execute when sending an HTTP request before throwing an exception.
		/// Maximum value: 5. Default: 0.
		/// </param>
		/// <param name="minimumBackoff">
		/// The minimum time to wait between HTTP retries. Default: 0 seconds.
		/// </param>
		/// <param name="maximumBackOff">
		/// The maximum time to wait between HTTP retries. Maximum: 30 seconds. Default: 0 seconds.
		/// </param>
		/// <param name="deltaBackOff">
		/// The value used to calculate a random delta in the exponential delay between retries.
		/// Default: 0 seconds.
		/// </param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown when any argument is out of valid range.
		/// </exception>
		public ReliabilitySettings(int maximumNumberOfRetries, TimeSpan minimumBackoff, TimeSpan maximumBackOff, TimeSpan deltaBackOff)
		{
			if (maximumNumberOfRetries < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(maximumNumberOfRetries), "maximumNumberOfRetries must be greater than 0");
			}

			if (maximumNumberOfRetries > 5)
			{
				throw new ArgumentOutOfRangeException(nameof(maximumNumberOfRetries), "The maximum number of retries allowed is 5");
			}

			if (minimumBackoff.Ticks < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(minimumBackoff), "minimumBackoff must be greater than 0");
			}

			if (maximumBackOff.Ticks < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(maximumBackOff), "maximumBackOff must be greater than 0");
			}

			if (maximumBackOff.TotalSeconds > 30.0)
			{
				throw new ArgumentOutOfRangeException(nameof(maximumBackOff), "maximumBackOff must be less than 30 seconds");
			}

			if (deltaBackOff.Ticks < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(deltaBackOff), "deltaBackOff must be greater than 0");
			}

			if (minimumBackoff.TotalMilliseconds > maximumBackOff.TotalMilliseconds)
			{
				throw new ArgumentOutOfRangeException(nameof(minimumBackoff), "minimumBackoff must be less than maximumBackOff");
			}

			MaximumNumberOfRetries = maximumNumberOfRetries;
			MinimumBackOff = minimumBackoff;
			DeltaBackOff = deltaBackOff;
			MaximumBackOff = maximumBackOff;
		}
	}
}
