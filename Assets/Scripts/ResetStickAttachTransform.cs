using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Threading.Tasks;

public class ResetStickAttachTransform : MonoBehaviour
{
    public List<StickAttachTransformData> attachTransformPoints;

    public XRSocketInteractor stickSocketTop;
    public XRSocketInteractor stickSocketBottom;
    public float waitSeconds = 0.5f;

    public void SetAttachTransformStates()
    {
        foreach (StickAttachTransformData attachTransformPoint in attachTransformPoints)
        {
            if (attachTransformPoint.isInSocketTrigger)
            {
                attachTransformPoint.isAttached = true;
            }
        }
    }

    public async void ResetAttachTransform()
    {
        SetAttachTransformStates();
        await Task.Delay((int)(waitSeconds * 1000));
        foreach (StickAttachTransformData attachTransformPoint in attachTransformPoints)
        {
            if (attachTransformPoint.isAttached)
            {
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
        foreach (StickAttachTransformData attachTransformPoint in attachTransformPoints)
        {
            if (
                attachTransformPoint.isAttached
                && attachTransformPoint.location == STICK_END_LOCATION.TOP
            )
            {
                Debug.Log("activating bottom socket");
                stickSocketBottom.gameObject.SetActive(true);
            }
            else if (
                attachTransformPoint.isAttached
                && attachTransformPoint.location == STICK_END_LOCATION.BOTTOM
            )
            {
                Debug.Log("activating top socket");
                stickSocketTop.gameObject.SetActive(true);
            }
        }
    }
}
