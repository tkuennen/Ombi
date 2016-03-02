﻿#region Copyright
// /************************************************************************
//    Copyright (c) 2016 Jamie Rees
//    File: TheTvDbApi.cs
//    Created By: Jamie Rees
//   
//    Permission is hereby granted, free of charge, to any person obtaining
//    a copy of this software and associated documentation files (the
//    "Software"), to deal in the Software without restriction, including
//    without limitation the rights to use, copy, modify, merge, publish,
//    distribute, sublicense, and/or sell copies of the Software, and to
//    permit persons to whom the Software is furnished to do so, subject to
//    the following conditions:
//   
//    The above copyright notice and this permission notice shall be
//    included in all copies or substantial portions of the Software.
//   
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//    EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//    NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//    LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//    OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//    WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//  ************************************************************************/
#endregion

using Newtonsoft.Json;

using RequestPlex.Api.Models.Tv;

using RestSharp;

namespace RequestPlex.Api
{
    public class TheTvDbApi : TvBase
    {
        public TheTvDbApi()
        {
            Api = new ApiRequest();
        }
        private ApiRequest Api { get; }

        /// <summary>
        /// Authenticates against TheTVDB.
        /// </summary>
        /// <returns></returns>
        public Authentication Authenticate()
        {
            var request = new RestRequest
            {
                Method = Method.POST,
                Resource = "login"
            };
            var apiKey = new { apikey = ApiKey };
            var json = JsonConvert.SerializeObject(apiKey);
            request.AddBody(json);

            return Api.Execute<Authentication>(request, Url);
        }

        /// <summary>
        /// Refreshes the token.
        /// </summary>
        /// <param name="oldToken">The old token.</param>
        /// <returns></returns>
        public Authentication RefreshToken(string oldToken)
        {
            var request = new RestRequest
            {
                Method = Method.POST,
                Resource = "refresh_token"
            };
            request.AddHeader("Authorization", $"Bearer {oldToken}");

            return Api.Execute<Authentication>(request, Url);
        }

        /// <summary>
        /// Searches for a tv series.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public TvSearchResult SearchTv(string searchTerm, string token)
        {
            var request = new RestRequest
            {
                Method = Method.POST,
                Resource = "search/series?name={searchTerm}"
            };
            request.AddUrlSegment("searchTerm", searchTerm);
            request.AddHeader("Authorization", $"Bearer {token}");

            return Api.Execute<TvSearchResult>(request, Url);
        }


        /// <summary>
        /// Gets the information for a TV Series.
        /// </summary>
        /// <param name="tvdbId">The TVDB identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public TvSearchResult GetInformation(int tvdbId, string token)
        {
            var request = new RestRequest
            {
                Method = Method.POST,
                Resource = "search/{id}"
            };
            request.AddUrlSegment("id", tvdbId.ToString());
            request.AddHeader("Authorization", $"Bearer {token}");

            return Api.Execute<TvSearchResult>(request, Url);
        }
    }
}
