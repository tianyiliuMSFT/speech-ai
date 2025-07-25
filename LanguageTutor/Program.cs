//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

// <code>
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageTutor
{
    class Program
    {
        // If input equals one of these words, the program will exit (case-insensitive)
        public static string[] ExitPrompt = ["q", "quit", "exit"];
       
        static async Task Main()
        {
            // ChatBot utilizes AzureOpenAI API to talk with models
            ChatBot bot = new ChatBot()
            {
                // Deploy an Azure Open AI model, and fill in the parameters here
                OpenAIKey = "",
                DeploymentName = "",
                OpenAIAccount = ""
            };

            // SpeechService converts text to speech (tts)
            SpeechService speechService = new SpeechService()
            {
                // Deploy an Azure AI Foundry, and fill in the parameters here
                SpeechKey = "",
                Region = "",
                VoiceName = "en-US-AvaMultilingualNeural"
            };

            bot.Connect();
            speechService.Connect();
            Console.WriteLine("------");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("This is a helpful assistant that helps users learn new languages.");
            Console.WriteLine("Type in 'q' or 'quit' or 'exit' to quit.");
            Console.ResetColor();
            Console.WriteLine("------");

            while (true)
            {
                Console.Write("User: ");
                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) 
                {
                    Console.WriteLine("Input nothing");
                }

                if (ExitPrompt.Contains(input.ToLower())) 
                {
                    Console.WriteLine("See you next time.");
                    break;
                }

                // Response from AzureOpenAI model
                var response = await bot.GetChatResponse(input);

                // Convert text to voice
                await speechService.SpeakWithTTS(response);
            }
        }
    }
}
// </code>
