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
            transform.parent.gameObject.GetComponent<BallData>().SetActiveSocketData(socketIndex);

            if (!other.gameObject.GetComponent<Stick_AttachTransformData>().isAttached)
            {
                canBeAttached = true;
                attachedObj = other.gameObject;

                if (
                    other.transform.parent.gameObject
                        .GetComponent<XRGrabInteractable>()
                        .interactionLayers == InteractionLayerMask.GetMask("Attached Stick")
                )
                {
                    gameObject.GetComponent<XRSocketInteractor>().enabled = false;
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (
            !isAttached
            && attachedObj
            && attachedObj?.GetComponent<XRSocketInteractor>()?.enabled == true
        )
        {
            crossProduct = transform.parent.GetComponent<BallData>().CalculateCrossProduct();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Stick_AttachTransformData>() && !isAttached)
        {
            transform.parent.gameObject.GetComponent<BallData>().ResetActiveSocketData();
            gameObject.GetComponent<XRSocketInteractor>().enabled = true;
            canBeAttached = false;
            attachedObj = null;
        }
    }
}
