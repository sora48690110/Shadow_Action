using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Gimick_Mane
{

    //実体か影か
    [SerializeField] private bool existenceOrShadow;
    //*********


    //スイッチ本体
    [SerializeField] GameObject change_Switch;
    //************


    //消したい壁
    [SerializeField] GameObject guard_Wall;
    //**********



    void Start()
    {
        //裏世界のみの場合初期で非表示
        if (!existenceOrShadow)
            change_Switch.SetActive(false);
        //****************************
    }


    void Update()
    {
        //表世界・裏世界どちらかで出現
        Existence_Change(change_Switch, existenceOrShadow, StageMovement.Instance.frontOrBack);
    }


    private void OnTriggerEnter(Collider other)
    {
        //プレイヤーが触れたとき
        if (other.CompareTag("Player"))
        {
            //対応した壁を消す
            guard_Wall.SetActive(false);
            //***************


            //スイッチ自身も消す
            gameObject.SetActive(false);
            //******************
        }
    }
}