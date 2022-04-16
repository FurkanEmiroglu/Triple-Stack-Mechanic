using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HelperMovement : MonoBehaviour
{
    public Transform[] waypoints;

    [SerializeField] NavMeshAgent _agent;
    [SerializeField] AnimationController _animController;
    private int waypointIndex;
    private Vector3 target;

    private void Start() {
        UpdateDestination();
        _animController.StartWalkingAnim();                         // ToDo not working.

    }

    private void Update() {
        if (Vector3.Distance(transform.position, target) < 1) {
        IterateWaypoint();                                          // maybe an if statement here
        UpdateDestination();                                        // they should stop when storage capacity is full
        }
        if (_agent.isStopped) {
            _animController.StopWalkingAnim();
        }
    }


    void UpdateDestination() {
        target = waypoints[waypointIndex].position;
        _agent.SetDestination(target);
    }

    void IterateWaypoint() {
        waypointIndex++;
        if (waypointIndex == waypoints.Length) {
            waypointIndex = 0;
        }
    }
}
