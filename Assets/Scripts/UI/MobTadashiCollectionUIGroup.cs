using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobTadashiCollectionUIGroup : MonoBehaviour {
    [SerializeField]
    private Transform contentsRoot;

    void Start () {
        InitializeData ();

        for (int i = 0; i < 7; i++) {
            FactoryMobTadashi.ArgsMobTadashi tadashi;

            if (PlayerPrefs.GetInt (string.Format ("{0}", i), 0) > 0) {
                tadashi = new FactoryMobTadashi.ArgsMobTadashi () {
                    uid = i,
                    name = nameData[i],
                    image = imageData[i],
                };
            } else {
                tadashi = new FactoryMobTadashi.ArgsMobTadashi () {
                    uid = -1,
                    name = "?????",
                    image = Resources.Load<Sprite> ("Images/tadashi/t-1"),
                };
            }

            var obj = FactoryMobTadashi.Create (tadashi);

            obj.transform.SetParent (contentsRoot);
        }
    }

    public void onPushCloseButton () {
        Destroy (this.gameObject);
    }

    //TODO:データ仮置き、他の場所に置く
    public Dictionary<int, string> nameData = new Dictionary<int, string> ();
    public Dictionary<int, Sprite> imageData = new Dictionary<int, Sprite> ();

    public void InitializeData () {
        nameData.Add (0, "ジャスティスただし");
        nameData.Add (1, "ディストーションひずみ");
        nameData.Add (2, "ストップとまり");
        nameData.Add (3, "エンシャントただし");
        nameData.Add (4, "グレートキングおう");
        nameData.Add (5, "アラビアンただし");
        nameData.Add (6, "ユニコードただし");

        for (int i = 0; i < 7; i++) {
            imageData.Add (i, Resources.Load<Sprite> ("Images/tadashi/t" + i));
        }
    }
}