using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI_Integration
{
    public class GroqAIManager : MonoBehaviour
    {
        public enum DialogueType { StartGame = 0, BoxEnter = 1, BoxLeave = 2, CollectKey = 3 }

        [SerializeField] private List<string> dialogueOptions = new List<string>
        {
            "Cat in a game saying something at the beginning of a multiplayer platformer level kinda funny short one-liner. The second player is a box. Don't give me a list or anything, just the one line---say nothing more than the one line.",
            "Cat in a game saying something after entering a box in a multiplayer platformer level kinda funny short one-liner. Don't give me a list or anything, just the one line---say nothing more than the one line.",
            "Cat in a game saying something after leaving a box in a multiplayer platformer level kinda funny short one-liner. Don't give me a list or anything, just the one line---say nothing more than the one line.",
            "Cat in a game saying something in a multiplayer platformer game after collecting the key required to open the door to leave the level --- kinda funny short one-liner. The second player is a box. Don't give me a list or anything, just the one line---say nothing more than the one line."
        };

        public static GroqAIManager Instance { get; private set; }

        public event Action<string> OnResponse;

        [SerializeField] private GroqAIController controller;

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
        }

        private void Start()
        {
            if (controller == null) controller = FindFirstObjectByType<GroqAIController>();
            if (controller != null) controller.OnResponse += HandleControllerResponse;
        }

        private void OnDestroy()
        {
            if (controller != null) controller.OnResponse -= HandleControllerResponse;
        }

        private void HandleControllerResponse(string msg) => OnResponse?.Invoke(msg);

        public void Request(DialogueType type)
        {
            int prompt = (int)type;
            if (prompt < 0 || prompt >= dialogueOptions.Count) return;
            if (controller == null) controller = FindFirstObjectByType<GroqAIController>();
            if (controller == null) return;
            controller.AskGroq(dialogueOptions[prompt]);
        }

    }
}