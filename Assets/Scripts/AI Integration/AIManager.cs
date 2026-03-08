using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace AI_Integration
{
    public class AIManager : Singleton<AIManager>
    {
        public enum DialogueType { StartGame = 0, CollectKey = 1 }

        private readonly List<string> _prompts = new()
        {
            "Cat in a game saying something at the beginning of a multiplayer platformer level kinda funny short one-liner. The second player is a cardboard box. Don't give me a list or anything, just the one line---say nothing more than the one line.",
            "A cat saying a silly one-liner after it collected a key required to open the exit door. Don't give me a list or anything, just the one line---say nothing more than the one line."
        };

        private readonly GroqAIController _controller = new();

        public Task<string> Request(DialogueType type)
        {
            return _controller.Ask(_prompts[(int)type]);
        }
    }
}