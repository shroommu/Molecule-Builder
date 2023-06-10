using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetStickAttachTransform : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<XRSocketInteractor>())
        {
            transform.parent.gameObject.GetComponent<XRGrabInteractable>().attachTransform =
                transform;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<XRSocketInteractor>())
        {
            Transform parent = transform.parent;
            Vector3 temp = new Vector3(
                -parent.eulerAngles.x,
                -parent.eulerAngles.y,
                -parent.eulerAngles.z
            );
            transform.localRotation = Quaternion.Euler(temp);
        }
    }
}
