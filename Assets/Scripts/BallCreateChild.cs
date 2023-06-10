using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCreateChild : MonoBehaviour
{
    public void OnAttach()
    {
        GameObject child = new GameObject();
        child.transform.SetParent(transform);
    }
}
