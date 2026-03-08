using System;
using System.Collections.Generic;

namespace AI_Integration
{
    [Serializable]
    public class GroqRequest
    {
        public string model;
        public List<Message> messages;
        public float temperature;
        public int max_completion_tokens;
        public bool stream;
    }

    [Serializable]
    public class Message
    {
        public string role;
        public string content;
    }

    // Parsing the response
    [Serializable]
    public class GroqResponse
    {
        public List<Choice> choices;
    }

    [Serializable]
    public class Choice
    {
        public Message message;
    }
}