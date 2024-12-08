using _Elementa.Player;

namespace FSM.Scripts
{
    public class FsmStateHunting : FsmStateEnemy
    {
        private PlayerBase _player;
        public FsmStateHunting(Fsm fsm, Enemy enemy, PlayerBase player) : base(fsm, enemy)
        {
            _player = player;
        }


        public override void Enter()
        {
            Enemy.SetTarget(_player.transform);
        }


        public override void Update()
        {
            
            if (Enemy.HasReachedDestination())
            {
                _fsm.SetState<FsmStateAttack>();
            }
            else
            {
                Enemy.SetTarget(_player.transform);

            }
        }
    }
}