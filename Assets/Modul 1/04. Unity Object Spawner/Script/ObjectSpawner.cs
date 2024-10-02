using UnityEngine;

namespace ModulKetiga {
    public class ObjectSpawner : MonoBehaviour
    {
        public GameObject objectToSpawn; // Prefab objek yang akan di-spawn
        [SerializeField] private Transform _startTransform;
        [SerializeField] private Transform _targetTransform;

        public void SpawnObject()
        {
            Vector3 spawnPosition = transform.position; // Posisi spawn berdasarkan posisi spawner
            GameObject gameObject = Instantiate(objectToSpawn, _startTransform.position, _startTransform.rotation); // Menggunakan Instantiate untuk spawn objek

            AnimationCharacter controller = gameObject.GetComponent<AnimationCharacter>();

            controller.Initialize(_startTransform, _targetTransform);
        }
    }
}