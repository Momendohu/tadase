using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public enum GameStatus
    {
        InGameReady,
        InGame,
        ResultDisplay,
        ResultReady,
    }

    public GameStatus status { get { return _status; } }
    public int correctNum { get { return _correctNum; } }
    public TadashiManager tadashiManager;

    // Start is called before the first frame update
    void Awake()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        switch (_status)
        {
            case GameStatus.InGameReady:
                tadashiManager.TadashiSetting(_tadashiNum);

                UIManager.Instance.ShowScoreUI();
                UIManager.Instance.UpdateScoreUI(_correctNum.ToString());

                UIManager.Instance.ShowTimeUI();
                UIManager.Instance.UpdateTimeUI(_limitTime.ToString());

                Debug.Log("ゲーム開始準備");
                _status = GameStatus.InGame;
                break;
            case GameStatus.InGame:
                _limitTime -= Time.deltaTime;

                UIManager.Instance.UpdateTimeUI(((int)_limitTime).ToString());

                if (_limitTime < 0)
                    _status = GameStatus.ResultDisplay;
                break;

            case GameStatus.ResultDisplay:
                //Debug.Log("ゲーム終了");
                UIManager.Instance.ShowResultUIGroup();
                _status = GameStatus.ResultReady;
                break;

            case GameStatus.ResultReady:
                break;
        }
    }

    public void Initialize()
    {
        _status = GameStatus.InGameReady;
        _tadashiNum = TadashiMinimum;
        _correctNum = 0;
        _bonusScore = 0;
        _score = 0;
        _limitTime = LimitMaxTime;
    }

    public void UpdateLevel(bool isCorrect)
    {
        if (isCorrect)
        {
            NextLevel();
        }
        else
        {
            ResetLevel();
        }

        tadashiManager.TadashiSetting(_tadashiNum);
    }

    public void NextLevel()
    {
        _correctNum++;
        _score += CalcScore(_correctNum);

        UIManager.Instance.UpdateScoreUI(_score.ToString());

        if (_tadashiNum < TadashiMax)
        {
            _tadashiNum++;
        }
    }

    public bool IsPlayebleStatus()
    {
        bool result = false;

        switch (_status)
        {
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

    public void CheckAnswer(int uniqueId)
    {
        var entity = tadashiManager.GetTadashiEntity(uniqueId);

        UpdateLevel(entity.isAnswer);
    }

    private void ResetLevel()
    {
        _correctNum = 0;
        _bonusScore = 0;
        _tadashiNum = TadashiMinimum;
    }

    private int CalcScore(int correctNum)
    {
        int bonus = CalcBonus(correctNum);

        return bonus += 1;
    }

    private int CalcBonus(int correctNum)
    {
        if (correctNum % BonusLine == 0)
            _bonusScore += AddBonusScore;

        return _bonusScore;
    }

    const float LimitMaxTime = 20.0f;
    const int TadashiMinimum = 3;
    const int TadashiMax = 30;

    const int BonusLine = 5;
    const int AddBonusScore = 2;

    private float _limitTime = 0.0f;
    private GameStatus _status;
    private int _tadashiNum;
    private int _correctNum;
    private int _bonusScore;
    private int _score;
}