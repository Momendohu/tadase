using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ExtendedTimeUI : MonoBehaviour {
    [SerializeField]
    private RectTransform rectTransform = null;

    public void Initialize () {
        var sequence = DOTween.Sequence ();

        sequence.Append (rectTransform.DOLocalMoveY (-500, 0))
            .Append (rectTransform.DOLocalMoveY (-10, 0.5f))
            .Append (rectTransform.DOLocalMoveY (10, 1))
            .Append (rectTransform.DOLocalMoveY (500, 0.5f))
            .OnComplete (() => Destroy (this.gameObject));
    }
}