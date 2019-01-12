// ===============================
// AUTHOR     : Guillaume Vachon Bureau
// CREATE DATE     : 2018-08-29
//==================================

namespace BehaviourTree
{
    public class RepeaterDecorator : TaskDecorator {
        /// <summary>
        /// If _timeToRepeat is set to 0, repeat an infinite number of time
        /// </summary>
        protected int _timeToRepeat;
        private int _timeLeftToRepeat;

        public RepeaterDecorator(int timeToRepeat , Task decoratedTask, BlackBoard blackBoard) : base(decoratedTask, blackBoard)
        {
            _timeToRepeat = timeToRepeat;
            _timeLeftToRepeat = timeToRepeat;
        }

        protected override TaskStatus BodyLogic()
        {
            if (_timeToRepeat == 0)
            {
                while(true)
                {
                    if (_taskStatus != TaskStatus.Running)
                    {
                        _decoratedTask.ResetTask();
                    }
                    _taskStatus = _decoratedTask.RunTask();
                    if (_taskStatus == TaskStatus.Running)
                    {
                        break;
                    }
                }
            }
            else
            {
                while (_timeLeftToRepeat > 0)
                {
                    if (_taskStatus != TaskStatus.Running)
                    {
                        _decoratedTask.ResetTask();
                    }
                    _taskStatus = _decoratedTask.RunTask();
                    --_timeLeftToRepeat;
                    if (_taskStatus == TaskStatus.Running)
                    {
                        break;
                    }
                }
            }
            return _taskStatus;

        }

        protected override void EndLogic()
        {
            
        }

        protected override void StartLogic()
        {
           
        }

        public override void ResetTask()
        {
            base.ResetTask();
            _timeLeftToRepeat = _timeToRepeat;
        }
    }
}

