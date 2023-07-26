using System;
using System.Collections;
using UnityEngine;

namespace Patterns
{
    public class CoroutineUpdater
    {
        private UpdaterCoroutineComponent _updater;
        private static Transform _coroutineParent;
        public CoroutineUpdater(string name, float time, Action method)
        {
            if (_coroutineParent == null)
            {
                _coroutineParent = new GameObject("CoroutineUpdater").transform;
            }
            
            GameObject gm = new GameObject(name);
            gm.transform.parent = _coroutineParent;
            _updater = gm.AddComponent<UpdaterCoroutineComponent>();
            _updater.Init(time, method);
        }

        public void Cancel()
        {
            if (_updater != null)
            {
                _updater.Cancel();
            }
        }
        
        public class UpdaterCoroutineComponent : MonoBehaviour
        {
            private float _time;
            private Action _action;
            private Coroutine _coroutine;
            
            public void Init(float time, Action method)
            {
                _time = time;
                _action = method;
                _coroutine = StartCoroutine(CoroutineUpdate());
            }
            
            public void Cancel()
            {
                StopCoroutine(_coroutine);
                Destroy(gameObject);
            }
            
            private IEnumerator CoroutineUpdate()
            {
                yield return new WaitForSeconds(_time);
                _action?.Invoke();
                _coroutine = StartCoroutine(CoroutineUpdate());
            }
        }
    }
}
