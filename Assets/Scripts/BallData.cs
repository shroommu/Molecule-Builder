using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BallData : MonoBehaviour
{
    public bool isAnchor = false;

    public List<XRSocketInteractor> sockets;

    [HideInInspector]
    public int activeSocket = -1;

    public void OnDrop()
    {
        SetAttachTransformStates();
    }

    public void SetAttachTransformStates()
    {
        if (
            activeSocket > -1
            && sockets[activeSocket].gameObject.GetComponent<Ball_AttachTransformData>().attachedObj
            && !sockets[activeSocket].gameObject.GetComponent<Ball_AttachTransformData>().isAttached
        )
        {
            sockets[activeSocket].gameObject.GetComponent<Ball_AttachTransformData>().isAttached =
                true;
            sockets[activeSocket].gameObject
                .GetComponent<Ball_AttachTransformData>()
                .attachedObj.transform.parent.gameObject.GetComponent<StickData>()
                .OnDrop();
            DeactivateCurrentSocket();
        }
    }

    public void DeactivateCurrentSocket()
    {
        sockets[activeSocket].interactionLayers = InteractionLayerMask.GetMask("Attached Stick");
    }
}
