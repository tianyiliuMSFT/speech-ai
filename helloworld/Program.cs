//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

// <code>
using Azure;
using Azure.AI.OpenAI;
using Microsoft.CognitiveServices.Speech;
using OpenAI.Chat;
using System;
using System.ClientModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace helloworld
{
    class Program
    {
        public static string openAIKey = "";
        public static string speechKey = "";

        static async Task Main()
        {

            var response = await GetChatResponse("hello, I want someone to talk");
            Console.WriteLine($"AI: {response}");

            await SpeakWithTTS(response);
        }

        public static async Task<string> GetChatResponse(string prompt)
        {
            var deploymentName = "gpt-4o";
            var endpointUrl = "https://tianyiaincus.openai.azure.com/";

            Console.WriteLine("connecting to azure open ai");
            var client = new AzureOpenAIClient(new Uri(endpointUrl), new ApiKeyCredential(openAIKey));

            var chatClient = client.GetChatClient(deploymentName);

            // AzureOpenAIClient openAIClient = new AzureOpenAIClient(new Uri(endpointUrl), new AzureKeyCredential(openAIKey));

            var messages = new List<ChatMessage>();
            messages.Add(new UserChatMessage(prompt));
            var response = await chatClient.CompleteChatAsync(messages);
            var chatResponse = response.Value.Content.Last().Text;

            messages.Add(new AssistantChatMessage(chatResponse));
            return chatResponse;
        }

        public static async Task SpeakWithTTS(string text)
        {
            // Creates an instance of a speech config with specified endpoint and subscription key.
            // Replace with your own endpoint and subscription key.
            var speechConfig = SpeechConfig.FromEndpoint(new Uri("https://eastus2.api.cognitive.microsoft.com"),
                speechKey);

            // Set the voice name, refer to https://aka.ms/speech/voices/neural for full list.
            speechConfig.SpeechSynthesisVoiceName = "en-US-AriaNeural";

            // Creates a speech synthesizer using the default speaker as audio output.
            using (var synthesizer = new SpeechSynthesizer(speechConfig))
            {
                using (var result = await synthesizer.SpeakTextAsync(text))
                {
                    if (result.Reason == ResultReason.SynthesizingAudioCompleted)
                    {
                        Console.WriteLine($"After speaking ...");
                    }
                    else if (result.Reason == ResultReason.Canceled)
                    {
                        var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                        Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                        if (cancellation.Reason == CancellationReason.Error)
                        {
                            Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                            Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                            Console.WriteLine($"CANCELED: Did you update the subscription info?");
                        }
                    }
                }
            }
        }
    }
}
// </code>
