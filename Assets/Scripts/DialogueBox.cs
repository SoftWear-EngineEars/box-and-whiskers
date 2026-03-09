using System;
using System.Collections.Generic;
using AI_Integration;
using UnityEngine;

public class DialogueBox : MonoBehaviour, IDependency<IAIManager>, ISubscriber<KeyCollectEvent>
{
    [SerializeField] private UndertaleText undertaleText;
    [SerializeField] private GameObject[] children;

    private IAIManager _aiManager;

    private Queue<string> _messageQueue = new();
    private bool isPlaying = false;

    public void SetDependency(IAIManager dependency)
    {
        _aiManager = dependency;
        SetChildrenActive(false);
        Play(DialogueType.StartGame);
    }

    public void SetDependency(INotifier<KeyCollectEvent> dependency)
    {
        dependency.RegisterSubscriber(this);
    }

    public async void Play(DialogueType type)
    {
        var aiText = await _aiManager.Request(type);
        aiText = aiText.Replace("\"", "").Replace("*", "");
        _messageQueue.Enqueue(aiText);
        if (!isPlaying)
            PlayNextItem();
    }

    private void PlayText(string text)
    {
        SetChildrenActive(true);
        isPlaying = true;
        StartCoroutine(undertaleText.TypeTextRoutine("*  " + text, () => { Hide(); PlayNextItem(); }));
    }

    private void PlayNextItem()
    {
        if (_messageQueue.Count == 0)
        {
            isPlaying = false;
            return;
        }

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