using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//振動して落下する
public class Block_VibrationFall : Gimick_Mane
{

    //振動に必要
    [SerializeField]private float vibration_Width;
    [SerializeField]private float vibration_Speed;
    private Vector3 start_Pos;
    //**********


    //落下用
    [SerializeField]private float limit_Time;
    private float count_Time;
    private bool count_Flag;
    private Rigidbody rb;
    //******


   private void Start()
    {

        //重力無効化
        rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        //**********


        //初期位置保存
        start_Pos = transform.position;
        //************
    }


    private void Update()
    {
        //計測有効時
        if (count_Flag)
        {

            //制限時間を超えたら落下する
            if (count_Time < limit_Time)
            {

                //乗っている時間計測
                count_Time += Time.deltaTime;
                //******************


                //振動
                Vibration(gameObject, start_Pos, vibration_Width, vibration_Speed);
                vibration_Width *= -1;
                //*****

            }
            else rb.isKinematic = false;

        }
        //***********

    }

    //トリガー接触時
    private void OnTriggerEnter(Collider other)
    {

        //計測有効化
        if (other.CompareTag("Player"))
        {
            count_Flag = true;
        }
        //**********

    }

    //トリガー離脱時
    private void OnTriggerExit(Collider other)
    {

        //制限時間超えてなければ位置修正
        if (other.CompareTag("Player"))
        {
            if (count_Time < limit_Time)
                transform.position = start_Pos;
        }
        //********************************


        //計測無効化
        count_Flag = false;
        //**********

    }
}
