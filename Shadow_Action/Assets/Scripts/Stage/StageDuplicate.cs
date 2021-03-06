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
    GameObject obj;
    //****************


    //プレハブのパス
    string Start_Path = "Assets/Resouces/Stage/StageParent.prefab";
    string Next_Path = "Assets/Resouces/Stage/NextParent.prefab";
    //*************


    //Directorが破棄される都合上初回判定用
    public static bool FirstGame = true;
    //************************************
    void Start()
    {
        DontDestroyOnLoad(gameObject);
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
            Instantiate(Sample);
            FirstGame = false;
        }
        else
        {
            //指定パスのプレハブ読み込み（パスに.prefab、まで指定すること）
            Sample = PrefabUtility.LoadPrefabContents(Next_Path);
            obj = Instantiate(Sample);
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
        //******************


    }



    //プレハブ更新処理
    void Prefab_Update()
    {
        //変更前のプレハブ読み込み
        Sample = PrefabUtility.LoadPrefabContents(Next_Path);
        GameObject[] ParentGameObjects = Sample.GetComponentsInChildren<Transform>().Select(t => t.gameObject).ToArray();
        //************************


        //シーン上にあるプレハブのインスタンス取得
        GameObject[] childGameObjects = obj.GetComponentsInChildren<Transform>().Select(t => t.gameObject).ToArray();
        //****************************************


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
        //******************
    }
}
