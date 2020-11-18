using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float turnAroundSpeed = 0.2f;
    [SerializeField] private float forwardMoveSpeed = 20.0f;
    [SerializeField] private float forwardMin = 20.0f;
    [SerializeField] private float speedBoost = 10f;
    [SerializeField] private float forwardMax = 80f;
    [SerializeField] private float sideMoveSpeed = 10.0f;
    [SerializeField] private float upMoveSpeed = 6.0f;
    [SerializeField] private Vector3 currentSpeed = Vector3.zero;
    [SerializeField] private Vector3 currentVelocity = Vector3.zero;
    [SerializeField] private float speedSmoothTime = 1.0f;

    [SerializeField] private float maxSpeed = 40.0f;
    [SerializeField] private float acceleration = 2.0f;

    [SerializeField] private ParticleSystem[] forwardParticles;
    [SerializeField] private ParticleSystem[] leftParticles;
    [SerializeField] private ParticleSystem[] rightParticles;

    [SerializeField] private bool dockable = false;
    public bool canMove = false;

    [SerializeField] private Vector3 alignmentPosition = new Vector3();
    [SerializeField] private Quaternion alignmentRotation = new Quaternion();

    public PropellerSpin propellerManager;
    public int movingPropScale = 10;
    public int idlePropScale = 1;
    public int strafePropScale = 6;
    public int boostPropScale = 15;


    //public UI



    CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        Systems.player = this;
        cc = GetComponent<CharacterController>();
        foreach (ParticleSystem particle in forwardParticles) { particle.emissionRate = 0f; }
        foreach (ParticleSystem particle in rightParticles) { particle.emissionRate = 0f; }
        foreach (ParticleSystem particle in forwardParticles) { particle.emissionRate = 0f; }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetSpeed = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(SpeedBoost(5f));

        }

        if (Input.GetKey(KeyCode.W))
        {
            targetSpeed.x = forwardMoveSpeed;
            foreach(ParticleSystem particle in forwardParticles) { particle.emissionRate = 40f; }
            propellerManager.SetTimeScale(movingPropScale);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            targetSpeed.x = -forwardMoveSpeed * 0.5f;
            propellerManager.SetTimeScale(-strafePropScale);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            targetSpeed.z = -sideMoveSpeed;
            foreach (ParticleSystem particle in leftParticles) { particle.emissionRate = 40f; }
            propellerManager.SetTimeScale(strafePropScale);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            targetSpeed.z = sideMoveSpeed;
            foreach (ParticleSystem particle in rightParticles) { particle.emissionRate = 40f; }
            propellerManager.SetTimeScale(-strafePropScale);
        }
        else
        {
            foreach (ParticleSystem particle in forwardParticles) { particle.emissionRate = 0f; }
            foreach (ParticleSystem particle in rightParticles) { particle.emissionRate = 0f; }
            foreach (ParticleSystem particle in leftParticles) { particle.emissionRate = 0f; }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            targetSpeed.y = upMoveSpeed;
            //propellerManager.SetTimeScale(strafePropScale);
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            targetSpeed.y = -upMoveSpeed;
            propellerManager.SetTimeScale(strafePropScale);
        }

        currentSpeed = Vector3.SmoothDamp(currentSpeed, targetSpeed, ref currentVelocity, speedSmoothTime);
        if (canMove) { Move(); }
        if (dockable && Input.GetKeyDown(KeyCode.E)) { dockable = false; StartCoroutine(Dock(2f, 2f)); }
    }

    void Move()
    {
        Vector3 velocity = (transform.forward * currentSpeed.x) + (Vector3.up * currentSpeed.y) + (transform.right * currentSpeed.z);
        cc.Move(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pillar")
        {
            StartCoroutine(Systems.UI.FadeInText(Systems.UI.pillarText, "Press E to Dock", 1f, Color.white));
            dockable = true;
            alignmentPosition = other.GetComponent<PillarData>().pillarPos;
            alignmentRotation = Quaternion.Euler(other.GetComponent<PillarData>().pillarRot);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Pillar")
        {
            StartCoroutine(Systems.UI.FadeOutText(Systems.UI.pillarText, "Press E to Dock", 1f, Color.white));
            dockable = false;
        }
    }

    private IEnumerator Dock(float time, float waitTime)
    {
        StartCoroutine(Systems.UI.FadeOutText(Systems.UI.pillarText, "Press E to Dock", 1f, Color.white));
        canMove = false;
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;
        Vector3 camPosition = Systems   .cam.gameObject.transform.position;
        Quaternion camRotation = Systems.cam.gameObject.transform.rotation;
        for (float timer = 0; timer < time; timer += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(position, alignmentPosition, timer / time);
            transform.rotation = Quaternion.Slerp(rotation, alignmentRotation, timer / time);
            Systems.cam.gameObject.transform.position = Vector3.Lerp(camPosition, alignmentPosition, timer / time);
            Systems.cam.gameObject.transform.rotation = Quaternion.Slerp(camRotation, alignmentRotation, timer / time);
            yield return null;
        }

        transform.position = alignmentPosition;
        transform.rotation = alignmentRotation;
        yield return new WaitForSeconds(waitTime);
        canMove = true;
    }

    private IEnumerator SpeedBoost(float timeLimit)
    {
        if (forwardMoveSpeed < forwardMax) { forwardMoveSpeed = 32f; }
        foreach (ParticleSystem particle in forwardParticles) { particle.emissionRate = 480f; }
        propellerManager.SetTimeScale(boostPropScale);

        for (float timer = 0; timer < timeLimit; timer += Time.deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                break;
            }
            yield return null;
        }

        foreach (ParticleSystem particle in forwardParticles) { particle.emissionRate = 40f; }
        forwardMoveSpeed = 8f;
        //StopAllCoroutines();
    }
}
