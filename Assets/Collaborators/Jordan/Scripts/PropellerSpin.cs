using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerSpin : MonoBehaviour
{
    private float time = 0;

    [SerializeField]private int timeScale = 1;

    private int shaderScale = 1;

    private Material propellerMat;
    private Material propellerMat1;

    // Start is called before the first frame update
    void Start()
    {
        propellerMat = gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material;
        propellerMat1 = gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material;

    }

    // Update is called once per frame
    void Update()
    {
        time = Time.time;

        if (time % 1 >= 0.97 || time % 1 <= 0.03)
        {
            //Debug.Log("OnSec: " + (timeScale - shaderScale));
            if (timeScale != shaderScale)
            {
                propellerMat.SetFloat("_TimeScale", timeScale);
                propellerMat1.SetFloat("_TimeScale", timeScale);
                shaderScale = timeScale;

            }
        }
    }

    public void SetTimeScale (int newScale)
    {
        timeScale = newScale;
    }
}
