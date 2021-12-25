using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TadashiEntity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialzie(int uniqueId, int pictId)
    {
        ChangeImage(pictId);

        _uniqueId = uniqueId;
        _pictId = pictId;
    }

    public void CheckAnswer()
    {

    }

    public void ChangeImage(int pictId)
    {
        var renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = _sprites[pictId];
    }

    [SerializeField]
    private Sprite[] _sprites;

    private int _uniqueId;
    private int _pictId;
}
