using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;

    Vector3 initialPosition;

    private void Start()
    {
        initialPosition = this.transform.position;
    }

    void Update()
    {
        this.transform.position = initialPosition.ChangeX(0) + target.position.ChangeY(0);
    }
}
