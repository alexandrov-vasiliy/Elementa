using _Elementa.Player;
using UnityEngine;

namespace FSM.Scripts
{
    public class FsmStateIdle : FsmStateEnemy
    {
        public FsmStateIdle(Fsm fsm, Enemy enemy) : base(fsm, enemy)
        {
        }

        public override void Update()
        {
            if (Enemy.isActiveAndEnabled)
            {
                Collider[] cols = Physics.OverlapSphere(Enemy.transform.position, Enemy.DetectRadius);

                foreach (var collider in cols)
                {
                    if (collider.TryGetComponent(out PlayerBase playerBase))
                    {
                        _fsm.SetState<FsmStateHunting>();
                    }
                }
            }
        }
    }
}