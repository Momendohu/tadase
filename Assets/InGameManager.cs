using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public bool canClick {
        get => _canClick;
        set => _canClick = value;
    }

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
                    if (1 <= (int) _limitTime && (int) _limitTime <= 3) {
                        AudioManager.Instance.PlaySE ("endcount", 0.5f);
                    }
                }

                if ((int) _limitTime <= 0)
                    _status = GameStatus.ResultDisplay;

                _beforeLimitTime = _limitTime;
                break;

            case GameStatus.ResultDisplay:
                _status = GameStatus.ResultReady;
                Model.Instance.hiScore = this._correctNum;

                AudioManager.Instance.PlaySE ("exp1", 0.5f);
                AudioManager.Instance.FadeOutBGM ("game", 0, 0.001f);
                AudioManager.Instance.PlayBGM ("result", true, 0.5f);

                UIManager.Instance.ShowGameEndUI ();
                UIManager.Instance.InitializeGameEndUI (
                    () => {
                        UIManager.Instance.ShowResultUIGroup ();
                        naichilab.RankingLoader.Instance.SendScoreAndShowRanking (Model.Instance.hiScore);
                    }
                );
                break;

            case GameStatus.ResultReady:
                _canClick = false; //OPTIMIZE:常にクリック不可更新して他のフラグを潰してる
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
        _canClick = true;

        UIManager.Instance.ShowCountDownTextUI ();
        UIManager.Instance.ShowTransitionBackground ();
        UIManager.Instance.DisplayTransitionBackground ();
        UIManager.Instance.TransitionOut (() => StartGame (), 500);

        AudioManager.Instance.PlayBGM ("game", true, 0.1f);
    }

    public void UpdateLevel (bool isCorrect) {
        if (isCorrect) {
            //TODO:演出を入れる
            NextLevel ();
        } else {
            //TODO:演出を入れる
            Incorrect ();
        }

        tadashiManager.TadashiSetting (_tadashiNum);
    }

    public void NextLevel () {
        _correctNum++;
        AudioManager.Instance.PlaySE ("correct", 0.5f);

        CheckBonus (_correctNum);

        if (_tadashiNum < TadashiMax) {
            _tadashiNum++;
        }
    }

    private async void Incorrect () {
        canClick = false;
        AudioManager.Instance.PlaySE ("wrong", 0.5f);
        UIManager.Instance.DisplayTadashiTextUI (
            "間違ってて草wwwwwww",
            3f
        );

        await Task.Delay (100);
        canClick = true;
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

    private void CheckBonus (int correctNum) {
        if (correctNum % BonusLine == 0) {
            AudioManager.Instance.PlaySE ("year1", 0.5f);
            UIManager.Instance.DisplayTadashiTextUI (
                string.Format ("{0}ただし!やるじゃん", correctNum),
                2.5f);

            UIManager.Instance.ShowExtendedTimeUI ();
            UIManager.Instance.InitializeExtendedTimeUI ();

            _limitTime += AddTime;
            UIManager.Instance.DisplayTimeUIAddText (AddTime.ToString ());
        }
    }

    const float LimitMaxTime = 20.99f;
    const int TadashiMinimum = 3;
    const int TadashiMax = 100;

    const int BonusLine = 5;

    const float AddTime = 10f;

    private float _limitTime = 0.0f;
    private float _beforeLimitTime = 0.0f;
    private GameStatus _status;
    private int _tadashiNum;
    private int _correctNum;
    private bool _canClick = true;
}