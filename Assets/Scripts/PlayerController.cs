using System;
using Sirenix.OdinInspector;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(CircleCollider2D))]
public class PlayerController : MonoBehaviour
{
    private PlayerSpawn _playerSpawn;

    private void Awake()
    {
        _playerSpawn = FindFirstObjectByType<PlayerSpawn>();

        _playerSpawn.playerRb = GetComponent<Rigidbody2D>();
        _playerSpawn.playerAlive.Value = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //if other has tag kill, destroy self
        if (other.gameObject.CompareTag("Kill"))
        {
            _playerSpawn.playerAlive.Value = false;
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