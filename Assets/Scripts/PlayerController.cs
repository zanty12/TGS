using System;
using Sirenix.OdinInspector;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(CircleCollider2D))]
public class PlayerController : MonoBehaviour
{
    private void Awake()
    {
        PlayerSpawn.Instance.PlayerRb = GetComponent<Rigidbody2D>();
        PlayerSpawn.Instance.PlayerAlive.Value = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //if other has tag kill, destroy self
        if (other.gameObject.CompareTag("Kill"))
        {
            PlayerSpawn.Instance.PlayerAlive.Value = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("You Win!");
        }
    }
}