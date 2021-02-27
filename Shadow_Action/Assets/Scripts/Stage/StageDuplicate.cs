using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StageDuplicate : MonoBehaviour
{
    //参考サイト
    //https://qiita.com/dynamonda/items/2368c24edef187775bcb



    [SerializeField] GameObject assetComponentOrGameObject;
    GameObject Sample;
    string assetPath = "Assets/Prefab/StageParent.prefab";
    string assetPath2 = "Assets/Prefab/Stage/StageParent.prefab";
    void Start()
    {
        //プレハブをそのままの位置で生成
        //PrefabUtility.InstantiatePrefab(assetComponentOrGameObject);



        //指定パスの場所にプレハブを更新（パスに.prefab、まで指定すること）
         Sample = PrefabUtility.LoadPrefabContents(assetPath2);
        //オブジェクト名から（clone）を消す&上下反転
        var obj= Instantiate(Sample)as GameObject;
        obj.transform.localScale = new Vector3(1, -1, 1);
        obj.name = Sample.name;
        //*********************************

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            PrefabUtility.SaveAsPrefabAssetAndConnect(Sample, assetPath, InteractionMode.AutomatedAction);
            PrefabUtility.UnloadPrefabContents(Sample);
        }
    }
}
