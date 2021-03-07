using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StageMovement : MonoBehaviour
{
    public string NowStage { get; set; }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        NowStage = SceneManager.GetActiveScene().name;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Change_Stage();
        }
    }

    //ステージ切り替え
    void Change_Stage()
    {
       gameObject.GetComponent<StageDuplicate>().Prefab_Update();
        NowStage = SceneManager.GetActiveScene().name;
        switch (NowStage)
        {
            case "SampleScene":
                SceneManager.LoadScene("SampleScene2");
                break;
            case "SampleScene2":
                SceneManager.LoadScene("SampleScene");
                break;
            default:
                break;
        }
        //ステージ反転作成のために無駄に見えるが必要
        //ステージ切り替えの際にGameDirectorが重複しないように削除
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
        //********************************************************
    }
    //******************
}
