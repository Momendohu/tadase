using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {
    [SerializeField]
    private Text text;

    public void UpdateText (string str) {
        text.text = string.Format ("ただしポイント\n{0}", str);
    }
}