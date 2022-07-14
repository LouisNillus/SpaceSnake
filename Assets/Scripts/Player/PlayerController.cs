using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    
    float deltaX;

    [HideInInspector] public float xSpeed;
    [HideInInspector] public float zSpeed = 6f;

    public float initialXSpeed;
    public float initialZSpeed = 6f;

    [HideInInspector] public Rigidbody rb;

    public static PlayerController instance;

    Vector3 initialPosition;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = this.transform.position;
        rb = GetComponent<Rigidbody>();

        xSpeed = 0f;
        zSpeed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Keyboard Debug
        if (Input.GetKey(KeyCode.RightArrow)) this.transform.position += Vector3.right * Time.deltaTime * xSpeed;
        if (Input.GetKey(KeyCode.LeftArrow)) this.transform.position += Vector3.left * Time.deltaTime * xSpeed;

        Movement();
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.right * Time.deltaTime * deltaX * xSpeed;
    }

    public void Movement()
    {
        //Z Axis
        this.transform.position += Vector3.forward * Time.deltaTime * zSpeed * RunHandler.instance.zSpeedModifier;

        //X Axis
        if (Input.touchCount > 0)
        {
            deltaX = Input.GetTouch(0).deltaPosition.x;
        }
        else
        {
            deltaX = 0f;
        }
    }

    public void StartPlayer()
    {
        xSpeed = initialXSpeed;
        zSpeed = initialZSpeed;
    }

    public void StopPlayer()
    {
        rb.velocity = Vector3.zero;
        xSpeed = 0f;
        zSpeed = 0f;
    }

    public void Kill(bool withSlowDown = true)
    {

        if(withSlowDown)
        {
            StartCoroutine(KilledSlowDownZ(1f));
        }
        else
        {
            ResetPlayer();
            UIManager.instance.LostPanel();
        }

        xSpeed = 0f;        
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public IEnumerator KilledSlowDownZ(float duration)
    {
        float t = 0f;

        float currentZSpeed = zSpeed;

        while(t < duration)
        {
            zSpeed = Mathf.Lerp(currentZSpeed, 0, t/duration);
            t += Time.deltaTime;
            yield return null;
        }

        ResetPlayer();
        UIManager.instance.LostPanel();
    }

    public IEnumerator SlowlyIncreaseSpeed(float duration)
    {
        float t = 0f;

        while (t < duration)
        {
            zSpeed = Mathf.Lerp(0f, initialZSpeed, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
    }

    public void StartBackSlowly(float duration)
    {
        xSpeed = initialXSpeed;
        StopAllCoroutines();
        StartCoroutine(SlowlyIncreaseSpeed(duration));
    }

    public void ResetPlayer()
    {
        this.transform.position = initialPosition;
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        StopPlayer();
    }
}
