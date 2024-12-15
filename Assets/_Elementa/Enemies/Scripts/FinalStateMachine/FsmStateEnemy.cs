namespace FSM.Scripts
{
    public class FsmStateEnemy : FsmState
    {
        protected readonly Enemy Enemy;

        public FsmStateEnemy(Fsm fsm, Enemy enemy) : base(fsm)
        {
            Enemy = enemy;
        }
    }
}