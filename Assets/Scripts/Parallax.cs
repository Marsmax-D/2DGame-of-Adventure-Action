using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    //背景跟随相机移动
    public Transform Camin;
    public float Moverate;
    private float StartPoint;
    // Start is called before the first frame update
    void Start()
    {
        StartPoint = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(StartPoint+Camin.position.x*Moverate,transform.position.y);
    }
}
