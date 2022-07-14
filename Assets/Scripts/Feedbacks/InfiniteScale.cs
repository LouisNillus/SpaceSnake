using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InfiniteScale : MonoBehaviour
{
    public float duration = 1f;
    public float offset = 0.5f;
    public Ease ease = Ease.InOutSine;

    void Start()
    {
        Vector3 scale = this.transform.localScale;
        this.transform.DOScale(scale * (1 + offset), duration).SetLoops(-1, LoopType.Yoyo).SetEase(ease);
    }
}
