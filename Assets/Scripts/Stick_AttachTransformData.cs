using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Stick_AttachTransformData : ABS_AttachTransformData
{
    void OnTriggerEnter(Collider other)
    {
        if (
            other.gameObject.GetComponent<Ball_AttachTransformData>()
            && !isAttached
            && !other.gameObject.GetComponent<Ball_AttachTransformData>().isAttached
        )
        {
            canBeAttached = true;
            attachedObj = other.gameObject;

            Transform ballSocketTransform = other.transform;
            Transform stickSocketAttachTransform = gameObject
                .GetComponent<XRSocketInteractor>()
                .attachTransform;

            Vector3 newRotation = new Vector3(
                ballSocketTransform.rotation.x,
                ballSocketTransform.rotation.y,
                180
            );

            stickSocketAttachTransform.localRotation = Quaternion.Euler(newRotation);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Ball_AttachTransformData>() && !isAttached)
        {
            canBeAttached = false;
            attachedObj = null;
            gameObject.GetComponent<XRSocketInteractor>().attachTransform.rotation =
                Quaternion.identity;
        }
    }
}
