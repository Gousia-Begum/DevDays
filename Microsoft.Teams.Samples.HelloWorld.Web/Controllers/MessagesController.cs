using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Teams;
using Microsoft.Bot.Connector.Teams.Models;
using Microsoft.Teams.Samples.HelloWorld.Web.Models;
using Microsoft.Teams.Samples.HelloWorld.Web.Helper;
using Microsoft.Bot.Builder.Dialogs;

namespace Microsoft.Teams.Samples.HelloWorld.Web.Controllers
{

    [BotAuthentication]
    public class MessagesController : ApiController
    {
        public static Dictionary<int, StudentValues> result = new Dictionary<int, StudentValues>();
        private static int i = 0;

        //public static List<string> speakers;

        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            using (var connector = new ConnectorClient(new Uri(activity.ServiceUrl)))
            {
                var val = activity.Value;
                if (activity.IsComposeExtensionQuery())
                {
                    var response = MessageExtension.HandleMessageExtensionQuery(connector, activity, result);
                    return response != null
                    ? Request.CreateResponse<ComposeExtensionResponse>(response)
                    : new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    if (activity.Value != null)
                    {
                        result = await GetActivity(activity);
                    }
                    else
                    {
                        await EchoBot.EchoMessage(connector, activity);
                    }
                }
                if (activity.Type == ActivityTypes.ConversationUpdate)
                {
                    for (int i = 0; i < activity.MembersAdded.Count; i++)
                    {
                        if (activity.MembersAdded[i].Id == activity.Recipient.Id)
                        {
                            var reply = activity.CreateReply();
                            reply.Text = "Hi! I am the Event Management bot for Dev Days 2019 happening in Taiwan. Ask me questions about the event and I can help you find answers Ask me questions like \n\n" +
                                  "   *\r What is the venue? \r\n\n" +
                                  "   *\r What tracks are available? \r\n\n" +
                                  "   *\r Do we have workshops planned? \n\n".Replace("\n", Environment.NewLine);

                            await connector.Conversations.ReplyToActivityAsync(reply);

                            var channelData = activity.GetChannelData<TeamsChannelData>();
                            if(channelData.Team != null)
                            {
                                ConversationParameters param = new ConversationParameters()
                                {
                                    Members = new List<ChannelAccount>() { new ChannelAccount() { Id = activity.From.Id } },
                                    ChannelData = new TeamsChannelData()
                                    {
                                        Tenant = channelData.Tenant,
                                        Notification = new NotificationInfo() { Alert = true}
                                    }
                                    
                                };

                                var resource = await connector.Conversations.CreateConversationAsync(param);
                                await connector.Conversations.SendToConversationAsync(resource.Id, reply);


                            }
                            break;
                        }
                    }
                }
                return new HttpResponseMessage(HttpStatusCode.Accepted);
            }

        }





        public async static Task<Dictionary<int, StudentValues>> GetActivity(Activity a)
        {
            var jSonObj = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(a.Value.ToString());

            StudentValues item = new StudentValues
            {
                Name = jSonObj["myName"],
                Email = jSonObj["myEmail"],
                Phone = jSonObj["myTel"]
            };

            result.Add(i++, item);

            return result;
        }
    }
}


