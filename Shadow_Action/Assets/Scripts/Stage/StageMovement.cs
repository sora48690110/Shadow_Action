using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//表世界・裏世界間の移動処理
public class StageMovement : MonoBehaviour
{
    //表世界・裏世界判別用（デフォルト・表）
    //リザルト時参照するためpublic
    public bool FrontOrBack = true;
    //*************************************

    //カメラの向きと座標調整用

    //表世界用
    Quaternion Front_Rotation = new Quaternion(0, 0, 0, 1);
    Vector3 Front_Pos = new Vector3(0, 1, -20);
    //********

    //裏世界用
    Quaternion Back_Rotation = new Quaternion(-1, 0, 0, 0);
    Vector3 Back_Pos = new Vector3(0,1, 20);
    //********

    //それぞれ参照用
    [SerializeField] GameObject MainCamera;
    [SerializeField] GameObject DirectionalLight;
    [SerializeField] GameObject Back_Panel;
    //**************


    //ステージ切り替え
    public void Change_Stage()
    {
        //表世界・裏世界判別
        FrontOrBack = !FrontOrBack;
        //******************


        switch (FrontOrBack)
        {
            //表世界だった場合
            case true:
                //カメラの向き調整
                MainCamera.transform.SetPositionAndRotation(Front_Pos, Front_Rotation);
                //****************

                //ライトON
                DirectionalLight.SetActive(true);
                //********

                //暗くするためのパネルOFF
                Back_Panel.SetActive(false);
                //***********************

                break;
            //裏世界だった場合
            case false:
                //カメラの向き調整
                MainCamera.transform.SetPositionAndRotation(Back_Pos, Back_Rotation);
                //****************

                //ライトOFF
                DirectionalLight.SetActive(false);
                //********

                //暗くするためのパネルON
                Back_Panel.SetActive(true);
                //***********************

                break;
            default:
                break;
        }
    }
    //******************
}
