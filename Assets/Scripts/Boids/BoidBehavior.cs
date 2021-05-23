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

    public Vector3 direction;

    float travelDistance;
    Vector3 acceleration;
    Vector3 randDirection;

    public void Initialize(BoidSettings settings)
    {
        this.settings = settings;
        
        direction = transform.forward;
        position = transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(transform.position + transform.forward * settings.collisionAvoidDst, settings.boundsRadius);

        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, settings.perceptionRadius);

        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, settings.avoidanceRadius);
        Gizmos.DrawRay(transform.position, acceleration);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, velocity);
    }

    // Update is called once per frame
    public void BoidUpdate()
    {
        acceleration = Vector3.zero;

        //if (settings.IsTargetValid())
        //{
        //    Vector3 targetDir = settings.target.transform.position - transform.position;
        //    Vector3 targetForce = GetSteering(targetDir) * settings.targetWeight;
        //    acceleration += targetForce;
        //}

        if (travelDistance > 0)
        {
            travelDistance -= Time.deltaTime;           
            acceleration += GetSteering(randDirection) * settings.randWeight;
        }
        else
        {
            travelDistance = Random.Range(2.0f, 4.0f);
            randDirection = Random.insideUnitSphere;
        }

        if (perceivedFlockmates != 0)
        {
            flockCenter /= perceivedFlockmates;
            avgFlockDirection /= perceivedFlockmates;

            Vector3 flockOffset = flockCenter - position;
            acceleration += GetSteering(flockOffset) * settings.cohesionWeight;
            acceleration += GetSteering(avgFlockDirection) * settings.alignWeight;
            acceleration += GetSteering(avoidanceDirection) * settings.seperateWeight;
        }
        
        if(IsOnCollisionPath())
        {
            Vector3 collisionAvoidDir = ObstacleRays();
            Vector3 collisionAvoidForce = GetSteering(collisionAvoidDir) * settings.avoidCollisionWeight;
            acceleration += collisionAvoidForce;
        }

        // get speed from velocity magnitude and clamp it
        velocity += acceleration * Time.deltaTime;
        float speed = velocity.magnitude;
        speed = Mathf.Clamp(speed, settings.minSpeed, settings.maxSpeed);
        velocity.Normalize();
        velocity *= speed;

        transform.position += velocity * Time.deltaTime;
        transform.forward = velocity.normalized;
        position = transform.position;
        direction = velocity.normalized;
    }

    Vector3 GetSteering(Vector3 input)
    {
        // get clamped steering vector from direction input
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
            Vector3 dir = transform.TransformDirection(rayDirections[i]);
            Ray ray = new Ray(position, dir);
            if(!Physics.SphereCast(ray, settings.boundsRadius, settings.collisionAvoidDst, settings.obstacleMask))
            {
                return dir;
            }
        }

        return direction;
    }

    public void SetVelocity(Vector3 newVelocity)
    {
        velocity = newVelocity;
    }

    public Vector3 GetVelocity()
    {
        return velocity;
    }
}
