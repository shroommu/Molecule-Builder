using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetSocketTransform : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<XRSocketInteractor>())
        {
            float offsetY = -(
                transform.position.y - other.gameObject.GetComponent<SphereCollider>().radius
            );
            Vector3 temp = new Vector3(0, offsetY, 0);
            other.gameObject.GetComponent<XRSocketInteractor>().attachTransform.localPosition =
                temp;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<XRSocketInteractor>())
        {
            Transform parent = transform.parent;
            Vector3 temp = new Vector3(
                parent.eulerAngles.x,
                parent.eulerAngles.y,
                parent.eulerAngles.z
            );
            other.gameObject.GetComponent<XRSocketInteractor>().attachTransform.localRotation =
                Quaternion.Euler(temp);
        }
    }
}
