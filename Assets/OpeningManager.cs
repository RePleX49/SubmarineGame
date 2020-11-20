using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpeningManager : MonoBehaviour
{
    //[SerializeField] private string[] openingText = {"A Major Studio Game", "This game has autosaving"};
    //[SerializeField] private TMP_Text openingTextMesh;
    //[SerializeField] private float fadeTime = 1f;
    //[SerializeField] private float stayTime = 3f;
    //[SerializeField] private Color textColor;

    public TMP_Text gameText;
    public TMP_Text arjunName;
    public TMP_Text jordanName;
    public TMP_Text lukeName;
    public TMP_Text warningText1;
    public TMP_Text warningText2;
    public TMP_Text warningText3;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Systems.UI.TextCutscene(openingTextMesh, openingText, fadeTime, stayTime, textColor));
        StartCoroutine(TextCutscene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator TextCutscene()
    {
        StartCoroutine(TextFade(gameText, 1, 5f));
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade(arjunName, 1, 4f));
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade(jordanName, 1, 3f));
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade(lukeName, 1, 2f));
        yield return new WaitForSeconds(5f);
        StartCoroutine(TextFade(warningText1, 1, 15));
        yield return new WaitForSeconds(3f);
        StartCoroutine(TextFade(warningText2, 1, 12));
        yield return new WaitForSeconds(3f);
        StartCoroutine(TextFade(warningText3, 1, 9));
    }

    public IEnumerator TextFade(TMP_Text text, float fadeTime, float stayTime)
    {
        Color zeroAlpha = new Color(text.color.r, text.color.g, text.color.b, 0f);
        Color fullAlpha = new Color(text.color.r, text.color.g, text.color.b, 1f);
        for (float timer = 0; timer < fadeTime; timer += Time.deltaTime)
        {
            text.color = Color.Lerp(zeroAlpha, fullAlpha, timer / fadeTime);
            yield return null;
        }

        text.color = fullAlpha;

        yield return new WaitForSeconds(stayTime);

        for (float timer = 0; timer < fadeTime; timer += Time.deltaTime)
        {
            text.color = Color.Lerp(fullAlpha, zeroAlpha, timer / fadeTime);
            yield return null;
        }

        text.color = zeroAlpha;
    }

    //public IEnumerator TextCutscene(TMP_Text textMesh, string[] textArr, float fadeTime, float stayTime, Color textColor)
    //{
    //    foreach (string text in textArr)
    //    {
    //        textMesh.text = text;
    //        Color zeroAlpha = new Color(textColor.r, textColor.g, textColor.b, 0);
    //        textMesh.color = zeroAlpha;
    //        for (float timer = 0; timer < fadeTime; timer += Time.deltaTime)
    //        {
    //            textMesh.color = Color.Lerp(zeroAlpha, textColor, timer / fadeTime);
    //            yield return null;
    //        }

    //        textMesh.color = textColor;

    //        yield return new WaitForSeconds(stayTime);

    //        for (float timer = 0; timer < fadeTime; timer += Time.deltaTime)
    //        {
    //            textMesh.color = Color.Lerp(textColor, zeroAlpha, timer / fadeTime);
    //            yield return null;
    //        }

    //        textMesh.color = zeroAlpha;
    //    }
    //}
}
