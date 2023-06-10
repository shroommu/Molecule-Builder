using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class ApplyAttachTransform : MonoBehaviour
{
    private Transform attachedObj;

    public GameObject childObjectPrefab;

    void OnTriggerEnter(Collider other)
    {
        attachedObj = other.GetComponent<Transform>();
    }

    void OnTriggerExit()
    {
        attachedObj = null;
    }

    public void OnAttach()
    {
        GameObject child = GameObject.Instantiate(childObjectPrefab);
        child.transform.SetParent(attachedObj);
    }
}
