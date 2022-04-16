using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorController : MonoBehaviour
{
    private Vector3 standardPosition;
    private BoxCollider thisCollider;
    public int id; // Will be used on opening the doorways

    private void Start() {
        thisCollider = GetComponent<BoxCollider>();
        standardPosition = transform.localPosition; // Door returns to this position when its closed.
        CustomEvents.Instance.onDoorwayEnter += OpenDoorway;
        CustomEvents.Instance.onDoorwayExit += CloseDoorway;
    }

    private void OnDestroy() {
        CustomEvents.Instance.onDoorwayEnter -= OpenDoorway;
        CustomEvents.Instance.onDoorwayExit -= CloseDoorway;
    }

    private void OpenDoorway(int id) {
        if (id == this.id) {
            transform.DOMove(new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), .5f).SetEase(Ease.OutExpo);
            transform.DOLocalMove(new Vector3(-0.36f, 2.61f, 0.54f), .5f);
            transform.DOLocalRotate(new Vector3(0, 90, 90), .5f);
            thisCollider.enabled = false;
            
        }
    }

    private void CloseDoorway(int id) {
        if (id == this.id) {
            transform.DOLocalMove(standardPosition, 2f).SetEase(Ease.OutExpo);
            transform.DOLocalRotate(new Vector3(0, 90, 0), .5f);
            thisCollider.enabled = true;
        }
    }
}
