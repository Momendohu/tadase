using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultUIGroup : MonoBehaviour {
    public void OnPushRankingButton () {
        AudioManager.Instance.PlaySE ("button01");
        UIManager.Instance.OnPushRankingButton ();
    }

    public void OnPushGameRestartButton () {
        AudioManager.Instance.PlaySE ("button01");
        UIManager.Instance.OnPushGameRestartButton ();
    }

    public void OnPushGameTitleButton () {
        AudioManager.Instance.PlaySE ("button01");
        UIManager.Instance.OnPushTitleButton ();
    }
}