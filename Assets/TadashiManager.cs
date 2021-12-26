using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TadashiManager : MonoBehaviour
{
    public int answerPictId;
    public int notAnswerPictId;

    public Sprite[] sprites;

    public enum RotateKind
    {
        None = -1,
        X,
        Y,
        Z
    }

    // Start is called before the first frame update
    void Awake()
    {
        Initialzie();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Initialzie()
    {
        _tadashiList.Clear();
    }

    public void TadashiSetting(int tadashiNum)
    {
        SetAnswer();

        float speed = 0;
        float rotateSpeed = 0;
        RotateKind rotateKind = RotateKind.None;

        bool isMove = UnityEngine.Random.Range(0, MoveProbability) == 0;
        bool isRotate = UnityEngine.Random.Range(0, RotateProbability) == 0;

        if(isMove)
            speed = UnityEngine.Random.Range(SpeedMinimum, SpeedMax);

        if (isRotate)
        {
            rotateSpeed = UnityEngine.Random.Range(RotateSpeedMinimux, RotateSpeedMax);
            rotateKind = (RotateKind)UnityEngine.Random.Range(0, Enum.GetValues(typeof(RotateKind)).Length);
        }

        for (int idx = 0; idx < tadashiNum; idx++)
        {
            int uniqueId = 0;
            int pictId = idx == 0 ? answerPictId : notAnswerPictId;

            TadashiEntity entity;

            if ((_tadashiList.Count - 1) < idx)
            {
                GameObject obj = (GameObject)Resources.Load("Prefabs/Tadashi");

                GameObject tadashi = (GameObject)Instantiate(obj, Vector2.zero, Quaternion.identity);
                entity = tadashi.GetComponent<TadashiEntity>();
                _tadashiList.Add(entity);
                Debug.Log("ìoò^ÅIÅI : " + idx);
            }
            else
            {
                entity = _tadashiList[idx];
                entity.gameObject.SetActive(true);
            }

            RandomPos(entity.gameObject);

            entity.Initialzie(uniqueId, pictId, pictId == answerPictId, sprites[pictId], speed, rotateSpeed, rotateKind);

            uniqueId++;
        }

        DeactiveTadashi(tadashiNum);

        Resources.UnloadUnusedAssets();
    }

    public TadashiEntity GetTadashiEntity(int uniqueId)
    {
        for(int idx = 0; idx < _tadashiList.Count; idx++)
        {
            if(_tadashiList[idx].uniqueId == uniqueId)
            {
                return _tadashiList[idx];
            }
        }

        return null;
    }

    private void SetAnswer()
    {
        answerPictId = UnityEngine.Random.Range(0, sprites.Length);
        Debug.Log("ê≥âID : " + answerPictId);

        int pictId = -1;
        while (pictId < 0 || answerPictId == pictId)
        {
            pictId = UnityEngine.Random.Range(0, sprites.Length);
        }

        notAnswerPictId = pictId;
        Debug.Log("ïsê≥âID : " + notAnswerPictId);

    }

    private void RandomPos(GameObject obj)
    {
        Vector2 pos = new Vector2();

        pos.x = UnityEngine.Random.Range((ScreenWidthMax * -1), ScreenWidthMax);
        pos.y = UnityEngine.Random.Range((ScreenHeightMax * -1), ScreenHeightMax);

        obj.transform.localPosition = pos;
    }

    private void DeactiveTadashi(int tadashiNum)
    {
        for(int idx = 0; idx < _tadashiList.Count; idx++)
        {
            if (tadashiNum > idx)
                continue;

            if (_tadashiList[idx].gameObject.activeSelf)
                _tadashiList[idx].gameObject.SetActive(false);
        }
    }

    const float ScreenHeightMax = 4.0f;
    const float ScreenWidthMax = 7.0f;

    const float SpeedMinimum = 0.0f;
    const float SpeedMax = 5.0f;
    const float RotateSpeedMinimux = 0.0f;
    const float RotateSpeedMax = 1.0f;

    const int MoveProbability = 5;
    const int RotateProbability = 5;

    const int TadashiMax = 3;

    List<TadashiEntity> _tadashiList = new List<TadashiEntity>();
}
