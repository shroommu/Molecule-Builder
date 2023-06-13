using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public abstract class ABS_AttachTransformData : MonoBehaviour
{
    public bool canBeAttached = false;
    public bool isAttached = false;

    public GameObject attachedObj;
}
