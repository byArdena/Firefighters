using System;
using UnityEngine;

namespace Players
{
    public class PlayerOverview : MonoBehaviour, IReadable
    {
        private Transform _cameraPoint;
        private Vector3 _rotation;
        private Vector2 _input;

        public event Action<float, float> GoingToCalculateRotation;

        public void Initialize(Transform cameraPoint)
        {
            _cameraPoint = cameraPoint;
        }
        
        public void ReadInput(Vector2 input)
        {
            _input = input;
        }

        public void SetRotation(Vector3 rotation)
        {
            _cameraPoint.eulerAngles = rotation;
        }

        private void Update()
        {
            Rotate();
        }
        
        private void Rotate()
        {
            if (_input == Vector2.zero)
                return;
            
            GoingToCalculateRotation?.Invoke(_input.x, _input.y);
        }
    }
}
