using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//ゲームシーン間の移動
public class StageMovement : MonoBehaviour
{
    string NowStage;
    Vector3 pos;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Change_Stage(GameObject.Find("Player").transform.position);
        }
    }

    //ステージ切り替え
    public void Change_Stage(Vector3 Player_Pos)
    {
        pos = Player_Pos;
        SceneManager.sceneLoaded += GameSceneLoaded;
        switch (SceneManager.GetActiveScene().name)
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
    }
    //******************

    private void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        var gamemanager = GameObject.Find("Player").GetComponent<Player_Con>();
        gamemanager.Player_Pos(pos);
        SceneManager.sceneLoaded -= GameSceneLoaded;
    }
}
