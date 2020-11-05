using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformations : MonoBehaviour
{
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

}
