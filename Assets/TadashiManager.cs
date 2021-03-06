using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TadashiManager : MonoBehaviour {
    public int answerPictId;
    public int notAnswerPictId;

    public Sprite[] sprites;

    public enum RotateKind {
        None = 0,
        X,
        Y,
        Z
    }

    void Awake () {
        Initialize ();
    }

    void Update () {

    }

    public void Initialize () {
        _tadashiList.Clear ();
    }

    public void TadashiSetting (int tadashiNum) {
        SetAnswer ();

        float speed = 0;
        float rotateSpeed = 0;
        RotateKind rotateKind = RotateKind.None;

        bool isMove = UnityEngine.Random.Range (0, MoveProbability) == 0;
        bool isRotate = UnityEngine.Random.Range (0, RotateProbability) == 0;

        if (isMove) speed = UnityEngine.Random.Range (SpeedMinimum, SpeedMax);

        if (isRotate) {
            rotateSpeed = UnityEngine.Random.Range (RotateSpeedMinimux, RotateSpeedMax);
            rotateKind = (RotateKind) UnityEngine.Random.Range (0, Enum.GetValues (typeof (RotateKind)).Length);
        }

        GameObject obj = (GameObject) Resources.Load ("Prefabs/Tadashi");
        for (int idx = 0; idx < tadashiNum; idx++) {
            int pictId = idx == 0 ? answerPictId : notAnswerPictId;

            TadashiEntity entity;

            if ((_tadashiList.Count - 1) < idx) {
                GameObject tadashi = (GameObject) Instantiate (obj, Vector2.zero, Quaternion.identity);
                entity = tadashi.GetComponent<TadashiEntity> ();
                _tadashiList.Add (entity);
            } else {
                entity = _tadashiList[idx];
                entity.gameObject.SetActive (true);
            }

            RandomPos (entity.gameObject);

            //memo:uidにpictIDを入れている
            entity.Initialzie (pictId, pictId, pictId == answerPictId, sprites[pictId], speed, rotateSpeed, rotateKind);
        }

        //コレクション
        PlayerPrefs.SetInt (string.Format ("{0}", answerPictId), 1);
        PlayerPrefs.SetInt (string.Format ("{0}", notAnswerPictId), 1);
        PlayerPrefs.Save ();

        DeactiveTadashi (tadashiNum);

        Resources.UnloadUnusedAssets ();
    }

    public TadashiEntity GetTadashiEntity (int uniqueId) {
        for (int idx = 0; idx < _tadashiList.Count; idx++) {
            if (_tadashiList[idx].uniqueId == uniqueId) {
                return _tadashiList[idx];
            }
        }

        return null;
    }

    private void SetAnswer () {
        //TODO:確率仮置き
        answerPictId = GetPictIdAppliedWeightPreb ();

        /*
        answerPictId = UnityEngine.Random.Range (1, sprites.Length);
        int pictId = -1;
        while (pictId < 0 || answerPictId == pictId) {
            pictId = UnityEngine.Random.Range (0, sprites.Length);
        }

        notAnswerPictId = pictId;
        */
        notAnswerPictId = 0;
    }

    //TODO:仮置き
    private int GetPictIdAppliedWeightPreb () {
        int sum = 0;
        for (int i = 0; i < sprites.Length - 1; i++) {
            sum += weightProbs[i];
        }

        int pick = UnityEngine.Random.Range (0, sum);

        int select = 0;
        for (int i = 0; i < sprites.Length; i++) {
            select += weightProbs[i];
            if (select >= pick) {
                return (i + 1); //memo:id0をデフォルトに当てているため
            }
        }

        print ("確率計算おかしいよ☆");
        return 1;
    }

    //TODO:仮置き
    private int[] weightProbs = {
        100,
        100,
        100,
        5,
        3,
        2,
    };

    private void RandomPos (GameObject obj) {
        Vector2 pos = new Vector2 ();

        pos.x = UnityEngine.Random.Range ((ScreenWidthMax * -1), ScreenWidthMax);
        pos.y = UnityEngine.Random.Range ((ScreenHeightMax * -1), ScreenHeightMax);

        obj.transform.localPosition = pos;
    }

    private void DeactiveTadashi (int tadashiNum) {
        for (int idx = 0; idx < _tadashiList.Count; idx++) {
            if (tadashiNum > idx) continue;

            if (_tadashiList[idx].gameObject.activeSelf) _tadashiList[idx].gameObject.SetActive (false);
        }
    }

    const float ScreenHeightMax = 4.0f;
    const float ScreenWidthMax = 7.0f;

    const float SpeedMinimum = 0.0f;
    const float SpeedMax = 5.0f;
    const float RotateSpeedMinimux = 0.0f;
    const float RotateSpeedMax = 1.0f;

    const int MoveProbability = 0;
    const int RotateProbability = 0;

    const int TadashiMax = 3;

    List<TadashiEntity> _tadashiList = new List<TadashiEntity> ();
}