## Introduction
This demo utilizes Azure OpenAI and Azure AI Foundry Text-To-Speech (TTS) service. It acts as a language tutor that assists users to study new languages.

### Workflow
0. User needs to configure the Azure OpenAI (OpenAI Key, deployment, OpenAI Account) and Speech Service (SpeechKey, region)
1. The program takes input from the user.
2. The input is sent to the Azure OpenAI model.
3. The response is then returned as text.
4. The text is sent to TTS Service and a voice is being played.

## Prerequisites
* A deployment and endpoint on Azure OpenAI. See [Azure OpenAI in Foundry Models](https://azure.microsoft.com/en-us/products/ai-services/openai-service).
* A subscription key for the Speech service. See [Try the speech service for free](https://docs.microsoft.com/azure/cognitive-services/speech-service/get-started).
* A PC with a working speaker or headset. See the [Speech SDK installation quickstart](https://learn.microsoft.com/azure/ai-services/speech-service/quickstarts/setup-platform?pivots=programming-language-csharp) for details on system requirements and setup.

## Build the sample
* Edit the `Program.cs` source:
  * Replace the string `OpenAIKey` and `OpenAIAccount` with your own AzureOpenAI key and account.
  * Replace the string `DeploymentName`. You can find this deployment name in Playgrounds - Chat - Deployment.
  * Replace the string `SpeechKey` and `Region`. You can find them on Azure AI Foundry - Overview. `SpeechKey` is API Key.


## Run the sample

### Using Visual Studio

To debug the app and then run it, press F5 or use **Debug** \> **Start Debugging**. To run the app without debugging, press Ctrl+F5 or use **Debug** \> **Start Without Debugging**.

### Using the .NET Core CLI

Run the following command from the directory that contains this sample:

```bash
dotnet LanguageTutor/bin/Debug/net8.0/LanguageTutor.dll
```

## References

* [Azure OpenAI client library for .NET - version 2.1.0](https://learn.microsoft.com/en-us/dotnet/api/overview/azure/ai.openai-readme?view=azure-dotnet)
* [Quickstart article on the SDK documentation site (Windows)](https://docs.microsoft.com/azure/cognitive-services/speech-service/quickstart-text-to-speech-dotnetcore)
* [Speech SDK API reference for C#](https://aka.ms/csspeech/csharpref)