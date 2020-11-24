using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cam : MonoBehaviour
{
    public Transform target;
    public Transform viewCamera;

    public float raycastDistance = 20.0f;
    public float racastDistanceLong = 35.0f;

    [Header("Player Settings")]
    public float mouseSensitivity = 10.0f;
    public float turnRate = 1.0f;

    [Header("Camera settings")]
    public Vector2 pitchMinMax = new Vector2(-45, 90);

    public float targetSmoothTime;
    Vector3 targetSmoothVelocity;

    public float pitch, yaw;

    public Vector3 cameraOffset;
    Vector3 startingEuler;

    public Image crosshair;

    public Sprite crossFull;
    public Sprite crossEmpty;

    private Sprite prevSprite;

    public Vector3 crossSmall;
    public Vector3 crossLarge;

    public float crossChangeRate = .0075f;

    private string buttonTag = "Button";



    // Start is called before the first frame update
    void Start()
    {
        Systems.cam = this;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        prevSprite = crossEmpty;

        startingEuler = transform.eulerAngles;
    }

    void Update()
    {

            //Debug.Log("Click");
            RaycastHit Hit;
        Debug.DrawRay(viewCamera.position, viewCamera.forward * raycastDistance, Color.green, 2);

        if (Physics.Raycast(viewCamera.position, viewCamera.forward, out Hit, raycastDistance))
        {
            //Debug.Log("Hit");
            if (Hit.transform.gameObject.CompareTag(buttonTag) && !Hit.collider.isTrigger)
            {
                if (prevSprite != crossEmpty)
                {
                    crosshair.sprite = crossEmpty;
                    prevSprite = crossEmpty;
                }
                if (crosshair.transform.localScale.x < crossLarge.x)
                {
                    crosshair.transform.localScale = new Vector3(crosshair.transform.localScale.x + (crossChangeRate * Time.deltaTime), crosshair.transform.localScale.y + (crossChangeRate * Time.deltaTime), crosshair.transform.localScale.z);
                }


                if (Input.GetKeyDown(KeyCode.F))
                {
                    ButtonScript button = Hit.transform.gameObject.GetComponent<ButtonScript>();

                    if (button)
                    {
                        button.UseButton();

                    }
                }
            }
            else
            {
                //Debug.DrawRay(crosshair.transform.position, transform.TransformDirection(Vector3.forward) * castDist, Color.yellow);


                if (crosshair.transform.localScale.x > crossSmall.x)
                {
                    crosshair.transform.localScale = new Vector3(crosshair.transform.localScale.x - (crossChangeRate * Time.deltaTime), crosshair.transform.localScale.y - (crossChangeRate * Time.deltaTime), crosshair.transform.localScale.z);
                } else if (prevSprite != crossFull)
                {
                    crosshair.sprite = crossFull;
                    prevSprite = crossFull;
                }
            }
        }
        else
        {
            //Debug.DrawRay(crosshair.transform.position, transform.TransformDirection(Vector3.forward) * castDist, Color.yellow);

            if (crosshair.transform.localScale.x > crossSmall.x)
            {
                crosshair.transform.localScale = new Vector3(crosshair.transform.localScale.x - (crossChangeRate * Time.deltaTime), crosshair.transform.localScale.y - (crossChangeRate * Time.deltaTime), crosshair.transform.localScale.z);
            } else if (prevSprite != crossFull)
            {
                crosshair.sprite = crossFull;
                prevSprite = crossFull;
            }
        }


        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    //Debug.Log("Click");
        //    RaycastHit Hit;

        //    if (Physics.Raycast(viewCamera.position, viewCamera.forward, out Hit, raycastDistance))
        //    {
        //        //Debug.Log("Hit");
        //        if (Hit.transform.gameObject.CompareTag("Button"))
        //        {
        //            ButtonScript button = Hit.transform.gameObject.GetComponent<ButtonScript>();

        //            if(button)
        //            {
        //                button.UseButton();

        //            }
        //        }
        //    }
        //}
    }

    void LateUpdate()
    {
        if (Systems.player.canMove)
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RaycastTrigger")
        {
            raycastDistance = racastDistanceLong;
        }
    }
}

