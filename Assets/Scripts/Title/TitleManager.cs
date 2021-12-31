using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour {
    void Awake () {
        Model.Instance.Initialize ();
    }

    void Start () {
        AudioManager.Instance.PlayBGM ("title", true, 0.3f);
    }

    void Update () {

    }

    public void OnPushRankingButton () {
        AudioManager.Instance.PlaySE ("ohyear");
        UIManager.Instance.OnPushRankingButton ();
    }

    public void OnPushGameStartButton () {
        AudioManager.Instance.PlaySE ("ohyear");
        AudioManager.Instance.FadeOutBGM ("title");
        UIManager.Instance.ShowTransitionBackground ();
        UIManager.Instance.TransitionIn (() => UIManager.Instance.OnPushGameStartButton ());
    }
}