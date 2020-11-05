using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpeningManager : MonoBehaviour
{
    [SerializeField] private string[] openingText = {"A Major Studio Game", "This game has autosaving"};
    [SerializeField] private TMP_Text openingTextMesh;
    [SerializeField] private float fadeTime = 1f;
    [SerializeField] private float stayTime = 3f;
    [SerializeField] private Color textColor;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Systems.UI.TextCutscene(openingTextMesh, openingText, fadeTime, stayTime, textColor));
    }

    // Update is called once per frame
    void Update()
    {
        
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
