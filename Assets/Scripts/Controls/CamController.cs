using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public Transform target;
    public Transform viewCamera;

    [Header("Player Settings")]
    public float mouseSensitivity = 10.0f;
    public float turnRate = 1.0f;

    [Header("Camera settings")]
    public Vector2 pitchMinMax = new Vector2(-45, 90);

    public float targetSmoothTime;
    Vector3 targetSmoothVelocity;

    private float pitch, yaw;

    public Vector3 cameraOffset;
    Vector3 startingEuler;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        startingEuler = transform.eulerAngles;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // yaw for looking side to side, pitch for looking up and down
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        Vector3 newRotation = new Vector3(pitch, yaw) + startingEuler;

        transform.eulerAngles = newRotation;

        // SmoothDamp for camera lag
        Vector3 newPosition = target.position + cameraOffset;
        if (target)
        {
            target.forward = Vector3.Lerp(target.forward, transform.forward, turnRate * Time.deltaTime);
            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref targetSmoothVelocity, targetSmoothTime);
        }
    }
}
