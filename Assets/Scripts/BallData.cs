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

                if (activeSocketData.attachedObj.GetComponent<XRSocketInteractor>().enabled == true)
                {
                    // calculate rotation to line up with correct socket
                    // Step 2: Figure out a vector line extension from each socket.
                    Transform activeSocketTransform = activeSocketData.transform;
                    Vector3 worldSpaceActiveSocketUpVector = activeSocketTransform.up; //.normalized;
                    Vector3 worldSpaceStickSocketVector = activeSocketData.attachedObj.transform.up;
                    //.normalized;

                    Debug.Log(
                        gameObject.name
                            + " "
                            + activeSocketData.gameObject.name
                            + " World Space Vector: "
                            + worldSpaceActiveSocketUpVector
                    );
                    Debug.Log(
                        activeSocketData.attachedObj.transform.parent.gameObject.name
                            + " "
                            + activeSocketData.attachedObj.name
                            + " World Space Vector: "
                            + worldSpaceStickSocketVector
                    );

                    //Step 3: Vector3.Angle(socketALine, socketBLine)
                    float angleBetween = Vector3.Angle(
                        worldSpaceActiveSocketUpVector,
                        worldSpaceStickSocketVector
                    );
                    Debug.Log(
                        $"angle between current and desired rotations:  {angleBetween}",
                        transform
                    );
                    //Step 4: Vector3.Cross(socketALine, socketBLine) -> This will find the perpendicular line of those two vectors (if they aren't parallel).
                    Vector3 crossProduct = Vector3.Cross(
                        worldSpaceActiveSocketUpVector,
                        worldSpaceStickSocketVector
                    );
                    Debug.Log("cross-product: " + crossProduct);
                }
                activeSocketData.GetComponent<XRSocketInteractor>().interactionLayers =
                    InteractionLayerMask.GetMask("Attached Stick");
            }
        }
    }
}
