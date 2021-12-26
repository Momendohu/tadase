using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : SingletonMonoBehaviour<Model> {
    private int _hiScore = 0;

    public int hiScore {
        get => _hiScore;
        set => _hiScore = value;
    }

    public void Initialize () {
        this.hiScore = 0;
    }
}