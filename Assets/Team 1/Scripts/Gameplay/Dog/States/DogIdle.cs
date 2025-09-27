﻿
using System.Collections;

using UnityEngine;

namespace Gameplay.Dog
{
    /// <summary>
    /// Dog is not moving.
    /// </summary>
    public class DogIdle : DogState
    {
        private readonly Transform _player;
        private readonly float _distanceToStartFollow;

        private Coroutine _delayCoroutine;


        /// <param name="playerTransform">Transform of player object to follow.</param>
        /// <param name="distanceToStartFollow">Distance between dog and player to start move.</param>
        public DogIdle(DogStateManager manager, Transform playerTransform, float distanceToStartFollow) : base(manager)
        {
            _player = playerTransform;
            _distanceToStartFollow = distanceToStartFollow;
        }


        public override void OnStart()
        {
            _manager.CurrentTarget.OnValueChanged += OnTargetChanged;
        }

        public override void OnStop()
        {
            if (_delayCoroutine != null)
            {
                _manager.StopCoroutine(_delayCoroutine);
                _delayCoroutine = null;
            }

            _manager.CurrentTarget.OnValueChanged -= OnTargetChanged;
        }

        public override void OnUpdate()
        {
            if (Vector3.Distance(_manager.MovementController.transform.position, _player.position) > _distanceToStartFollow && _delayCoroutine == null)
                _delayCoroutine = _manager.StartCoroutine(MoveDelay());
        }


        private IEnumerator MoveDelay()
        {
            yield return new WaitForSeconds(1.5f);
            _delayCoroutine = null;
            _manager.SetState<DogFollowPlayer>();
        }

        private void OnTargetChanged()
        {
            _manager.SetState<DogMove>();
        }
    }
}