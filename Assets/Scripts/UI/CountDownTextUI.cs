using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTextUI : MonoBehaviour {
    private int _prevCountInt = 0;

    private float _count = 0;

    private bool isInitialized = false;

    private bool isCountEnd = false;

    private Action onComplete = null;

    [SerializeField]
    private Text _text = null;

    void Update () {
        CountDown ();
    }

    private void CountDown () {
        if (!this.isInitialized) return;

        this._count -= Time.deltaTime;
        if (_prevCountInt > Mathf.CeilToInt (this._count)) {
            _prevCountInt = Mathf.CeilToInt (this._count);
            this.UpdateText (string.Format ("{0}", _prevCountInt));
            AudioManager.Instance.PlaySE (string.Format ("count{0}", _prevCountInt));
            this.animateText ();
        }

        if (this._count <= 0) {
            this._count = 0;
            if (this.isCountEnd) return;
            this.isCountEnd = true;

            AudioManager.Instance.PlaySE ("horn", 0.2f);
            this.UpdateText ("スタート!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

            //OPTIMIZE:とりあえず1秒待ち
            StartCoroutine (WaitAsync (
                1,
                () => {
                    this.onComplete ();
                    Destroy (this.gameObject);
                }
            ));
        }
    }

    private IEnumerator WaitAsync (float interval, Action onComplete) {
        yield return new WaitForSeconds (interval);
        onComplete ();
    }

    private void UpdateText (string str) {
        _text.text = str;
    }

    private void SetAtive (bool isActive) {
        _text.gameObject.SetActive (isActive);
    }

    public void Initialize (int countNum, Action onComplete) {
        if (this.isInitialized) return;
        this.isInitialized = true;

        _count = countNum;
        _prevCountInt = Mathf.CeilToInt (countNum);
        this.onComplete = onComplete;

        this.SetAtive (true);
        this.UpdateText (string.Format ("{0}", Mathf.CeilToInt (countNum)));
        AudioManager.Instance.PlaySE (string.Format ("count{0}", _prevCountInt));
        this.animateText ();
    }

    private void animateText () {
        var sequence = DOTween.Sequence ();

        var tr = _text.GetComponent<RectTransform> ();
        sequence.Append (tr.DOLocalRotate (new Vector3 (0, 0, 50), 0))
            .Join (tr.DOScale (Vector3.one * 0.5f, 0))
            .Append (tr.DOLocalRotate (new Vector3 (0, 0, 0), 0.1f))
            .Join (tr.DOScale (Vector3.one * 1f, 0.1f))
            .AppendInterval (0.7f)
            .Append (tr.DOLocalRotate (new Vector3 (0, 0, -360), 0.2f))
            .Join (tr.DOScale (Vector3.zero, 0.2f));
    }
}