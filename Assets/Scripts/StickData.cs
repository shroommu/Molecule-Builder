using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Threading.Tasks;

public class StickData : MonoBehaviour
{
    public GameObject stickSocketTop;
    public GameObject stickSocketBottom;
    public float waitSeconds = 0.5f;

    public void SetChildAttachedStates()
    {
        if (
            stickSocketTop.GetComponent<Stick_AttachTransformData>().canBeAttached
            && !stickSocketTop.GetComponent<Stick_AttachTransformData>().isAttached
        )
        {
            stickSocketTop.GetComponent<Stick_AttachTransformData>().isAttached = true;
        }
        if (
            stickSocketBottom.GetComponent<Stick_AttachTransformData>().canBeAttached
            && !stickSocketBottom.GetComponent<Stick_AttachTransformData>().isAttached
        )
        {
            stickSocketBottom.GetComponent<Stick_AttachTransformData>().isAttached = true;
        }
    }

    public async void OnDrop()
    {
        SetChildAttachedStates();
        await Task.Delay((int)(waitSeconds * 1000));

        Stick_AttachTransformData[] attachTransformPoints =
        {
            stickSocketTop.GetComponent<Stick_AttachTransformData>(),
            stickSocketBottom.GetComponent<Stick_AttachTransformData>()
        };

        foreach (Stick_AttachTransformData attachTransformPoint in attachTransformPoints)
        {
            if (attachTransformPoint.isAttached)
            {
                gameObject.GetComponent<XRGrabInteractable>().attachTransform = transform;
                gameObject.GetComponent<XRGrabInteractable>().interactionLayers =
                    InteractionLayerMask.GetMask("Attached Stick");
                Debug.Log(attachTransformPoint.attachedObj);
                attachTransformPoint.attachedObj.transform.parent.gameObject
                    .GetComponent<BallData>()
                    .OnDrop();
                break;
            }
        }
        AddSocketToStick();
    }

    public void AddSocketToStick()
    {
        if (stickSocketTop.GetComponent<Stick_AttachTransformData>().isAttached)
        {
            stickSocketBottom.GetComponent<XRSocketInteractor>().enabled = true;
        }
        if (stickSocketBottom.GetComponent<Stick_AttachTransformData>().isAttached)
        {
            stickSocketTop.GetComponent<XRSocketInteractor>().enabled = true;
        }
    }
}
