// ===============================
// AUTHOR     : Guillaume Vachon Bureau
// CREATE DATE     : 2018-08-29
//==================================

using System.Collections.Generic;
namespace BehaviourTree
{
    /// <summary>
    /// Parent class for all CompositeTask of a behaviour tree
    /// </summary>
    public abstract class CompositeTask : Task
    {
        protected List<Task> _tasks = new List<Task>();
        protected int _currentTaskIndex = 0;

        public CompositeTask(BlackBoard blackBoard) : base(blackBoard)
        {
        }

        public CompositeTask(List<Task> tasks, BlackBoard blackBoard) : base(blackBoard)
        {
            _tasks = tasks;
        }

        public override void ResetTask()
        {
            base.ResetTask();
            _currentTaskIndex = 0;
            foreach (var task in _tasks)
            {
                task.ResetTask();
            }
        }

        /// <summary>
        /// Add the task to the task list and then return the current
        /// Used as an alternative method to fill the composite task node instead of the constructor
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public CompositeTask AddTask(Task task)
        {
            _tasks.Add(task);
            return this;
        }
    }
}