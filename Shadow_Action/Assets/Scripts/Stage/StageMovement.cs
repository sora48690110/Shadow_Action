using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//ゲームシーン間の移動
public class StageMovement : MonoBehaviour
{
    bool FrontOrBack = true;



    Quaternion Front_Rotation = new Quaternion(0, 0, 0, 1);
    Quaternion Back_Rotation = new Quaternion(-1, 0, 0, 0);
    Vector3 Front_Pos = new Vector3(0, 1, -20);
    Vector3 Back_Pos = new Vector3(0,1, 20);

    [SerializeField] GameObject MainCamera;
    [SerializeField] GameObject DirectionalLight;
    [SerializeField] GameObject Back_Panel;
    private void Awake()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Change_Stage();
        }
    }

    //ステージ切り替え
    public void Change_Stage()
    {
        FrontOrBack = FrontOrBack ? false : true;
        switch (FrontOrBack)
        {
            case true:
                MainCamera.transform.SetPositionAndRotation(Front_Pos, Front_Rotation);
                DirectionalLight.SetActive(true);
                Back_Panel.SetActive(false);
                break;
            case false:
                MainCamera.transform.SetPositionAndRotation(Back_Pos, Back_Rotation);
                DirectionalLight.SetActive(false);
                Back_Panel.SetActive(true);
                break;
            default:
                break;
        }
    }
    //******************
}
