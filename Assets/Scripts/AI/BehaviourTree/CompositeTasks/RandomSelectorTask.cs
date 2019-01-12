// ===============================
// AUTHOR     : Guillaume Vachon Bureau
// CREATE DATE     : 2018-08-29
//==================================

using System.Collections.Generic;
namespace BehaviourTree
{
    /// <summary>
    /// like SelectorTask, but randomize the task order
    /// </summary>
    public class RandomSelectorTask : SelectorTask
    {
        public RandomSelectorTask(List<Task> tasks, BlackBoard blackBoard) : base(tasks, blackBoard)
        {
        }

        protected override bool CheckConditions()
        {
            return base.CheckConditions();
        }

        protected override TaskStatus BodyLogic()
        {
            return base.BodyLogic();
        }

        protected override void EndLogic()
        {
            base.EndLogic();
        }

        public override void ResetTask()
        {
            base.ResetTask();
        }

        protected override void StartLogic()
        {
            base.StartLogic();
            IListExtensions.Shuffle(_tasks);
        }
    }
}

