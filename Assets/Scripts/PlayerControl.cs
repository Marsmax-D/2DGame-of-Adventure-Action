using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    
    private Rigidbody2D rb;
    private Animator anim;
    private  Collider2D coll;
    public Collider2D DisColl;


    public float speed, JumpForce;
    public LayerMask Ground;
    public Transform CeilingCheck,GroundCheck;

    private int DoubleJump;
    private bool IsGround;
    private bool IsHurt;//默认False
    public int CherryCount;

    public Text CherryNum;
    public AudioSource JumpAudio, HurtAudio, CherryAudio;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        coll = GetComponent<CircleCollider2D>();
    }

    // 每帧循环一次
    void FixedUpdate()
    {
        if (!IsHurt)
        {
            Movement();
        }
        
        SwitchAnim();
        IsGround = Physics2D.OverlapCircle(GroundCheck.position,0.2f,Ground);
    }
    public void Update()
    {
        Jump();
        Crouch();
        CherryNum.text = CherryCount.ToString();

    }
    //角色移动
    void Movement()
    {
        //角色移动
        float Move = Input.GetAxis("Horizontal");
        float FaceDirection = Input.GetAxisRaw("Horizontal");

        if (Move!=0f)
        {
            rb.velocity = new Vector2(Move*speed*Time.fixedDeltaTime , rb.velocity.y);
            anim.SetFloat("running",Mathf.Abs(FaceDirection));
        }
        //角色转向
        if (FaceDirection!=0f)
        {
            transform.localScale = new Vector3(FaceDirection,1,1);
        }
        
    }
//角色跳跃
    //void Jump()
    //{
    //    if (Input.GetButton("Jump") && coll.IsTouchingLayers(Ground))
    //    {
    //        JumpAudio.Play();
    //        rb.velocity = new Vector2(rb.velocity.x, JumpForce);
    //        anim.SetBool("Jumpup", true);
    //    }
        
    //}
    void Jump()
    {
        if (IsGround)
        {
            DoubleJump = 1;
        }
        if (Input.GetButtonDown("Jump") && DoubleJump > 0)
        {
            rb.velocity = Vector2.up*JumpForce ;
            DoubleJump--;
            anim.SetBool("Jumpup",true);
        }
        if (Input.GetButtonDown("Jump") && DoubleJump == 0 && IsGround)
        {
            rb.velocity = Vector2.up * JumpForce;
            anim.SetBool("Jumpup", true);
        }
    }
    
        
//角色动画转换
void SwitchAnim()
    {
        anim.SetBool("Idle", false);
        if (rb.velocity.y<0.1f && !coll.IsTouchingLayers(Ground) )
        {
            anim.SetBool("Jumpdown",true);
        }
        if (anim.GetBool("Jumpup"))
        {
            if (rb.velocity.y<0)
            {
                anim.SetBool("Jumpup",false);
                anim.SetBool("Jumpdown",true);
            }
        }
        
        else if (IsHurt)
        {
            anim.SetBool("Hurt",true);
            if (Mathf.Abs(rb.velocity.x)<0.1f)
            {
                anim.SetBool("Hurt",false);
                anim.SetBool("Idle",true);
                IsHurt = false;
            }
        }
        if (coll.IsTouchingLayers(Ground))
        {
            anim.SetBool("Jumpdown",false);
            anim.SetBool("Idle",true);
        }
    }

    //Trigger触发器
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //收集樱桃计数
        if (collision.tag=="Collection")
        {
            CherryAudio.Play();
            //Destroy(collision.gameObject);
            //CherryCount++;
            
            collision.GetComponent<Animator>().Play("IsGot");
             
            
        }
        //延迟死亡触发
        if (collision.tag=="DeathLine")
        {
            
            GetComponent<AudioSource>().enabled = false;
            
            Invoke("ReStart",1f);//延迟1秒

        }
        

    }

    //消灭敌人
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Enemy_frog frog = collision.gameObject.GetComponent<Enemy_frog>();
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (anim.GetBool("Jumpdown"))
            {
                enemy.Jumpon();
                rb.velocity = new Vector2(rb.velocity.x, JumpForce);
                anim.SetBool("Jumpup", true);
            }
            //受伤
            else if (transform.position.x<collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-8, rb.velocity.y);
                HurtAudio.Play(); 
                IsHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(8, rb.velocity.y);
                HurtAudio.Play();
                IsHurt = true;
            }

        }
    }


    void Crouch()
    {
        if (!Physics2D.OverlapCircle(CeilingCheck.position,0.2f,Ground))
        {
            if (Input.GetButton("Crouch"))
            {
                anim.SetBool("crouch", true);
                DisColl.enabled = false;
                
                
            }
            else 
            {
                anim.SetBool("crouch", false);
                DisColl.enabled = true;
                
            }
        }
    }

    //死亡触发
    public void ReStart()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void CherryC()
    {
        CherryCount++;
    }

    //暂停菜单声音暂停
    public void Audiooff()
    {
        GetComponent<AudioSource>().enabled = false;
    }
    public void Audioon()
    {
        GetComponent<AudioSource>().enabled = true;
    }



}
