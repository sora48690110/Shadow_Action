using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ギミックの元締め
public class Gimick_Mane : MonoBehaviour
{

    //移動量&向き
    private int amount;
    private int direction = 1;
    //***********


    //表世界・裏世界どちらかで出現させる
    public void Existence_Change(GameObject gameObject, bool flag,bool FrontOrBack)
    {
        if(flag)
            gameObject.SetActive(FrontOrBack);
        else
            gameObject.SetActive(!FrontOrBack);

    }


    //反復移動
    public void Iteration(GameObject gameObject,int width, Vector3 vec)
    {
        //移動量カウント
        amount += 1;
        //**************


        //位置更新
        gameObject.transform.Translate(direction * vec * Time.deltaTime);
        //********


        //リセットして反転
        if (amount % width == 0)
        {
            amount = 0;
            direction *= -1;
        }
        //****************
    }


    //回転
    public void Roaling(GameObject gameObject,Vector3 direction_Amount)
    {
        gameObject.transform.Rotate(direction_Amount);
    }


    //振動(未完成)
    public void Vibration(GameObject gameObject,Vector3 pos,float width,float speed)
    {
        gameObject.transform.position = pos + new Vector3(Mathf.Sin(Time.deltaTime * speed) * width, 0, 0);
    }
}
