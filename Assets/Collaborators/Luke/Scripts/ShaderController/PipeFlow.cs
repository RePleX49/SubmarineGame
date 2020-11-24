using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeFlow : MonoBehaviour
{
    MeshRenderer renderer;
    float flowDuration = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            float initialVal = renderer.material.GetFloat("_FillValue");
            if (initialVal < 0.1f)
                StartCoroutine(ChangeFlow(2.0f));
            else if (initialVal >= 1.9f)
                StartCoroutine(ChangeFlow(0.0f));
        }
    }

    IEnumerator ChangeFlow(float targetVal)
    {
        float elaspedTime = 0.0f;
        float initialVal = renderer.material.GetFloat("_FillValue");

        while(elaspedTime < flowDuration)
        {
            float newVal = Mathf.Lerp(initialVal, targetVal, elaspedTime / flowDuration);
            renderer.material.SetFloat("_FillValue", newVal);
            elaspedTime += Time.deltaTime;

            yield return null;
        }
    }
}
