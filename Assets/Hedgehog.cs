using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hedgehog : MonoBehaviour
{
    [Range(0,10)] public int degreeOfStrength;
    public float damage;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Car car))
        {
            car.HP -= damage;
            if (degreeOfStrength < 10)
            {
                car.speed -= car.speed * degreeOfStrength / 10;
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Game Over");
                car.speed = 0;
                car.speedBoost = 0;
                car.isCarDestroyed = true;
            }
        }
    }
}

