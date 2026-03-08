using AI_Integration;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] private UndertaleText undertaleText;
    [SerializeField] private GameObject[] children;

    private void Start()
    {
        SetChildrenActive(false);
    }

    public async void Play(AIManager.DialogueType type)
    {
        SetChildrenActive(true);
        var aiText = await AIManager.Instance.Request(type);
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