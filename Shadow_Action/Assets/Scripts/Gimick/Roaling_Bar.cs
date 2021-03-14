using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//回転実行
public class Roaling_Bar : Gimick_Mane
{
    //軸ごとの回転量
    [SerializeField] Vector3 direction_Amount;
    //**************
    void Update()
    {
        //時止時無効
        if (Mathf.Approximately(Time.timeScale, 0f))
            return;
        //**********

        //回転
        Roaling(gameObject, direction_Amount);
        //****
    }
}
