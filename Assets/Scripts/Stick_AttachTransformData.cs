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
            canBeAttached = true;
            attachedObj = other.gameObject;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (
            other.gameObject.GetComponent<Ball_AttachTransformData>()
            && !other.gameObject.GetComponent<Ball_AttachTransformData>().isAttached
            && !isAttached
        )
        {
            Transform ball = other.transform.parent;
            Transform ballSocketAttachTransform = other.gameObject
                .GetComponent<XRSocketInteractor>()
                .attachTransform;
            Transform stick = transform.parent;
            Transform stickSocketAttachTransform = gameObject
                .GetComponent<XRSocketInteractor>()
                .attachTransform;

            Quaternion newRotation =
                stick.rotation
                * Quaternion.Inverse((ball.rotation * ballSocketAttachTransform.rotation));

            stickSocketAttachTransform.rotation = newRotation;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Ball_AttachTransformData>() && !isAttached)
        {
            canBeAttached = false;
            attachedObj = null;
        }
    }
}
