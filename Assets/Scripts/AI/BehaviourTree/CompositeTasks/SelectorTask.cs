// ===============================
// AUTHOR     : Guillaume Vachon Bureau
// CREATE DATE     : 2018-08-29
//==================================

using System.Collections.Generic;
namespace BehaviourTree
{
    /// <summary>
    /// Return failure if all class return failure, else it return sucess or running
    /// </summary>
    public class SelectorTask : CompositeTask
    {
        public SelectorTask(List<Task> tasks, BlackBoard blackBoard) : base(tasks, blackBoard)
        {
        }

        protected override TaskStatus BodyLogic()
        {
            for (int i = _currentTaskIndex; i < _tasks.Count; ++i)
            {
                _currentTaskIndex = i;
                TaskStatus currentTaskStatus = _tasks[i].RunTask();
                // one task succeded so we return sucess
                if (currentTaskStatus == TaskStatus.Sucess)
                {
                    _taskStatus = TaskStatus.Sucess;
                    return _taskStatus;
                }
                else if (currentTaskStatus == TaskStatus.Running)
                {
                    _taskStatus = TaskStatus.Running;
                    return _taskStatus;
                }
            }
            // if we are here, all failed so we return failure
            _taskStatus = TaskStatus.Failure;
            return _taskStatus;
        }

        protected override void EndLogic()
        {
            
        }

        protected override void StartLogic()
        {
            
        }
    }
}