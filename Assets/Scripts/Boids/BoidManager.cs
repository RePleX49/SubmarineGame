using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    BoidBehavior[] boids;
    public BoidSettings settings;
    public GameObject targetObject;

    // Start is called before the first frame update
    void Start()
    {
        settings.target = targetObject;

        boids = FindObjectsOfType<BoidBehavior>();
        foreach(BoidBehavior b in boids)
        {
            b.Initialize(settings);
        }
    }

    // Update is called once per frame
    void Update()
    {
        BoidCalculation(boids);
    }

    void BoidCalculation(BoidBehavior[] boids)
    {
        foreach(BoidBehavior boid in boids)
        {
            float distance;
            Vector3 averageCenter = boid.position;
            Vector3 averageDir = boid.direction;
            int numFlockmates = 0;

            foreach (BoidBehavior other in boids)
            {
                if(boid == other)
                {
                    continue;
                }

                distance = Vector3.Distance(boid.transform.position, other.transform.position);

                if(distance <= settings.perceptionRadius)
                {
                    numFlockmates++;
                    averageCenter += other.position;
                    averageDir += other.direction;
                    
                    if(distance <= settings.avoidanceRadius)
                    {
                        Vector3 avoidDir = other.transform.position - boid.transform.position;
                        // divide by distance so avoid magnitude is inversely proportional to distance
                        avoidDir /= distance;
                        boid.avoidanceDirection += avoidDir;
                    }
                }
            }

            if(boids.Length > 1)
            {
                boid.perceivedFlockmates = numFlockmates;
                boid.flockCenter = averageCenter / numFlockmates;
                boid.avgFlockDirection = averageDir;
            }

            boid.BoidUpdate();
        }
    }
}
