using Core.Shared.StateMachine;

namespace Gameplay.Dog 
{
    public abstract class DogState : IState
    {
        protected DogStateManager _manager;


        public DogState(DogStateManager manager)
        {
            _manager = manager;
        }


        public abstract void OnStart();

        public abstract void OnStop();

        public abstract void OnUpdate();
    }
}