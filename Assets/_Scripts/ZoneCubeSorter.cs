using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZoneCubeSorter : MonoBehaviour
{
    public List<Vector3> spawnLocations = new List<Vector3>();
    public List<GameObject> cubesAtZone = new List<GameObject>();
    [SerializeField] int _columns;
    [SerializeField] int _rows;
    [SerializeField] TextMeshPro textPro;


    private void Start() {
        DetermineZoneLocations();
    }

    private void Update() {
        textPro.text = GetEmptySpaces().ToString();
    }

    private void DetermineZoneLocations() {
        for (int i = 0; i < _columns; i++) {
            for (int j = 0; j < _rows; j++) {
                var currentSpawn = new Vector3((i * .3f), 0.125f, (j * .3f) - 1.5f);
                spawnLocations.Add(currentSpawn);
            }
        }
    }

    public void AddCubeToZone(GameObject obj) {
        if (GetEmptySpaces() > 0) {
            cubesAtZone.Add(obj);
        }
    }

    public void RemoveFromStorage(GameObject obj) {
        cubesAtZone.Remove(obj);
    }

    public int GetEmptySpaces() {
        if (cubesAtZone == null) {
            return 0;
        }
        return (_rows * _columns) - cubesAtZone.Count;
    }

    public int GetFilledSpaces() {

        return cubesAtZone.Count;
    }
}
