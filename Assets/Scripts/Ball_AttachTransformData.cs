using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Ball_AttachTransformData : ABS_AttachTransformData
{
    void OnTriggerStay(Collider other)
    {
        if (
            other.gameObject.GetComponent<XRSocketInteractor>()
            && other.transform.parent.gameObject.GetComponent<StickData>()
        )
        {
            isInSocketTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (
            other.gameObject.GetComponent<XRSocketInteractor>()
            && other.transform.parent.gameObject.GetComponent<StickData>()
        )
        {
            isInSocketTrigger = false;
        }
    }
}
