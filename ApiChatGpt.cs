using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApiChatGpt
{

    public class ApiChatGpt
    {
        static string? openAiKey = System.Environment.GetEnvironmentVariable("apikey"); 
        const string chatComplectionUri = "https://api.pawan.krd/v1/chat/completions";
        ChatGptRequest chatGptRequest = new ChatGptRequest() { ModelId = "gpt-3.5-turbo" };

        public string Error = string.Empty;
        private HttpClient httpClient = new HttpClient()
        {
            DefaultRequestHeaders =
        {
            Authorization=AuthenticationHeaderValue.Parse($"Bearer { openAiKey}")
        }
        };

        async public Task<string> GetResult(string chatContent)
        {   
            chatGptRequest.Messages.Clear();
            chatGptRequest.Messages.Add(new Message() { Role = "user", Content = chatContent });
            Error= string.Empty;
            try
            {
                if (string.IsNullOrEmpty(openAiKey)) { throw new Exception("Не задана переменная среды apikey"); }
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(chatComplectionUri, chatGptRequest);
                ChatGptResponse chatGptResponse = await response.Content.ReadFromJsonAsync<ChatGptResponse>();
                if (response.StatusCode==System.Net.HttpStatusCode.BadRequest) 
                {
                    throw new Exception("Ошибка в запросе");
                }
                return chatGptResponse.Choices[0].Message.Content.ToString();
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return string.Empty;
            }
            
        }
    }
}