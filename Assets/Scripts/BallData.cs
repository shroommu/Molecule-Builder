using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BallData : MonoBehaviour
{
    public bool isAnchor = false;

    public List<GameObject> sockets;

    public Ball_AttachTransformData activeSocketData = null;

    public Vector3 CalculateCrossProduct()
    {
        Vector3 activeSocketUpVector = activeSocketData.transform.up;
        Vector3 stickSocketVector = activeSocketData.attachedObj.transform.up;
        return Vector3.Cross(activeSocketUpVector, stickSocketVector);
    }

    public void SetActiveSocketData(int socketIndex)
    {
        activeSocketData = sockets[socketIndex].GetComponent<Ball_AttachTransformData>();
    }

    public void ResetActiveSocketData()
    {
        activeSocketData = null;
    }

    public void OnDrop()
    {
        if (activeSocketData)
        {
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
