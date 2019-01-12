// ===============================
// AUTHOR     : Guillaume Vachon Bureau
// CREATE DATE     : 2018-08-29
//==================================

namespace BehaviourTree
{
    public abstract class TaskDecorator : Task
    {
        protected Task _decoratedTask;

        public TaskDecorator(BlackBoard blackBoard) : base(blackBoard) { }

        public TaskDecorator(Task decoratedTask, BlackBoard blackBoard) : base(blackBoard)
        {
            _decoratedTask = decoratedTask;
        }

        public override void ResetTask()
        {
            base.ResetTask();
            _decoratedTask.ResetTask();
        }

        /// <summary>
        /// Return the decorator Node
        /// </summary>
        /// <param name="decoratedTask"></param>
        /// <returns></returns>
        public TaskDecorator SetDecoratedTask(Task decoratedTask)
        {
            _decoratedTask = decoratedTask;
            return this;
        }
    }
}

