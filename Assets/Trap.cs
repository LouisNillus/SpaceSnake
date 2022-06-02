using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{

    public bool breakable = false;

    [Tooltip("In seconds")]
    public float poppingSpeed;
    public TrapDirection direction;
    [Tooltip("In tiles")]
    public int activationDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum TrapDirection
{
    Up,
    Down,
    Left,
    Right,
    Forward,
    Backward
}
