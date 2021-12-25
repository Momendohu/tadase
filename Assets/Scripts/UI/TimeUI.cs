using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour {
    [SerializeField]
    private Text text;

    [SerializeField]
    private GameObject addText;

    public void UpdateText (string str) {
        text.text = string.Format ("残りただしタイム\n{0}", str);
    }

    public void DisplayAddText (string str) {
        addText.GetComponent<Text> ().text = string.Format ("+{0}", str);

        var sequence = DOTween.Sequence ();

        sequence.Append (addText.GetComponent<RectTransform> ().DOLocalMoveX (30, 0))
            .Append (addText.GetComponent<Text> ().DOFade (0, 0f))
            .Append (addText.GetComponent<Text> ().DOFade (1, 0.2f))
            .Join (addText.GetComponent<RectTransform> ().DOLocalMoveX (50, 0.2f))
            .AppendInterval (1)
            .Append (addText.GetComponent<Text> ().DOFade (0, 0.2f))
            .Join (addText.GetComponent<RectTransform> ().DOLocalMoveX (70, 0.2f));
    }
}