using UnityEngine;
using Zenject;

namespace _Elementa.PlayerController.Scripts.CharacterController
{
    public class CharacterInputEvent: ITickable
    {
        private CharacterMovement _characterMovement;
       
        
        public CharacterInputEvent(CharacterMovement characterMovement)
        {
            _characterMovement = characterMovement;
        }

        public void Tick()
        {
            if (!Application.isMobilePlatform)
            {
                _characterMovement.MoveCharacter(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
                _characterMovement.RotateCharacter(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
            }
           
        }
    }
}