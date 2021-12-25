using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : SingletonMonoBehaviour<InGameManager>
{
    public enum GameStatus
    {
        InGameReady,
        InGame,
        Result,
    }

    public GameStatus status { get { return _status; } }
    public int correctNum { get { return _correctNum; } }

    // Start is called before the first frame update
    void Start()
    {
        _status = GameStatus.InGameReady;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_status)
        {
            case GameStatus.InGameReady:
                _limitTime = LimitMaxTime;
                _correctNum = 0;
                TadashiManager.Instance.TadashiSetting();
                Debug.Log("ゲーム開始準備");
                _status = GameStatus.InGame;
                break;
            case GameStatus.InGame:
                _limitTime -= Time.deltaTime;

                if (_limitTime < 0)
                    _status = GameStatus.Result;
                break;

            case GameStatus.Result:
                Debug.Log("ゲーム終了");
                break;
        }
    }

    public void NextLevel()
    {
        _correctNum++;

        TadashiManager.Instance.TadashiSetting();
    }

    const float LimitMaxTime = 10f;
    private float _limitTime = 0.0f;

    private int _correctNum;
    private GameStatus _status;
}
