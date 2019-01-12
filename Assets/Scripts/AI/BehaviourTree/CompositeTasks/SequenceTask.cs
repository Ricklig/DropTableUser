// ===============================
// AUTHOR     : Guillaume Vachon Bureau
// CREATE DATE     : 2018-08-29
//==================================

using System.Collections.Generic;
namespace BehaviourTree
{
    /// <summary>
    /// Run all the task in sequence one after the other. If one task fail, the sequence fail. Succeed when all task return success
    /// </summary>
    public class SequencesTask : CompositeTask
    {
        public SequencesTask(List<Task> tasks, BlackBoard blackBoard) : base(tasks, blackBoard)
        {
        }

        protected override bool CheckConditions()
        {
            return true;
        }
        protected override TaskStatus BodyLogic()
        {
            for (int i = _currentTaskIndex; i < _tasks.Count; ++i)
            {
                _currentTaskIndex = i;
                TaskStatus currentTaskStatus = _tasks[i].RunTask();
                if (currentTaskStatus == TaskStatus.Running)
                {
                    _taskStatus = TaskStatus.Running;
                    return _taskStatus;
                }
                // one task failed, so as a sequence we must return failure
                else if (currentTaskStatus == TaskStatus.Failure)
                {
                    _taskStatus = TaskStatus.Failure;
                    return _taskStatus;
                }
            }
            // if we are here, all task succeded so we  return Sucess
            _taskStatus = TaskStatus.Sucess;
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