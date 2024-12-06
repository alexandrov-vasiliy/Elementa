namespace FSM.Scripts
{
    public class FsmStateEnemy : FsmState
    {
        protected readonly Enemy Enemy;

        public FsmStateEnemy(Fsm _fsm, Enemy _enemy) : base(_fsm)
        {
            Enemy = _enemy;
        }
    }
}