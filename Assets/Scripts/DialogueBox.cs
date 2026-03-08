using AI_Integration;
using UnityEngine;

public class DialogueBox : MonoBehaviour, IDependency<IAIManager>
{
    [SerializeField] private UndertaleText undertaleText;
    [SerializeField] private GameObject[] children;

    private IAIManager _aiManager;

    public void SetDependency(IAIManager dependency)
    {
        _aiManager = dependency;
        Play(DialogueType.StartGame);
    }

    private void Start()
    {
        SetChildrenActive(false);
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
        SetChildrenActive(false);
    }

    private void SetChildrenActive(bool active)
    {
        foreach (var child in children)
            child.SetActive(active);
    }
}