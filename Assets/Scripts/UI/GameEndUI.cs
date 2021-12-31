using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameEndUI : MonoBehaviour {
    [SerializeField]
    private Text _text = null;

    void Update () { }

    public void Initialize (Action onComplete) {
        var sequence = DOTween.Sequence ();

        var tr = _text.GetComponent<RectTransform> ();
        sequence.Append (tr.DOLocalRotate (new Vector3 (0, 0, 50), 0))
            .Join (tr.DOScale (Vector3.one * 0.5f, 0))
            .Append (tr.DOLocalRotate (new Vector3 (0, 0, 0), 0.1f))
            .Join (tr.DOScale (Vector3.one * 1f, 0.1f))
            .AppendInterval (0.7f)
            .Append (tr.DOLocalRotate (new Vector3 (0, 0, -360), 0.2f))
            .Join (tr.DOScale (Vector3.zero, 0.2f))
            .OnComplete (() => { onComplete (); });
    }
}