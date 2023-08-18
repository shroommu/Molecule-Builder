using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleculeData : MonoBehaviour
{
    public BallData anchor;

    public HashSet<MoleculeData> parts = new HashSet<MoleculeData>();

    public List<MoleculeData> partsList;

    // when new part is connected to the molecule, add it to the parts list
    // and pass the anchor to its MoleculeData
    // anchor is determined by whichever molecule has more parts
    // otherwise it is set according to whichever pre-existing molecule is dropped first

    void Start()
    {
        parts.Add(this);
    }

    void OnUpdateHashSet(MoleculeData newPart)
    {
        if (!partsList.Contains(newPart))
        {
            partsList.Add(newPart);
        }
    }

    public BallData DetermineAnchor(MoleculeData other)
    {
        if (other.parts.Count > parts.Count)
        {
            anchor = other.anchor;
        }
        else
        {
            other.anchor = anchor;
        }

        if (other.parts.Count == 1 && this.parts.Count > 1)
        {
            Debug.Log($"1. setting parent of other.transform {other.transform} to {transform}");
            other.transform.SetParent(transform);
        }
        else if (other.parts.Count > 1 && this.parts.Count == 1)
        {
            Debug.Log(
                $"2. setting parent of transform {transform} to other.transform {other.transform}"
            );
            transform.SetParent(other.transform);
        }
        else if (
            this.parts.Count == 1
            && other.parts.Count == 1
            && anchor == gameObject.GetComponent<BallData>()
        )
        {
            Debug.Log(
                $"3. setting parent of other.transform {other.transform} to transform {transform}"
            );
            other.transform.SetParent(transform);
        }
        else if (
            this.parts.Count == 1
            && other.parts.Count == 1
            && anchor != gameObject.GetComponent<BallData>()
        )
        {
            Debug.Log(
                $"4. setting parent of transform {transform} to other.transform {other.transform}"
            );
            transform.SetParent(other.transform);
        }

        foreach (MoleculeData part in other.parts)
        {
            parts.Add(part);
            OnUpdateHashSet(part);
        }

        UpdateAllPartsLists();
        UpdateAllAnchors();

        return anchor;
    }

    public void UpdateAllPartsLists()
    {
        foreach (MoleculeData part in parts)
        {
            part.parts.Add(this);
            part.OnUpdateHashSet(this);
        }
    }

    public void UpdateAllAnchors()
    {
        foreach (MoleculeData part in parts)
        {
            part.anchor = anchor;
        }
    }
}
