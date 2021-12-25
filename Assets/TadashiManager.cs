using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TadashiManager : SingletonMonoBehaviour<TadashiManager>
{
    public int answerPictId;
    public int notAnswerPictId;

    public Sprite[] sprites;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
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

            entity.Initialzie(uniqueId, pictId, pictId == answerPictId, sprites[pictId]);
        }

        DeactiveTadashi(tadashiNum);

        Resources.UnloadUnusedAssets();
    }

    private void SetAnswer()
    {
        answerPictId = Random.Range(0, sprites.Length);
        Debug.Log("ê≥âID : " + answerPictId);

        int pictId = -1;
        while (pictId < 0 || answerPictId == pictId)
        {
            pictId = Random.Range(0, sprites.Length);
        }

        notAnswerPictId = pictId;
        Debug.Log("ïsê≥âID : " + notAnswerPictId);

    }

    private void RandomPos(GameObject obj)
    {
        Vector2 pos = new Vector2();

        pos.x = Random.Range((ScreenWidthMax * -1), ScreenWidthMax);
        pos.y = Random.Range((ScreenHeightMax * -1), ScreenHeightMax);

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

    const float ScreenHeightMax = 5.0f;
    const float ScreenWidthMax = 8.0f;

    const int TadashiMax = 3;

    List<TadashiEntity> _tadashiList = new List<TadashiEntity>();
}
