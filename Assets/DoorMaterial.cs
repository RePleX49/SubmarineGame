using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMaterial : MonoBehaviour
{
    public MaterialsLerp matSystem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            matSystem.isMat1 = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            matSystem.isMat1 = true;
        }
    }
}
