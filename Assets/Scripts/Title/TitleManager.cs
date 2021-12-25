using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour {
    void Start () {

    }

    void Update () {

    }

    public void OnPushRankingButton () {
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking (0);
    }

    public void OnPushGameStartButton () {
        print ("ゲームスタート");
    }
}