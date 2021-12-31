using System;
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
    private GameObject extendedTimeUI = null;

    [SerializeField]
    private GameObject tadashiTextUI = null;

    [SerializeField]
    private GameObject countDownTextUI = null;

    [SerializeField]
    private GameObject transitionBackground = null;

    [SerializeField]
    private GameObject gameEndUI = null;

    private GameObject timeUIInstance = null;

    private GameObject extendedTimeUIInstance = null;

    private GameObject countDownTextUIInstance = null;

    private GameObject transitionBackgroundInstance = null;

    private GameObject gameEndUIInstance = null;

    protected override void Awake () {
        base.Awake ();
    }

    public void ShowResultUIGroup () {
        this.CreateUI (uiResultGroup);
    }

    public void ShowExtendedTimeUI () {
        extendedTimeUIInstance = this.CreateUI (extendedTimeUI);
    }

    public void ShowTimeUI () {
        timeUIInstance = this.CreateUI (timeUI);
    }

    public void ShowTransitionBackground () {
        transitionBackgroundInstance = this.CreateUI (transitionBackground);
    }

    public void ShowCountDownTextUI () {
        if (countDownTextUIInstance != null) return;
        countDownTextUIInstance = this.CreateUI (countDownTextUI);
    }

    public void ShowGameEndUI () {
        gameEndUIInstance = this.CreateUI (gameEndUI);
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

    public void InitializeExtendedTimeUI () {
        if (!extendedTimeUIInstance) {
            print ("extendedTimeUIInstanceがない");
            return;
        }

        extendedTimeUIInstance.GetComponent<ExtendedTimeUI> ().Initialize ();
    }

    public void DisplayTimeUIAddText (string str) {
        if (!timeUIInstance) {
            print ("timeUIInstanceがない");
            return;
        }

        timeUIInstance.GetComponent<TimeUI> ().DisplayAddText (str);
    }

    public void TransitionIn (Action onComplete) {
        if (!transitionBackgroundInstance) {
            print ("transitionBackgroundInstanceがない");
            return;
        }

        transitionBackgroundInstance.GetComponent<TransitionBackground> ().TransitionIn (onComplete);
    }

    public void DisplayTransitionBackground () {
        if (!transitionBackgroundInstance) {
            print ("transitionBackgroundInstanceがない");
            return;
        }

        transitionBackgroundInstance.GetComponent<TransitionBackground> ().Display ();
    }

    public void TransitionOut (Action onComplete, int interval = 0) {
        if (!transitionBackgroundInstance) {
            print ("transitionBackgroundInstanceがない");
            return;
        }

        transitionBackgroundInstance.GetComponent<TransitionBackground> ().TransitionOut (onComplete, interval);
    }

    public void DisplayTadashiTextUI (string str, float interval, float targetPosY = -180, float targetPosX = -320) {
        this.CreateUI (tadashiTextUI).GetComponent<TadashiTextUI> ().Initialize (str, interval, targetPosY, targetPosX);
    }

    public void InitializeCountDownTextUI (int countDownNum, Action onComplete) {
        countDownTextUIInstance.GetComponent<CountDownTextUI> ().Initialize (countDownNum, onComplete);
    }

    public void InitializeGameEndUI (Action onComplete) {
        gameEndUIInstance.GetComponent<GameEndUI> ().Initialize (onComplete);
    }
}