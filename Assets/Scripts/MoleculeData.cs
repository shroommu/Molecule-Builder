using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleculeData : MonoBehaviour
{
    public BallData anchor;

    public HashSet<MoleculeData> parts = new HashSet<MoleculeData>();

    // when new part is connected to the molecule, add it to the parts list
    // and pass the anchor to its MoleculeData
    // anchor is determined by whichever molecule has more parts
    // otherwise it is set according to whichever pre-existing molecule is dropped first

    void Start()
    {
        parts.Add(this);
    }

    public BallData DetermineAnchor(MoleculeData other)
    {
        UpdateAllPartsLists();

        if (other.parts.Count > parts.Count)
        {
            anchor = other.anchor;
        }
        else
        {
            other.anchor = anchor;
        }

        UpdateAllAnchors();

        return anchor;
    }

    public void UpdateAllPartsLists()
    {
        foreach (MoleculeData part in parts)
        {
            part.parts.Add(this);
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
