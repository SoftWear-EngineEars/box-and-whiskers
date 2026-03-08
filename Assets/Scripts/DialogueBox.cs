using System;
using System.Collections.Generic;
using AI_Integration;
using UnityEngine;

public class DialogueBox : MonoBehaviour, IDependency<IAIManager>, ISubscriber<KeyCollectEvent>
{
    [SerializeField] private UndertaleText undertaleText;
    [SerializeField] private GameObject[] children;

    public INotifier<KeyCollectEvent> Notifier { get; set; }
    private IAIManager _aiManager;

    private Queue<string> _messageQueue = new();

    private void Start()
    {
        Notifier.RegisterSubscriber(this);
    }

    public void SetDependency(IAIManager dependency)
    {
        _aiManager = dependency;
        SetChildrenActive(false);
        Play(DialogueType.StartGame);
    }

    public async void Play(DialogueType type)
    {
        var aiText = await _aiManager.Request(type);
        aiText = aiText.Replace("\"", "").Replace("*", "");
        if (_messageQueue.Count == 0)
            PlayText(aiText);
        _messageQueue.Enqueue(aiText);
    }

    private void PlayText(string text)
    {
        SetChildrenActive(true);
        StartCoroutine(undertaleText.TypeTextRoutine("*  " + text, () => { Hide(); PlayNextItem(); }));
    }

    private void PlayNextItem()
    {
        if (_messageQueue.Count == 0)
            return;
        
        PlayText(_messageQueue.Dequeue());
    }

    private void Hide()
    {
        undertaleText.Clear();
        SetChildrenActive(false);
    }

    private void SetChildrenActive(bool active)
    {
        foreach (var child in children)
            child.SetActive(active);
    }

    public void ReceiveEvent(KeyCollectEvent message)
    {
        Play(DialogueType.CollectKey);
    }
}