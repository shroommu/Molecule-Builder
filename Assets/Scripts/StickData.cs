using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Threading.Tasks;

public class StickData : MonoBehaviour
{
    public List<Stick_AttachTransformData> stickSocketData;

    private List<XRSocketInteractor> stickSockets = new List<XRSocketInteractor>();
    public float waitSeconds = 0.5f;

    void Start()
    {
        for (int i = 0; i < stickSocketData.Count; i++)
        {
            stickSockets.Add(stickSocketData[i].gameObject.GetComponent<XRSocketInteractor>());
        }
    }

    public void SetChildAttachedStates()
    {
        for (int i = 0; i < stickSocketData.Count; i++)
        {
            if (stickSocketData[i].attachedObj && !stickSocketData[i].isAttached)
            {
                stickSocketData[i].isAttached = true;
            }
        }
    }

    public async void OnDrop()
    {
        await Task.Delay((int)(waitSeconds * 1000));

        for (int i = 0; i < stickSocketData.Count; i++)
        {
            if (!stickSocketData[i].isAttached && stickSocketData[i].attachedObj)
            {
                gameObject.GetComponent<XRGrabInteractable>().interactionLayers =
                    InteractionLayerMask.GetMask("Attached Stick");
                stickSocketData[i].attachedObj.transform.parent.gameObject
                    .GetComponent<BallData>()
                    .OnDrop();
                SetChildAttachedStates();
            }

            if (stickSockets[i].enabled == true && stickSocketData[i].attachedObj)
            {
                stickSockets[i].interactionLayers = InteractionLayerMask.GetMask("Attached Ball");

                Ball_AttachTransformData attachedBallSocket = stickSocketData[
                    i
                ].attachedObj.GetComponent<Ball_AttachTransformData>();

                stickSockets[i].attachTransform.RotateAround(
                    stickSockets[i].attachTransform.position,
                    attachedBallSocket.crossProduct,
                    attachedBallSocket.rotationAngle
                );

                if (attachedBallSocket.socketIndex == 0)
                {
                    Vector3 newRot = stickSockets[i].attachTransform.rotation.eulerAngles;
                    newRot.y = 0;
                    stickSockets[i].attachTransform.rotation = Quaternion.Euler(newRot);
                }
            }
        }

        AddSocketToStick();
    }

    public void AddSocketToStick()
    {
        if (stickSocketData[0].isAttached && !stickSocketData[1].isAttached)
        {
            stickSockets[1].enabled = true;
        }
        if (stickSocketData[1].isAttached && !stickSocketData[0].isAttached)
        {
            stickSockets[0].enabled = true;
        }
    }
}
