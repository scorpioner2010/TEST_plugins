using UnityEngine;

namespace Patterns.Others
{
    public class ValueTimeUpdater
    {
        public EventBool IsActive;
        private float _timer;
        public float InfluenceFactor;

        public ValueTimeUpdater()
        {
            /*
            if (ScriptContainer.In != null)
            {
                ScriptContainer.In.Set(Update);
            }
            */
        }
        
        public void UpdateValue(bool isActive, float influenceFactor = 0, float time = 0)
        {
            IsActive.Value = isActive;
            InfluenceFactor = influenceFactor;
            _timer = Time.time + time;
        }

        private void Update()
        {
            if (_timer < Time.time && IsActive.Value)
            {
                UpdateValue(false);
            }
        }
    }
}