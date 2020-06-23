using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public int angle;
    private bool popped;
    private float speed = 16;
    private MeshCollider collider;
    private Vector3 firstPos;
    private Quaternion targetQuaternion;

    // Start is called before the first frame update
    void Start()
    {
        popped = false;
        firstPos = transform.position;
    }

    void Awake() 
    {
        collider = GetComponent<MeshCollider>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (popped)
        {
            collider.enabled = false;
            transform.parent = null;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + (transform.rotation.eulerAngles.y>180 ? Vector3.right : Vector3.left) + transform.up, Time.deltaTime * speed);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetQuaternion, Time.deltaTime * speed / 2);
            if (Vector3.Distance(transform.position, firstPos)>100)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnBecameInvisible() {
        if(popped)
        {    
            Destroy(this.gameObject);
        }
    }

    public void pop()
    {
        targetQuaternion = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, Random.Range(0, 45)));
        popped = true;
    }
}
