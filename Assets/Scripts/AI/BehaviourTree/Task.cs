// ===============================
// AUTHOR     : Guillaume Vachon Bureau
// CREATE DATE     : 2018-08-29
//==================================
namespace BehaviourTree
{
    public abstract class Task
    {
        protected BlackBoard  _blackBoard;
        public Task(BlackBoard blackBoard)
        {
            _blackBoard = blackBoard;
            _taskStatus = TaskStatus.Sucess;
        }
        protected TaskStatus _taskStatus;

        /// <summary>
        /// Node to call to to run the task
        /// </summary>
        /// <returns></returns>
        public TaskStatus RunTask()
        {
            if (!CheckConditions())
            {
                EndLogic();
                _taskStatus = TaskStatus.Failure;
                return TaskStatus.Failure;
            }
            if (_taskStatus != TaskStatus.Running)
            {
                StartLogic();
            }
            _taskStatus = BodyLogic();
            if (_taskStatus != TaskStatus.Running)
            {
                EndLogic();
            }
            return _taskStatus;
        }

        /// <summary>
        /// Run each time the node is ticked
        /// </summary>
        /// <returns></returns>
        abstract protected TaskStatus BodyLogic();

        /// <summary>
        /// Run each time the node is ticked at start before any other function. Make the node return Failure if condition return false. Return true by default, overload to change it
        /// </summary>
        /// <returns></returns>
        virtual protected bool CheckConditions()
        {
            return true;
        }

        /// <summary>
        /// Run only the first time this node is ticked between reset
        /// </summary>
        virtual protected void StartLogic() { }

        /// <summary>
        /// run each time the node return failure or success or if it is reset"/>
        /// </summary>
        virtual protected void EndLogic() { }

        /// <summary>
        /// when you overload, ALWAYS call the parent version first to make sure EndLogic() is called;
        /// </summary>
        virtual public void ResetTask()
        {
            EndLogic();
            _taskStatus = TaskStatus.Sucess;
        }

        public TaskStatus GetTaskStatus()
        {
            return _taskStatus;
        }
    }
}