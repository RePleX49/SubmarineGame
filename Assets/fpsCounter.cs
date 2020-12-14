using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fpsCounter : MonoBehaviour
{
    Text outputText;
    float avgDeltaTime;

    // Start is called before the first frame update
    void Start()
    {
        outputText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        avgDeltaTime = Mathf.Lerp(Time.deltaTime, avgDeltaTime, .99f);
        outputText.text = (1 / avgDeltaTime).ToString();
    }
}
