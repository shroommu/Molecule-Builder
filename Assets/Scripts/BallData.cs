using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BallData : MonoBehaviour
{
    public bool isAnchor = false;

    public List<XRSocketInteractor> sockets;

    private int activeSockets = 1;

    public void OnDrop()
    {
        SetAttachTransformStates();
    }

    public void ActivateNextSocket()
    {
        sockets[activeSockets].gameObject.SetActive(true);
        activeSockets++;
    }

    public void SetAttachTransformStates()
    {
        if (
            sockets[activeSockets - 1].gameObject
                .GetComponent<Ball_AttachTransformData>()
                .attachedObj
        )
        {
            sockets[activeSockets - 1].gameObject
                .GetComponent<Ball_AttachTransformData>()
                .isAttached = true;
            sockets[activeSockets - 1].gameObject
                .GetComponent<Ball_AttachTransformData>()
                .attachedObj.transform.parent.gameObject.GetComponent<StickData>()
                .OnDrop();
            DeactivateCurrentSocket();
            ActivateNextSocket();
        }
    }

    public void DeactivateCurrentSocket()
    {
        sockets[activeSockets - 1].interactionLayers = InteractionLayerMask.GetMask(
            "Attached Object"
        );
        sockets[activeSockets - 1].showInteractableHoverMeshes = false;
    }
}
