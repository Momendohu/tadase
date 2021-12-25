using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {
    [SerializeField]
    private Text text;

    [SerializeField]
    private GameObject addText;

    public void UpdateText (string str) {
        text.text = string.Format ("ただしポイント\n{0}", str);
    }

    public void DisplayAddText (string str) {
        addText.GetComponent<Text> ().text = string.Format ("+{0}", str);

        var sequence = DOTween.Sequence ();

        sequence.Append (addText.GetComponent<Text> ().DOFade (0, 0f))
            .Append (addText.GetComponent<Text> ().DOFade (1, 0.1f))
            .Join (addText.transform.DOLocalMoveX (50, 0.1f))
            .Append (addText.GetComponent<Text> ().DOFade (0, 0.1f))
            .Join (addText.transform.DOLocalMoveX (70, 0.1f));
    }
}