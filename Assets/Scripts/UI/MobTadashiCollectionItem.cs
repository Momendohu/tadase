using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobTadashiCollectionItem : MonoBehaviour {
    [SerializeField]
    private Text nameText = null;

    [SerializeField]
    private Image image = null;

    private EntityMobTadashi entity;

    public void Initialize (EntityMobTadashi entity) {
        this.entity = entity;

        nameText.text = this.entity.name;
        this.image.sprite = this.entity.image;
    }
}