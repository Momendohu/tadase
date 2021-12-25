using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : SingletonMonoBehaviour<UIManager> {
    protected override void Awake () {
        base.Awake ();
    }

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public void OnPushRankingButton () {
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking (0);
    }

    public void OnPushGameStartButton () {
        SceneManager.LoadScene("InGame");
    }

    public void OnPushGameRestartButton () {
        SceneManager.LoadScene("InGame");
    }

    public void OnPushTitleButton () {
        SceneManager.LoadScene("Title");
    }
}