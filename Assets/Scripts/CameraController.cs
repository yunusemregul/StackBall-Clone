using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject ball;
    public GameObject level;
    public float followHeight = 1;
    private Vector3 lastPos;
    private Vector3 min;

    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (ball.transform.position.y + followHeight < transform.position.y && transform.position.y-4 > level.transform.position.y)
        {
            Vector3 target = new Vector3(transform.position.x, ball.transform.position.y + followHeight, transform.position.z);

            transform.position = Vector3.Lerp(lastPos, target, Time.time * 0.5f);
        }
    }
}
