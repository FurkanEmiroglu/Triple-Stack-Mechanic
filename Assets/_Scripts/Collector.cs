using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Collector : MonoBehaviour
{
    public Transform Holster;
    [SerializeField] StorageCubeSorter _cubeStorageSorter;
    [SerializeField] TextMeshProUGUI countTxt;

    public List<Vector3> HolsterLocations = new List<Vector3>(); // Determines locations of cubes at characters back
    public Stack<GameObject> InsideHolster = new Stack<GameObject>(); // A stack of whats inside the bag(holster)
    public int cubeCount = 0;


    private void Start() {
        DetermineHolsterLocations();
    }

    private void Update() {
        countTxt.text = cubeCount.ToString();
    }


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Cube") && cubeCount < 31) {
            AddNewCube(other.transform);
        } else if (other.CompareTag("Storage") && cubeCount > 0) {
            //ToDo 30 sayısı ihtiyaç duyulan donation'a eşitlenecek. if statement ile storage full check kontrol edilecek.
            DonateToStorage(other.transform);
        } else if (other.CompareTag("Zone") && cubeCount > 0) {
            DonateToZone(other.transform, other.transform.parent.GetChild(0).gameObject); // tech debt this needs another solution
        } else if (other.CompareTag("PullTrigger") && cubeCount < 31) {
            PullNewCube();
        }
    }

    private void DetermineHolsterLocations() {
        for (int i = 0; i < 10; i++) {
            for (int j = 0; j < 3; j++) {
                var currentLocation = new Vector3(-.9f + j * .25f, 0 + i * .25f, 0);
                HolsterLocations.Add(currentLocation);
            }
        }
    }

    public void AddNewCube(Transform CubeToAdd) {
        if (Holster.childCount < 30) {
            StartCoroutine(RestockCubeAfterDelay(CubeToAdd.localPosition, CubeToAdd.localRotation, CubeToAdd.parent));
            CubeToAdd.SetParent(Holster);
            CubeToAdd.DOLocalJump(HolsterLocations[cubeCount], 1, 1, .5f);
            CubeToAdd.DOLocalRotate(Vector3.zero, .5f);
            CubeToAdd.DOScale(Vector3.one*.25f, .5f);
            InsideHolster.Push(CubeToAdd.gameObject);
            CubeToAdd.GetComponent<BoxCollider>().enabled = false;
            cubeCount++;
        }
    }

    public void PullNewCube() {
        while (Holster.childCount < 30 && _cubeStorageSorter.cubesAtStorage2.Count > 0) {
            var CubeToPull = _cubeStorageSorter.RemoveFromStorage().transform;
            CubeToPull.SetParent(Holster);
            CubeToPull.DOLocalJump(HolsterLocations[cubeCount], 1f, 1, .75f).SetEase(Ease.InOutSine);
            CubeToPull.DOLocalRotate(Vector3.zero, .5f);
            CubeToPull.DOScale(Vector3.one * .25f, .5f);
            InsideHolster.Push(CubeToPull.gameObject);
            cubeCount++;
        }
    }


    public void DonateToStorage(Transform receiver) {
        var DonatableAmount = cubeCount;                                            // amount of cubes the character carries
        var EmptySpaces = _cubeStorageSorter.GetEmptySpaces();                      // amount of empty spaces in storage
        var FilledSpaces = _cubeStorageSorter.GetFilledSpaces();                    // amount of occupied spaces in storage
        var LoopTimes = Math.Min(DonatableAmount, EmptySpaces);                     // how many cubes will move from character to storage


        for (int i = 0; i < LoopTimes; i++) {
            var CubeToDonate = InsideHolster.Pop().transform;                       // popping a cube out of the stack
            CubeToDonate.SetParent(receiver);                                       // setting its parent as storage
            CubeToDonate.DOLocalJump(_cubeStorageSorter.spawnLocations[i + FilledSpaces], 1, 1, 1f).SetEase(Ease.InExpo);
            CubeToDonate.DOLocalRotate(Vector3.zero, 1f);
            CubeToDonate.DOScale(Vector3.one * .17f, 1f);
            _cubeStorageSorter.AddCubeToStorage(CubeToDonate.gameObject);           // changing amount of empty spaces at storage
            cubeCount--;                                                            // decreasing amount of cubes the character carries
        }
    }

    public void DonateToZone(Transform receiver, GameObject zoneObj) {
        ZoneCubeSorter cubeZoneSorter = zoneObj.GetComponent<ZoneCubeSorter>();     // exactly same system as above but this line
        var DonatableAmount = cubeCount;                                            // ToDo this should have a better solution, getcomponent is high-cost
        var EmptySpaces = cubeZoneSorter.GetEmptySpaces();
        var FilledSpaces = cubeZoneSorter.GetFilledSpaces();
        var LoopTimes = Math.Min(DonatableAmount, EmptySpaces);

        for (int i = 0; i < LoopTimes; i++) {
            var CubeToDonate = InsideHolster.Pop().transform;
            CubeToDonate.SetParent(receiver);
            CubeToDonate.DOLocalJump(cubeZoneSorter.spawnLocations[i + FilledSpaces], 1, 1, 1f).SetEase(Ease.InExpo);
            CubeToDonate.DOLocalRotate(Vector3.zero, 1f);
            CubeToDonate.DOScale(Vector3.one * .17f, 1f);
            cubeZoneSorter.AddCubeToZone(CubeToDonate.gameObject);
            cubeCount--;
        }
        CheckComplete(DonatableAmount, EmptySpaces, cubeZoneSorter.GetComponentInParent<ZoneManager2>().ZoneID);
    }

    // Respawning from pool after 3 seconds.
    public IEnumerator RestockCubeAfterDelay(Vector3 position, Quaternion rotation, Transform parent) {
        yield return new WaitForSecondsRealtime(3f);
        ObjectPooler.Instance.SpawnFromPool("CollectableCube", position, rotation, parent);
    }

    private void CheckComplete(int DonatableAmount, int emptyspace, int id) {
        if (DonatableAmount >= emptyspace) {
            CustomEvents.Instance.ZoneComplete(id);
        }
    }
}
