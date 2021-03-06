using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask : MonoBehaviour
{
    public float turnSpeed = 90f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Virus>() != null || 
            other.gameObject.GetComponent<Bat>() != null ||
            other.gameObject.GetComponent<People>() != null ||
            other.gameObject.GetComponent<Arch>() != null)
        {
            Destroy(gameObject);
            return;
        }
        // check the object we collide with is the player
        if (other.gameObject.name != "Player") return;

        // Add to the player's score
        if (!GameManager.inst.isDoctor)
            GameManager.inst.IncrementMask();

        // Destroy the mask object
        Destroy(gameObject);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
    }
}
