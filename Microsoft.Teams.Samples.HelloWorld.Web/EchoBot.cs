using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using AdaptiveCards;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Teams;
using Microsoft.Teams.Samples.HelloWorld.Web.Helper;

namespace Microsoft.Teams.Samples.HelloWorld.Web
{
    public class EchoBot
    {
        public static async Task EchoMessage(ConnectorClient connector, Activity activity)
        {
            var message = activity.GetTextWithoutMentions();

            if(string.Compare( message,"hello", true) == 0 || string.Compare(message, "help", true) == 0)
            {
                // Send help message
                var response = activity.CreateReply();
                response.Text = "Hi! I am the Event Management bot for Dev Days 2019 happening in Taiwan. Ask me questions about the event and I can help you find answers Ask me questions like \n\n" +
                                "   *\r What is the venue? \r\n\n" +
                                "   *\r What tracks are available? \r\n\n" +
                                "   *\r Do we have workshops planned? \n\n".Replace("\n", Environment.NewLine);
                await connector.Conversations.ReplyToActivityWithRetriesAsync(response);
                return;

            }
            //if(string.Compare(message, "feedback", true) == 0)
            //{
            //    var replyy = activity.CreateReply();
            //    var cardRes = CardResponse();
            //    replyy.Attachments.Add(cardRes);
            //    await connector.Conversations.ReplyToActivityWithRetriesAsync(replyy);
            //    return;
            //}
            Helperr helper = new Helperr();
            var userInput = activity.GetTextWithoutMentions();
            var reply = activity.CreateReply();
            var apiResponse = await helper.GetQnAAnswer(userInput);

            if (!string.IsNullOrEmpty(apiResponse))
            {
                QnAAnswer ans = (new JavaScriptSerializer().Deserialize<QnAAnswer>(apiResponse));
                var answer = ans.answers.FirstOrDefault();
                if (answer != null)
                {
                    if (answer.answer == "No good match found in KB.")
                    {
                        reply.Text = "Sorry! I do not have answer to this question. Please ask me questions about events";
                    }
                    else
                        reply.Text = answer.answer;
                }
                else
                {
                    reply.Text = "Sorry, did not get you question.";
                }
            }
            else
            {
                reply.Text = "Unable to fetch response from QnA maker service. Please try again.";
            }
            await connector.Conversations.ReplyToActivityWithRetriesAsync(reply);

        }

        public static Attachment CardResponse()
        {
            AdaptiveCardParseResult result = AdaptiveCard.FromJson(getJSon());
            return new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = result.Card
            };
        }

        public static string getJSon()
        {
            var path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/Cards/feedback.json");
            return File.ReadAllText(path);
        }
    }


    public class QnAAnswer

    {

        public Answer[] answers { get; set; }

        public object debugInfo { get; set; }

    }
    public class Answer
    {
        public string[] questions { get; set; }

        public string answer { get; set; }

        public float score { get; set; }

        public int id { get; set; }

        public string source { get; set; }

        public object[] metadata { get; set; }

        public Context context { get; set; }

    }
          
    public class Context
    {
        public bool isContextOnly { get; set; }

        public object[] prompts { get; set; }

    }


}
