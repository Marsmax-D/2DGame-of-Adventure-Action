using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_frog : Enemy
{
    private Rigidbody2D rb;
    public Transform LDirectionx, RDirectionx;
    private float Lx, Rx;
    private bool FaceDirection = true;
    public float Speed, JumpForce;

    //private Animator Anim;

    public LayerMask Ground;
    private Collider2D Coll;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();

        //Anim = GetComponent<Animator>();

        Coll = GetComponent<Collider2D>();


        transform.DetachChildren();
        Lx = LDirectionx.position.x;
        Rx = RDirectionx.position.x;
        Destroy(LDirectionx.gameObject);
        Destroy(RDirectionx.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        SwitchAnim();
    }
    //敌人的移动
    void Movenment()
    {
        if (FaceDirection)//面朝左边
        {
            if (Coll.IsTouchingLayers(Ground))
            {
                Anim.SetBool("jumping", true);
                rb.velocity = new Vector2(-Speed, JumpForce);
            }
            if (transform.position.x < Lx)
            {
                rb.velocity = new Vector2(0, 0);
                transform.localScale = new Vector3(-1, 1, 1);
                FaceDirection = false;
            }
        }
        else//面朝右边
        {
            if (Coll.IsTouchingLayers(Ground))
            {
                Anim.SetBool("jumping", true);
                rb.velocity = new Vector2(Speed, JumpForce);
            }

            if (transform.position.x > Rx)
            {
                rb.velocity = new Vector2(0, 0);
                transform.localScale = new Vector3(1, 1, 1);
                FaceDirection = true;
            }
        }

    }
    //青蛙动画跳跃下降转换
    void SwitchAnim()
    {
        if (Anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0.1)
            {
                Anim.SetBool("jumping", false);
                Anim.SetBool("falling", true);
            }
        }
        if (Coll.IsTouchingLayers(Ground) && Anim.GetBool("falling"))
        {
            Anim.SetBool("falling", false);
        }
    }
    
}
