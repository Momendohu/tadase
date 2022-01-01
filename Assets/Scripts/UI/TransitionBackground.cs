using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TransitionBackground : MonoBehaviour {
    [SerializeField]
    private Image background = null;

    void Start () {

    }

    void Update () {

    }

    public void Display () {
        background.transform.SetAsLastSibling ();
        background.raycastTarget = true;
        background.fillAmount = 1;
    }

    public void TransitionIn (Action onComplete) {
        background.raycastTarget = true;
        background.fillAmount = 0;

        DOTween.To (
                () => background.fillAmount,
                num => background.fillAmount = num,
                1,
                0.5f
            )
            .SetEase (Ease.InOutQuart)
            .OnComplete (() => onComplete ());
    }

    public void TransitionOut (Action onComplete, float interval = 0) {
        background.fillAmount = 1;

        StartCoroutine (
            WaitAsync (
                interval,
                () => {
                    DOTween.To (
                            () => background.fillAmount,
                            num => background.fillAmount = num,
                            0,
                            0.5f
                        )
                        .SetEase (Ease.InOutQuart)
                        .OnComplete (() => onComplete ());

                    background.raycastTarget = false;

                    onComplete ();
                }
            ));
    }

    private IEnumerator WaitAsync (float interval, Action onComplete) {
        yield return new WaitForSeconds (interval);
        onComplete ();
    }
}