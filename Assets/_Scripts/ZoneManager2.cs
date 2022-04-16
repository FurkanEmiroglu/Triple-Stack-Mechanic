using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZoneManager2 : MonoBehaviour
{
    public int ZoneID;

    [SerializeField] BoxCollider _cubeLocationsCollider;
    [SerializeField] BoxCollider _triggerAreaCollider;
    [SerializeField] GameObject _activeClone, _completedClone, _deactiveClone;
    private GameObject _helper2, _helper3, _helper1;

    private void Start() {
        CustomEvents.Instance.onZoneActivate += ActiveMode;
        CustomEvents.Instance.onZoneComplete += CompletedMode;
        DeactiveMode(ZoneID);
        ActiveMode(0);
        _helper2 = ObjectHolder.Instance.helper2;
        _helper3 = ObjectHolder.Instance.helper3;
        _helper1 = ObjectHolder.Instance.helper1;
    }

    private void OnDestroy() {
        CustomEvents.Instance.onZoneActivate -= ActiveMode;
        CustomEvents.Instance.onZoneComplete -= CompletedMode;
    }

    private void ActiveMode(int id) {
        if (id == ZoneID) {
            _activeClone.SetActive(true);
            _deactiveClone.SetActive(false);
            _completedClone.SetActive(false);
            EnableColliders(id);
        }
        
    }

    private void DeactiveMode(int id) {
        if (id == ZoneID) {
            _deactiveClone.SetActive(true);
            _activeClone.SetActive(false);
            _completedClone.SetActive(false);
            DisableColliders(id);

        }
    }

    private void CompletedMode(int id) {
        if (id == ZoneID) {
            _completedClone.SetActive(true);
            _activeClone.SetActive(false);
            _deactiveClone.SetActive(false);
            DisableColliders(id);
            CustomEvents.Instance.ZoneActivate(id+1);
            if (id == 2) {
                _helper2.SetActive(true);

            } else if (id == 4) {
                _helper1.SetActive(true);
            } else if (id == 6) {
                _helper3.SetActive(true);
            }
        }
    }

    private void EnableColliders(int id) {
        if (id == ZoneID) {
            _cubeLocationsCollider.enabled = true;
            _triggerAreaCollider.enabled = true;
        }
    }

    private void DisableColliders(int id) {
        if (id == ZoneID) {
            _cubeLocationsCollider.enabled = false;
            _triggerAreaCollider.enabled = false;
        }
    }
}
