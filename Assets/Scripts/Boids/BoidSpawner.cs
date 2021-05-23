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

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnPoint = transform.position + (Random.insideUnitSphere * radius);
            BoidBehavior boid = Instantiate(prefab);

            boid.transform.position = spawnPoint;
            boid.transform.forward = Random.insideUnitSphere;
            boid.SetVelocity(Random.insideUnitSphere);
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
