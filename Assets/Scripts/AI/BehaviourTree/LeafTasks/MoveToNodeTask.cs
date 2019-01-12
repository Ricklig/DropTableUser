using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BehaviourTree
{
    public class MoveToNodeTask : LeafTask
    {
        public MoveToNodeTask(BlackBoard blackBoard) : base(blackBoard)
        {

        }
        protected override TaskStatus BodyLogic()
        {
            throw new System.NotImplementedException();
        }
    }
}
