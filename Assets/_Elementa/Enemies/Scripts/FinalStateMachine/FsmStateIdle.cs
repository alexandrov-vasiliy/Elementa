using UnityEngine;

namespace FSM.Scripts
{
    public class FsmStateIdle : FsmStateEnemy
    {
        public FsmStateIdle(Fsm _fsm, Enemy _enemy) : base(_fsm, _enemy)
        {
        }

        public override void Update()
        {
            if (Enemy.isActiveAndEnabled)
            {
                _fsm.SetState<FsmStateAttack>();
            }
        }
    }
}