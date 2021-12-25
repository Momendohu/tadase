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

    public GameStatus status;

    // Start is called before the first frame update
    void Start()
    {
        status = GameStatus.InGameReady;
    }

    // Update is called once per frame
    void Update()
    {
        switch (status)
        {
            case GameStatus.InGameReady:
                _limitTime = LimitMaxTime;
                Debug.Log("ゲーム開始準備");
                status = GameStatus.InGame;
                break;
            case GameStatus.InGame:
                _limitTime -= Time.deltaTime;

                if (_limitTime < 0)
                    status = GameStatus.Result;
                break;

            case GameStatus.Result:
                Debug.Log("ゲーム終了");
                break;
        }
    }

    const float LimitMaxTime = 10f;

    private float _limitTime = 0.0f;


}
