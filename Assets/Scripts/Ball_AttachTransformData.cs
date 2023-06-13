using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Ball_AttachTransformData : ABS_AttachTransformData
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Stick_AttachTransformData>())
        {
            if (
                !other.gameObject.GetComponent<Stick_AttachTransformData>().isAttached
                && other.transform.parent.gameObject
                    .GetComponent<XRGrabInteractable>()
                    .interactionLayers == InteractionLayerMask.GetMask("Attached Object")
                && !other.gameObject.GetComponent<Stick_AttachTransformData>().attachedObj
                    != gameObject
            )
            {
                gameObject.GetComponent<XRSocketInteractor>().enabled = false;
                canBeAttached = true;
                attachedObj = other.gameObject;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Stick_AttachTransformData>())
        {
            gameObject.GetComponent<XRSocketInteractor>().enabled = true;
            canBeAttached = false;
            attachedObj = null;
        }
    }
}
