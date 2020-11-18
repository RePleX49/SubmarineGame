using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarPuzzleGen : MonoBehaviour
{
    public bool isPlayer1;
    public int pillar1;
    public int pillar2;
    public int pillar3;
    public GameObject[] pillars1;
    public GameObject[] pillars2;
    public GameObject[] pillars3;
    public Transform pillar1Transform;
    public Transform pillar2Transform;
    public Transform pillar3Transform;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(pillars1[pillar1], pillar1Transform.position, pillar1Transform.rotation);
        Instantiate(pillars2[pillar2], pillar2Transform.position, pillar2Transform.rotation);
        Instantiate(pillars3[pillar3], pillar3Transform.position, pillar3Transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
