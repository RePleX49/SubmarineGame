using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BoidBehavior : MonoBehaviour
{
    BoidSettings settings;

    Vector3 velocity;

    public Vector3 flockCenter;
    public Vector3 avgFlockDirection;
    public Vector3 avoidanceDirection;
    public int perceivedFlockmates;

    [HideInInspector]
    public Vector3 position;
    [HideInInspector]
    public Vector3 direction;

    Transform boidTransform;

    private void Awake()
    {
        boidTransform = transform;
    }

    public void Initialize(BoidSettings settings)
    {
        this.settings = settings;
        
        direction = boidTransform.forward;
        position = boidTransform.position;
    }

    // Update is called once per frame
    public void BoidUpdate()
    {
        Vector3 acceleration = Vector3.zero;

        if (perceivedFlockmates != 0)
        {        
            acceleration += GetSteering(flockCenter) * settings.cohesionWeight;
            acceleration += GetSteering(avgFlockDirection) * settings.alignWeight;
            acceleration += GetSteering(avoidanceDirection) * settings.seperateWeight;
        }
        
        if(IsOnCollisionPath())
        {
            Vector3 collisionAvoidDir = ObstacleRays();
            Vector3 collisionAvoidForce = GetSteering(collisionAvoidDir) * settings.avoidCollisionWeight;
            acceleration += collisionAvoidForce;
        }

        if(settings.target)
        {
            Vector3 targetDir = settings.target.transform.position - transform.position;
            Vector3 targetForce = GetSteering(targetDir) * settings.targetWeight;
            acceleration += targetForce;
        }

        // get speed from velocity magnitude and clamp it
        velocity += acceleration * Time.deltaTime;
        float speed = velocity.magnitude;
        speed = Mathf.Clamp(speed, settings.minSpeed, settings.maxSpeed);
        velocity.Normalize();
        velocity *= speed;

        boidTransform.position += velocity * Time.deltaTime;
        boidTransform.forward = velocity.normalized;
        position = boidTransform.position;
        direction = velocity.normalized;
    }

    Vector3 GetSteering(Vector3 input)
    {
        Vector3 outVector = input.normalized * settings.maxSpeed - velocity;
        return Vector3.ClampMagnitude(outVector, settings.maxSteerForce);
    }

    bool IsOnCollisionPath()
    {
        RaycastHit hit;
        if(Physics.SphereCast(position, settings.boundsRadius, direction, out hit, settings.collisionAvoidDst, settings.obstacleMask))
        {
            return true;
        }

        return false;
    }

    Vector3 ObstacleRays()
    {
        Vector3[] rayDirections = CollisionHelper.directions;

        for(int i = 0; i < rayDirections.Length; i++)
        {
            Vector3 dir = boidTransform.TransformDirection(rayDirections[i]);
            Ray ray = new Ray(position, dir);
            if(!Physics.SphereCast(ray, settings.boundsRadius, settings.collisionAvoidDst, settings.obstacleMask))
            {
                return dir;
            }
        }

        return direction;
    }
}
