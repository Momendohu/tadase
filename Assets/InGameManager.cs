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
                _countDown -= Time.deltaTime;

                string text = (int) _countDown <= 0 ? "GO" : ((int) _countDown).ToString ();
                UIManager.Instance.UpdateCountDownTextUI (text);

                if (_countDown < 0.0f) {
                    _status = GameStatus.InGameReady;
                }

                break;
            case GameStatus.InGameReady:
                UIManager.Instance.SetActiveCountDownTextUI (false);

                tadashiManager.TadashiSetting (_tadashiNum);

                UIManager.Instance.ShowScoreUI ();
                UIManager.Instance.UpdateScoreUI (_correctNum.ToString ());

                UIManager.Instance.ShowTimeUI ();
                UIManager.Instance.UpdateTimeUI (_limitTime.ToString ());

                Debug.Log ("�Q�[���J�n����");
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
                //Debug.Log("�Q�[���I��");
                UIManager.Instance.ShowResultUIGroup ();
                naichilab.RankingLoader.Instance.SendScoreAndShowRanking (Model.Instance.hiScore);
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
        _bonusScore = 0;
        _score = 0;
        _limitTime = LimitMaxTime;
        _countDown = CountDownMax;

        UIManager.Instance.ShowCountDownTextUI ();
        UIManager.Instance.ShowTransitionBackground ();
        UIManager.Instance.DisplayTransitionBackground ();
        UIManager.Instance.TransitionOut (() => StartGame (), 500);

        AudioManager.Instance.PlayBGM ("game");
    }

    public void UpdateLevel (bool isCorrect) {
        if (isCorrect) {
            NextLevel ();
        } else {
            ResetLevel ();
        }

        tadashiManager.TadashiSetting (_tadashiNum);
    }

    public void NextLevel () {
        _correctNum++;

        int addScore = CalcScore (_correctNum);
        UIManager.Instance.DisplayScoreUIAddText (addScore.ToString ());
        _score += addScore;

        if (_score > Model.Instance.hiScore) {
            Model.Instance.hiScore = _score;
        }

        UIManager.Instance.UpdateScoreUI (_score.ToString ());

        _limitTime += AddTime;
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
        _bonusScore = 0;
        _tadashiNum = TadashiMinimum;
    }

    private int CalcScore (int correctNum) {
        int bonus = CalcBonus (correctNum);

        return bonus += 1;
    }

    private int CalcBonus (int correctNum) {
        if (correctNum % BonusLine == 0) {
            _bonusScore += AddBonusScore;
            UIManager.Instance.ShowTadashiTextUI ();
            UIManager.Instance.DisplayTadashiTextUI ("シビれる！憧れるゥ…！", 1);
        }

        return _bonusScore;
    }

    const float LimitMaxTime = 20.99f;
    const int TadashiMinimum = 3;
    const int TadashiMax = 100;

    const int BonusLine = 5;
    const int AddBonusScore = 2;

    const float AddTime = 5.0f;
    const float CountDownMax = 3.99f;

    private float _limitTime = 0.0f;
    private float _beforeLimitTime = 0.0f;
    private GameStatus _status;
    private int _tadashiNum;
    private int _correctNum;
    private int _bonusScore;
    private int _score;
    private float _countDown;
}