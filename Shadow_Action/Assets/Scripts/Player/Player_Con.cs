using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤー処理
public class Player_Con : MonoBehaviour
{
    //プレイヤー移動量計算用
    float Movement = new float();
    //**********************


    //リトライ座標保存用
    float Retry_Movement;
    //******************


    //表世界・裏世界切替時座標保存用
    Vector3 Save_Pos;
    //******************************


    //すり抜け可能判定用
    bool Transparent_Check;
    //******************


    //接地判定用
    bool Ground_Check;
    //**********


    Rigidbody rb;
    BoxCollider bc;


    [SerializeField] GameObject gameDirector;
    [SerializeField] GameObject result_Canvas;
    [SerializeField] int Jump_Force;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
        Retry_Movement = 0;
    }


    private void Update()
    {
        //時が止まっている間は処理しない
        if (Mathf.Approximately(Time.timeScale, 0f))
            return;
        //******************************

        //左移動
        if (Input.GetKey(KeyCode.A))
        {
            Movement -= 0.01f;
            Chara_Move(Movement,-1);
        }

        //右移動
        if (Input.GetKey(KeyCode.D))
        {
            Movement += 0.01f;
            Chara_Move(Movement, 0);
        }

        //ジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && Ground_Check)
        {
            Chara_Jump(Jump_Force);
        }

        //すり抜け
        if (Input.GetKeyDown(KeyCode.LeftControl) && Transparent_Check)
        {

            //すり抜ける前の位置保存
            Save_Pos = gameObject.transform.position;
            //**********************

            //BoxCollider無効化
            bc.enabled = false;
            //*****************

            //ジャンプ無効化
            Ground_Check = false;
            //**************
        }
        //ギミックにはじかれた際にずれない用
        Movement = gameObject.transform.position.x;
        //**********************************
    }



    //プレイヤー左右移動
    private void Chara_Move(float movement,int Direction)
    {
        //1文を短くするためのもの
        Vector3 Player_Pos= new Vector3(movement, gameObject.transform.position.y, 0);
        Quaternion Player_Rot= new Quaternion(0, Direction, 0, 0);
        //**********************


        gameObject.transform.SetPositionAndRotation(Player_Pos, Player_Rot);
    }
    //******************


    //プレイヤージャンプ
    private void Chara_Jump(float jump_Force)
    {
        rb.AddForce(transform.up * jump_Force);
    }
    //******************


    private void Chara_Death()
    {
        Debug.Log("死亡");
        //移動量をリトライ位置に変更
        Movement = Retry_Movement;
        //**************************


        //保存座標をリトライ位置に変更
        Save_Pos = new Vector3(0, 0, 0);
        //****************************


        //リザルトUI表示
        //互いに参照しあっている為、いい方法がないか検討中
        result_Canvas.GetComponent<Result_UI>().Active_Result("Game Over");
        //**************


    }


    //表世界・裏世界の移動時位置同期
    public void Player_Pos()
    {
        gameObject.transform.position = Save_Pos;
    }
    //********************


    //カメラの範囲外に出た場合一度だけ実行
    private void OnBecameInvisible()
    {

        //BoxCollider有効化
        bc.enabled = true;
        //*****************


        //すり抜け不可状態で接地していないとき死亡判定
        if (!Transparent_Check && !Ground_Check)
            Chara_Death();
        //********************************************

        //死亡判定じゃないとき
        //表世界・裏世界切り替え(カメラ反転させてるだけ)
        //実行終了時に出るエラーを防ぐためnullチェック
        else if (gameDirector != null)
        {
            gameDirector.GetComponent<StageMovement>().Change_Stage();
            //********************************************


            //プレイヤーの位置修正
            Player_Pos();
            //********************
        }
    }

    //トリガー接触時
    private void OnTriggerStay(Collider other)
    {
        //何であれ接地すればジャンプ可能
        Ground_Check = true;
        //******************************


        //許可された範囲内（本来影ができる場所の予定）であればすり抜け可能
        if (other.CompareTag("Change_Possible"))
            Transparent_Check = true;
        //****************************************
    }


    //トリガー離脱時
    private void OnTriggerExit(Collider other)
    {

        //何であれ離れればジャンプ不可
        Ground_Check = false;
        //******************************


        //許可された範囲内（本来影ができる場所の予定）から離れた場合すり抜け不可
        if (other.CompareTag("Change_Possible"))
            Transparent_Check = false;
        //***************************************


    }
}
