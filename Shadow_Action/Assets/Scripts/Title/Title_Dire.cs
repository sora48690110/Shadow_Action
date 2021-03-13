using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//タイトル処理
public class Title_Dire : MonoBehaviour
{

    //ゲームスタート
    public void Game_Start()
    {
        SceneManager.LoadScene("SampleScene");
    }
    //**************

    //ゲーム終了
    public void Game_End()
    {
        Application.Quit();
    }
    //*********
}
