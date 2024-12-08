using Sounds;
using UnityEngine;

namespace Players
{
    public class MovementPresenter
    {
        private readonly Movement _model;
        private readonly PlayerMovement _playerMovement;
        private readonly PlayerOverview _playerOverview;
        private readonly NormalCollisionDetector _normalCollisionDetector;
        private readonly EnvironmentSound _sound;
    
        public MovementPresenter(Movement model, PlayerMovement playerMovement, PlayerOverview playerOverview,
            NormalCollisionDetector normalCollisionDetector, EnvironmentSound sound)
        {
            _model = model;
            _playerMovement = playerMovement;
            _playerOverview = playerOverview;
            _normalCollisionDetector = normalCollisionDetector;
            _sound = sound;
        }

        public void Enable()
        {
            _playerMovement.GoingToCalculateDirection += OnGoingToCalculateDirection;
            _playerMovement.Moved += OnMoved;
            _playerMovement.Stopped += OnStopped;
            _playerOverview.GoingToCalculateRotation += OnGoingToCalculateRotation;
            _normalCollisionDetector.Detected += OnDetected;
        }

        public void Disable()
        {
            _playerMovement.GoingToCalculateDirection -= OnGoingToCalculateDirection;
            _playerMovement.Moved -= OnMoved;
            _playerMovement.Stopped -= OnStopped;
            _playerOverview.GoingToCalculateRotation -= OnGoingToCalculateRotation;
            _normalCollisionDetector.Detected -= OnDetected;
        }

        private void OnGoingToCalculateDirection(Vector3 direction)
        {
            Vector3 directionAlongPlane = _model.CalculateVectorAlongPlane(direction);
            _playerMovement.SetDirection(directionAlongPlane);
        }

        private void OnMoved()
        {
            _sound.PlayWalk();
        }

        private void OnStopped()
        {
            _sound.StopWalk();
        }

        private void OnGoingToCalculateRotation(float axisX, float axisY)
        {
            Vector3 rotation = _model.CalculateRotationVector(axisX, axisY);
            _playerOverview.SetRotation(rotation);
        }

        private void OnDetected(Vector3 normal)
        {
            _model.SetNormal(normal);
        }
    }
}