using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    [SerializeField] private GameObject _blockPrefab;
    [SerializeField] private Transform[] _spawnPos;

    void Start()
    {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
    }

    public void StartSpawningBlock()
    {
        Transform spawnPos = _spawnPos[Random.Range(0, _spawnPos.Length)];
        Instantiate(_blockPrefab, spawnPos.position, Quaternion.identity);
    }

}
