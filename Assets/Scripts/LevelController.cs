using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public float rotateSpeed;
    public GameObject platform;
    private PlatformController platformController;
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
        platformController = platform.GetComponent<PlatformController>();
        
        floors = new GameObject[floorCount][];
        lastFloor = floorCount-1;
        generateLevel();
    }

    void generateLevel()
    {
        int lastRandomFloor = floorCount;
        for (int floor = 0; floor < floorCount; floor++)
        {
            floors[floor] = new GameObject[360/platformController.angle];

            for (int angle = platformController.angle; angle <= 360; angle += platformController.angle)
            {
                GameObject p = Instantiate(platform, transform.position+new Vector3(0, (platformHeight + platformGap) * floor, 0), Quaternion.Euler(0, angle + angleTurn * floor, 0));
                p.transform.parent = this.transform;

                if (platformController.angle==angle)
                {    
                    p.GetComponent<MeshRenderer>().material = pass;
                    p.gameObject.tag = "Pass";
                }
                else
                    p.GetComponent<MeshRenderer>().material = block;

                floors[floor][(angle/platformController.angle)-1] = p;
            }
        }
    }

    public void popFloor()
    {
        for (int i=0; i<360/platformController.angle; i++)
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
