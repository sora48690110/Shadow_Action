using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//振動して落下する(未完)
public class Block_VibrationFall : Gimick_Mane
{

    //[SerializeField] float vibration_Width;
    //[SerializeField] float vibration_Speed;
    Rigidbody rb;


    void Start()
    {
        //重力無効化
        rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        //**********
    }


    private void OnTriggerEnter(Collider other)
    {
        //重力有効化
        if(other.CompareTag("Player"))
            rb.isKinematic = false;
        //**********
    }
}
