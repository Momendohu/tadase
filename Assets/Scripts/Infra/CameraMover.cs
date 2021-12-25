using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class CameraMover : MonoBehaviour {
    [SerializeField]
    private float easingSpeed = 1f; //カメラ追従速度

    [SerializeField]
    private Vector3 fix = new Vector3 (0, 10, 0); //位置補正

    [SerializeField]
    private Vector3 rot = new Vector3 (75, 0, 0); //角度補正

    [SerializeField]
    private GameObject root; //注目オブジェクト

    private Camera cam; //カメラ

    private void Init () {
        cam = this.GetComponent<Camera> ();
    }

    private void Awake () {
        Init ();
    }

    private void Update () {
        if (root == null) return;

        var goal = root.transform.position + fix;
        transform.position = Vector3.Lerp (transform.position, goal, easingSpeed);

        var goalRotate = rot;
        transform.eulerAngles = Vector3.Lerp (transform.eulerAngles, goalRotate, easingSpeed);
    }
}