using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TadashiButton : MonoBehaviour {
    public void OnPush () {
        //TODO:イベントの作成
        AudioManager.Instance.PlaySE ("hun");
    }
}