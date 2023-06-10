using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.Profiling;
using UnityEngine.XR.Interaction.Toolkit.Transformers;
using UnityEngine.XR.Interaction.Toolkit.Utilities;
using UnityEngine.XR.Interaction.Toolkit.Utilities.Pooling;

namespace UnityEngine.XR.Interaction.Toolkit
{
    public partial class XRGrabInteractableExtended : XRGrabInteractable
    {
        public UnityEvent OnAttach;

        public override Transform GetAttachTransform(IXRInteractor interactor)
        {
            Transform baseTransform = base.GetAttachTransform(interactor);
            OnAttach.Invoke();
            return baseTransform;
        }
    }
}
