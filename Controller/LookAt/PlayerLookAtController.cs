using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAtController : MonoBehaviour {

    public Transform player;

    private float axisX;
    private float axisY;
    public float sensitivityX;
    public float sensitivityY;

    public float height;
    public float margin;

    Vector3 rotation;    

    void Awake()
    {
        axisX = 0;
        axisY = transform.rotation.eulerAngles.y;
        rotation = new Vector3(axisX, axisY, 0);
    }

    // Use this for initialization
    void Start () {
        height = player.localScale.y - player.localScale.y/2;
    }
	
	// Update is called once per frame
	void Update () {

        transform.position = player.position + new Vector3(0, height);
        axisY += Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
        axisX -= Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;
        axisX = Mathf.Clamp(axisX, -60, 70);
        rotation.Set(axisX, axisY, 0);
        transform.eulerAngles = rotation;

    }
}
