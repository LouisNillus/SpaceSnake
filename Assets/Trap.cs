using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Trap : MonoBehaviour
{

    public bool breakable = false;

    [Tooltip("In seconds")]
    public float poppingSpeed = 0.5f;
    public TrapDirection direction;
    public int tilesMove = 1;
    [Tooltip("In tiles")]
    public int activationDistance;

    bool activated;

    Transform player;
    Vector3 initPos;

    [HideInInspector]
    public Chunk chunk;


    // Start is called before the first frame update
    void Start()
    {
        initPos = this.transform.localPosition;
        player = PlayerController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (chunk.ingame && !activated && Mathf.Abs(player.position.z - this.transform.position.z) <= activationDistance)
        {
            Move();
            activated = true;
        }
    }

    public string GetTrapCode()
    {
        return direction.ToString()[0] + activationDistance.ToString();
    }

    public void Move()
    {
        initPos = this.transform.localPosition;

        if(direction == TrapDirection.Up)
        {
            this.transform.DOLocalMove(initPos + Methods.DirectionToVector(direction) * tilesMove * (this.transform.localScale.y + 0.1f), poppingSpeed).SetEase(Ease.InOutSine);
        }
        else
        {
            this.transform.DOLocalMove(initPos + Methods.DirectionToVector(direction) * tilesMove, poppingSpeed).SetEase(Ease.InOutSine);
        }
    }

    public void Reset()
    {
        this.transform.localPosition = initPos;
        activated = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            Debug.Log("Collision");
        }
    }

}

public enum TrapDirection
{
    Up,
    Down,
    Left,
    Right,
    Forward,
    Backward,
    Static
}
