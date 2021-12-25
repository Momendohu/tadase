using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TadashiManager : MonoBehaviour
{
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
        for (int idx = 0; idx < TadashiMax; idx++)
        {
            int uniqueId = 0;

            GameObject obj = (GameObject)Resources.Load("Prefabs/Tadashi");

            Vector2 pos = RandomPos();

            GameObject tadashi = (GameObject)Instantiate(obj, pos, Quaternion.identity);
            TadashiEntity entity = tadashi.GetComponent<TadashiEntity>();
            entity.Initialzie(uniqueId, 3);
        }

        Resources.UnloadUnusedAssets();
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
    const int TadashiMax = 30;

    List<TadashiEntity> _tadashiList = new List<TadashiEntity>();
}
