using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TadashiEntity : MonoBehaviour
{
    public bool isAnswer { get { return _isAnswer; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialzie(int uniqueId, int pictId, bool isAnswer, Sprite sprite)
    {
        ChangeImage(sprite);

        _uniqueId = uniqueId;
        _pictId = pictId;
        _isAnswer = isAnswer;
    }

    public void CheckAnswer()
    {
        if (InGameManager.Instance.IsPlayebleStatus())
            return;

        bool isCorrect = false;
        if (_isAnswer)
        {
            isCorrect = true;
            Debug.Log("�����I�I�I�I�I�I�I�I�I�I�I");
        }
        else
        {
            Debug.Log("�s�����E�E�E�E");
        }

        InGameManager.Instance.UpdateLevel(isCorrect);
    }

    public void ChangeImage(Sprite sprite)
    {
        var renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = sprite;
    }

    private int _uniqueId;
    private int _pictId;

    private bool _isAnswer = false;
}
