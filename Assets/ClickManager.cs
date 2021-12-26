using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public InGameManager inGameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ClickGameObject();
    }

    private void ClickGameObject()
    {
        // 左クリックされた場所のオブジェクトを取得
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] collider2ds = Physics2D.OverlapPointAll(tapPoint);

            for (int idx = 0; idx < collider2ds.Length; idx++)
            {
                if (collider2ds[idx] == null)
                    continue;

                var entity = collider2ds[idx].transform.gameObject.GetComponent<TadashiEntity>();

                if (entity == null)
                    continue;

                if (entity.isAnswer || collider2ds.Length == (idx + 1))
                {
                    inGameManager.CheckAnswer(entity);
                }
            }
        }
    }
}
