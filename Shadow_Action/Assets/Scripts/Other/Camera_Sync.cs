using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//カメラ同期
public class Camera_Sync : MonoBehaviour
{

    [SerializeField] GameObject Player;


    void Update()
    {
        //カメラの位置プレイヤーに同期（X軸のみ）
        transform.position = new Vector3(Player.transform.position.x, transform.position.y, transform.position.z);
        //**************************************
    }
}
