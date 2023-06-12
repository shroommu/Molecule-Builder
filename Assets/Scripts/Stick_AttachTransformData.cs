using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Stick_AttachTransformData : ABS_AttachTransformData
{
    void OnTriggerEnter(Collider other)
    {
        if (
            other.gameObject.GetComponent<XRSocketInteractor>()
            && other.transform.parent.gameObject.GetComponent<BallData>()
        )
        {
            transform.parent.gameObject.GetComponent<XRGrabInteractable>().attachTransform =
                transform;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (
            other.gameObject.GetComponent<XRSocketInteractor>()
            && other.transform.parent.gameObject.GetComponent<BallData>()
        )
        {
            isInSocketTrigger = true;

            Transform parent = transform.parent;
            Vector3 temp = new Vector3(
                -parent.eulerAngles.x,
                -parent.eulerAngles.y,
                -parent.eulerAngles.z
            );
            transform.localRotation = Quaternion.Euler(temp);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (
            other.gameObject.GetComponent<XRSocketInteractor>()
            && other.transform.parent.gameObject.GetComponent<BallData>()
        )
        {
            isInSocketTrigger = false;
        }
    }
}
