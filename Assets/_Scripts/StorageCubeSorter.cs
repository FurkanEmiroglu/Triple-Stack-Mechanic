using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageCubeSorter : MonoBehaviour
{
    public List<Vector3> spawnLocations = new List<Vector3>();
    public Stack<GameObject> cubesAtStorage2 = new Stack<GameObject>();
    public List<GameObject> cubesAtStorage = new List<GameObject>();
    [SerializeField] int StorageCapacity;

    private void Start() {
        DetermineStorageLocations();
    }

    private void DetermineStorageLocations() {
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 5; j++) {
                var currentSpawn = new Vector3((i * .3f), 0.125f, (j * .4f) - 1.5f);
                spawnLocations.Add(currentSpawn);
            }
        }
    }

    public void AddCubeToStorage(GameObject obj) {
        if (GetEmptySpaces() > 0) {
            //cubesAtStorage.Add(obj);
            cubesAtStorage2.Push(obj);
        }
    }

    public GameObject RemoveFromStorage() {
        if (cubesAtStorage2.Count != 0) {
        return cubesAtStorage2.Pop();
        } return null;
    }

    public int GetEmptySpaces() {
        return StorageCapacity-cubesAtStorage2.Count;
    }

    public int GetFilledSpaces() {

        return cubesAtStorage2.Count;
    }
}
