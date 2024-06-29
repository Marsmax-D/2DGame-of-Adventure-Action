using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator Anim;

    protected AudioSource DeathAudio;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Anim = GetComponent<Animator>();
        DeathAudio = GetComponent<AudioSource>();
    }

    //死亡动画
    public void Death()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        Destroy(gameObject);
        
        
    }
    public void Jumpon()
    {
        DeathAudio.Play();
        Anim.SetTrigger("death");
    }
}
