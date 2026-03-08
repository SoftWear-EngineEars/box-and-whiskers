using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AI_Integration
{
    public class GroqAIController
    {
        private readonly string _url = "https://api.groq.com/openai/v1/chat/completions";
        private readonly string _apiKey;

        private static readonly HttpClient Client = new();

        public GroqAIController()
        {
            _apiKey = EnvLoader.GetApiKey();
        }

        public async Task<string> Ask(string prompt)
        {
            var requestData = new GroqRequest
            {
                model = "openai/gpt-oss-120b",
                messages = new List<Message>
                {
                    new() { role = "user", content = prompt }
                },
                temperature = 1f,
                max_completion_tokens = 1024,
                stream = false
            };

            var jsonPayload = JsonUtility.ToJson(requestData);

            using var request = new HttpRequestMessage(HttpMethod.Post, _url);
            request.Headers.Add("Authorization", $"Bearer {_apiKey}");
            request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            HttpResponseMessage response;
            try
            {
                response = await Client.SendAsync(request);
            }
            catch (Exception ex)
            {
                Debug.LogError($"GroqAIController: request failed - {ex.Message}");
                return null;
            }

            var responseText = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                Debug.LogError($"GroqAIController: HTTP {response.StatusCode} - {responseText}");
                return null;
            }

            GroqResponse groqResponse = null;
            try
            {
                groqResponse = JsonUtility.FromJson<GroqResponse>(responseText);
            }
            catch (Exception ex)
            {
                Debug.LogError($"GroqAIController: failed to parse response - {ex.Message}");
                return null;
            }

            return groqResponse?.choices?[0]?.message?.content;
        }
    }
}