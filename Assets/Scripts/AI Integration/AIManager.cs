using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace AI_Integration
{
    public class AIManager : Singleton<AIManager>, IAIManager
    {
        private readonly string BasePrompt =
            "Box and Whiskers is a two-player, cooperative platformer game where the two characters are 'Whiskers', a talking cat, and 'Box', a sentient cardboard box. The goal of the game is to beat the level, optionally as fast as possible. Below is a situation that has just happened in the game. Please write a short, slightly-funny one-liner that the cat will say in the given context. Only include the one-liner. Do not include a list. Do not include markdown or LaTeX formatting. Do not include quotation marks.\n \nThe context is: ";
        
        private readonly List<string> _prompts = new()
        {
            "The level has just started.",
            "The cat has just collected the key necessary to complete the level."
        };

        private IGroqAIController _controller = new GroqAIController();
        
        public void SetController(IGroqAIController controller) => _controller = controller;

        public Task<string> Request(DialogueType type)
        {
            return _controller.Ask(BasePrompt + _prompts[(int)type]);
        }
    }
}