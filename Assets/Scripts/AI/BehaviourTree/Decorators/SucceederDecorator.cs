// ===============================
// AUTHOR     : Guillaume Vachon Bureau
// CREATE DATE     : 2018-08-29
//==================================
namespace BehaviourTree
{
    /// <summary>
    /// Always return true, no matter what the child TaskStatus return
    /// </summary>
    public class SucceederDecorator : TaskDecorator
    {
        public SucceederDecorator(Task decoratedTask, BlackBoard blackBoard) : base(decoratedTask, blackBoard)
        {
        }

        protected override TaskStatus BodyLogic()
        {
            _taskStatus = _decoratedTask.RunTask();
            if (_taskStatus != TaskStatus.Running)
            {
                _taskStatus = TaskStatus.Sucess;
            }
            return _taskStatus;
        }
    }
}

