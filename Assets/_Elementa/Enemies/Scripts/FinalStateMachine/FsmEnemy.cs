using UnityEngine;


public class FsmEnemy : MonoBehaviour
{
    private Fsm _fsm;

    private void Start()
    {
        _fsm = new Fsm();
        //_fsm.AddState(new FsmStateIdle(_fsm /*cюда можно передавать и другие вещи*/))
        //_fsm.SetState<FsmStateIdle>();
    }

    private void Update()
    {
        _fsm.Update();
    }
}