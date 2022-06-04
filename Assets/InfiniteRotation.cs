using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InfiniteRotation : MonoBehaviour
{
    public float fullRotationDuration = 2f;

    void Start()
    {
        this.transform.DOLocalRotate((Vector3.up * 360f).ChangeX(90), fullRotationDuration, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
    }
}
