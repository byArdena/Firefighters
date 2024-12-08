using ActionBars;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Players
{
    public class InputPresenter
    {
        private readonly PlayerInput Model;
        private readonly IPlayable Playable;
        private readonly IReadable Movement;
        private readonly IReadable Rotator;
        private readonly Joystick JoystickRotator;
        private readonly ActionRadar Radar;

        public InputPresenter(PlayerInput model, IPlayable playable, IReadable movement, IReadable rotator,
            Joystick joystickRotator, ActionRadar radar)
        {
            Model = model;
            Playable = playable;
            Movement = movement;
            Rotator = rotator;
            JoystickRotator = joystickRotator;
            Radar = radar;
        }

        public void Enable()
        {
            Model.Player.Throw.started += OnStartedThrow;
            Model.Player.Throw.performed += OnPerformedThrow;
            
            Model.Player.Touch.performed += OnTouched;
            
            Model.Player.Move.performed += OnMove;
            Model.Player.Move.canceled += OnMoverReleased;
            
            Model.Player.Rotate.performed += OnRotate;
            Model.Player.Rotate.canceled += OnRotatorReleased;
            
            Model.Player.Interact.started += OnInteract;
            Model.Player.Interact.canceled += OnCancelInteraction;

            Model.Enable();
        }

        public void Disable()
        {
            Model.Player.Throw.started -= OnStartedThrow;
            Model.Player.Throw.performed -= OnPerformedThrow;
            
            Model.Player.Touch.performed -= OnTouched;
            
            Model.Player.Move.performed -= OnMove;
            Model.Player.Move.canceled -= OnMoverReleased;
            
            Model.Player.Rotate.performed -= OnRotate;
            Model.Player.Rotate.canceled -= OnRotatorReleased;
            
            Model.Player.Interact.started -= OnInteract;
            Model.Player.Interact.canceled -= OnCancelInteraction;

            Model.Disable();
        }

        private void OnStartedThrow(InputAction.CallbackContext context)
        {
            Playable.Play();
        }

        private void OnPerformedThrow(InputAction.CallbackContext context)
        {
            Playable.Stop();
        }
        
        private void OnTouched(InputAction.CallbackContext context)
        {
            Vector2 position = Model.Player.ScreenPosition.ReadValue<Vector2>();
            
            if (position == Vector2.zero)
                return;
            
            JoystickRotator.CalculatePosition(position);
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            Movement.ReadInput(context.ReadValue<Vector2>());
        }
        
        private void OnMoverReleased(InputAction.CallbackContext context)
        {
            Movement.ReadInput(Vector2.zero);
        }

        private void OnRotate(InputAction.CallbackContext context)
        {
            Rotator.ReadInput(context.ReadValue<Vector2>());
        }
        
        private void OnRotatorReleased(InputAction.CallbackContext context)
        {
            Rotator.ReadInput(Vector2.zero);
        }
        
        private void OnInteract(InputAction.CallbackContext context)
        {
            Radar.Interact();
        }
        
        private void OnCancelInteraction(InputAction.CallbackContext context)
        {
            Radar.StopInteraction();
        }
    }
}