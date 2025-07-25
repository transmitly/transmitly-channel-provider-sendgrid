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

using SendGrid;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Transmitly.ChannelProvider.SendGrid.Configuration;

namespace Transmitly.ChannelProvider.SendGrid.Sdk
{
	static class DispatchResultExtension
	{
		public static async Task<IReadOnlyCollection<SendGridDispatchResult>> ToDispatchResultsAsync(this Response? response, CancellationToken cancellationToken = default)
		{
			var result = new SendGridDispatchResult();
			if (response == null)
			{
				result.Status = CommunicationsStatus.ServerError(SendGridConstant.Id, "NoResponse", 1);
				return [result];
			}

			if (response.Headers?.TryGetValues("X-Message-ID", out var messageId) ?? false)
				result.ResourceId = messageId.FirstOrDefault();

			string? body = null;
			if (response.Body != null)
			{
#if !NET5_0_OR_GREATER
				body = await response.Body.ReadAsStringAsync();
#else
				body = await response.Body.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
#endif
			}
			var statusCode = (int)response.StatusCode;
			result.Status = response!.IsSuccessStatusCode ?
				CommunicationsStatus.Success(SendGridConstant.Id, "Dispatched", statusCode, body) :
				CommunicationsStatus.ServerError(SendGridConstant.Id, "Error", statusCode, body);

			return [result];
		}
	}
}