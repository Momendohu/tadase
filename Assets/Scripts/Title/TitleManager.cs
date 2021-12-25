using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour {
    void Start () {

    }

    void Update () {

    }

    public void OnPushRankingButton () {
        UIManager.Instance.OnPushRankingButton ();
    }

    public void OnPushGameStartButton () {
        UIManager.Instance.OnPushGameStartButton ();
    }
}