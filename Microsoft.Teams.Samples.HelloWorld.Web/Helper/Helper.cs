using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Web.Script.Serialization;
using Microsoft.Teams.Samples.HelloWorld.Web.Models;
using Microsoft.Bot.Connector;
using System.Net.Http;
using System.Text;

namespace Microsoft.Teams.Samples.HelloWorld.Web.Helper
{
    public class Helperr
    {
        public List<SpeakerBios> SpeakerInformation()
        {
            string file = System.Web.Hosting.HostingEnvironment.MapPath("~/json/")+  @"/SpeakerBios.json";

            List<SpeakerBios> speakers = new List<SpeakerBios>();

            string json = File.ReadAllText(file).Replace("##BaseURL##",ApplicationSettings.BaseUrl);
            speakers = (new JavaScriptSerializer().Deserialize<List<SpeakerBios>>(json));

            return speakers;
        }

        public List<SessionList> SessionInformation()
        {
            string file = System.Web.Hosting.HostingEnvironment.MapPath("~/json/") + @"/SessionList.json";

            List<SessionList> sessions = new List<SessionList>();

            string json = File.ReadAllText(file);
            sessions = (new JavaScriptSerializer().Deserialize<List<SessionList>>(json));

            return sessions;
        }

        public async Task<string> GetQnAAnswer(string question)
        {
            try
            {
                string endPointKey = "EndpointKey f6f95a38-6b7d-4fae-94ce-668c4de13f85";
                string endpoint = "https://devdaysqna.azurewebsites.net/qnamaker/knowledgebases/7a9b5033-64c4-4095-8387-02ba04df32ae";
                var uri = endpoint + "/generateAnswer";

                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage())
                {
                    request.Method = HttpMethod.Post;
                    request.RequestUri = new Uri(uri);
                    request.Content = new StringContent("{question:'" + question + "'}", Encoding.UTF8, "application/json");

                    // NOTE: The value of the header contains the string/text 'EndpointKey ' with the trailing space
                    request.Headers.Add("Authorization", endPointKey);

                    var response = await client.SendAsync(request);
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}