using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LottoFunctions.Data
{
    public class Facebook
    {
        readonly string _accessToken;
        readonly string _pageID;
        readonly string _facebookAPI = "https://graph.facebook.com/";
        readonly string _pageEdgeFeed = "feed";
        readonly string _pageEdgePhotos = "photos";
        readonly string _postToPageURL;
        readonly string _postToPagePhotosURL;

        public Facebook(string accessToken, string pageID)
        {
            _accessToken = accessToken;
            _pageID = pageID;
            _postToPageURL = $"{_facebookAPI}{pageID}/{_pageEdgeFeed}";
            _postToPagePhotosURL = $"{_facebookAPI}{pageID}/{_pageEdgePhotos}";
        }

        /// <summary>
        /// Publish a simple text post
        /// </summary>
        /// <returns>StatusCode and JSON response</returns>
        /// <param name="postText">Text for posting</param>
        public async Task<Tuple<int, string>> PublishSimplePost(string postText)
        {
            using (var http = new HttpClient())
            {
                var postData = new Dictionary<string, string> {
                { "access_token", _accessToken },
                { "message", postText }//,
                // { "formatting", "MARKDOWN" } // doesn't work
            };

                var httpResponse = await http.PostAsync(
                    _postToPageURL,
                    new FormUrlEncodedContent(postData)
                    );
                var httpContent = await httpResponse.Content.ReadAsStringAsync();

                return new Tuple<int, string>(
                    (int)httpResponse.StatusCode,
                    httpContent
                    );
            }
        }      
       
    }
}
