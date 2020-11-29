using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningManager : MonoBehaviour
{
    public TMP_Text gameText;
    public TMP_Text arjunName;
    public TMP_Text jordanName;
    public TMP_Text lukeName;
    public TMP_Text warningText1;
    public TMP_Text warningText2;
    public TMP_Text warningText3;
    public TMP_Text gameTitle;
    public Button playButton;
    public Button creditsButton;
    public Button quitButton;
    public Image borderImage;
    public RawImage rendTexture;
    public TMP_Text controlTitle;
    public Image controlImage;
    public TMP_Text[] controlText;
    public TMP_Text[] controlInsturctions;
    public Button player1Button;
    public Button player2Button;
    public TMP_Text player1Title;
    public TMP_Text seedGen;
    public TMP_Text player1Insturctions;
    public Button player1Start;
    public TMP_Text player2Title;
    public TMP_InputField seedInput;
    public TMP_Text player2Insturctions;
    public Button player2Start;
    public TMP_Text creditTitle;
    public TMP_Text[] credits;
    public TMP_Text invalidText;
    bool invalidOn = false;
    public string cutscene1Scene;
    public string cutscene2Scene;
    public TMP_Text optionsTitle;
    public TMP_Text musicTitle;
    public TMP_Text sfxTitle;
    public Button returnButton;
    public Button optionsButton;
    public GameObject musicSlider;
    public GameObject sfxSlider;
    public TMP_Text[] assistText;
    public Image[] imgArr1;
    public Image[] imgArr2;
    public TMP_InputField player1SeedInput;
    public Button player1SeedButton;
    public TMP_Text player1SeedInputInsturctions;
    private bool customSeed = false;
    public Button goBackToSeed;
    public Button back1;
    public Button back2;

    public bool longMenu;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        //StartCoroutine(Systems.UI.TextCutscene(openingTextMesh, openingText, fadeTime, stayTime, textColor));
        if (longMenu) { StartCoroutine(TextCutscene()); } else { StartCoroutine(ShorterTextCutscene()); }
        playButton.gameObject.SetActive(false);
        creditsButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        player1Button.gameObject.SetActive(false);
        player2Button.gameObject.SetActive(false);
        player1Start.gameObject.SetActive(false);
        player2Start.gameObject.SetActive(false);
        seedInput.gameObject.SetActive(false);
        returnButton.gameObject.SetActive(false);
        optionsButton.gameObject.SetActive(false);
        musicSlider.SetActive(false);
        sfxSlider.SetActive(false);
        player1SeedInput.gameObject.SetActive(false);
        player1SeedButton.gameObject.SetActive(false);
        goBackToSeed.gameObject.SetActive(false);
        back1.gameObject.SetActive(false);
        back2.gameObject.SetActive(false);

    }

    public IEnumerator TextCutscene()
    {
        StartCoroutine(TextFade(gameText, 1, 5f, true));
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade(arjunName, 1, 4f, true));
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade(jordanName, 1, 3f, true));
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade(lukeName, 1, 2f, true));
        yield return new WaitForSeconds(5f);
        StartCoroutine(TextFade(warningText1, 1, 15, true));
        yield return new WaitForSeconds(3f);
        StartCoroutine(TextFade(warningText2, 1, 12, true));
        yield return new WaitForSeconds(3f);
        StartCoroutine(TextFade(warningText3, 1, 9, true));
        yield return new WaitForSeconds(12f);
        StartCoroutine(TextFade(borderImage, 1, 0, false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade(rendTexture, 1, 0, false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade(gameTitle, 1, 0, false));
        yield return new WaitForSeconds(1f);
        playButton.gameObject.SetActive(true);
        creditsButton.gameObject.SetActive(true);
        optionsButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        StartCoroutine(TextFade(playButton.GetComponentInChildren<TMP_Text>(), 1, 0, false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade(optionsButton.GetComponentInChildren<TMP_Text>(), 1, 0, false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade(creditsButton.GetComponentInChildren<TMP_Text>(), 1, 0, false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade(quitButton.GetComponentInChildren<TMP_Text>(), 1, 0, false));
    }

    public IEnumerator ShorterTextCutscene()
    {
        StartCoroutine(TextFade(borderImage, 1, 0, false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade(rendTexture, 1, 0, false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade(gameTitle, 1, 0, false));
        yield return new WaitForSeconds(1f);
        playButton.gameObject.SetActive(true);
        creditsButton.gameObject.SetActive(true);
        optionsButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        StartCoroutine(TextFade(playButton.GetComponentInChildren<TMP_Text>(), 1, 0, false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade(optionsButton.GetComponentInChildren<TMP_Text>(), 1, 0, false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade(creditsButton.GetComponentInChildren<TMP_Text>(), 1, 0, false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade(quitButton.GetComponentInChildren<TMP_Text>(), 1, 0, false));
    }

    public IEnumerator Play()
    {
        StartCoroutine(Fade(borderImage, 1));
        StartCoroutine(Fade(rendTexture, 1));
        StartCoroutine(Fade(gameTitle, 1));
        StartCoroutine(Fade(playButton.GetComponentInChildren<TMP_Text>(), 1));
        StartCoroutine(Fade(optionsButton.GetComponentInChildren<TMP_Text>(), 1));
        StartCoroutine(Fade(creditsButton.GetComponentInChildren<TMP_Text>(), 1));
        StartCoroutine(Fade(quitButton.GetComponentInChildren<TMP_Text>(), 1));
        yield return new WaitForSeconds(1f);
        playButton.gameObject.SetActive(false);
        optionsButton.gameObject.SetActive(false);
        creditsButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        StartCoroutine(TextFade(controlTitle, 1, 20, true));
        yield return new WaitForSeconds(2f);
        StartCoroutine(TextFade(controlImage, 1, 18, true));
        yield return new WaitForSeconds(1f);
        foreach (TMP_Text text in controlText) { StartCoroutine(TextFade(text, 1, 7, true)); }
        yield return new WaitForSeconds(9f);
        foreach (TMP_Text text in assistText) { StartCoroutine(TextFade(text, 1, 8, true)); }
        foreach (TMP_Text text in controlInsturctions) { StartCoroutine(TextFade(text, 1, 8, true)); }
        yield return new WaitForSeconds(12f);
        player1Button.gameObject.SetActive(true);
        player2Button.gameObject.SetActive(true);
        StartCoroutine(TextFade(player1Button.GetComponentInChildren<TMP_Text>(), 1, 0, false));
        StartCoroutine(TextFade(player2Button.GetComponentInChildren<TMP_Text>(), 1, 0, false));
    }

    public IEnumerator Player1Start()
    {
        Systems.randomSeeding.GenerateNewSeed();
        seedGen.text = Systems.randomSeeding.seed;
        StartCoroutine(Fade(player1Button.GetComponentInChildren<TMP_Text>(), 1));
        StartCoroutine(Fade(player2Button.GetComponentInChildren<TMP_Text>(), 1));
        yield return new WaitForSeconds(1f);
        player1Button.gameObject.SetActive(false);
        player2Button.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade(player1Title, 1, 0, false));
        yield return new WaitForSeconds(2f);
        StartCoroutine(TextFade(seedGen, 1, 0, false));
        yield return new WaitForSeconds(4f);
        StartCoroutine(TextFade(player1Insturctions, 1, 0, false));
        yield return new WaitForSeconds(5f);

        player1Start.gameObject.SetActive(true);
        StartCoroutine(TextFade(player1Start.GetComponentInChildren<TMP_Text>(), 1, 0, false));
        yield return new WaitForSeconds(2f);
        player1SeedButton.gameObject.SetActive(true);
        StartCoroutine(TextFade(player1SeedButton.GetComponentInChildren<TMP_Text>(), 1, 0, false));
        yield return new WaitForSeconds(2f);
        back1.gameObject.SetActive(true);
        StartCoroutine(TextFade(back1.GetComponentInChildren<TMP_Text>(), 1, 0, false));
    }

    public IEnumerator Player2Start()
    {
        StartCoroutine(Fade(player1Button.GetComponentInChildren<TMP_Text>(), 1));
        StartCoroutine(Fade(player2Button.GetComponentInChildren<TMP_Text>(), 1));
        yield return new WaitForSeconds(1f);
        player1Button.gameObject.SetActive(false);
        player2Button.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade(player2Title, 1, 0, false));
        yield return new WaitForSeconds(2f);
        seedInput.gameObject.SetActive(true);
        StartCoroutine(TextFade(seedInput.GetComponentsInChildren<TMP_Text>()[0], 1, 0, false));
        StartCoroutine(TextFade(seedInput.GetComponentsInChildren<TMP_Text>()[1], 1, 0, false));

        yield return new WaitForSeconds(4f);
        StartCoroutine(TextFade(player2Insturctions, 1, 0, false));
        yield return new WaitForSeconds(5f);
        player2Start.gameObject.SetActive(true);
        StartCoroutine(TextFade(player2Start.GetComponentInChildren<TMP_Text>(), 1, 0, false));
        yield return new WaitForSeconds(2f);
        back2.gameObject.SetActive(true);
        StartCoroutine(TextFade(back2.GetComponentInChildren<TMP_Text>(), 1, 0, false));
    }

    public void PlayCoroutine()
    {
        Debug.Log("Play");
        StartCoroutine(Play());
    }

    public void Player1Setup()
    {
        Debug.Log("Play1");
        StartCoroutine(Player1Start());
    }

    public void Player2Setup()
    {
        Debug.Log("Play2");
        StartCoroutine(Player2Start());
    }



    public IEnumerator TextFade(TMP_Text text, float fadeTime, float stayTime, bool fadeOut)
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

        if (fadeOut) {
            for (float timer = 0; timer < fadeTime; timer += Time.deltaTime)
            {
                text.color = Color.Lerp(fullAlpha, zeroAlpha, timer / fadeTime);
                yield return null;
            }
    
            text.color = zeroAlpha;
        }
    }

    public IEnumerator TextFade(Image text, float fadeTime, float stayTime, bool fadeOut)
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

        if (fadeOut)
        {
            for (float timer = 0; timer < fadeTime; timer += Time.deltaTime)
            {
                text.color = Color.Lerp(fullAlpha, zeroAlpha, timer / fadeTime);
                yield return null;
            }

            text.color = zeroAlpha;
        }
    }

    public IEnumerator TextFade(RawImage text, float fadeTime, float stayTime, bool fadeOut)
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

        if (fadeOut)
        {
            for (float timer = 0; timer < fadeTime; timer += Time.deltaTime)
            {
                text.color = Color.Lerp(fullAlpha, zeroAlpha, timer / fadeTime);
                yield return null;
            }

            text.color = zeroAlpha;
        }
    }

    public IEnumerator Fade(TMP_Text text, float fadeTime)
    {
        Color zeroAlpha = new Color(text.color.r, text.color.g, text.color.b, 0f);
        Color fullAlpha = new Color(text.color.r, text.color.g, text.color.b, 1f);
        for (float timer = 0; timer < fadeTime; timer += Time.deltaTime)
        {
            text.color = Color.Lerp(fullAlpha, zeroAlpha, timer / fadeTime);
            yield return null;
        }
        text.color = zeroAlpha;
    }

    public IEnumerator Fade(Image text, float fadeTime)
    {
        Color zeroAlpha = new Color(text.color.r, text.color.g, text.color.b, 0f);
        Color fullAlpha = new Color(text.color.r, text.color.g, text.color.b, 1f);
        for (float timer = 0; timer < fadeTime; timer += Time.deltaTime)
        {
            text.color = Color.Lerp(fullAlpha, zeroAlpha, timer / fadeTime);
            yield return null;
        }
        text.color = zeroAlpha;
    }

    public IEnumerator Fade(RawImage text, float fadeTime)
    {
        Color zeroAlpha = new Color(text.color.r, text.color.g, text.color.b, 0f);
        Color fullAlpha = new Color(text.color.r, text.color.g, text.color.b, 1f);
        for (float timer = 0; timer < fadeTime; timer += Time.deltaTime)
        {
            text.color = Color.Lerp(fullAlpha, zeroAlpha, timer / fadeTime);
            yield return null;
        }
        text.color = zeroAlpha;
    }

    public IEnumerator Credits()
    {
        StartCoroutine(Fade(borderImage, 1));
        StartCoroutine(Fade(rendTexture, 1));
        StartCoroutine(Fade(gameTitle, 1));
        StartCoroutine(Fade(playButton.GetComponentInChildren<TMP_Text>(), 1));
        StartCoroutine(Fade(optionsButton.GetComponentInChildren<TMP_Text>(), 1));
        StartCoroutine(Fade(creditsButton.GetComponentInChildren<TMP_Text>(), 1));
        StartCoroutine(Fade(quitButton.GetComponentInChildren<TMP_Text>(), 1));
        yield return new WaitForSeconds(1f);
        playButton.gameObject.SetActive(false);
        optionsButton.gameObject.SetActive(false);
        creditsButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        StartCoroutine(TextFade(creditTitle, 1, 12, false));
        yield return new WaitForSeconds(1f);
        foreach (TMP_Text text in credits) { StartCoroutine(TextFade(text, 1f, 20, false)); yield return new WaitForSeconds(0.5f); }
        yield return new WaitForSeconds(5f);
        StartCoroutine(backToMain());

    }

    public IEnumerator backToMain()
    {
        foreach(TMP_Text text in credits) { StartCoroutine(Fade(text, 1f)); }
        StartCoroutine(Fade(creditTitle, 1f));
        yield return new WaitForSeconds(2f);
        StartCoroutine(TextFade(borderImage, 1, 0, false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade(rendTexture, 1, 0, false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade(gameTitle, 1, 0, false));
        yield return new WaitForSeconds(1f);
        playButton.gameObject.SetActive(true);
        optionsButton.gameObject.SetActive(true);
        creditsButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        StartCoroutine(TextFade(playButton.GetComponentInChildren<TMP_Text>(), 1, 0, false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade(optionsButton.GetComponentInChildren<TMP_Text>(), 1, 0, false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade(creditsButton.GetComponentInChildren<TMP_Text>(), 1, 0, false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade(quitButton.GetComponentInChildren<TMP_Text>(), 1, 0, false));
    }

    public void StartCredits()
    {
        StartCoroutine(Credits());
    }

    public IEnumerator StartGamePlayer1()
    {
        StartCoroutine(Fade(player1Title, 1));
        StartCoroutine(Fade(seedGen, 1));
        StartCoroutine(Fade(player1Insturctions, 1));
        StartCoroutine(Fade(player1Start.GetComponentInChildren<TMP_Text>(), 1));
        StartCoroutine(Fade(back1.GetComponentInChildren<TMP_Text>(), 1));

        if (customSeed) { Systems.randomSeeding.seed = player1SeedInput.text; }

        yield return new WaitForSeconds(1.2f);
        player1Start.gameObject.SetActive(false);

        SceneManager.LoadScene(cutscene1Scene);
    }

    public IEnumerator StartGamePlayer2()
    {
        StartCoroutine(Fade(player2Title, 1));
        StartCoroutine(Fade(seedInput.GetComponentsInChildren<TMP_Text>()[0], 1));
        StartCoroutine(Fade(seedInput.GetComponentsInChildren<TMP_Text>()[1], 1));
        StartCoroutine(Fade(player2Insturctions, 1));
        StartCoroutine(Fade(player2Start.GetComponentInChildren<TMP_Text>(), 1));
        StartCoroutine(Fade(back2.GetComponentInChildren<TMP_Text>(), 1));
        if (invalidOn) { StartCoroutine(Fade(invalidText, 1)); }

        Systems.randomSeeding.seed = seedInput.text;

        yield return new WaitForSeconds(1.2f);
        player2Start.gameObject.SetActive(false);

        SceneManager.LoadScene(cutscene2Scene);
    }

    public IEnumerator Player1InputSeed()
    {
        StartCoroutine(Fade(player1Title, 1));
        StartCoroutine(Fade(seedGen, 1));
        StartCoroutine(Fade(player1SeedButton.GetComponentInChildren<TMP_Text>(), 1));
        customSeed = true;

        yield return new WaitForSeconds(1.2f);
        player1SeedButton.gameObject.SetActive(false);
        

        StartCoroutine(TextFade(player1SeedInputInsturctions, 1, 0, false));
        yield return new WaitForSeconds(1f);
        player1SeedInput.gameObject.SetActive(true);
        StartCoroutine(TextFade(player1SeedInput.GetComponentsInChildren<TMP_Text>()[0], 1, 0, false));
        StartCoroutine(TextFade(player1SeedInput.GetComponentsInChildren<TMP_Text>()[1], 1, 0, false));
        yield return new WaitForSeconds(1f);
        goBackToSeed.gameObject.SetActive(true);
        StartCoroutine(TextFade(goBackToSeed.GetComponentInChildren<TMP_Text>(), 1, 0, false));
        back1.gameObject.SetActive(true);
        StartCoroutine(TextFade(back1.GetComponentInChildren<TMP_Text>(), 1, 0, false));

    }

    public void InputCustomSeed()
    {
        StartCoroutine(Player1InputSeed());
    }

    public IEnumerator Player1GoBackInputSeed()
    {
        StartCoroutine(Fade(player1SeedInputInsturctions, 1));
        StartCoroutine(Fade(player1SeedInput.GetComponentsInChildren<TMP_Text>()[0], 1));
        StartCoroutine(Fade(player1SeedInput.GetComponentsInChildren<TMP_Text>()[1], 1));
        StartCoroutine(Fade(goBackToSeed.GetComponentInChildren<TMP_Text>(), 1));
        yield return new WaitForSeconds(1.1f);
        player1SeedInput.gameObject.SetActive(false);
        goBackToSeed.gameObject.SetActive(false);
        customSeed = false;

        StartCoroutine(TextFade(player1Title, 1, 0, false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade(seedGen, 1, 0, false));
        yield return new WaitForSeconds(1f);
        player1SeedButton.gameObject.SetActive(true);
        StartCoroutine(TextFade(player1SeedButton.GetComponentInChildren<TMP_Text>(), 1, 0, false));
    }

    public void DontInputCustomSeed()
    {
        StartCoroutine(Player1GoBackInputSeed());
    }

    public IEnumerator InvalidSeed()
    {
        StartCoroutine(TextFade(invalidText, 1, 0, false));
        invalidOn = true;
        yield return null;
    }

    public void StartPlayer1()
    {
        StartCoroutine(StartGamePlayer1());
        Debug.Log("NewSceneA");
    }

    public void StartPlayer2()
    {
        if (seedInput.text.Length != 4) { StartCoroutine(InvalidSeed()); }
        else { StartCoroutine(StartGamePlayer2()); Debug.Log("NewSceneB"); }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public IEnumerator OptionsMenuIn()
    {
        StartCoroutine(Fade(borderImage, 1));
        StartCoroutine(Fade(rendTexture, 1));
        StartCoroutine(Fade(gameTitle, 1));
        StartCoroutine(Fade(playButton.GetComponentInChildren<TMP_Text>(), 1));
        StartCoroutine(Fade(optionsButton.GetComponentInChildren<TMP_Text>(), 1));
        StartCoroutine(Fade(creditsButton.GetComponentInChildren<TMP_Text>(), 1));
        StartCoroutine(Fade(quitButton.GetComponentInChildren<TMP_Text>(), 1));
        yield return new WaitForSeconds(1f);
        playButton.gameObject.SetActive(false);
        optionsButton.gameObject.SetActive(false);
        creditsButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        StartCoroutine(TextFade(optionsTitle, 1, 2, false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade(musicTitle, 1, 2, false));
        yield return new WaitForSeconds(1f);
        musicSlider.gameObject.SetActive(true);
        foreach (Image img in imgArr1) { StartCoroutine(TextFade(img, 1f, 20, false)); }
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade(sfxTitle, 1, 2, false));
        yield return new WaitForSeconds(1f);
        sfxSlider.gameObject.SetActive(true);
        foreach (Image img in imgArr2) { StartCoroutine(TextFade(img, 1f, 20, false)); }
        returnButton.gameObject.SetActive(true);
        StartCoroutine(TextFade(returnButton.GetComponentInChildren<TMP_Text>(), 1, 2, false));
    }

    public IEnumerator OptionsMenuOut()
    {
        StartCoroutine(Fade(optionsTitle, 1));
        StartCoroutine(Fade(musicTitle, 1));
        StartCoroutine(Fade(sfxTitle, 1));
        foreach (Image img in imgArr1) { StartCoroutine(Fade(img, 1f)); }
        foreach (Image img in imgArr2) { StartCoroutine(Fade(img, 1f)); }
        StartCoroutine(Fade(returnButton.GetComponentInChildren<TMP_Text>(), 1));
        

        yield return new WaitForSeconds(2f);
        musicSlider.gameObject.SetActive(false);
        sfxSlider.gameObject.SetActive(false);
        returnButton.gameObject.SetActive(false);
        StartCoroutine(TextFade(borderImage, 1, 0, false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade(rendTexture, 1, 0, false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade(gameTitle, 1, 0, false));
        yield return new WaitForSeconds(1f);
        playButton.gameObject.SetActive(true);
        optionsButton.gameObject.SetActive(true);
        creditsButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        StartCoroutine(TextFade(playButton.GetComponentInChildren<TMP_Text>(), 1, 0, false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade(optionsButton.GetComponentInChildren<TMP_Text>(), 1, 0, false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade(creditsButton.GetComponentInChildren<TMP_Text>(), 1, 0, false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade(quitButton.GetComponentInChildren<TMP_Text>(), 1, 0, false));
    }

    public void OptionsIn()
    {
        StartCoroutine(OptionsMenuIn());
    }

    public void OptionsOut()
    {
        StartCoroutine(OptionsMenuOut());
    }

    public IEnumerator goBackPlayer1()
    {
        StartCoroutine(Fade(player1Title, 1));
        if (!customSeed)
        {
            StartCoroutine(Fade(seedGen, 1));
            StartCoroutine(Fade(player1SeedButton.GetComponentInChildren<TMP_Text>(), 1));
            StartCoroutine(Fade(player1Title, 1));

        }
        else
        {
            StartCoroutine(Fade(player1SeedInput.GetComponentsInChildren<TMP_Text>()[0], 1));
            //StartCoroutine(Fade(player1SeedInput.GetComponentsInChildren<TMP_Text>()[1], 1));
            StartCoroutine(Fade(player1SeedInputInsturctions, 1));
            StartCoroutine(Fade(goBackToSeed.GetComponentInChildren<TMP_Text>(), 1));
        }
        StartCoroutine(Fade(player1Insturctions, 1));
        StartCoroutine(Fade(player1Start.GetComponentInChildren<TMP_Text>(), 1));

        StartCoroutine(Fade(back1.GetComponentInChildren<TMP_Text>(), 1));

        yield return new WaitForSeconds(1.2f);
        player1SeedInput.gameObject.SetActive(false);
        player1SeedButton.gameObject.SetActive(false);
        goBackToSeed.gameObject.SetActive(false);
        player1Start.gameObject.SetActive(false);
        back1.gameObject.SetActive(false);

        player1Button.gameObject.SetActive(true);
        player2Button.gameObject.SetActive(true);
        StartCoroutine(TextFade(player1Button.GetComponentInChildren<TMP_Text>(), 1, 0, false));
        StartCoroutine(TextFade(player2Button.GetComponentInChildren<TMP_Text>(), 1, 0, false));

    }

    public IEnumerator goBackPlayer2()
    {
        StartCoroutine(Fade(player2Title, 1));
        StartCoroutine(Fade(seedInput.GetComponentsInChildren<TMP_Text>()[0], 1));
        StartCoroutine(Fade(seedInput.GetComponentsInChildren<TMP_Text>()[1], 1));
        StartCoroutine(Fade(player2Insturctions, 1));
        StartCoroutine(Fade(player2Start.GetComponentInChildren<TMP_Text>(), 1));
        StartCoroutine(Fade(back2.GetComponentInChildren<TMP_Text>(), 1));
        if (invalidOn) { StartCoroutine(Fade(invalidText, 1)); }

        yield return new WaitForSeconds(1.2f);
        player2Start.gameObject.SetActive(false);
        seedInput.gameObject.SetActive(false);
        player2Start.gameObject.SetActive(false);
        back2.gameObject.SetActive(false);

        player1Button.gameObject.SetActive(true);
        player2Button.gameObject.SetActive(true);
        StartCoroutine(TextFade(player1Button.GetComponentInChildren<TMP_Text>(), 1, 0, false));
        StartCoroutine(TextFade(player2Button.GetComponentInChildren<TMP_Text>(), 1, 0, false));

    }

    public void goback1()
    {
        StartCoroutine(goBackPlayer1());
    }

    public void goback2()
    {
        StartCoroutine(goBackPlayer2());
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
