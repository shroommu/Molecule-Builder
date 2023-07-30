using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Stick_AttachTransformData : ABS_AttachTransformData
{
    void OnTriggerEnter(Collider other)
    {
        if (
            other.gameObject.GetComponent<Ball_AttachTransformData>()
            && !isAttached
            && !other.gameObject.GetComponent<Ball_AttachTransformData>().isAttached
        )
        {
            canBeAttached = true;
            attachedObj = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (
            other.gameObject.GetComponent<Ball_AttachTransformData>()
            && !isAttached
            && !other.gameObject.GetComponent<Ball_AttachTransformData>().isAttached
        )
        {
            canBeAttached = false;
            attachedObj = null;
        }
    }
}
