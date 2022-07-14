using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InfiniteTranslation : MonoBehaviour
{
    public float duration = 1f;
    public float offset = 0.5f;
    public Ease ease = Ease.InOutSine;

    void Start()
    {
        float y = this.transform.position.y;
        this.transform.DOLocalMoveY(y + offset, duration).SetLoops(-1, LoopType.Yoyo).SetEase(ease);
    }
}
