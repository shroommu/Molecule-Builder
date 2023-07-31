using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Ball_AttachTransformData : ABS_AttachTransformData
{
    public int socketIndex;

    public Vector3 crossProduct;
    public float rotationAngle;

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
                transform.parent.gameObject.GetComponent<BallData>().activeSocketIndex =
                    socketIndex;
                canBeAttached = true;
                attachedObj = other.gameObject;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (attachedObj.GetComponent<XRSocketInteractor>().enabled == true)
        {
            crossProduct = transform.parent.GetComponent<BallData>().CalculateCrossProduct();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Stick_AttachTransformData>() && !isAttached)
        {
            transform.parent.gameObject.GetComponent<BallData>().activeSocketIndex = -1;
            gameObject.GetComponent<XRSocketInteractor>().enabled = true;
            canBeAttached = false;
            attachedObj = null;
        }
    }
}
