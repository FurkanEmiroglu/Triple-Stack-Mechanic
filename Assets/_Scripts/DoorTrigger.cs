using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public int id;
    public bool isUnlocked = false;


    private void Start() {
        CustomEvents.Instance.onZoneComplete += setUnlocked;
    }

    private void OnDestroy() {
        CustomEvents.Instance.onZoneComplete -= setUnlocked;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && isUnlocked) {
        CustomEvents.Instance.DoorwayEnter(id);
        }
    }

    private void OnTriggerExit(Collider other) {
        CustomEvents.Instance.DoorwayExit(id);
    }

    public void setUnlocked(int ZoneId) {
        if (ZoneId == 2 && id == 2) {
            isUnlocked = true;
        }
        else if (ZoneId == 4 && id == 1) {          // bad choice of ID numbers
            isUnlocked = true;
        }
        else if (ZoneId == 6 && id == 3) {
            isUnlocked = true;                      // this is already true
        }
    }

    private void UnlockDoor(int ZoneId) {
        if (ZoneId == 2 && id == 2) {


        }
    }
}
