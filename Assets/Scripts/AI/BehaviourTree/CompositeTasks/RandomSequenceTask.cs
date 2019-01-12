// ===============================
// AUTHOR     : Guillaume Vachon Bureau
// CREATE DATE     : 2018-08-29
//==================================

using System.Collections.Generic;
namespace BehaviourTree
{
    /// <summary>
    /// Like sequencetask, but randomise the task order
    /// </summary>
    public class RandomSequenceTask : SequencesTask
    {
        public RandomSequenceTask(List<Task> tasks, BlackBoard blackBoard) : base(tasks, blackBoard)
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
            IListExtensions.Shuffle(_tasks);
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}