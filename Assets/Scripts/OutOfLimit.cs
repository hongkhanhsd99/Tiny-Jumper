using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfLimit : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagConst.PLATTFORM))
        {
            Destroy(collision.gameObject);
        }
    }
}
