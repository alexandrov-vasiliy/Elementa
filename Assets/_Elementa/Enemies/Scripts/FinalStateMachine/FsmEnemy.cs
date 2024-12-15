using _Elementa.Player;
using FSM.Scripts;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Enemy))]
public class FsmEnemy : MonoBehaviour
{
    private Fsm _fsm;
    private Enemy _enemy;
    [Inject] private PlayerBase _player;
    

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _fsm = new Fsm();
        _fsm.AddState(new FsmStateHunting(_fsm, _enemy, _player));
        _fsm.AddState(new FsmStateIdle(_fsm, _enemy));
        _fsm.AddState(new FsmStateAttack(_fsm, _enemy));
        _fsm.AddState(new FsmStateDead(_fsm, _enemy));
    }

    private void OnEnable()
    {
        _fsm.SetState<FsmStateIdle>();
        _enemy.OnDead += HandleDead;
    }
    
    private void OnDisable()
    {
        _fsm.SetState<FsmStateIdle>();
        _enemy.OnDead -= HandleDead;

    }

    private void Update()
    {
        _fsm.Update();
    }
    
    private void HandleDead()
    {
        _fsm.SetState<FsmStateDead>();
    }




   
}