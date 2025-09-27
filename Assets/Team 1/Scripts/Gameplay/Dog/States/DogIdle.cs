
using System.Collections;
using UnityEngine;

namespace Gameplay.Dog
{
    public class DogIdle : DogState
    {
        private readonly Transform _player;
        private readonly float _distanceToStartFollow;

        private Coroutine _delayCoroutine;


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

        public void OnTargetChanged()
        {
            _manager.SetState<DogMove>();
        }
    }
}
