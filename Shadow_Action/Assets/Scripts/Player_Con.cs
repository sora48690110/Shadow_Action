using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Con : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Game_Change();
        }

    }
     void Game_Change()
    {
        //ステージ反転作成のために無駄に見えるが必要
        //ステージ切り替えの際にGameDirectorが重複しないように削除
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
        //********************************************************
    }
}
