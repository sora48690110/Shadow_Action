using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Con : MonoBehaviour
{
    float Movement = new float();
    Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {

        //左移動
        if (Input.GetKey(KeyCode.A))
        {
            Movement -= 0.01f;
            Chara_Move(Movement);
        }

        //右移動
        if (Input.GetKey(KeyCode.D))
        {
            Movement += 0.01f;
            Chara_Move(Movement);
        }

        //ジャンプ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Chara_Jump(500);
        }

    }



    //プレイヤー左右移動
    private void Chara_Move(float movement)
    {
        gameObject.transform.position = new Vector3(movement, gameObject.transform.position.y);
    }
    //******************


    //プレイヤージャンプ
    private void Chara_Jump(float Jump)
    {
        rb.AddForce(transform.up * Jump);
    }
    //******************


    //シーン移動時位置同期
    public void Player_Pos(Vector3 pos)
    {
        Movement = pos.x;
        gameObject.transform.position=pos;
    }
    //********************


}
