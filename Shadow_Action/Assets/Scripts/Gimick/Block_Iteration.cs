using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//反復移動実行
public class Block_Iteration : Gimick_Mane
{
    //移動幅
    [SerializeField] private int width;
    //******


    //向き指定
    [SerializeField] private bool left;
    [SerializeField] private bool right;
    [SerializeField] private bool up;
    //********


    void Update()
    {
        //時止時無効
        if (Mathf.Approximately(Time.timeScale, 0f))
            return;
        //**********

        //上下
        if (up)
            Iteration(gameObject, width, Vector3.up);

        //右(斜め用に二つ用意)
        if(right)
            Iteration(gameObject, width, Vector3.right);

        //左(斜め用に二つ用意)
        if (left)
            Iteration(gameObject, width, -Vector3.right);

    }
}
