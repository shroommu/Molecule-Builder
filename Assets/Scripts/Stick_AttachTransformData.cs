using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Stick_AttachTransformData : ABS_AttachTransformData
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Ball_AttachTransformData>())
        {
            transform.parent.gameObject.GetComponent<XRGrabInteractable>().attachTransform =
                transform;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Ball_AttachTransformData>() && !isAttached)
        {
            canBeAttached = true;

            Transform parent = transform.parent;
            Vector3 temp = new Vector3(
                -parent.eulerAngles.x,
                -parent.eulerAngles.y,
                -parent.eulerAngles.z
            );
            transform.localRotation = Quaternion.Euler(temp);

            attachedObj = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Ball_AttachTransformData>())
        {
            canBeAttached = false;
            attachedObj = null;
        }
    }
}
