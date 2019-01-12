// ===============================
// AUTHOR     : Guillaume Vachon Bureau
// CREATE DATE     : 2018-08-29
//==================================

namespace BehaviourTree
{
    abstract public class LeafTask : Task
    {
        public LeafTask(BlackBoard blackBoard) : base(blackBoard)
        {
        }

        public override void ResetTask()
        {
            _taskStatus = TaskStatus.Sucess;
        }
    }

}
