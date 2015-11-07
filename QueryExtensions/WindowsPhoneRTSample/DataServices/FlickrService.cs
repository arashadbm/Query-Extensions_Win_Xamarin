using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using WindowsPhoneRTSample.Models.Request;
using WindowsPhoneRTSample.Models.Response;
using Newtonsoft.Json;
using QueryExtensions;

namespace WindowsPhoneRTSample.DataServices
{

    public class FlickrService
    {
        #region Fields
        private const string BaseUrl = "https://api.flickr.com/services/rest/";
        //This API key is used in another sample for FLicker API: https://github.com/AhmedRashad/Explore-Flicker
        //DON'T USE THIS API KEY INSIDE ANY OF YOUR SOLUTIONS.
        //You can request API for flicker from here:https://www.flickr.com/services/
        private const string APiKey = "8cacc0510b4acf1e0434c70da9ec04a9";
        private const string Format = "json";
        private const int NoJsonCallBack = 1;
        #endregion

        #region Constructor

        #endregion

        //See documentation  of flikcer API:
        //https://www.flickr.com/services/api/flickr.photos.getRecent.html
        public async Task<ResponseWrapper<RecentPhotosResponse>> GetRecentPhotosAsync(RecentQueryParamters parameters)
        {
            parameters.ApiKey = APiKey;
            parameters.Format = Format;
            parameters.Nojsoncallback = NoJsonCallBack;
            //Construct the query string by calling the extension method AppendQueryString
            string requestUrl = (BaseUrl).AppendQueryString(parameters);
            return await GetAsync<RecentPhotosResponse>(requestUrl);
        }

        private async Task<ResponseWrapper<T>> GetAsync<T>(string url)
        {
            //wrap response to return other info about the operation when it fails
            var response = new ResponseWrapper<T>();
            if (!HasInternetAccess())
            {
                response.ResponseStatus = ResponseStatus.NoInternet;
                return response;
            }

            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue() { NoCache = true };
                var getResponse = await httpClient.GetAsync(url);

                var stringValue = await getResponse.Content.ReadAsStringAsync();
                response.StatusCode = getResponse.StatusCode;
                //check whether the operation is succesfull or not.
                if (!getResponse.IsSuccessStatusCode)
                {
                    response.ResponseStatus = ResponseStatus.HttpError;//Override responseWrapper status value to be HttpError
                }
                //parse Result
                response.Result = JsonConvert.DeserializeObject<T>(stringValue);
                return response;
            }
            catch (Exception)
            {
                response.ResponseStatus = ResponseStatus.ClientSideError;
            }
            return response;
        }

        private bool HasInternetAccess()
        {
            var connections = NetworkInformation.GetInternetConnectionProfile();
            var internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
            return internet;
        }

    }
}
