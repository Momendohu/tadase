using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TadashiEntity : MonoBehaviour
{
    public bool isAnswer { get { return _isAnswer; } }
    public int uniqueId { get { return _uniqueId; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(_rotateSpeed);
    }

    public void Initialzie(int uniqueId, int pictId, bool isAnswer, Sprite sprite, float speed, float rotateSpeed)
    {
        ChangeImage(sprite);

        transform.rotation = Quaternion.identity; ;

        _uniqueId = uniqueId;
        _pictId = pictId;
        _isAnswer = isAnswer;

        if (rigidBody == null)
            rigidBody = this.gameObject.GetComponent<Rigidbody2D>();

        _speed = speed;
        _rotateSpeed = new Vector3(0, 0, rotateSpeed);

        rigidBody.velocity = new Vector2(_speed, _speed);
    }

    public bool CheckAnswer()
    {
        bool isCorrect = false;
        if (_isAnswer)
        {
            isCorrect = true;
            Debug.Log("正解！！！！！！！！！！！");
        }
        else
        {
            Debug.Log("不正解・・・・");
        }

        return isCorrect;
    }

    public void ChangeImage(Sprite sprite)
    {
        var renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = sprite;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 velocity = rigidBody.velocity;

        switch (other.gameObject.tag)
        {
            case "Wall_Top":
            case "Wall_Under":
                velocity.y *= -1;
                velocity.x = Random.Range(_speed * -1, _speed);
                break;
            case "Wall_Left":
            case "Wall_Right":
                velocity.x *= -1;
                velocity.y = Random.Range(_speed * -1, _speed);
                break;
        }

        if (rigidBody.velocity != velocity)
            rigidBody.velocity = velocity;
    }

    private int _uniqueId;
    private int _pictId;

    private bool _isAnswer = false;

    private Rigidbody2D rigidBody = null;
    private float _speed = 0f;
    private Vector3 _rotateSpeed = Vector3.zero;
}
