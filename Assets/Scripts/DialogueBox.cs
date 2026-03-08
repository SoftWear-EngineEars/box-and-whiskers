using System;
using AI_Integration;
using UnityEngine;

public class DialogueBox : MonoBehaviour, IDependency<IAIManager>, ISubscriber<KeyCollectEvent>
{
    [SerializeField] private UndertaleText undertaleText;
    [SerializeField] private GameObject[] children;

    public INotifier<KeyCollectEvent> Notifier { get; set; }
    private IAIManager _aiManager;

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
        SetChildrenActive(true);
        var aiText = await _aiManager.Request(type);
        aiText = aiText.Replace("\"", "").Replace("*", "");
        StartCoroutine(undertaleText.TypeTextRoutine("*  " + aiText, Hide));
    }

    public void Hide()
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