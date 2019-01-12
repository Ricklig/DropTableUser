using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavNode : INavNode  {
    [SerializeField] private INavNode nextNavNode;
    [SerializeField] private INavNode backNavNode;
    public override INavNode NextNavNode(INavNode previousNode)
    {
        if (previousNode != null && (nextNavNode == null || nextNavNode == previousNode))
        {
            return backNavNode;
        }        
        else
        {
            return nextNavNode;
        }
    }
}
