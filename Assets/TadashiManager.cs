using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TadashiManager : MonoBehaviour
{
    public int answerPictId;
    public int notAnswerPictId;

    // Start is called before the first frame update
    void Start()
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

        SetAnswer();

        for (int idx = 0; idx < TadashiMax; idx++)
        {
            int uniqueId = 0;

            GameObject obj = (GameObject)Resources.Load("Prefabs/Tadashi");

            Vector2 pos = RandomPos();

            int pictId = idx == 0 ? answerPictId : notAnswerPictId;

            GameObject tadashi = (GameObject)Instantiate(obj, pos, Quaternion.identity);
            TadashiEntity entity = tadashi.GetComponent<TadashiEntity>();
            entity.Initialzie(uniqueId, pictId, pictId == answerPictId);

            _tadashiList.Add(entity);
        }

        Resources.UnloadUnusedAssets();
    }

    private void SetAnswer()
    {
        answerPictId = Random.Range(0, 3);
        Debug.Log("ê≥âID : " + answerPictId);

        int pictId = -1;
        while(pictId < 0 || answerPictId == pictId)
        {
            pictId = Random.Range(0, 3);
        }

        notAnswerPictId = pictId;
        Debug.Log("ïsê≥âID : " + notAnswerPictId);

    }

    private Vector2 RandomPos()
    {
        Vector2 pos = new Vector2();

        pos.x = Random.Range((ScreenWidthMax * -1), ScreenWidthMax);
        pos.y = Random.Range((ScreenHeightMax * -1), ScreenHeightMax);

        return pos;
    }

    const float ScreenHeightMax = 5.0f;
    const float ScreenWidthMax = 8.0f;
    const int TadashiMax = 3;

    List<TadashiEntity> _tadashiList = new List<TadashiEntity>();
}
