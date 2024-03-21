using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshChangeLayer : MonoBehaviour
{
    public void ChangeLayer(string name)
    {
        ChangeLayersRecursively(transform, name);
    }

    public void ChangeLayersRecursively(Transform trans, string name)
    {
        trans.gameObject.layer = LayerMask.NameToLayer(name);
        foreach (Transform child in trans)
        {
            ChangeLayersRecursively(child, name);
        }
    }
}
