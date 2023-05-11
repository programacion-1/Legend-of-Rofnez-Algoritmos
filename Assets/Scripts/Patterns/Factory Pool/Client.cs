using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Pedirle al Factory una bala
            var newBullet = BulletFactory.Instance.GetObject();

            //Seteamos la posicion
            newBullet.transform.position = transform.position;

            //y la rotacion
            newBullet.transform.forward = transform.forward;
        }
    }
}
