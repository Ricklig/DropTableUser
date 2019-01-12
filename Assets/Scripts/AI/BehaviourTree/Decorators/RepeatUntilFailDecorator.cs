// ===============================
// AUTHOR     : Guillaume Vachon Bureau
// CREATE DATE     : 2018-08-29
//==================================

namespace BehaviourTree
{
    public class RepeateUntilFailDecorator : TaskDecorator
    {
        public RepeateUntilFailDecorator(Task decoratedTask, BlackBoard blackBoard) : base(decoratedTask, blackBoard)
        {
        }

        protected override TaskStatus BodyLogic()
        {
            TaskStatus taskResult = TaskStatus.Sucess;
            while (taskResult == TaskStatus.Sucess)
            {
                if (_taskStatus != TaskStatus.Running)
                {
                    _decoratedTask.ResetTask();
                }
                taskResult = _decoratedTask.RunTask();
            }
            _taskStatus = taskResult;
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

