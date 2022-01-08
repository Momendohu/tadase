using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TadashiEntity : MonoBehaviour {
    public bool isAnswer { get { return _isAnswer; } }
    public int uniqueId { get { return _uniqueId; } }

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        transform.Rotate (_rotateSpeed);
        Debug.Log(transform.localEulerAngles.x + " : " + transform.localEulerAngles.y);
        CheckRotation(transform.localEulerAngles.x, transform.localEulerAngles.y);
    }

    public void Initialzie (int uniqueId, int pictId, bool isAnswer, Sprite sprite, float speed, float rotateSpeed, TadashiManager.RotateKind rotateKind) {
        ChangeImage (sprite);

        transform.rotation = Quaternion.identity;;

        _uniqueId = uniqueId;
        _pictId = pictId;
        _isAnswer = isAnswer;

        if (rigidBody == null)
            rigidBody = this.gameObject.GetComponent<Rigidbody2D> ();

        _speed = speed;

        switch (rotateKind) {
            case TadashiManager.RotateKind.None:
                _rotateSpeed = Vector3.zero;
                break;
            case TadashiManager.RotateKind.X:
                _rotateSpeed = new Vector3 (rotateSpeed, 0, 0);
                break;
            case TadashiManager.RotateKind.Y:
                _rotateSpeed = new Vector3 (0, rotateSpeed, 0);
                break;
            case TadashiManager.RotateKind.Z:
                _rotateSpeed = new Vector3 (0, 0, rotateSpeed);
                break;
        }

        rigidBody.velocity = new Vector2 (_speed, _speed);
        _saveVelocity = rigidBody.velocity;
    }

    public bool CheckAnswer () {
        bool isCorrect = false;
        if (_isAnswer) {
            isCorrect = true;
        }

        return isCorrect;
    }

    public void ChangeImage (Sprite sprite) {
        var renderer = GetComponent<SpriteRenderer> ();
        renderer.sprite = sprite;
    }

    void OnTriggerEnter2D (Collider2D other) {
        Vector2 velocity = rigidBody.velocity;

        switch (other.gameObject.tag) {
            case "Wall_Top":
            case "Wall_Under":
                velocity.y *= -1;
                velocity.x = Random.Range (_speed * -1, _speed);
                break;
            case "Wall_Left":
            case "Wall_Right":
                velocity.x *= -1;
                velocity.y = Random.Range (_speed * -1, _speed);
                break;
        }

        if (rigidBody.velocity != velocity)
        {
            rigidBody.velocity = velocity;
            _saveVelocity = velocity;
        }
    }

    void CheckRotation(float rotate_x, float rotate_y)
    {
        if (rotate_x == 90.0f || rotate_x == 270.0f)
            rigidBody.velocity = Vector2.zero;

        if (rotate_y == 90.0f || rotate_y == 270.0f)
            rigidBody.velocity = Vector2.zero;

        if (rigidBody.velocity == Vector2.zero && rigidBody.velocity != _saveVelocity)
            rigidBody.velocity = _saveVelocity;
    }

    private int _uniqueId;
    private int _pictId;

    private bool _isAnswer = false;

    private Rigidbody2D rigidBody = null;
    private float _speed = 0f;
    private Vector3 _rotateSpeed = Vector3.zero;
    private Vector2 _saveVelocity = Vector2.zero;
}