using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMobTadashi {
    private int _uid;
    private string _name;
    private Sprite _image;

    public int uid {
        get => _uid;
        set => _uid = value;
    }

    public string name {
        get => _name;
        set => _name = value;
    }

    public Sprite image {
        get => _image;
        set => _image = value;
    }
}