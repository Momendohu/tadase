using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour {
    public enum GameStatus {
        StartCountDownReady,
        StartCountDown,
        InGameReady,
        InGame,
        ResultDisplay,
        ResultReady,
    }

    public GameStatus status { get { return _status; } }
    public int correctNum { get { return _correctNum; } }
    public TadashiManager tadashiManager;

    // Start is called before the first frame update
    void Awake () {
        Initialize ();
    }

    // Update is called once per frame
    void Update () {
        switch (_status) {
            case GameStatus.StartCountDownReady:
                break;

            case GameStatus.StartCountDown:
                //OPTIMIZE:毎フレーム呼んでる(現状UIの中でリターンして回避)
                UIManager.Instance.InitializeCountDownTextUI (
                    3,
                    () => { _status = GameStatus.InGameReady; }
                );

                break;

            case GameStatus.InGameReady:
                tadashiManager.TadashiSetting (_tadashiNum);

                UIManager.Instance.ShowTimeUI ();
                UIManager.Instance.UpdateTimeUI (_limitTime.ToString ());

                _status = GameStatus.InGame;
                break;

            case GameStatus.InGame:
                _limitTime -= Time.deltaTime;

                if ((int) _limitTime != (int) _beforeLimitTime) {
                    UIManager.Instance.UpdateTimeUI (((int) _limitTime).ToString ());
                }

                if ((int) _limitTime <= 0)
                    _status = GameStatus.ResultDisplay;

                _beforeLimitTime = _limitTime;
                break;

            case GameStatus.ResultDisplay:
                UIManager.Instance.ShowGameEndUI ();
                UIManager.Instance.InitializeGameEndUI (
                    () => {
                        UIManager.Instance.ShowResultUIGroup ();
                        naichilab.RankingLoader.Instance.SendScoreAndShowRanking (Model.Instance.hiScore);
                    }
                );
                _status = GameStatus.ResultReady;
                break;

            case GameStatus.ResultReady:
                break;
        }
    }

    public void StartGame () {
        _status = GameStatus.StartCountDown;
    }

    public void Initialize () {
        _status = GameStatus.StartCountDownReady;
        _tadashiNum = TadashiMinimum;
        _correctNum = 0;
        _limitTime = LimitMaxTime;

        UIManager.Instance.ShowCountDownTextUI ();
        UIManager.Instance.ShowTransitionBackground ();
        UIManager.Instance.DisplayTransitionBackground ();
        UIManager.Instance.TransitionOut (() => StartGame (), 500);

        AudioManager.Instance.PlayBGM ("game");
    }

    public void UpdateLevel (bool isCorrect) {
        if (isCorrect) {
            //TODO:演出を入れる
            NextLevel ();
        } else {
            //TODO:演出を入れる
            ResetLevel ();
        }

        tadashiManager.TadashiSetting (_tadashiNum);
    }

    public void NextLevel () {
        _correctNum++;
        _limitTime += AddTime;

        CheckBonus (_correctNum);
        UIManager.Instance.DisplayTimeUIAddText (AddTime.ToString ());

        if (_tadashiNum < TadashiMax) {
            _tadashiNum++;
        }
    }

    public bool IsPlayebleStatus () {
        bool result = false;

        switch (_status) {
            case GameStatus.InGame:
                result = true;
                break;
            case GameStatus.InGameReady:
            case GameStatus.ResultDisplay:
            case GameStatus.ResultReady:
                result = false;
                break;
        }

        return result;
    }

    public void CheckAnswer (TadashiEntity entity) {
        UpdateLevel (entity.isAnswer);
    }

    private void ResetLevel () {
        _correctNum = 0;
        _tadashiNum = TadashiMinimum;
    }

    private void CheckBonus (int correctNum) {
        if (correctNum % BonusLine == 0) {
            UIManager.Instance.ShowTadashiTextUI ();
            UIManager.Instance.DisplayTadashiTextUI (
                string.Format ("{0}コンボ！やるじゃん", correctNum),
                1);

            UIManager.Instance.ShowExtendedTimeUI ();
            UIManager.Instance.InitializeExtendedTimeUI ();
        }
    }

    const float LimitMaxTime = 20.99f;
    const int TadashiMinimum = 3;
    const int TadashiMax = 100;

    const int BonusLine = 5;

    const float AddTime = 1.0f;

    private float _limitTime = 0.0f;
    private float _beforeLimitTime = 0.0f;
    private GameStatus _status;
    private int _tadashiNum;
    private int _correctNum;
}