using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤー処理
public class Player_Con : MonoBehaviour
{
    //プレイヤー移動量計算用
    [SerializeField] float speed;
    [SerializeField] int Jump_Force;
    float moveX;
    //**********************


    //世界渡り時座標同期用
    Vector3 Save_Pos;
    //********************


    //接地判定用
    [SerializeField] bool Ground_Check;
    Ray ground_Ray;
    //**********


    //すり抜け可能判定用
    [SerializeField] bool Transparent_Check;
    Ray transparent_Ray;
    //******************


    //それぞれ参照用
    Rigidbody rb;
    BoxCollider bc;
    [SerializeField] GameObject gameDirector;
    [SerializeField] GameObject result_Canvas;
    [SerializeField] GameObject player_Nose;
    //**************

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
    }

    private void FixedUpdate()
    {
        //移動   
        if (moveX != 0)
            Chara_Move(moveX);

        //接地判定
        Chara_Grounded();

        //すり抜け判定
        Chara_Transparent();
    }

    private void Update()
    {
        //時が止まっている間は処理しない
        if (Mathf.Approximately(Time.timeScale, 0f))
            return;
        //******************************

        //移動量計測
        moveX = Input.GetAxis("Horizontal") * speed;

        
        //ジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && Ground_Check)
        {
            Chara_Jump(Jump_Force);
        }

        //すり抜け
        if (Input.GetKeyDown(KeyCode.LeftControl) && Transparent_Check)
        {

            //すり抜ける前の位置保存
            Save_Pos = transform.position;
            //**********************

            //BoxCollider無効化
            bc.enabled = false;
            player_Nose.GetComponent<BoxCollider>().enabled = false;
            //*****************

        }

    }


    //プレイヤー左右移動
    private void Chara_Move(float movement)
    {
        //向き修正
        if (movement < 0) transform.rotation = new Quaternion(0, -1, 0, 0);
        else transform.rotation = new Quaternion(0, 0, 0, 0);
        //********

        //接地しているとき
        if(Ground_Check)
        rb.velocity = new Vector3(movement, rb.velocity.y, 0);
        //*********
    }


    //プレイヤージャンプ
    private void Chara_Jump(float jump_Force)
    {
        rb.AddForce(transform.up * jump_Force);
    }


    //プレイヤー接地判定
    private void Chara_Grounded()
    {
        //下方向にRayを飛ばして代入
        ground_Ray = new Ray(transform.position + 0.01f * transform.up, -transform.up);
        Ground_Check = Physics.SphereCast(ground_Ray, 0.375f, 0.2f);
        //*************************
    }


    //プレイヤー世界渡り判定
    private void Chara_Transparent()
    {
        //下方向にRayを飛ばして代入
        transparent_Ray = new Ray(transform.position + 0.01f * transform.up, -transform.up);
        Transparent_Check = Physics.SphereCast(transparent_Ray, 0.2f, 0.2f, LayerMask.GetMask("Floor"));
        //*************************
    }


    //死亡処理
    private void Chara_Death()
    {

        //保存座標をリトライ位置に変更
        Save_Pos = new Vector3(0, -5, 0);
        //****************************


        //移動量初期化
        rb.velocity = new Vector3(0, 0, 0);
        //************


        //リザルトUI表示
        //互いに参照しあっている為、いい方法がないか検討中
        if (result_Canvas != null)
            result_Canvas.GetComponent<Result_UI>().Active_Result("Game Over");
        //**************
    }


    //クリア処理
    private void Game_Clear()
    {
        //リザルトUI表示
        //互いに参照しあっている為、いい方法がないか検討中
        if (result_Canvas != null)
            result_Canvas.GetComponent<Result_UI>().Active_Result("Game Clear");
        //保存座標をスタート位置に変更
        Save_Pos = new Vector3(0, -5, 0);
        //****************************
    }


    //世界渡り時位置同期
    //Result_Uiで参照
    public void Chara_PosSync()
    {
        transform.position = Save_Pos;
    }


    //カメラの範囲外に出た場合一度だけ実行
    private void OnBecameInvisible()
    {

        //BoxCollider有効化
        bc.enabled = true;
        player_Nose.GetComponent<BoxCollider>().enabled = true;
        //*****************

        //実行終了時に出るエラーを防ぐためnullチェック
        if (gameDirector != null)
        {
            //範囲外にでたときにレイヤー(Floor)と重なっていたら世界渡り
            if (Physics.CheckSphere(transform.position, 0.375f, LayerMask.GetMask("Floor")))
            {
                //世界渡り(カメラ反転させてるだけ)
                gameDirector.GetComponent<StageMovement>().Change_Stage();
                //********************************************


                //プレイヤーの位置修正
                Chara_PosSync();
                //********************
            }
            //重なっていなかったら死亡判定
            else Chara_Death();
        }
    }


    //トリガー接触時
    private void OnTriggerEnter(Collider other)
    {
        //Clear_Flagに触れた場合
        if (other.CompareTag("Clear_Flag"))
            Game_Clear();
        //****************************************
    }
}
