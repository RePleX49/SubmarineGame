using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOpening : MonoBehaviour
{
    public Image background;
    public TMP_Text seedText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeIn()
    {
        yield return null;
        seedText.text = "Seed: " + Systems.randomSeeding.seed;
        Systems.player.canMove = false;
        Color zeroAlpha = new Color(background.color.r, background.color.g, background.color.b, 0);
        Color fullAlpha = new Color(background.color.r, background.color.g, background.color.b, 1);
        background.color = fullAlpha;
        for (float timer = 0; timer < 5f; timer += Time.deltaTime)
        {
            background.color = Color.Lerp(fullAlpha, zeroAlpha, timer / 5f);
            yield return null;
        }
        background.color = zeroAlpha;
        Systems.player.canMove = true;
    }
}
