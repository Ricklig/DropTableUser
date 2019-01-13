using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunctionNavNodes : INavNode {
    int currentIndex = 0;
    private void Start()
    {
        foreach (var node in possibleNextNodes)
        {
            if (node != null)
            {
                nonNullNodes.Add(node);
            }
        }

    }
    public List<INavNode> possibleNextNodes;
    private List<INavNode> nonNullNodes = new List<INavNode>();
    public override INavNode NextNavNode(INavNode previousNode)
    {
        bool isNodeSelected = false;
        while(!isNodeSelected)
        {
            currentIndex = currentIndex % nonNullNodes.Count;
            INavNode selectedNode = nonNullNodes[currentIndex++];
            if (selectedNode != previousNode && nonNullNodes.Count != 1)
            {
                isNodeSelected = true;
                return selectedNode;
            }
        }
        return null;
    }
}
