using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerCharacter : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private Vector2 _movementInput = Vector2.zero;
 
        [SerializeField] private float moveSpeed = 1.0f;
    
        [SerializeField] private float collisionOffset = 0.05f;
        [SerializeField] private ContactFilter2D collisionSettings;
        private readonly List<RaycastHit2D> _collisionResults = new List<RaycastHit2D>();

        private static readonly int DirectionX = Animator.StringToHash("DirectionX");
        private static readonly int DirectionY = Animator.StringToHash("DirectionY");

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        private void FixedUpdate()
        {
            if (_movementInput == Vector2.zero)
            {
                SetDirectionValues(_movementInput);
                return;
            }
            
            SetDirectionValues(_movementInput);

            _spriteRenderer.flipX = _movementInput.x < 0;

            bool canMove = TryMove(_movementInput);
            if (canMove) return;
            
            canMove = TryMove(new Vector2(_movementInput.x, 0));
            if (canMove) return;
            
            TryMove(new Vector2(0, _movementInput.y));
        }

        private void SetDirectionValues(Vector2 movementVector)
        {
            _animator.SetFloat(DirectionX, movementVector.x);
            _animator.SetFloat(DirectionY, movementVector.y);
        }

        private bool TryMove(Vector2 direction)
        {
            float raycastLength = moveSpeed * Time.fixedDeltaTime + collisionOffset;
            int count = _rb.Cast(direction, collisionSettings, _collisionResults, raycastLength);

            if (count != 0)
            {
                return false;
            }

            _rb.MovePosition(_rb.position + (direction * (moveSpeed * Time.fixedDeltaTime)));
            return true;
        }

        private void OnMove(InputValue movementValue)
        {
            _movementInput = movementValue.Get<Vector2>();
        }
    }
}