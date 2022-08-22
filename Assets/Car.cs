using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private Rigidbody rb;
    private float rotateDirection;
    public float speed;
    public float speedBoost;
    [SerializeField] private float maxSpeed;
    public float rotateSpeed;
    public float rotateAngle;
    [SerializeField] private float turnBraking;
    public float mass;
    public float HP;
    private TouchController touchController;
    public bool isCarDestroyed;
    public ParticleSystem[] particleDeath;
    public ParticleSystem[] particleDust;
    public bool goToFinish;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = mass/100;
        touchController = GetComponent<TouchController>();
    }
    private void Update()
    {
        rotateDirection = Input.GetAxisRaw("Horizontal");
        Death();
        GoToFinish();
    }
    private void FixedUpdate()
    {
        Moving();
        Rotation();
        GoToFinish();
    }
    private void Rotation()
    {
        if (speed > speedBoost)
        {
            transform.Rotate(0, rotateAngle * rotateSpeed * touchController.moveDirection * Time.deltaTime, 0);
            transform.Rotate(0, rotateAngle * rotateSpeed * rotateDirection * Time.deltaTime, 0);
        }

        if (rotateDirection != 0 && speed > 0)
        {
            speed -= turnBraking*Time.fixedDeltaTime;
        }
        if (touchController.moveDirection != 0 && speed >0)
        {
            speed -= turnBraking * Time.fixedDeltaTime;
        }
    }
    private void Moving()
    {
        //rb.velocity = transform.forward * speed;
        //rb.AddForce(transform.forward * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        if (speed < maxSpeed)
        {
            speed += speedBoost * Time.fixedDeltaTime;
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.Space) && speed > 0)
        {           
            speed -= speed*0.1f;
        }
    }
    private void Death()
    {
        if (isCarDestroyed)
        {
            foreach (ParticleSystem particle in particleDeath)
            {
                particle.Play();
            }
        }
    }
    private void GoToFinish()
    {
        Vector3 finish = new Vector3(0, 0, 500);
        if (goToFinish)
        {
            Debug.Log("finish");
            transform.LookAt(finish);
            transform.position = Vector3.MoveTowards(transform.position, finish, 40 * Time.fixedDeltaTime);
            float distance = Vector3.Distance(finish, transform.position);
            if (distance < 0.3f)
            {
                goToFinish = false;
                foreach (ParticleSystem particle in particleDust)
                {
                    particle.Stop();
                }

            }         
        }
    }
}
