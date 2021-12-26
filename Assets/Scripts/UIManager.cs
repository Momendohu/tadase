using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : SingletonMonoBehaviour<UIManager> {
    [SerializeField]
    private GameObject uiResultGroup = null;

    [SerializeField]
    private GameObject timeUI = null;

    [SerializeField]
    private GameObject scoreUI = null;

    [SerializeField]
    private GameObject tadashiTextUI = null;

    private GameObject timeUIInstance = null;

    private GameObject scoreUIInstance = null;

    private GameObject tadashiUIInstance = null;

    protected override void Awake () {
        base.Awake ();
    }

    public void ShowResultUIGroup () {
        this.CreateUI (uiResultGroup);
    }

    public void ShowTimeUI () {
        timeUIInstance = this.CreateUI (timeUI);
    }

    public void ShowScoreUI () {
        scoreUIInstance = this.CreateUI (scoreUI);
    }

    public void ShowTadashiTextUI () {
        tadashiUIInstance = this.CreateUI (tadashiTextUI);
    }

    private GameObject CreateUI (GameObject prefab) {
        var obj = Instantiate (prefab);

        var canvasRoot = getCanvasRoot ();
        if (!canvasRoot) {
            print ("canvasがうまく参照できてないよ");
            Destroy (obj);
            return null;
        }

        obj.transform.SetParent (canvasRoot, false);

        return obj;
    }

    private Transform getCanvasRoot () {
        var uiRootList = GameObject.FindGameObjectsWithTag ("UI");
        if (uiRootList.Length == 0) {
            return null;
        }

        return uiRootList[0].transform; //OPTIMIZE:UIタグのCanvasがある前提
    }

    public void OnPushRankingButton () {
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking (Model.Instance.hiScore);
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

    public void UpdateTimeUI (string str) {
        if (!timeUIInstance) {
            print ("timeUIInstanceがない");
            return;
        }

        timeUIInstance.GetComponent<TimeUI> ().UpdateText (str);
    }

    public void UpdateScoreUI (string str) {
        if (!scoreUIInstance) {
            print ("scoreUIInstanceがない");
            return;
        }

        scoreUIInstance.GetComponent<ScoreUI> ().UpdateText (str);
    }

    public void DisplayTimeUIAddText (string str) {
        if (!timeUIInstance) {
            print ("timeUIInstanceがない");
            return;
        }

        timeUIInstance.GetComponent<TimeUI> ().DisplayAddText (str);
    }

    public void DisplayScoreUIAddText (string str) {
        if (!scoreUIInstance) {
            print ("scoreUIInstanceがない");
            return;
        }

        scoreUIInstance.GetComponent<ScoreUI> ().DisplayAddText (str);
    }

    public void DisplayTadashiTextUI (string str, float interval) {
        if (!tadashiUIInstance) {
            print ("tadashiUIInstanceがない");
            return;
        }

        tadashiUIInstance.GetComponent<TadashiTextUI> ().Initialize (str, interval);
    }
}