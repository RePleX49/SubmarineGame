using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    public BoidBehavior prefab;
    public float radius = 10.0f;
    public int spawnAmount = 10;
    public bool bPreviewRegion = false;
    public int baseBody = 1;

    public Material[] bodyMats;

    //public Color colorOne;
    //public Color colorTwo;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnPoint = transform.position + (Random.insideUnitSphere * radius);
            BoidBehavior boid = Instantiate(prefab);

            boid.transform.position = spawnPoint;
            boid.transform.forward = Random.insideUnitSphere;

            float colorLerp = Random.Range(0.0f, 1.0f);
            //for (int j = 0; j < bodyMats.Length; j++)
            //{
            //    boid.GetComponent<MeshRenderer>().materials[i] = bodyMats[i];
            //}

            boid.GetComponent<MeshRenderer>().materials[baseBody] = bodyMats[Random.Range(0, bodyMats.Length)];

            //boid.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.Lerp(colorOne, colorTwo, colorLerp));
        }
    }

    void OnDrawGizmos()
    {
        if(bPreviewRegion)
        {
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}
