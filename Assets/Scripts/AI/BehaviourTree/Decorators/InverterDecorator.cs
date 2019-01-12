// ===============================
// AUTHOR     : Guillaume Vachon Bureau
// CREATE DATE     : 2018-08-29
//==================================

namespace BehaviourTree
{
    public class InverterDecorator : TaskDecorator
    {
        public InverterDecorator(Task decoratedTask, BlackBoard blackBoard) : base(decoratedTask, blackBoard)
        {
        }

        protected override TaskStatus BodyLogic()
        {
            _taskStatus = _decoratedTask.RunTask();
            if (_taskStatus == TaskStatus.Sucess)
            {
                _taskStatus = TaskStatus.Failure;
            }
            else if (_taskStatus == TaskStatus.Failure)
            {
                _taskStatus = TaskStatus.Sucess;
            }
            return _taskStatus;
        }
    }
}

