using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class INavNode : MonoBehaviour
{
    public virtual INavNode NextNavNode(INavNode previousNode)
    {
        return null;
    }

}

