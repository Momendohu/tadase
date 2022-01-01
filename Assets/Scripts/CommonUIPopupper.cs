using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CommonUIPopupper : MonoBehaviour {
    [SerializeField]
    private RectTransform rectTransform = null;

    public void In (float interval, Action onComplete) {
        DOTween.Sequence ().Append (rectTransform.DOScale (0, 0))
            .Append (rectTransform.DOScale (1, interval).SetEase (Ease.OutCirc))
            .OnComplete (() => onComplete ());
    }

    public void Out (float interval, Action onComplete) {
        DOTween.Sequence ().Append (rectTransform.DOScale (1, 0))
            .Append (rectTransform.DOScale (0, interval).SetEase (Ease.InCirc))
            .OnComplete (() => onComplete ());
    }
}