using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InfiniteRotation : MonoBehaviour
{
    public float fullRotationDuration = 2f;

    public bool X;
    public bool Y;
    public bool Z;

    void Start()
    {
        Vector3 initRot = this.transform.localEulerAngles;

        if (X)
            this.transform.DOBlendableLocalRotateBy((Vector3.right * 360f), fullRotationDuration, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        if (Y)
            this.transform.DOBlendableLocalRotateBy((Vector3.up * 360f), fullRotationDuration, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        if (Z)
            this.transform.DOBlendableLocalRotateBy((Vector3.forward * 360f), fullRotationDuration, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }
}
