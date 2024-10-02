using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ModulKetiga
{
    public class SpawnerController : MonoBehaviour
    {
        public ObjectSpawner _objectSpawner;
        public float delayTime;
        public float spawnRate;

        public UnityEvent onSpawnObject;

        private void Start()
        {
            InvokeRepeating("SpawnObjectOvertime", delayTime, spawnRate);
        }

        private void SpawnObjectOvertime()
        {
            _objectSpawner.SpawnObject();

            if (onSpawnObject != null)
            {
                onSpawnObject.Invoke();
            }
        }

        //public void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        _objectSpawner.SpawnObject();

        //        if (onSpawnObject != null)
        //        {
        //            onSpawnObject.Invoke();
        //        }
        //    }
        //}
    }
}
