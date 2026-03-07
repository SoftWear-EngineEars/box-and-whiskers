using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace AI_Integration
{
    public class GroqAIController : MonoBehaviour
    {
        private string _apiKey;
        private readonly string _url = "https://api.groq.com/openai/v1/chat/completions";

        // Event that other systems can subscribe to for AI responses
        public event Action<string> OnResponse;

        private void Start()
        {
            _apiKey = EnvLoader.GetApiKey();
        }

        public void AskGroq(string prompt)
        {
            if (string.IsNullOrEmpty(_apiKey)) _apiKey = EnvLoader.GetApiKey();

            StartCoroutine(PostRequest(prompt));
        }

        private IEnumerator PostRequest(string prompt)
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

            if (string.IsNullOrEmpty(_apiKey))
                Debug.LogWarning("Groq API key is missing — ensure .env contains GROQ_API_KEY");

            Debug.Log($"Sending POST {_url} with payload: {jsonPayload}");

            using (var request = new UnityWebRequest(_url, "POST"))
            {
                var bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();

                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Authorization", $"Bearer {_apiKey}");

                yield return request.SendWebRequest();

                var responseText = request.downloadHandler?.text;
                var status = request.responseCode;
                Debug.Log($"Groq response (HTTP {status}): {responseText}");

                if (request.result == UnityWebRequest.Result.Success)
                {
                    if (string.IsNullOrEmpty(responseText))
                    {
                        Debug.LogWarning("Groq returned an empty response body.");
                    }
                    else
                    {
                        GroqResponse response = null;
                        try
                        {
                            response = JsonUtility.FromJson<GroqResponse>(responseText);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError($"Failed to parse Groq response JSON: {ex.Message}");
                        }

                        if (response != null && response.choices != null && response.choices.Count > 0 &&
                            response.choices[0].message != null)
                        {
                            var aiText = response.choices[0].message.content;
                            Debug.Log($"Groq says: {aiText}");
                            
                            OnResponse?.Invoke(aiText);
                        }
                        else
                        {
                            Debug.LogWarning(
                                $"Groq response did not contain choices/message fields. Raw: {responseText}");
                        }
                    }
                }
                else
                {
                    Debug.LogError($"Request failed (HTTP {status}): {request.error} | {responseText}");
                }
            }
        }
    }
}