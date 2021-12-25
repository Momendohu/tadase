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

                UIManager.Instance.ShowScoreUI();
                UIManager.Instance.UpdateScoreUI(_correctNum.ToString());

                UIManager.Instance.ShowTimeUI();
                UIManager.Instance.UpdateTimeUI(_limitTime.ToString());

                Debug.Log("�Q�[���J�n����");
                _status = GameStatus.InGame;
                break;
            case GameStatus.InGame:
                _limitTime -= Time.deltaTime;

                UIManager.Instance.UpdateTimeUI(((int)_limitTime).ToString());

                if (_limitTime < 0)
                    _status = GameStatus.Result;
                break;

            case GameStatus.Result:
                //Debug.Log("�Q�[���I��");
                UIManager.Instance.ShowResultUIGroup();
                break;
        }
    }

    public void NextLevel()
    {
        _correctNum++;

        UIManager.Instance.UpdateScoreUI(_correctNum.ToString());

        TadashiManager.Instance.TadashiSetting();
    }

    const float LimitMaxTime = 10f;
    private float _limitTime = 0.0f;

    private int _correctNum;
    private GameStatus _status;
}
