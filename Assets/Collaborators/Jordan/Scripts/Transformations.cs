using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformations : MonoBehaviour
{
    [SerializeField] private AnimationCurve doorCurve;

    // Start is called before the first frame update
    void Start()
    {
        Systems.transforms = this;
    }


    public IEnumerator LerpMove (Transform obj, Vector3 targetPos, Quaternion targetRot, Vector3 targetScale, float duration) 
    {
        Debug.Log("Should be Transforming");
        Vector3 startPos = obj.position;
        Quaternion startRot = obj.rotation;
        Vector3 startScale = obj.localScale;

        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            obj.position = Vector3.Lerp(startPos, targetPos, timer / duration);
            obj.rotation = Quaternion.Slerp(startRot, targetRot, timer / duration);
            obj.localScale = Vector3.Lerp(startScale, targetScale, timer / duration);

            yield return null;
        }


        obj.position = targetPos;
        obj.rotation = targetRot;
        obj.localScale = targetScale;
        
    }

    public IEnumerator LerpMove(Transform obj, Vector3 targetPos, Quaternion targetRot, Vector3 targetScale, float duration, AnimationCurve acPos, AnimationCurve acRot) 
    {
        Debug.Log("Should be Transforming");
        Vector3 startPos = obj.position;
        Quaternion startRot = obj.rotation;
        Vector3 startScale = obj.localScale;

        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            obj.position = Vector3.Lerp(startPos, targetPos, acPos.Evaluate(timer / duration));
            obj.rotation = Quaternion.Slerp(startRot, targetRot, acRot.Evaluate(timer / duration));
            obj.localScale = Vector3.Lerp(startScale, targetScale, timer / duration);

            yield return null;
        }


        obj.position = targetPos;
        obj.rotation = targetRot;
        obj.localScale = targetScale;
        
    }

    public IEnumerator DoorLerp(Transform obj, Vector3 targetPos, float duration)
    {
        Debug.Log("Should be Transforming");
        Vector3 startPos = obj.localPosition;

        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            //obj.position = Vector3.Lerp(startPos, targetPos, timer / duration);
            float doorY = Mathf.Abs(startPos.y - targetPos.y) * doorCurve.Evaluate(timer / duration);
            obj.localPosition = new Vector3(startPos.x, startPos.y - doorY, startPos.z);
            yield return null;
        }

        obj.localPosition = targetPos;
    }

    public IEnumerator LerpMaterial(MeshRenderer rend, Material newMat, float duration)
    {
        Material oldMat = rend.materials[0];
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            rend.materials[0].Lerp(oldMat, newMat, timer / duration);
            yield return null;
        }
        rend.materials[0] = newMat;
    }

}
