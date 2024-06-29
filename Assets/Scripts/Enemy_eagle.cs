 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_eagle : Enemy
{
    private Rigidbody2D rb;
    //private Collider2D Coll;
    public Transform top, buttom;
    public float Speed;
    private float Upy, Downy;

    private bool Isup = true;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        //Coll = GetComponent<Collider2D>();
        Upy = top.position.y;
        Downy = buttom.position.y;
        Destroy(top.gameObject);
        Destroy(buttom.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movenment();
    }
    void Movenment()
    {
        if (Isup)
        {
            rb.velocity = new Vector2(rb.velocity.x,Speed);
            if (transform.position.y>Upy)
            {
                Isup = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x,-Speed);
            if (transform.position.y<Downy)
            {
                Isup = true;
            }
        }
    }
}
