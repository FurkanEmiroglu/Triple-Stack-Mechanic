using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomEvents : MonoBehaviour
{
    public static CustomEvents Instance;

    private void Awake() {
        Instance = this;
    }

    public event Action<int> onDoorwayEnter;

    public void DoorwayEnter(int id) {
        if (onDoorwayEnter != null) {
            onDoorwayEnter.Invoke(id);
        }
    }

    public event Action<int> onDoorwayExit;

    public void DoorwayExit(int id) {
        if (onDoorwayExit != null) {
            onDoorwayExit.Invoke(id);
        }
    }

    public event Action<int> onZoneActivate;
    
    public void ZoneActivate(int id) {
        if (onZoneActivate != null) {
            onZoneActivate.Invoke(id);
        }
    }

    public event Action<int> onZoneComplete;

    public void ZoneComplete(int id) {
        if (onZoneComplete != null) {
            onZoneComplete.Invoke(id);
        }
    }

    public event Action onReward;

    public void Reward() {
        if (onReward != null) {
            onReward.Invoke();
        }
    }

}
