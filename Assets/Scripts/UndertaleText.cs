using UnityEngine;
using TMPro;
using System.Collections;

public class UndertaleText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    public float letterDelay = 0.04f;

    public void TypeText(string text)
    {
        StartCoroutine(TypeTextRoutine(text));
    }

    public IEnumerator TypeTextRoutine(string text, System.Action onComplete = null)
    {
        textComponent.text = "";
        foreach (char c in text)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(letterDelay);
        }

        yield return new WaitForSeconds(2f);
        onComplete?.Invoke();
    }
}