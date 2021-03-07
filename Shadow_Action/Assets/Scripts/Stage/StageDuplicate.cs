using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class StageDuplicate : MonoBehaviour
{
    //参考サイト
    //https://qiita.com/dynamonda/items/2368c24edef187775bcb
    //https://www.hanachiru-blog.com/entry/2019/07/20/000000

    //プレハブ情報維持
    GameObject Sample;
    GameObject Sample2;
    GameObject obj;
    GameObject obj2;
    //****************


    //プレハブのパス
    string Start_Path   = "Assets/Resouces/Stage/StageParent.prefab";
    string Next_Path    = "Assets/Resouces/Stage/NextParent.prefab";
    string Player_Path  = "Assets/Resouces/Player/Player.prefab";
    string Player2_Path = "Assets/Resouces/Player/Player2.prefab";
    //*************


    //Directorが破棄される都合上初回判定用
    public static bool FirstGame = true;
    //************************************
    void Start()
    {
        //ステージ作成
        Prefab_Instance();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //プレハブ更新
            Prefab_Update();
        }

    }

    //プレハブからステージ作成
    void Prefab_Instance()
    {
        if (FirstGame)
        {
            //指定パスのプレハブ読み込み（パスに.prefab、まで指定すること）
            Sample = PrefabUtility.LoadPrefabContents(Start_Path);
            obj=Instantiate(Sample);
            Sample2 = PrefabUtility.LoadPrefabContents(Player_Path);
            obj2=Instantiate(Sample2);
            obj2.name = "Player";
            FirstGame = false;
        }
        else
        {
            //指定パスのプレハブ読み込み（パスに.prefab、まで指定すること）
            Sample = PrefabUtility.LoadPrefabContents(Next_Path);
            obj = Instantiate(Sample);
            Sample2 = PrefabUtility.LoadPrefabContents(Player2_Path);
            obj2=Instantiate(Sample2);
            obj2.name = "Player";
            //************************************************************


            //表と裏で反転
            switch (gameObject.GetComponent<StageMovement>().NowStage)
            {
                case "SampleScene":
                    //上下正常化（重力含む）
                    Physics.gravity = new Vector3(0, -10f, 0);
                    obj.transform.rotation = Quaternion.AngleAxis(0, new Vector3(1, 0, 0));
                    //********************


                    break;
                case "SampleScene2":
                    //上下反転化（重力含む）
                    Physics.gravity = new Vector3(0, 10f, 0);
                    obj.transform.rotation = Quaternion.AngleAxis(180, new Vector3(1, 0, 0));
                    //*********************


                    break;
                default:
                    break;
            }
            //***********


        }
        //プレハブの中身更新
        PrefabUtility.SaveAsPrefabAsset(Sample, Next_Path);
        PrefabUtility.UnloadPrefabContents(Sample);
        PrefabUtility.SaveAsPrefabAsset(Sample2, Player2_Path);
        PrefabUtility.UnloadPrefabContents(Sample2);
        //******************


    }



    //プレハブ更新処理
    public void Prefab_Update()
    {
        //変更前のプレハブ読み込み
        Sample = PrefabUtility.LoadPrefabContents(Next_Path);
        GameObject[] ParentGameObjects = Sample.GetComponentsInChildren<Transform>().Select(t => t.gameObject).ToArray();
        //************************


        //シーン上にあるプレハブのインスタンス取得
        GameObject[] childGameObjects = obj.GetComponentsInChildren<Transform>().Select(t => t.gameObject).ToArray();
        //****************************************

        Sample2 = PrefabUtility.LoadPrefabContents(Player2_Path);
        Sample2.transform.position = GameObject.Find("Player").transform.position;
        Sample2.transform.rotation = GameObject.Find("Player").transform.rotation;
        Sample2.transform.localScale = GameObject.Find("Player").transform.localScale;


        //それぞれ位置・回転・サイズの同期
        for (int i = 0; i < childGameObjects.Length; i++)
        {
            ParentGameObjects[i].transform.position = childGameObjects[i].transform.position;
            ParentGameObjects[i].transform.rotation = childGameObjects[i].transform.rotation;
            ParentGameObjects[i].transform.localScale = childGameObjects[i].transform.localScale;

        }
        //********************************


        //プレハブの中身更新
        PrefabUtility.SaveAsPrefabAssetAndConnect(Sample, Next_Path, InteractionMode.AutomatedAction);
        PrefabUtility.UnloadPrefabContents(Sample);
        PrefabUtility.SaveAsPrefabAssetAndConnect(Sample2, Player2_Path, InteractionMode.AutomatedAction);
        PrefabUtility.UnloadPrefabContents(Sample2);
        //******************
    }
}
