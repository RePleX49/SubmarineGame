using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    BoidBehavior[] boids;
    public BoidSettings settings;
    public GameObject targetObject;
    public ComputeShader boidComputeShader;

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
        BoidCalculationGPU(boids);
    }

    void BoidCalculation(BoidBehavior[] boidArray)
    {
        foreach(BoidBehavior boid in boidArray)
        {
            float distance;
            Vector3 averageCenter = boid.position;
            Vector3 averageDir = boid.direction;
            int numFlockmates = 0;

            foreach (BoidBehavior other in boidArray)
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
                        boid.avoidanceDirection -= avoidDir;
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

    void BoidCalculationGPU(BoidBehavior[] boidArray)
    {
        BoidData[] boidsData = new BoidData[boidArray.Length];

        for(int i = 0; i < boidsData.Length; i++)
        {
            boidsData[i].position = boidArray[i].position;
            boidsData[i].direction = boidArray[i].direction;
        }

        ComputeBuffer boidBuffer = new ComputeBuffer(boidArray.Length, BoidData.Size());
        boidBuffer.SetData(boidsData);

        boidComputeShader.SetBuffer(0, "boids", boidBuffer);
        boidComputeShader.SetFloat("perceiveRadius", settings.perceptionRadius);
        boidComputeShader.SetFloat("avoidRadius", settings.avoidanceRadius);
        boidComputeShader.SetInt("numBoids", boids.Length);

        int groups = Mathf.CeilToInt(boids.Length / 8f);
        boidComputeShader.Dispatch(0, groups, 1, 1);

        boidBuffer.GetData(boidsData);

        // TODO for loop to assign boidsData from buffer to boids scripts
        for (int i = 0; i < boidArray.Length; i++)
        {
            boidArray[i].perceivedFlockmates = boidsData[i].flockMatesCount;
            boidArray[i].flockCenter = boidsData[i].flockCenter;
            boidArray[i].avgFlockDirection = boidsData[i].avgFlockDirection;
            boidArray[i].avoidanceDirection = boidsData[i].avoidDirection;

            boidArray[i].BoidUpdate();
        }

        boidBuffer.Release();
    }

    public struct BoidData
    {
        public Vector3 position;
        public Vector3 direction;
        public Vector3 flockCenter;
        public Vector3 avgFlockDirection;
        public Vector3 avoidDirection;

        public int flockMatesCount;

        public static int Size()
        {
            // 3 floats per Vector3, and 5 Vector3 fields
            return sizeof(float) * 3 * 5 + sizeof(int);
        }
    }
}

