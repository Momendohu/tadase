using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultUIGroup : MonoBehaviour {
    public void OnPushRankingButton () {
        UIManager.Instance.OnPushRankingButton ();
    }

    public void OnPushGameRestartButton () {
        UIManager.Instance.OnPushGameRestartButton ();
    }
}