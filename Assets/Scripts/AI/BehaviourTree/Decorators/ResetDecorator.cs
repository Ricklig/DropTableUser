// ===============================
// AUTHOR     : Guillaume Vachon Bureau
// CREATE DATE     : 2018-08-29
//==================================
namespace BehaviourTree
{
    /// <summary>
    /// Call ResetTask on the node it decorate if the task is not currently running
    /// </summary>
    public class ResetDecorator : TaskDecorator
    {
        public ResetDecorator(Task decoratedTask, BlackBoard blackBoard) : base(decoratedTask, blackBoard)
        {
        }

        protected override TaskStatus BodyLogic()
        {
            if (_decoratedTask.GetTaskStatus() != TaskStatus.Running)
            {
                _decoratedTask.ResetTask();
            }
            _taskStatus = _decoratedTask.RunTask();
            return _taskStatus;
        }
    }
}

