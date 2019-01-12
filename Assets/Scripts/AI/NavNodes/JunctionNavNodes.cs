using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunctionNavNodes : INavNode {
    public List<INavNode> possibleNextNodes;
    public override INavNode NextNavNode(INavNode previousNode)
    {
        List<INavNode> nextNodeChoices = new List<INavNode>();
        foreach (INavNode navNode in possibleNextNodes)
        {
            if (navNode != previousNode)
                nextNodeChoices.Add(navNode);
        }
        nextNodeChoices.Remove(previousNode);
        int index = Random.Range(0, nextNodeChoices.Count);
        return nextNodeChoices[index];
    }
}
