using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StickAttachTransformData : MonoBehaviour
{
    public bool isInSocketTrigger = false;
    public bool isAttached = false;

    public STICK_END_LOCATION location;

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<XRSocketInteractor>())
        {
            isInSocketTrigger = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<XRSocketInteractor>())
        {
            isInSocketTrigger = false;
        }
    }
}
