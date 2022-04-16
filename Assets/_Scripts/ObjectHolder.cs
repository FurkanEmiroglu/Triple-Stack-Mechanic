using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHolder : MonoBehaviour
{
    public static ObjectHolder Instance; // I'll ask a question about this script at code review
    public Transform PlayerTransform;
    public Collector collector;
    public GameObject helper1, helper2, helper3;

    private void Awake() {
        Instance = this;
    }
}
