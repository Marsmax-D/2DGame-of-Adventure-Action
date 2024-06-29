using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotCherry : MonoBehaviour
{
    public void Death()
    {
        FindObjectOfType<PlayerControl>().CherryC();
        Destroy(gameObject);
    }
}
