using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour {
    void Awake () {
        Model.Instance.Initialize ();
    }

    void Start () {
        AudioManager.Instance.PlayBGM ("bgm01", true, 0.3f);
    }

    void Update () {

    }

    public void OnPushRankingButton () {
        AudioManager.Instance.PlaySE ("button01");
        UIManager.Instance.OnPushRankingButton ();
    }

    public void OnPushGameStartButton () {
        AudioManager.Instance.PlaySE ("button01");
        UIManager.Instance.OnPushGameStartButton ();
    }
}