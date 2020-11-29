using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    public Image background;
    public TMP_Text titleText;
    public GameObject resumeButton;
    public GameObject quitButton;
    public TMP_Text soundTitle;
    public TMP_Text sfxTitle;
    public Slider soundSlider;
    public Slider sfxSlider;
    public Image quitImage;
    public Image resumeImage;
    public Image soundBackground;
    public Image soundFill;
    public Image soundHandle;
    public Image sfxBackground;
    public Image sfxFill;
    public Image sfxHandle;

    public bool menuAccess = false;

    private float alpha = 0;
    bool menuIsOn = false;

    // Start is called before the first frame update
    void Start()
    {
        Color zeroAlphaWhite = new Color(background.color.r, background.color.g, background.color.b, 0);
        Color zeroAlphaBlack = new Color(titleText.color.r, titleText.color.g, titleText.color.b, 0);

        alpha = 0;
        background.color = zeroAlphaWhite;
        titleText.color = zeroAlphaBlack;
        resumeButton.GetComponentInChildren<TMP_Text>().color = zeroAlphaBlack;
        quitButton.GetComponentInChildren<TMP_Text>().color = zeroAlphaBlack;
        soundTitle.color = zeroAlphaBlack;
        sfxTitle.color = zeroAlphaBlack;
        quitImage.color = zeroAlphaWhite;
        resumeImage.color = zeroAlphaWhite;
        soundBackground.color = zeroAlphaWhite;
        soundFill.color = zeroAlphaWhite;
        soundHandle.color = zeroAlphaWhite;
        sfxBackground.color = zeroAlphaWhite;
        sfxFill.color = zeroAlphaWhite;
        sfxHandle.color = zeroAlphaWhite;

        resumeButton.SetActive(false);
        quitButton.SetActive(false);
        soundSlider.gameObject.SetActive(false);
        sfxSlider.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && menuAccess)
        {
            if (!menuIsOn)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                StopAllCoroutines();
                StartCoroutine(MenuIn());
                
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                StopAllCoroutines();
                StartCoroutine(MenuOut());
            }
            menuIsOn = !menuIsOn;
        }

    }

    IEnumerator MenuIn()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Systems.player.canMove = false;
        Color zeroAlphaWhite = new Color(background.color.r, background.color.g, background.color.b, alpha);
        Color fullAlphaWhite = new Color(background.color.r, background.color.g, background.color.b, 1);
        Color zeroAlphaBlack = new Color(titleText.color.r, titleText.color.g, titleText.color.b, alpha);
        Color fullAlphaBlack = new Color(titleText.color.r, titleText.color.g, titleText.color.b, 1);

        resumeButton.SetActive(true);
        quitButton.SetActive(true);
        soundSlider.gameObject.SetActive(true);
        sfxSlider.gameObject.SetActive(true);

        for (float timer = 0; timer < 1; timer += Time.deltaTime)
        {
            alpha = timer;
            background.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, timer);
            titleText.color = Color.Lerp(zeroAlphaBlack, fullAlphaBlack, timer);
            resumeButton.GetComponentInChildren<TMP_Text>().color = Color.Lerp(zeroAlphaBlack, fullAlphaBlack, timer);
            quitButton.GetComponentInChildren<TMP_Text>().color = Color.Lerp(zeroAlphaBlack, fullAlphaBlack, timer);
            soundTitle.color = Color.Lerp(zeroAlphaBlack, fullAlphaBlack, timer);
            sfxTitle.color = Color.Lerp(zeroAlphaBlack, fullAlphaBlack, timer);
            quitImage.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, timer);
            resumeImage.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, timer);
            soundBackground.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, timer);
            soundFill.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, timer);
            soundHandle.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, timer);
            sfxBackground.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, timer);
            sfxFill.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, timer);
            sfxHandle.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, timer);
            yield return null;
        }

        alpha = 1;
        background.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, alpha);
        titleText.color = Color.Lerp(zeroAlphaBlack, fullAlphaBlack, alpha);
        resumeButton.GetComponentInChildren<TMP_Text>().color = Color.Lerp(zeroAlphaBlack, fullAlphaBlack, alpha);
        quitButton.GetComponentInChildren<TMP_Text>().color = Color.Lerp(zeroAlphaBlack, fullAlphaBlack, alpha);
        soundTitle.color = Color.Lerp(zeroAlphaBlack, fullAlphaBlack, alpha);
        sfxTitle.color = Color.Lerp(zeroAlphaBlack, fullAlphaBlack, alpha);
        quitImage.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, alpha);
        resumeImage.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, alpha);
        soundBackground.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, alpha);
        soundFill.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, alpha);
        soundHandle.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, alpha);
        sfxBackground.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, alpha);
        sfxFill.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, alpha);
        sfxHandle.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, alpha);
    }

    IEnumerator MenuOut()
    {
        
        Color zeroAlphaWhite = new Color(background.color.r, background.color.g, background.color.b, 0);
        Color fullAlphaWhite = new Color(background.color.r, background.color.g, background.color.b, alpha);
        Color zeroAlphaBlack = new Color(titleText.color.r, titleText.color.g, titleText.color.b, 0);
        Color fullAlphaBlack = new Color(titleText.color.r, titleText.color.g, titleText.color.b, alpha);

        for (float timer = 1; timer > 0; timer -= Time.deltaTime)
        {
            alpha = timer;
            background.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, timer);
            titleText.color = Color.Lerp(zeroAlphaBlack, fullAlphaBlack, timer);
            resumeButton.GetComponentInChildren<TMP_Text>().color = Color.Lerp(zeroAlphaBlack, fullAlphaBlack, timer);
            quitButton.GetComponentInChildren<TMP_Text>().color = Color.Lerp(zeroAlphaBlack, fullAlphaBlack, timer);
            soundTitle.color = Color.Lerp(zeroAlphaBlack, fullAlphaBlack, timer);
            sfxTitle.color = Color.Lerp(zeroAlphaBlack, fullAlphaBlack, timer);
            quitImage.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, timer);
            resumeImage.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, timer);
            soundBackground.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, timer);
            soundFill.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, timer);
            soundHandle.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, timer);
            sfxBackground.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, timer);
            sfxFill.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, timer);
            sfxHandle.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, timer);
            yield return null;
        }

        alpha = 0;
        background.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, alpha);
        titleText.color = Color.Lerp(zeroAlphaBlack, fullAlphaBlack, alpha);
        resumeButton.GetComponentInChildren<TMP_Text>().color = Color.Lerp(zeroAlphaBlack, fullAlphaBlack, alpha);
        quitButton.GetComponentInChildren<TMP_Text>().color = Color.Lerp(zeroAlphaBlack, fullAlphaBlack, alpha);
        soundTitle.color = Color.Lerp(zeroAlphaBlack, fullAlphaBlack, alpha);
        sfxTitle.color = Color.Lerp(zeroAlphaBlack, fullAlphaBlack, alpha);
        quitImage.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, alpha);
        resumeImage.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, alpha);
        soundBackground.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, alpha);
        soundFill.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, alpha);
        soundHandle.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, alpha);
        sfxBackground.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, alpha);
        sfxFill.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, alpha);
        sfxHandle.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, alpha);

        resumeButton.SetActive(false);
        quitButton.SetActive(false);
        soundSlider.gameObject.SetActive(false);
        sfxSlider.gameObject.SetActive(false);
        Systems.player.canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Resume()
    {
        StopAllCoroutines();
        StartCoroutine(MenuOut());
        menuIsOn = false;
    }

    public IEnumerator QuitCo()
    {
        Color zeroAlphaWhite = new Color(background.color.r, background.color.g, background.color.b, alpha);
        Color fullAlphaWhite = new Color(background.color.r, background.color.g, background.color.b, 1);

        for (float timer = 0; timer < 1; timer += Time.deltaTime)
        {
            background.color = Color.Lerp(zeroAlphaWhite, fullAlphaWhite, timer);
            yield return null;
        }
        background.color = fullAlphaWhite;
        SceneManager.LoadScene("ReturnMenu");
    }

    public void Quit()
    {
        StartCoroutine(QuitCo());
    }
}
