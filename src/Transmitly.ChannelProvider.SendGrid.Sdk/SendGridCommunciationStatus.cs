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

namespace Transmitly.ChannelProvider.SendGrid.Sdk
{
	/// <summary>
	/// SendGrid communication status.
	/// </summary>
	static class SendGridCommunciationStatus
	{
		private const string StatusId = "SendGrid";
		/// <summary>
		/// Success status for SendGrid communications.
		/// </summary>
		public static readonly CommunicationsStatus Success = CommunicationsStatus.Success(StatusId, "Dispatched");
		/// <summary>
		/// Server error status for SendGrid communications.
		/// </summary>
		public static readonly CommunicationsStatus ServerError = CommunicationsStatus.ServerError(StatusId, "Exception");
		/// <summary>
		/// Default status for SendGrid communications.
		/// </summary>
		public static readonly CommunicationsStatus Unknown = CommunicationsStatus.ClientError(StatusId, "NotSet");
		/// <summary>
		/// Get the status based on the success flag.
		/// </summary>
		/// <param name="isSuccess">Whether the dispatch was successful.</param>
		/// <returns>SendgGrid communications status.</returns>
		public static CommunicationsStatus GetStatus(bool isSuccess)
		{
			return isSuccess ? Success : ServerError;
		}
	}
}