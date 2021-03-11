using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Con : MonoBehaviour
{
    float Movement = new float();
    Rigidbody rb;
    BoxCollider bc;
    Vector3 pos;

    public GameObject gameDirector;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
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

        //すり抜け
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            pos = gameObject.transform.position;
            bc.enabled = false;
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
        Debug.Log("kokomade");
    }
    //********************


    void OnBecameInvisible()
    {
        Debug.Log("表示外");
        //StageMovement stm = GameObject.Find("GameDirector")?.GetComponent<StageMovement>();
        Debug.Log(gameDirector);
        if(gameDirector!=null)
        gameDirector.GetComponent<StageMovement>().Change_Stage(pos);
    }

}
