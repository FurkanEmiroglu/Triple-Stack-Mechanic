using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ZoneManager : MonoBehaviour
{
    public int ZoneID; // Zone ID
    private List<GameObject> childs = new List<GameObject>();

    [SerializeField] BoxCollider _cubeLocationsCollider;
    [SerializeField] BoxCollider _TriggerAreaCollider;


    private void Start() {
        GetChildsToList();
        if (ZoneID == 0) {
            ChangeColorToActive();
            UnlockZone(ZoneID);
        } else {
            ChangeColorToDeactive();
            LockZone(ZoneID);
        }
        // subscribe
        CustomEvents.Instance.onZoneActivate += ZoneActivate;
        CustomEvents.Instance.onZoneActivate += UnlockZone;
        CustomEvents.Instance.onZoneComplete += ChangeColorToCompleted;
    }

    private void OnDestroy() {
        CustomEvents.Instance.onZoneActivate -= ZoneActivate;
        CustomEvents.Instance.onZoneActivate -= UnlockZone;
        CustomEvents.Instance.onZoneComplete -= ZoneCompleted;
    }

    private void ZoneActivate(int id) {
        if (id == ZoneID) {
            ChangeColorToActive();
            UnlockZone(id);
        } else {
            LockZone(id);
        }
    }

    private void ZoneCompleted(int id) {
        if (id == ZoneID) {
            CustomEvents.Instance.ZoneComplete(id);
            CustomEvents.Instance.ZoneActivate(id+1);
        }
    }

    private void GetChildsToList() {                                    // adds childs with meshrenderer into a list
        for (int i = 0; i < transform.childCount; i++) {
            var childobj = transform.GetChild(i).gameObject;
            if (childobj.GetComponent<MeshRenderer>() != null) {
                childs.Add(childobj);
            }
        }
    }

    private void ChangeColorToActive() {
        foreach (var item in childs) {
            item.GetComponent<MeshRenderer>().material.DOColor(new Color32(142, 71, 55, 255), 2f);
        }
    }

    private void ChangeColorToDeactive() {
        foreach (var item in childs) {
            item.GetComponent<MeshRenderer>().material.DOColor(new Color32(63, 63, 63, 255), 2f);
        }
    }

    private void ChangeColorToCompleted(int id) {
        if (id == ZoneID) {
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    private void UnlockZone(int id) {
        if (id == ZoneID) {
            _cubeLocationsCollider.enabled = false;
            _TriggerAreaCollider.enabled = false;
        }
    }

    private void LockZone(int id) {
        if (id == ZoneID) {
            _cubeLocationsCollider.enabled = false;
            _TriggerAreaCollider.enabled = false;
        }
    }
}
