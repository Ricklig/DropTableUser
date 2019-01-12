// ===============================
// AUTHOR     : Guillaume Vachon Bureau
// CREATE DATE     : 2018-08-29
//==================================

using System.Collections.Generic;
namespace BehaviourTree
{
    /// <summary>
    /// when the task in array 0 return Sucess or fail, we stop the execution of the whole parallel node
    /// task 0 may be whatever, but would work best as a simple task
    /// </summary>
    public class SimpleParallelTask : CompositeTask
    {
        public SimpleParallelTask(List<Task> tasks, BlackBoard blackBoard) : base(tasks, blackBoard)
        {
        }

        protected override TaskStatus BodyLogic()
        {
            //since we execute ALL the node in a parallel, we set the index at 0, we will exit when the first task return true AND ONLY THEN or if we obv run all our child
            for (int i = 0; i < _tasks.Count; ++i)
            {
                TaskStatus currentTaskStatus = _tasks[i].RunTask();
                if (currentTaskStatus == TaskStatus.Sucess && i == 0)
                {
                    _taskStatus = TaskStatus.Sucess;
                    return _taskStatus = TaskStatus.Sucess;
                }
                // one task failed, so as a sequence we must return failure (if a check or something would fail)
                else if (currentTaskStatus == TaskStatus.Failure && i == 0)
                {
                    _taskStatus = TaskStatus.Failure;
                    return _taskStatus;
                }
            }
            // if we are here, all task are still running
            _taskStatus = TaskStatus.Running;
            return _taskStatus;
        }
    }
}
