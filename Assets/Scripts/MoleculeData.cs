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
