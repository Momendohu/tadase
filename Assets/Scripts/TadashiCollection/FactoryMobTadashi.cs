using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryMobTadashi {
    public struct ArgsMobTadashi {
        public int uid;
        public string name;
        public Sprite image;
    }

    public static GameObject Create (ArgsMobTadashi args) {
        var entity = new EntityMobTadashi ();

        entity.uid = args.uid;
        entity.name = args.name;
        entity.image = args.image;

        var obj = GameObject.Instantiate (Resources.Load<GameObject> ("Prefabs/InGame/UI/MobTadashiCollectionItem"));
        obj.GetComponent<MobTadashiCollectionItem> ().Initialize (entity);

        return obj;
    }
}