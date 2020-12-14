using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalManager : MonoBehaviour
{
    public Camera introCam;
    public Camera renderCam;
    public Camera finalCam;

    public GameObject holyLight;

    public MeshRenderer fadeCover;
    public Material fadeMat;
    public Color fadeCol;

    public bool fadingOut = false;
    public bool fadingIn = false;

    public bool transition = false;

    public bool holyDown = false;

    public GameObject rollboxVid;
    // Start is called before the first frame update
    void Start()
    {
        //introCam.GetComponent<Animator>().Play("IntroCam");
        finalCam.enabled = false;
        rollboxVid.SetActive(false);
        fadeCol = new Color(0, 0, 0, 1);
        fadingOut = true;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.F))
        {
            transition = true;
            fadingIn = true;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            rollboxVid.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            holyDown = true;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            fadingIn = true;
        }

        if (holyDown)
        {
            holyLight.transform.Translate(Vector3.down / 5, Space.World);
        }


        if (transition)
        {
            if (!fadingIn)
            {
                finalCam.enabled = !finalCam.enabled;
                introCam.enabled = !introCam.enabled;
                fadingOut = true;
            }
        }


        if (fadingOut)
        {
            if (fadeCol.a > 0)
            {
                fadeCol.a -= .05f;
            }
            else
            {
                fadingOut = false;
                transition = false;
            }
        }
        else if (fadingIn)
        {
            if (fadeCol.a < 1)
            {
                fadeCol.a += .05f;
            }
            else
            {
                fadingIn = false;
            }
        }


        fadeMat.color = fadeCol;
        fadeCover.material = fadeMat;

    }
}
