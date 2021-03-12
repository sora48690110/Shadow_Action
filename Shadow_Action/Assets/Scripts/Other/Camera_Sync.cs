using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Sync : MonoBehaviour
{
    [SerializeField] GameObject Player;

    Vector3 Camera_Pos;
    void Start()
    {
        
    }


    void Update()
    {
        Camera_Pos = gameObject.transform.position;
        gameObject.transform.position = new Vector3(Player.transform.position.x, Camera_Pos.y, Camera_Pos.z);
    }
}
