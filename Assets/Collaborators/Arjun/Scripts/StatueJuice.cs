using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueJuice : MonoBehaviour
{
    public bool isPuzzleComplete = false;
    public Renderer[] statues;
    public Material baseStatueMaterial;
    public Material[] completeStatueMaterials;
    public float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPuzzleComplete)
        {
            for (int i = 0; i < statues.Length; i++)
            {
                statues[i].materials[0].Lerp(baseStatueMaterial, completeStatueMaterials[i], Time.deltaTime * speed);
            }
        }
    }
}
