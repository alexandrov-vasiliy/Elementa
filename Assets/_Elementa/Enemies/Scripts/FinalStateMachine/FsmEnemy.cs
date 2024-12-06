using FSM.Scripts;
using UnityEngine;

public class FsmEnemy : MonoBehaviour
{
    private Fsm _fsm;
    private Enemy _enemy;


    private void Awake()
    {
        _fsm = new Fsm();
        _fsm.AddState(new FsmStateAttack(_fsm, gameObject.GetComponentInChildren<Enemy>()));
        _fsm.AddState(new FsmStateIdle(_fsm, gameObject.GetComponentInChildren<Enemy>()));
    }

    private void OnEnable()
    {
        _fsm.SetState<FsmStateIdle>();
    }


    private void Update()
    {
        _fsm.Update();
    }

    private void OnDisable()
    {
        _fsm.SetState<FsmStateIdle>();
    }
}