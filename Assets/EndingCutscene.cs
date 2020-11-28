using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingCutscene : MonoBehaviour
{
    public bool isGameOver = false;
    public TMP_Text winInputText;
    public Image background;
    public TMP_Text winText;
    public TMP_Text winText2;
    public GameObject submarine2;
    [Header("Sub1Initial")]
    public Vector3 sub1InitialPos;
    public Vector3 sub1InitialRot;
    [Header("Sub2PreInitial")]
    public Vector3 sub2PreInitialPos;
    public Vector3 sub2PreInitialRot;
    [Header("Sub2Initial")]
    public Vector3 sub2InitialPos;
    public Vector3 sub2InitialRot;
    [Header("Sub1Final")]
    public Vector3 sub1FinalPos;
    public Vector3 sub1FinalRot;
    [Header("Sub2Final")]
    public Vector3 sub2FinalPos;
    public Vector3 sub2FinalRot;
    [Header("Camera")]
    public Vector3 cameraPos;
    public Vector3 cameraRot;

    public bool showWinText = false;
    public float speed;

    public GameObject cam;
    public GameObject player;
    private bool safety = false;

    public AnimationCurve positionCurve;

    public bool canWin = false;


    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(WinGame());
    }

    // Update is called once per frame
    void Update()
    {
        if(showWinText && canWin)
        {
            if (!safety)
            {
                Color col = new Color(winInputText.color.r, winInputText.color.g, winInputText.color.b, 1f);
                winInputText.color = Color.Lerp(winInputText.color, col, Time.deltaTime * speed);
            }
            
            if (Input.GetKeyDown(KeyCode.F) && !safety)
            {
                StartCoroutine(WinGame());
                safety = true;
            }
            if (safety)
            {
                Color col = new Color(winInputText.color.r, winInputText.color.g, winInputText.color.b, 0f);
                winInputText.color = Color.Lerp(winInputText.color, col, Time.deltaTime * speed);
            }
        } else
        {
            Color col = new Color(winInputText.color.r, winInputText.color.g, winInputText.color.b, 0f);
            winInputText.color = Color.Lerp(winInputText.color, col, Time.deltaTime * speed);
        }
    }

    IEnumerator WinGame()
    {
        Vector3 initPosPlayer = player.gameObject.transform.position;
        Vector3 initPosCam = player.gameObject.transform.position;
        Quaternion initRotPlayer = player.gameObject.transform.rotation;
        Quaternion initRotCam = player.gameObject.transform.rotation;

        player.GetComponent<Player>().enabled = false;
        cam.GetComponent<Cam>().enabled = false;
        for (float timer = 0; timer < 5f; timer += Time.deltaTime)
        {
            player.gameObject.transform.position = Vector3.Lerp(initPosPlayer, sub1InitialPos, positionCurve.Evaluate(timer / 5f));
            cam.gameObject.transform.position = Vector3.Lerp(initPosCam, cameraPos, positionCurve.Evaluate(timer / 5f));
            player.gameObject.transform.rotation = Quaternion.Slerp(initRotPlayer, Quaternion.Euler(sub1InitialRot), positionCurve.Evaluate(timer / 5f));
            cam.gameObject.transform.rotation = Quaternion.Slerp(initRotCam, Quaternion.Euler(cameraRot), positionCurve.Evaluate(timer / 5f));

            yield return null;
        }

        yield return new WaitForSeconds(3f);
        submarine2.transform.position = sub2PreInitialPos;
        submarine2.transform.rotation = Quaternion.Euler(sub2PreInitialRot);

        for (float timer = 0; timer < 5f; timer += Time.deltaTime)
        {
            submarine2.transform.position = Vector3.Lerp(sub2PreInitialPos, sub2InitialPos, positionCurve.Evaluate(timer / 5f));
            submarine2.transform.rotation = Quaternion.Slerp(Quaternion.Euler(sub2PreInitialRot), Quaternion.Euler(sub2InitialRot), positionCurve.Evaluate(timer / 5f));
            yield return null;
        }

        yield return new WaitForSeconds(3f);
        player.gameObject.transform.position = sub1InitialPos;

        submarine2.transform.position = sub2InitialPos;
        StartCoroutine(CutsceneHelper());

        for (float timer = 0; timer < 9f; timer += Time.deltaTime)
        {
            player.gameObject.transform.position = Vector3.Lerp(sub1InitialPos, sub1FinalPos, positionCurve.Evaluate(timer / 9f));
            submarine2.transform.position = Vector3.Lerp(sub2InitialPos, sub2FinalPos, positionCurve.Evaluate(timer / 9f));
            yield return null;
        }

        StartCoroutine(TextFade(winText, 2f, 5f, true));
        yield return new WaitForSeconds(3f);
        StartCoroutine(TextFade(winText2, 2f, 2f, true));

        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene("Menuscene");
    }

    IEnumerator CutsceneHelper()
    {
        yield return new WaitForSeconds(3f);
        Color zeroAlpha = new Color(background.color.r, background.color.g, background.color.b, 0);
        Color fullAlpha = new Color(background.color.r, background.color.g, background.color.b, 1);
        background.color = zeroAlpha;
        for (float timer = 0; timer < 5f; timer += Time.deltaTime)
        {
            background.color = Color.Lerp(zeroAlpha, fullAlpha, timer / 5f);
            yield return null;
        }
        background.color = fullAlpha;
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
}
