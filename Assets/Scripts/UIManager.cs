using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : SingletonMonoBehaviour<UIManager> {

    [SerializeField]
    public GameObject uiResultGroup = null;

    protected override void Awake () {
        base.Awake ();
    }

    public void ShowResultUIGroup () {
        var obj = Instantiate (uiResultGroup);

        var uiRootList = GameObject.FindGameObjectsWithTag ("UI");
        var canvasRoot = uiRootList[0].transform; //OPTIMISE:UIタグのCanvasがある前提
        if (!canvasRoot) {
            print ("canvasがうまく参照できてないよ");
            Destroy (obj);
            return;
        }

        obj.transform.SetParent (canvasRoot, false);
    }

    public void OnPushRankingButton () {
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking (0);
    }

    public void OnPushGameStartButton () {
        SceneManager.LoadScene ("InGame");
    }

    public void OnPushGameRestartButton () {
        SceneManager.LoadScene ("InGame");
    }

    public void OnPushTitleButton () {
        SceneManager.LoadScene ("Title");
    }
}