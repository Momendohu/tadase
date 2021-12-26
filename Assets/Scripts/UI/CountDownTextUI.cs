using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTextUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText(string str)
    {
        _text.text = str;
    }

    public void SetAtive(bool isActive)
    {
        _text.gameObject.SetActive(isActive);
    }

    [SerializeField]
    private Text _text;
}
