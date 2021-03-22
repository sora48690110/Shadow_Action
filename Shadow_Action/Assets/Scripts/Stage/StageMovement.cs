using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//表世界・裏世界間の移動処理
public class StageMovement : SingletonMonoBehaviour<StageMovement>
{
    //表世界・裏世界判別用（デフォルト・表）
    //リザルト時参照するためpublic
    public bool frontOrBack { get; private set; }
    //*************************************


    //カメラの向きと座標調整用

    //表世界用
    private Quaternion front_Rot = new Quaternion(0, 0, 0, 0);
    private Vector3 front_Pos = new Vector3(0, 1, -20);
    //++++++++

    //裏世界用
    private Quaternion back_Rot = new Quaternion(-1, 0, 0, 0);
    private Vector3 back_Pos = new Vector3(0,1, 20);
    //++++++++

    //************************


    //それぞれ参照用
    [SerializeField]private GameObject directionalLight;
    [SerializeField]private GameObject back_Panel;
    //**************


    private void Start()
    {
        frontOrBack = true;
    }


    //ステージ切り替え
    public void Change_Stage()
    {
        //表世界・裏世界判別
        frontOrBack = !frontOrBack;
        //******************


        switch (frontOrBack)
        {
            //表世界だった場合
            case true:
                //カメラの向き調整
                Camera.main.transform.SetPositionAndRotation(front_Pos, front_Rot);
                //****************


                //ライトON
                directionalLight.SetActive(true);
                //********


                //暗くするためのパネルOFF
                back_Panel.SetActive(false);
                //***********************

                break;

            //裏世界だった場合
            case false:
                //カメラの向き調整
                Camera.main.transform.SetPositionAndRotation(back_Pos, back_Rot);
                //****************


                //ライトOFF
                directionalLight.SetActive(false);
                //********


                //暗くするためのパネルON
                back_Panel.SetActive(true);
                //***********************

                break;

            default:
                break;
        }
    }
}
