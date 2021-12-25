using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour {
    [SerializeField]
    private Text text;

    public void UpdateText (string str) {
        text.text = string.Format ("残りただしタイム\n{0}", str);
    }
}