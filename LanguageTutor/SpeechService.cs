using Microsoft.CognitiveServices.Speech;
using System;
using System.Threading.Tasks;

namespace LanguageTutor
{
    public class SpeechService
    {
        public string SpeechKey { get; set; }

        public string Region { get; set; }

        public string EndpointUri
        {
            get
            {
                return $"https://{Region}.api.cognitive.microsoft.com";
            }
        }

        public string VoiceName { get; set; }

        public SpeechConfig SpeechConfig { get; set; }

        public SpeechService()
        {
            
        }

        public SpeechService(string speechKey, string region, string voiceName)
        {
            SpeechKey = speechKey;
            Region = region;
            VoiceName = voiceName;
        }

        public void Connect()
        {
            Console.WriteLine("Connecting to Azure CognitiveServices ...");
            try
            {
                this.SpeechConfig = SpeechConfig.FromEndpoint(new Uri(this.EndpointUri), this.SpeechKey);
            }
            catch (UriFormatException ex)
            {
                Console.WriteLine($"Uri format is wrong: {ex.Message}");
            }

            this.SpeechConfig.SpeechSynthesisVoiceName = this.VoiceName;
            Console.WriteLine("OK!");
        }

        public async Task SpeakWithTTS(string text)
        {
            // Creates a speech synthesizer using the default speaker as audio output.
            using (var synthesizer = new SpeechSynthesizer(this.SpeechConfig))
            {
                using (var result = await synthesizer.SpeakTextAsync(text))
                {
                    if (result.Reason == ResultReason.SynthesizingAudioCompleted)
                    {
                        // The audio is completed successfully
                        Console.WriteLine($"------");
                    }
                    else if (result.Reason == ResultReason.Canceled)
                    {
                        // The audio cannot be synthesized
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
