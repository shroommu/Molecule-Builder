using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BallData : MonoBehaviour
{
    public bool isAnchor = true;

    public List<GameObject> sockets;

    public Ball_AttachTransformData activeSocketData = null;

    public MoleculeData moleculeData;

    void Start()
    {
        moleculeData = gameObject.GetComponent<MoleculeData>();
    }

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
        if (activeSocketData && activeSocketData.attachedObj && !activeSocketData.isAttached)
        {
            activeSocketData.isAttached = true;
            activeSocketData.attachedObj.transform.parent.gameObject
                .GetComponent<StickData>()
                .OnDrop();

            isAnchor =
                this
                == moleculeData.DetermineAnchor(
                    activeSocketData.attachedObj.transform.parent.gameObject.GetComponent<MoleculeData>()
                );

            gameObject.GetComponent<XRGrabInteractable>().interactionLayers =
                InteractionLayerMask.GetMask("Attached Ball");

            activeSocketData.GetComponent<XRSocketInteractor>().interactionLayers =
                InteractionLayerMask.GetMask("Attached Stick");
        }
    }
}
