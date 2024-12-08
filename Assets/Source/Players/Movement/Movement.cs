using UnityEngine;

namespace Players
{
    public class Movement
    {
        private readonly float _speedPerson;
        private readonly float _speedCamera;
        private readonly float _minAngle;
        private readonly float _maxAngle;

        private Vector2 _rotation;
        private Vector3 _normal;

        public Movement(float speedPerson, float speedCamera, float minAngle, float maxAngle, Vector2 rotation)
        {
            _speedPerson = speedPerson;
            _speedCamera = speedCamera;
            _minAngle = minAngle;
            _maxAngle = maxAngle;
            _rotation = rotation;
        }
    
        public Vector3 CalculateVectorAlongPlane(Vector3 direction)
        {
            //Vector3 vectorAlongPlane = GetVectorPlane(direction);
            return direction * _speedPerson;
        }
    
        public Vector3 CalculateRotationVector(float axisX, float axisY)
        {
            _rotation.x -= axisY * _speedCamera * Time.deltaTime;
            _rotation.y += axisX * _speedCamera * Time.deltaTime;

            _rotation.x = Mathf.Clamp(_rotation.x, _minAngle, _maxAngle);

            return new Vector3(_rotation.x, _rotation.y, 0);
        }

        public void SetNormal(Vector3 normal)
        {
            _normal = normal;
        }
    
        private Vector3 GetVectorPlane(Vector3 direction)
        {
            return direction - Vector3.Dot(direction, _normal) * _normal;
        }
    
    }
}
