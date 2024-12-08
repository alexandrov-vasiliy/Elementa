namespace FSM.Scripts
{
    public class FsmStateAttack : FsmStateEnemy
    {
        public FsmStateAttack(Fsm fsm, Enemy enemy) : base(fsm, enemy)
        {
        }
        
        public override void Enter()
        {
            Enemy.StartAttack();
        }


        public override void Update()
        {
            Enemy.Agr();
            if (!Enemy.HasReachedDestination())
            {
                Enemy.StopAttack();
                _fsm.SetState<FsmStateHunting>();
            }
        }
    }
}