using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDiolog : MonoBehaviour
{
    public GameObject Enter;

    //提示框的显示
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
            Enter.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Enter.SetActive(false);
        }
    }
}
