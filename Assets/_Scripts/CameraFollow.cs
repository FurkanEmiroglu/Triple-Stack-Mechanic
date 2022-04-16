using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField] Vector3 _camOffset;
    
    private void Start() {
        playerTransform = ObjectHolder.Instance.PlayerTransform;
    }


    private void Update() {
        transform.position = playerTransform.position + _camOffset;
        transform.LookAt(playerTransform);
    }
}
