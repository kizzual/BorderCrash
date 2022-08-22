using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hedgehog : MonoBehaviour
{
    [Range(0,10)] public int degreeOfStrength;
    private float damage;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Car car))
        {
            damage = degreeOfStrength * 100;
            car.HP -= damage;
            if (car.HP <= 0)
            {
                car.isCarDestroyed = true;
            }
            if (degreeOfStrength < 10)
            {
                car.speed -= car.speed * degreeOfStrength / 10;
                gameObject.SetActive(false);
            }
            else
            {
                car.isCarDestroyed = true;
            }
        }
    }
}

