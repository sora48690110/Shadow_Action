using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//リザルト処理
public class Result_UI : MonoBehaviour
{
    //それぞれ参照用
    [SerializeField] GameObject player;
    [SerializeField] GameObject gameDirector;
    [SerializeField] GameObject result_Panel;
    [SerializeField] Text game_Text;
    //**************

    //リザルトUI表示
    public void Active_Result(string result_Text)
    {
        //テキストを結果に変更
        game_Text.text = result_Text;
        //*******************

        //リザルトUI表示
        result_Panel.SetActive(true);
        //**************

        //時を止める
        Time.timeScale = 0;
        //**********
    }
    //*************

    //ゲームリトライ
    public void Game_Retry()
    {
        //裏世界だった場合表世界に戻す
        if (!gameDirector.GetComponent<StageMovement>().FrontOrBack)
            gameDirector.GetComponent<StageMovement>().Change_Stage();
        //****************************


        //プレイヤーの位置をリトライ位置に戻す
        player.GetComponent<Player_Con>().Chara_PosSync();
        //***********************************


        //リザルトUI非表示
        result_Panel.SetActive(false);
        //****************


        //時を戻す
        Time.timeScale = 1;
        //********

    }
    //************

    //タイトルに戻る
    public void Game_BackTitle()
    {
        //時を戻す
        Time.timeScale = 1;
        //********


        //タイトルに戻る
        SceneManager.LoadScene("Title");
        //**************

    }
    //*************
}
