using System.Collections.Generic;

using UnityEngine;
// The Game Events used across the Game.
// Anytime there is a need for a new event, it should be added here.

namespace Core.Events
{
    /// <summary>
    /// This class can be used as a container for events that have to be created only once per program.
    /// </summary>
    public static class Events
    {
    }

    //List of all available events.
    #region EVENTS

    /// <summary>
    /// To be removed later.
    /// </summary>
    public class ExampleEvent : GameEvent
    {
        private string _exampleField;


        public string ExampleField => _exampleField;


        public ExampleEvent(string exampleField)
        {
            _exampleField = exampleField;
        }
    }

    #endregion EVENTS

    #region DOG_EVENTS
    public class DogMoveCommandEvent : GameEvent
    {
        private Vector3 _moveTarget;


        public Vector3 MoveTarget => _moveTarget;


        public DogMoveCommandEvent(Vector3 moveTarget)
        {
            _moveTarget = moveTarget;
        }
    }

    public class DogFollowCommandEvent : GameEvent
    {
    }
    #endregion DOG_EVENTS
}