using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TadashiTextUI : MonoBehaviour {
    [SerializeField]
    private Text text;

    public void Initialize (string str, float interval, float targetPosY = -180, float targetPosX = -320) {
        text.text = str;

        var sequence = DOTween.Sequence ();

        sequence.Append (this.GetComponent<RectTransform> ().DOLocalMove (new Vector3 (targetPosX, -350, 0), 0))
            .Append (this.GetComponent<RectTransform> ().DOLocalMoveY (targetPosY, 0.2f).SetEase (Ease.OutCubic))
            .AppendInterval (interval)
            .Append (this.GetComponent<RectTransform> ().DOLocalMoveY (-350, 0.2f).SetEase (Ease.InCubic))
            .OnComplete (() => {
                Destroy (this.gameObject);
            });
    }
}