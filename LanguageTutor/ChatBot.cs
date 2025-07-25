using Azure.AI.OpenAI;
using OpenAI.Chat;
using System;
using System.ClientModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageTutor
{
    class ChatBot
    {
        #region OpenAI properties
        public string OpenAIKey { get; set; }

        public string DeploymentName { get; set; }

        public string OpenAIAccount { get; set; }

        public string EndpointUrl
        {
            get
            {
                return $"https://{this.OpenAIAccount}.openai.azure.com/";
            }
        }

        public AzureOpenAIClient OpenAIClient { get; set; }

        public ChatClient ChatClient { get; set; }

        public List<ChatMessage> ChatHistory { get; set; }
        #endregion


        public ChatBot()
        {
            this.ChatHistory = new List<ChatMessage>();

            // ChatHistory enables the model to act as a language tutor
            ChatHistory.Add(new SystemChatMessage(
                "You are a helpful assistant that helps users learn new languages." +
                "You are not allowed to include emoji in your response. " +
                "Keep your response as short as possible."
                ));
        }

        public ChatBot(string openAIKey, string DeploymentName, string OpenAIAccount) : this()
        {
            this.OpenAIKey = openAIKey;
            this.DeploymentName = DeploymentName;
            this.OpenAIAccount = OpenAIAccount;
        }

        public void Connect()
        {
            Console.WriteLine("Connecting to Azure OpenAI client ...");
            OpenAIClient = new AzureOpenAIClient(new Uri(this.EndpointUrl), new ApiKeyCredential(this.OpenAIKey));
            Console.WriteLine("OK!");

            Console.WriteLine($"Trying to get deployment: {this.DeploymentName} ...");
            ChatClient = OpenAIClient.GetChatClient(this.DeploymentName);
            Console.WriteLine("OK!");
        }

        public void ClearHistory()
        {
            this.ChatHistory.Clear();
        }


        public async Task<string> GetChatResponse(string prompt)
        {
            this.ChatHistory.Add(new UserChatMessage(prompt));

            var response = await ChatClient.CompleteChatAsync(this.ChatHistory);
            var chatResponse = response.Value.Content.Last().Text;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"AI: {chatResponse}");
            // Save the chat history so that the model can remember the context
            this.ChatHistory.Add(new AssistantChatMessage(chatResponse));

            Console.ResetColor();
            return chatResponse;
        }
    }
}
