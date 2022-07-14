using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidesKillZones : MonoBehaviour
{
    public Transform target;

    Vector3 initialPosition;

    private void Start()
    {
        initialPosition = this.transform.position;
    }

    void Update()
    {
        this.transform.position = initialPosition + Vector3.forward * target.position.z;
    }
}
