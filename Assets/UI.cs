using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public TMP_Text pillarText; 

    // Start is called before the first frame update
    void Start()
    {
        Systems.UI = this; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator FadeInText(TMP_Text textMesh, string text, float fadeTime, Color textColor)
    {
        textMesh.text = text;
        Color zeroAlpha = new Color(textColor.r, textColor.g, textColor.b, 0);
        textMesh.color = zeroAlpha;
        for (float timer = 0; timer < fadeTime; timer += Time.deltaTime)
        {
            textMesh.color = Color.Lerp(zeroAlpha, textColor, timer / fadeTime);
            yield return null;
        }

        textMesh.color = textColor;
    }

    public IEnumerator FadeOutText(TMP_Text textMesh, string text, float fadeTime, Color textColor)
    {
        textMesh.text = text;
        Color zeroAlpha = new Color(textColor.r, textColor.g, textColor.b, 0);
        textMesh.color = zeroAlpha;
        for (float timer = 0; timer < fadeTime; timer += Time.deltaTime)
        {
            textMesh.color = Color.Lerp(textColor, zeroAlpha, timer / fadeTime);
            yield return null;
        }

        textMesh.color = zeroAlpha;
    }

    public IEnumerator TextCutscene(TMP_Text textMesh, string[] textArr, float fadeTime, float stayTime, Color textColor)
    {
        foreach (string text in textArr)
        {
            textMesh.text = text;
            Color zeroAlpha = new Color(textColor.r, textColor.g, textColor.b, 0);
            textMesh.color = zeroAlpha;
            for (float timer = 0; timer < fadeTime; timer += Time.deltaTime)
            {
                textMesh.color = Color.Lerp(zeroAlpha, textColor, timer / fadeTime);
                yield return null;
            }

            textMesh.color = textColor;

            yield return new WaitForSeconds(stayTime);

            for (float timer = 0; timer < fadeTime; timer += Time.deltaTime)
            {
                textMesh.color = Color.Lerp(textColor, zeroAlpha, timer / fadeTime);
                yield return null;
            }

            textMesh.color = zeroAlpha;
        }
    }
}
