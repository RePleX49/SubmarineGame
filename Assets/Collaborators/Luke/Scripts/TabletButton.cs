using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletButton : MonoBehaviour
{
    [HideInInspector]
    public Symbols activeSymbol;
    public MeshRenderer symbolImage;

    TabletManager manager;

    private void Awake()
    {
        manager = FindObjectOfType<TabletManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void ChangeImage()
    {

    }

    IEnumerator ButtonPush()
    {
        float timeElapsed = 0.0f;

        while(timeElapsed < manager.tabletData.pushDuration)
        {
            timeElapsed += Time.deltaTime;
            Vector3 newPos = new Vector3(0.0f, transform.localPosition.y 
                - Mathf.Sin((2 * Mathf.PI) * (timeElapsed / manager.tabletData.pushDuration) * 2.0f), 0.0f);

            transform.localPosition += newPos;
        }

        yield return null;
    }
}
