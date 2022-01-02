using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour {
    void Awake () { }

    void Start () {
        AudioManager.Instance.PlayBGM ("title", true, 0.1f);
    }

    void Update () {

    }

    public void OnPushRankingButton () {
        AudioManager.Instance.PlaySE ("button01");
        UIManager.Instance.OnPushRankingButton ();
    }

    public void OnPushGameStartButton () {
        AudioManager.Instance.PlaySE ("ohyear", 0.5f);
        AudioManager.Instance.FadeOutBGM ("title");
        UIManager.Instance.ShowTransitionBackground ();
        UIManager.Instance.TransitionIn (() => UIManager.Instance.OnPushGameStartButton ());
    }

    public void OnPushCollectionButton () {
        AudioManager.Instance.PlaySE ("button01");
        UIManager.Instance.ShowMobTadashiCollectionUIGroup ();
    }
}