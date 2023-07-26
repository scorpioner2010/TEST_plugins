using System;
using UnityEngine;

namespace Patterns.Others
{
    public class Mover
    {
        public float Defect = 15f;
        private float _previousDistance;
        public Vector3 Move(Vector3 from, Vector3 to, float speed, Action<bool> onEndMove = null)
        {
            float distance = Vector3.Distance(from, to);
            
            if (distance < _previousDistance/Defect)
            {
                if (onEndMove != null)
                {
                    onEndMove(true);
                }
                return to;
            }

            if (_previousDistance == 0)
            {
                _previousDistance = distance;
            }
            
            Vector3 direction = to - from;
            direction = direction.normalized;
            return from + direction * speed;
        }
    }
}