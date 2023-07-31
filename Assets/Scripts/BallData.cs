using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BallData : MonoBehaviour
{
    public bool isAnchor = false;

    public List<GameObject> sockets;

    [HideInInspector]
    public int activeSocketIndex = -1;

    private Ball_AttachTransformData activeSocketData = null;

    public Vector3 CalculateCrossProduct()
    {
        Transform activeSocketTransform = activeSocketData.transform;
        Vector3 worldSpaceActiveSocketUpVector = activeSocketTransform.up;
        Vector3 worldSpaceStickSocketVector = activeSocketData.attachedObj.transform.up;
        float angleBetween = Vector3.Angle(
            worldSpaceActiveSocketUpVector,
            worldSpaceStickSocketVector
        );
        Vector3 crossProduct = Vector3.Cross(
            worldSpaceActiveSocketUpVector,
            worldSpaceStickSocketVector
        );
        return crossProduct;
    }

    public void OnDrop()
    {
        if (activeSocketIndex > -1)
        {
            activeSocketData = sockets[activeSocketIndex].GetComponent<Ball_AttachTransformData>();

            if (activeSocketData.attachedObj && !activeSocketData.isAttached)
            {
                activeSocketData.isAttached = true;
                activeSocketData.attachedObj.transform.parent.gameObject
                    .GetComponent<StickData>()
                    .OnDrop();

                gameObject.GetComponent<XRGrabInteractable>().interactionLayers =
                    InteractionLayerMask.GetMask("Attached Ball");

                activeSocketData.GetComponent<XRSocketInteractor>().interactionLayers =
                    InteractionLayerMask.GetMask("Attached Stick");
            }
        }
    }
}
