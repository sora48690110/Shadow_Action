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
    GameObject obj;
    //[SerializeField] GameObject assetComponentOrGameObject;
    GameObject Sample;


    string assetPath = "Assets/Resouces/Stage/NextParent.prefab";
    string Start_Path = "Assets/Resouces/Stage/StageParent.prefab";
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        //プレハブをそのままの位置で生成
        //PrefabUtility.InstantiatePrefab(assetComponentOrGameObject);
        switch (gameObject.GetComponent<StageMovement>().NowStage)
        {
            case "SampleScene":
                //指定パスの場所にプレハブを更新（パスに.prefab、まで指定すること）
                Sample = PrefabUtility.LoadPrefabContents(Start_Path);
                //オブジェクト名から（clone）を消す
                obj = Instantiate(Sample);
                //*********************************
                
                break;
            case "SampleScene2":
                //指定パスの場所にプレハブを更新（パスに.prefab、まで指定すること）
                Sample = PrefabUtility.LoadPrefabContents(assetPath);
                //オブジェクト名から（clone）を消す&上下反転
                obj = Instantiate(Sample);
                obj.transform.rotation = Quaternion.AngleAxis(180, new Vector3(1, 0, 0));
                break;
            default:
                break;

        }
        //プレハブの中身更新
        PrefabUtility.SaveAsPrefabAsset(Sample, assetPath);
        PrefabUtility.UnloadPrefabContents(Sample);
        //******************

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {

            GameObject[] childGameObjects = obj.GetComponentsInChildren<Transform>().Select(t => t.gameObject).ToArray();
            Sample = PrefabUtility.LoadPrefabContents(assetPath);
            GameObject[] ParentGameObjects = Sample.GetComponentsInChildren<Transform>().Select(t => t.gameObject).ToArray();
            for (int i = 0; i < childGameObjects.Length; i++)
            {
                ParentGameObjects[i].transform.position = childGameObjects[i].transform.position;
                ParentGameObjects[i].transform.rotation = childGameObjects[i].transform.rotation;
                ParentGameObjects[i].transform.localScale = childGameObjects[i].transform.localScale;
            }
            //プレハブの中身更新
            PrefabUtility.SaveAsPrefabAssetAndConnect(Sample, assetPath,InteractionMode.AutomatedAction);
            PrefabUtility.UnloadPrefabContents(Sample);
            //******************
        }

    }
}
