namespace FSM.Scripts
{
    public class FsmStateAttack : FsmStateEnemy
    {
        public FsmStateAttack(Fsm fsm, Enemy enemy) : base(fsm, enemy)
        {
        }


        public override void Enter()
        {
            Enemy.Attack();
        }


        public override void Update()
        {

            if (Enemy.HasReachedDestination())
            {
                _fsm.SetState<FsmStateIdle>();
            }
            else
            {
                Enemy.Attack();
            }
        }
    }
}