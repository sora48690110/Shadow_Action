using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Dire : MonoBehaviour
{
    //https://note.com/suzukijohnp/n/n050aa20a12f1
    [SerializeField] GameObject Stage;

    void Start()
    {
        //現状即時移動
        SceneManager.sceneLoaded += GameLoaded;
        SceneManager.LoadScene("SampleScene");
        //************
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //シーン移動前に移動先で実行
    private void GameLoaded(Scene next, LoadSceneMode mode)
    {
        //ステージ作成して破棄不能に
        GameObject Parent=Instantiate(Stage);
        DontDestroyOnLoad(Parent);
        //**************************

        //重複対策？？
        SceneManager.sceneLoaded -= GameLoaded;
        //************

    }
    //*******************

}
