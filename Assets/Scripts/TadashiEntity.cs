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
        _image = _sprite[pictId];

        _uniqueId = uniqueId;
        _pictId = pictId;
    }

    public void CheckAnswer()
    {

    }

    public void ChangeButtonImage()
    {
        
    }

    [SerializeField]
    private Sprite[] _sprite;

    [SerializeField]
    private Sprite _image;

    private int _uniqueId;
    private int _pictId;
}
