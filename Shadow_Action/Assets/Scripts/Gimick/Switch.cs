using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Gimick_Mane
{
    //実体か影か
    [SerializeField] private bool ExistenceOrShadow;
    //*********

    [SerializeField] GameObject Change_Switch;

    GameObject gameDirector;

    
    void Start()
    {
        //GameDirector取得
        gameDirector = GameObject.Find("GameDirector");
        //****************

        //裏世界のみの場合初期で非表示
        if (!ExistenceOrShadow)
            Change_Switch.SetActive(false);
        //****************************
    }


    void Update()
    {
        //表世界・裏世界どちらかで出現
        Existence_Change(Change_Switch, ExistenceOrShadow,gameDirector.GetComponent<StageMovement>().FrontOrBack);
    }
}
