using System;
using Sirenix.OdinInspector;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(CircleCollider2D))]
public class PlayerController : MonoBehaviour
{
    private void Start()
    {
        PlayerSpawn.Instance.PlayersAlive.Value += 1;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //ShotHistory.Instance.AddPoint(other.contacts[0].point);
        //if other has tag kill, destroy self
        if (other.gameObject.CompareTag("Kill"))
        {
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

    private void OnDestroy()
    {
        PlayerSpawn.Instance.PlayersAlive.Value -= 1;
    }
}