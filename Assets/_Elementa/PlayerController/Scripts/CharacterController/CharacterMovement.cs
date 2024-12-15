using System;
using _Elementa;
using _Elementa.Attack;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    [Header("Character movement stats")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;

    [Header("Gravity handling")]
    private float _currentAttractionCharacter = 0;
    [SerializeField] private float _gravityForce = 20;

    [Header("Character components")]
    private CharacterController _characterController;
    
    public Transform Target {  get; private set; } 
    
    [Inject] private AttackConfig _attackConfig;
    [Inject] private FindEnemy _findEnemy;
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    
    private void Update()
    {
       
         GravityHandling();
    }

    private void LateUpdate()
    {
        FaceNearestEnemy();
    }

    public void MoveCharacter(Vector3 moveDirection)
    {
        moveDirection *= _moveSpeed;
        moveDirection.y = _currentAttractionCharacter;
        _characterController.Move(moveDirection * Time.deltaTime);
    }

    public void RotateCharacter(Vector3 moveDirection) 
    {
        if (_characterController.isGrounded)
        {
            if (Vector3.Angle(transform.forward, moveDirection) > 0)
            {
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, moveDirection, _rotateSpeed, 0);
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
        }
    }

    private void GravityHandling()
    {
        if (!_characterController.isGrounded)
        {
            _currentAttractionCharacter -= _gravityForce * Time.deltaTime;
        }
        else
        {
            _currentAttractionCharacter = 0;
        }
    }
    
    private void FaceNearestEnemy()
    {
        var nearestEnemy = _findEnemy.Nearest(transform.position, _attackConfig.EnemyFindRadius);
        
        if (nearestEnemy != null)
        {
            Target = nearestEnemy;
            
            Vector3 directionToEnemy = (nearestEnemy.position - transform.position).normalized;
            directionToEnemy.y = 0;
            RotateCharacter(directionToEnemy);
        }
    }
}
