using UnityEngine.Events;

namespace Core.Shared
{
    public class Observable<T>
    {
        private T _value;


        /// <summary>
        /// Event invokes when value of observable has changed.
        /// </summary>
        public event UnityAction OnValueChanged;


        /// <summary>
        /// Current value of observable.
        /// </summary>
        public T Value
        {
            get { return _value; }

            set
            {
                if (!Equals(_value, value))
                {
                    _value = value;
                    OnValueChanged?.Invoke();
                }
            }
        }


        /// <param name="value">Start value.</param>
        public Observable(T value = default)
        {
            _value = value;
        }
    }
}
