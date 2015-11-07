using System;
using System.Collections.Generic;
using QueryExtensions;

namespace WindowsPhoneRTSample.Models.Request
{
    public class RecentQueryParamters
    {
        /// <summary>
        /// API Method
        /// </summary>
        [QueryParameter("method")]
        public string Method
        {
            get { return "flickr.photos.getRecent"; }
        }

        /// <summary>
        /// Your application ApiKey
        /// </summary>
        [QueryParameter("api_key")]
        public string ApiKey { get; set; }

        /// <summary>
        /// Format of response, ex: json
        /// </summary>
        [QueryParameter("format")]
        public string Format { get; set; }

        /// <summary>
        /// Default value is 1 for Json, as per flickr api sample
        /// </summary>
        [QueryParameter("nojsoncallback")]
        public int Nojsoncallback { get; set; }

        /// <summary>
        /// ArrayFormat has three possible values:
        /// DuplicateKeyWithBrackets => "cars[]=Saab&cars[]=Audi"
        /// DuplicateKey => "cars=Saab&cars=Audi"
        /// SingleKey => "cars=Saab,Audi"
        /// Default value is DuplicateKeyWithBrackets but we need SingleKey for Flicker API.
        /// </summary>
        [QueryArray("extras", ArrayFormat = ArrayFormat.SingleKey)]
        public List<string> Extras { get; set; }

        [QueryParameter("per_page")]
        public int PerPage { get; set; }

        /// <summary>
        /// Minimum value is 1
        /// </summary>
        [QueryParameter("page")]
        public int Page { get; set; }


    }

    public class RecentPhotosExtras
    {
        public const string Geo = "geo";
        public const string Description = "description";
    }
}
