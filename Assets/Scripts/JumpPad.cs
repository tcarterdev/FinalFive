using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float launchForce;
    public Rigidbody playerRB;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerRB.AddForce(0, launchForce, 0, ForceMode.Impulse);
        }
    }
}
