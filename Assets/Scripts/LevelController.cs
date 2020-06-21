using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public float rotateSpeed;

    public GameObject platform;
    public float platformHeight = 0.4f;
    public float platformGap = 0.1f;
    public float angleTurn = .6f;

    public Material pass;
    public Material block;

    public int floorCount = 30;
    private int lastFloor;

    private GameObject[][] floors;

    // Start is called before the first frame update
    void Start()
    {
        floors = new GameObject[floorCount][];
        lastFloor = floorCount-1;
        generateLevel();
    }

    void generateLevel()
    {
        int lastRandomFloor = floorCount;
        int angleToNotBlock = 30;
        for (int floor = 0; floor < floorCount; floor++)
        {
            floors[floor] = new GameObject[12];

            for (int angle = 30; angle <= 360; angle += 30)
            {
                GameObject p = Instantiate(platform, transform.position+new Vector3(0, (platformHeight + platformGap) * floor, 0), Quaternion.Euler(0, angle + angleTurn * floor, 0));
                p.transform.parent = this.transform;

                if (angleToNotBlock==angle)
                {    
                    p.GetComponent<MeshRenderer>().material = pass;
                    p.gameObject.tag = "Pass";
                }
                else
                    p.GetComponent<MeshRenderer>().material = block;

                floors[floor][(angle/30)-1] = p;
            }
        }
    }

    public void popFloor()
    {
        for (int i=0; i<12; i++)
        {
            floors[lastFloor][i].GetComponent<PlatformController>().pop();
        }

        lastFloor--;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(Vector3.up * rotateSpeed);
    }
}
