using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSDestroyZone : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("Rubble"))
        {
            Destroy(collision.gameObject);
        }
    }
}
