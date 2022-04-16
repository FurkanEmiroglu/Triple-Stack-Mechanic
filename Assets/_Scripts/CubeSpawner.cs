using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeSpawner : MonoBehaviour
{
    public List<Vector3> spawnLocations = new List<Vector3>();

    private void Start() {
        SpawnCube();
    }

    private void SpawnCube() {
        for (int i = 0; i < 2; i++) {
            for (int j = 0; j < 5; j++) { 
                var currentSpawn = new Vector3(( i * .8f), 0.125f, ( j * .8f) - 1.5f);
                spawnLocations.Add(currentSpawn);
                ObjectPooler.Instance.SpawnFromPool
                    ("CollectableCube", currentSpawn, Quaternion.identity, transform);
            }
        }
        spawnLocations.Reverse();
    }    
}
