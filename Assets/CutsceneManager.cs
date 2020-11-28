using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public GameObject player;
    public GameObject cam;
    public float speed;
    public Image whiteBackground;
    public Color backgroundColor;
    public GameObject player2;
    [Header("Unreavealed Transform")]
    public Vector3 sub2UnrevealedPos;
    public Vector3 sub2UnrevealedRot;
    [Header("Submarine 1 Together Transform")]
    public Vector3 sub1TogetherPos;
    public Vector3 sub1TogetherRot;
    [Header("Submarine 2 Together Transform")]
    public Vector3 sub2TogetherPos;
    public Vector3 sub2TogetherRot;

    public GameObject pillar;
    public Vector3 pillar1Pos;
    public Vector3 pillar2Pos;
    public float spacing = 80;

    public GameObject floor;
    public Vector3 floorPos;
    public float floorSpacing = 120;

    public Vector3 beat1_1pos;
    public Vector3 beat1_2pos;
    public Vector3 beat1_3pos;
    public Vector3 beat2_1pos;
    public Vector3 beat2_2pos;
    public Vector3 beat2_3pos;

    public Vector3 beat1_1rot;
    public Vector3 beat1_2rot;
    public Vector3 beat1_3rot;
    public Vector3 beat2_1rot;
    public Vector3 beat2_2rot;
    public Vector3 beat2_3rot;

    bool cameramoving = true;
    public float newSpeed = 1;
    public Image blackBackground;

    public AnimationCurve testCurve;
    public AnimationCurve testCurve2;
    public string scene;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Cutscene());
        for (int i = 0; i < 6; i++)
        {
            Instantiate(pillar, pillar1Pos + new Vector3(0, 0, spacing * i), Quaternion.identity);
            Instantiate(pillar, pillar2Pos + new Vector3(0, 0, spacing * i), Quaternion.identity);
            Instantiate(floor, floorPos + new Vector3(0, 0, floorSpacing * (i - 1)), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cameramoving)
        {
            player.transform.position += (new Vector3(0, 0, 1) * speed * Time.deltaTime);
            player2.transform.position += (new Vector3(0, 0, 1) * speed * Time.deltaTime);
            cam.transform.position += (new Vector3(0, 0, 1) * speed * Time.deltaTime);
        }
        else
        {
            speed = Mathf.Lerp(speed, newSpeed, Time.deltaTime);
            cam.transform.position += (new Vector3(0, 0, 1) * speed * Time.deltaTime);
        }
    }


    public IEnumerator Cutscene()
    {
        Color zeroAlpha = new Color(whiteBackground.color.r, whiteBackground.color.g, whiteBackground.color.b, 0);
        Color fullAlpha = new Color(whiteBackground.color.r, whiteBackground.color.g, whiteBackground.color.b, 1);
        whiteBackground.color = fullAlpha;
        for (float timer = 0; timer < 6f; timer += Time.deltaTime)
        {
            whiteBackground.color = Color.Lerp(fullAlpha, zeroAlpha, timer / 6f);
            yield return null;
        }
        whiteBackground.color = zeroAlpha;

        yield return new WaitForSeconds(2f);

        Color originalbackgroundColor = cam.GetComponent<Camera>().backgroundColor;

        for (float timer = 0; timer < 6f; timer += Time.deltaTime)
        {
            cam.GetComponent<Camera>().backgroundColor = Color.Lerp(originalbackgroundColor, backgroundColor, timer / 6f);
            yield return null;
        }
        cam.GetComponent<Camera>().backgroundColor = backgroundColor;

        yield return new WaitForSeconds(2f);

        CameraPlace(player2, sub2UnrevealedPos, sub2UnrevealedRot);

        StartCoroutine(Systems.transforms.LerpMove(player.transform, cam.transform.position + sub1TogetherPos, Quaternion.Euler(sub1TogetherRot), Vector3.one, 6f, testCurve, testCurve2));
        StartCoroutine(Systems.transforms.LerpMove(player2.transform, cam.transform.position + sub2TogetherPos, Quaternion.Euler(sub2TogetherRot), Vector3.one, 6f, testCurve, testCurve2));
        yield return new WaitForSeconds(12f);
            
        cameramoving = false;

        StartCoroutine(Systems.transforms.LerpMove(player.transform, beat1_1pos, Quaternion.Euler(beat1_1rot), Vector3.one, 3f, testCurve, testCurve2));
        StartCoroutine(Systems.transforms.LerpMove(player2.transform, beat2_1pos, Quaternion.Euler(beat2_1rot), Vector3.one, 3f, testCurve, testCurve2));
        yield return new WaitForSeconds(3f);

        StartCoroutine(BegoneBackground());
        StartCoroutine(Systems.transforms.LerpMove(player.transform, beat1_2pos, Quaternion.Euler(beat1_2rot), Vector3.one, 5f, testCurve, testCurve2));
        StartCoroutine(Systems.transforms.LerpMove(player2.transform, beat2_2pos, Quaternion.Euler(beat2_2rot), Vector3.one, 5f, testCurve, testCurve2));
        yield return new WaitForSeconds(5f);
        StartCoroutine(Systems.transforms.LerpMove(player.transform, beat1_3pos, Quaternion.Euler(beat1_3rot), Vector3.one, 2f, testCurve, testCurve2));
        StartCoroutine(Systems.transforms.LerpMove(player2.transform, beat2_3pos, Quaternion.Euler(beat2_3rot), Vector3.one, 2f, testCurve, testCurve2));
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(scene);

        

        //StartCoroutine(Systems.transforms.LerpMove(player2.transform, cam.transform.position + sub2TogetherPos, Quaternion.Euler(sub2TogetherRot), Vector3.one, 6f));
    }

    IEnumerator BegoneBackground()
    {
        Color zeroAlpha = new Color(blackBackground.color.r, blackBackground.color.g, blackBackground.color.b, 0);
        Color fullAlpha = new Color(blackBackground.color.r, blackBackground.color.g, blackBackground.color.b, 1);
        blackBackground.color = zeroAlpha;
        for (float timer = 0; timer < 8f; timer += Time.deltaTime)
        {
            blackBackground.color = Color.Lerp(zeroAlpha, fullAlpha, timer / 8f);
            yield return null;
        }
        blackBackground.color = fullAlpha;
    }

    public void CameraPlace(GameObject obj, Vector3 pos, Vector3 rot)
    {
        obj.transform.position = cam.transform.position + pos;
        obj.transform.rotation = Quaternion.Euler(rot);
    }
}
