using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Threading.Tasks;

public class StickData : MonoBehaviour
{
    public Stick_AttachTransformData attachTransformTop;
    public Stick_AttachTransformData attachTransformBottom;

    public XRSocketInteractor stickSocketTop;
    public XRSocketInteractor stickSocketBottom;
    public float waitSeconds = 0.5f;

    public void SetChildAttachedStates()
    {
        if (attachTransformTop.isInSocketTrigger && !attachTransformTop.isAttached)
        {
            attachTransformTop.isAttached = true;
            stickSocketTop.showInteractableHoverMeshes = false;
        }
        if (attachTransformBottom.isInSocketTrigger && !attachTransformBottom.isAttached)
        {
            attachTransformBottom.isAttached = true;
            stickSocketTop.showInteractableHoverMeshes = false;
        }
    }

    public async void OnDrop()
    {
        SetChildAttachedStates();
        await Task.Delay((int)(waitSeconds * 1000));

        Stick_AttachTransformData[] attachTransformPoints =
        {
            attachTransformTop,
            attachTransformBottom
        };

        foreach (Stick_AttachTransformData attachTransformPoint in attachTransformPoints)
        {
            if (attachTransformPoint.isAttached)
            {
                Debug.Log("setting stick ungrabbable");
                gameObject.GetComponent<XRGrabInteractable>().attachTransform = transform;
                gameObject.GetComponent<XRGrabInteractable>().interactionLayers =
                    InteractionLayerMask.GetMask("Attached Object");
                break;
            }
        }
        AddSocketToStick();
    }

    public void AddSocketToStick()
    {
        if (attachTransformTop.isAttached)
        {
            stickSocketBottom.gameObject.SetActive(true);
        }
        else if (attachTransformBottom.isAttached)
        {
            stickSocketTop.gameObject.SetActive(true);
        }
    }
}
