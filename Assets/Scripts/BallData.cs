using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BallData : MonoBehaviour
{
    public bool isAnchor = false;

    public List<XRSocketInteractor> sockets;

    private int activeSockets = 1;

    public void ActivateNextSocket()
    {
        sockets[activeSockets].gameObject.SetActive(true);
        activeSockets++;
    }

    public void SetAttachTransformStates()
    {
        if (
            sockets[activeSockets].gameObject
                .GetComponent<Ball_AttachTransformData>()
                .isInSocketTrigger
        )
        {
            sockets[activeSockets].gameObject.GetComponent<Ball_AttachTransformData>().isAttached =
                true;
            DeactivateCurrentSocket();
            ActivateNextSocket();
        }
    }

    public void DeactivateCurrentSocket()
    {
        sockets[activeSockets].interactionLayers = InteractionLayerMask.GetMask("Attached Object");
        sockets[activeSockets].showInteractableHoverMeshes = false;
    }
}
