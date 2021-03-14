using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//振動する(未完)
[RequireComponent(typeof(Rigidbody))]
public class Block_Vibration : Gimick_Mane
{

    //[SerializeField] float vibration_Width;
    //[SerializeField] float vibration_Speed;
    Rigidbody rb;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Vibration(gameObject, gameObject.transform.position, vibration_Width, vibration_Speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            rb.isKinematic = false;

    }
}
