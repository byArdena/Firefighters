using System;
using UnityEngine;

namespace Players
{
    [RequireComponent(typeof(Rigidbody))]

    public class PlayerMovement : MonoBehaviour, IReadable
    {
        private readonly Collider[] _results = new Collider[1];
    
        [SerializeField] private Transform _cameraPoint;
        [SerializeField] private Transform _leg;
        [SerializeField] private LayerMask _ground;
    
        [SerializeField] private float _radius;
        [SerializeField] private float _speedFall;
    
        private Rigidbody _rigidbody;
        private Vector3 _direction;
        private bool _isGrounded;
        private bool _didInitialize;

        public event Action<Vector3> GoingToCalculateDirection;
        public event Action Moved;
        public event Action Stopped;
    
        public void Initialize()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _didInitialize = true;
        }

        public void ReadInput(Vector2 input)
        {
            if (input == Vector2.zero)
            {
                SetDirection(Vector3.zero);
                Stopped?.Invoke();
                return;
            }
            
            GoingToCalculateDirection?.Invoke(_cameraPoint.right * input.x + _cameraPoint.forward * input.y);
        }

        public void SetDirection(Vector3 direction)
        {
            _direction = direction;
        }
    
        private void FixedUpdate()
        {
            if (_didInitialize == false)
                return;
            
            CheckIsGround();
            Move();
        }
    
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_leg.position, _radius);
        }
    
        private void Move()
        {
            if (_isGrounded == false)
            {
                _rigidbody.velocity += Vector3.down * _speedFall;
                return;
            }
            
            _rigidbody.velocity = _direction;
            
            if (_direction == Vector3.zero)
                return;
            
            Moved?.Invoke();
        }
    
        private void CheckIsGround()
        {
            _isGrounded = Physics.OverlapSphereNonAlloc(_leg.position, _radius, _results, _ground) > 0;
        }
    }
}