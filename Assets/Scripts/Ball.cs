using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    private Rigidbody rigidbody;
    private AudioSource audio;
    public float jumpPower = 10;

    private LevelController levelController;

    public bool pressing;
    public int breakCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody>();
        levelController =  GameObject.Find("Level").GetComponent<LevelController>();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0))
        {   
            pressing = true;
            rigidbody.velocity = Vector3.down * jumpPower;
        }
        else if (Input.GetMouseButtonUp(0))
            pressing = false;

        Debug.DrawRay(transform.position, Vector3.down, Color.red);
    }
    
    void FixedUpdate() {
        if (pressing)
        {
            if (rigidbody.velocity.magnitude > 7)
            {
                rigidbody.velocity = rigidbody.velocity.normalized * 7;
            }

            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, .5f))
            {
                if (hit.collider.gameObject.CompareTag("Pass"))
                {
                    Physics.IgnoreCollision(hit.collider, GetComponent<Collider>());
                    popFloor();
                }
            }
        }
    }

    void jump()
    {
        breakCount = 0;
        audio.pitch = 1;
        audio.Play();
        rigidbody.velocity = Vector3.up * jumpPower;
        Vibration.VibratePeek();
    }

    void popFloor()
    {
        levelController.popFloor();
        audio.pitch += 0.03f;
        audio.Play();
        breakCount++;
        Vibration.VibratePeek();
    }

    void OnCollisionEnter(Collision collision)
    {
        jump();
    }
}
