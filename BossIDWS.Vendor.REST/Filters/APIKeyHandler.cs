using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BossIDWS.Vendor.REST.Filters
{
	/// <summary>
	/// 
	/// </summary>
	public class APIKeyHandler : DelegatingHandler
	{
		//set a default API key 
		//Todo: Get valid VendorApiKey assigned by BIR BossID....
		private string _vendorApiKey;

		/// <summary>
		/// 
		/// </summary>
		public APIKeyHandler()
		{
			_vendorApiKey = Utils.GetAppSettingUsingConfigurationManager("VendorApiKey");
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var isValidAPIKey = false;
			IEnumerable<string> lsHeaders;

			//Validate that the api key exists

			var checkApiKeyExists = request.Headers.TryGetValues("API-KEY", out lsHeaders);

			if (checkApiKeyExists)
			{
				var firstOrDefault = lsHeaders.FirstOrDefault();
				if (firstOrDefault != null && firstOrDefault.Equals(_vendorApiKey))
				{
					isValidAPIKey = true;
				}
			}

			//If the key is not valid, return an http status code.
			if (!isValidAPIKey)
				return request.CreateResponse(HttpStatusCode.Forbidden, "Bad API Key");

			//Allow the request to process further down the pipeline
			var response = await base.SendAsync(request, cancellationToken);

			//Return the response back up the chain
			return response;
		}
	}
}