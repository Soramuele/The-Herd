using Core.Shared;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerMovement : MovementController
    {
        private CharacterController _controller;
        private Camera _camera;

        private float _walkSpeed;
        private float _runSpeed;
        private float _gravity;

        private float _verticalVelocity = 0f;


        public void Initialize(CharacterController characterController, PlayerConfig config)
        {
            _camera = Camera.main;
            _controller = characterController;

            _walkSpeed = config.WalkSpeed;
            _runSpeed = config.RunSpeed;
            _gravity = config.Gravity;
        }


        public override void MoveTo(Vector3 target)
        {
            _controller.Move(target);
        }


        public void ApplyGravity()
        {
            // Apply gravity
            if (_controller.isGrounded)
            {
                _verticalVelocity = -1f; // keep grounded
            }
            else
            {
                _verticalVelocity += _gravity;
            }

            _controller.Move(Vector3.up * _verticalVelocity * Time.deltaTime);
        }


        public Vector3 CalculateMovementTargetFromInput(Vector2 moveInput, bool isRunning)
        {
            // Convert input to world space relative to camera
            Vector3 forward = _camera.transform.forward;
            Vector3 right = _camera.transform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            Vector3 move = forward * moveInput.y + right * moveInput.x;

            if (move.magnitude > 1f)
                move.Normalize();

            float speed = isRunning ? _runSpeed : _walkSpeed;


            return move * speed * Time.deltaTime;
        }
    }
}