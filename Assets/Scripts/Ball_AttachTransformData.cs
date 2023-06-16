using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Ball_AttachTransformData : ABS_AttachTransformData
{
    public int socketIndex;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Stick_AttachTransformData>())
        {
            if (!other.gameObject.GetComponent<Stick_AttachTransformData>().isAttached)
            {
                if (
                    other.transform.parent.gameObject
                        .GetComponent<XRGrabInteractable>()
                        .interactionLayers == InteractionLayerMask.GetMask("Attached Stick")
                )
                {
                    gameObject.GetComponent<XRSocketInteractor>().enabled = false;
                }
                transform.parent.gameObject.GetComponent<BallData>().activeSocket = socketIndex;
                canBeAttached = true;
                attachedObj = other.gameObject;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Stick_AttachTransformData>() && !isAttached)
        {
            transform.parent.gameObject.GetComponent<BallData>().activeSocket = -1;
            gameObject.GetComponent<XRSocketInteractor>().enabled = true;
            canBeAttached = false;
            attachedObj = null;
        }
    }
}
