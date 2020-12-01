using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialsLerp : MonoBehaviour
{
    public Material mat1;
    public Material mat2;
    public bool isMat1 = true;
    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isMat1)
        {
            GetComponent<Renderer>().materials[0].Lerp(GetComponent<Renderer>().materials[0], mat1, Time.deltaTime * speed);
        } else
        {
            GetComponent<Renderer>().materials[0].Lerp(GetComponent<Renderer>().materials[0], mat2, Time.deltaTime * speed);
        }
    }
}
