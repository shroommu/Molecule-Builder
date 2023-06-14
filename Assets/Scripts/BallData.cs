using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BallData : MonoBehaviour
{
    public bool isAnchor = false;

    public List<XRSocketInteractor> sockets;

    private int activeSocket = 0;

    public void OnDrop()
    {
        SetAttachTransformStates();
    }

    public void ActivateNextSocket()
    {
        activeSocket++;
        sockets[activeSocket].gameObject.SetActive(true);
    }

    public void SetAttachTransformStates()
    {
        Debug.Log(
            sockets[activeSocket].gameObject.GetComponent<Ball_AttachTransformData>().attachedObj
        );

        if (sockets[activeSocket].gameObject.GetComponent<Ball_AttachTransformData>().attachedObj)
        {
            sockets[activeSocket].gameObject.GetComponent<Ball_AttachTransformData>().isAttached =
                true;
            sockets[activeSocket].gameObject
                .GetComponent<Ball_AttachTransformData>()
                .attachedObj.transform.parent.gameObject.GetComponent<StickData>()
                .OnDrop();
            DeactivateCurrentSocket();
            ActivateNextSocket();
        }
    }

    public void DeactivateCurrentSocket()
    {
        sockets[activeSocket].interactionLayers = InteractionLayerMask.GetMask("Attached Stick");
        sockets[activeSocket].showInteractableHoverMeshes = false;
    }
}
