using System;

namespace Patterns
{
    public struct EventBool
    {
        private bool _value;

        public bool Value
        {
            get => _value;
            set
            {
                if (_value == value)
                {
                    return;
                }
                
                OnChange?.Invoke(value);
                _value = value;
            }
        }

        public event Action<bool> OnChange;
    }
}