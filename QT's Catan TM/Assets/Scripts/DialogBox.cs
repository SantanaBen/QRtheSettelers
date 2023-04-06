using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogBox : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public float fadeTime = 1f;

    private Coroutine currentFade;

    private void Start()
    {
        currentFade = StartCoroutine(FadeOut());
    }

    public void UpdateText(string newText)
    {
        textBox.color = Color.black;
        textBox.text = newText;
        if (currentFade != null)
        {
            StopCoroutine(currentFade);
        }
        currentFade = StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(fadeTime);
        Color color = textBox.color;
        while (color.a > 0)
        {
            color.a -= Time.deltaTime / fadeTime;
            textBox.color = color;
            yield return null;
        }
    }
}