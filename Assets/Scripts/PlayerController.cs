using System;
using Sirenix.OdinInspector;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(CircleCollider2D))]
public class PlayerController : MonoBehaviour
{
    protected ColorState colorState = ColorState.White;
    private ShotHistory _shotHistory;

    private SpriteRenderer _spriteRenderer;
    private void Start()
    {
        PlayerSpawn.Instance.PlayersAlive.Value += 1;
        _shotHistory = ShotHistoryManager.Instance.CreateShotHistory();
        _shotHistory.AddPoint(transform.position);

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = StateToColor(colorState);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _shotHistory.AddPoint(other.contacts[0].point);
        //if other has tag kill, destroy self
        if (other.gameObject.CompareTag("Kill"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _shotHistory.AddPoint(transform.position);
        if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("You Win!");
        }

        if (other.gameObject.CompareTag("Player"))
        {
            //merge colors
            var otherPlayer = other.gameObject.GetComponent<PlayerController>();
            colorState = MergeColors(colorState, otherPlayer.colorState);
            // destroy the younger player
            Destroy(otherPlayer.gameObject.GetInstanceID() < gameObject.GetInstanceID()
                ? gameObject
                : other.gameObject);
        }
    }

    private void OnDestroy()
    {
        PlayerSpawn.Instance.PlayersAlive.Value -= 1;
    }

    public void SetColor(ColorState color)
    {
        colorState = color;
        _spriteRenderer.color = StateToColor(colorState);
    }

    public enum ColorState
    {
        Red,
        Green,
        Blue,
        Yellow,
        Magenta,
        Cyan,
        White,
    }

    private ColorState MergeColors(ColorState color1, ColorState color2)
    {
        if (color1 == color2)
        {
            return color1;
        }
        if (color1 == ColorState.Red && color2 == ColorState.Blue || color1 == ColorState.Blue && color2 == ColorState.Red)
        {
            return ColorState.Magenta;
        }
        if (color1 == ColorState.Red && color2 == ColorState.Green || color1 == ColorState.Green && color2 == ColorState.Red)
        {
            return ColorState.Yellow;
        }
        if (color1 == ColorState.Blue && color2 == ColorState.Green || color1 == ColorState.Green && color2 == ColorState.Blue)
        {
            return ColorState.Cyan;
        }
        return ColorState.White;
    }
    private Color StateToColor(ColorState colorState)
    {
        return colorState switch
        {
            ColorState.Red => UnityEngine.Color.red,
            ColorState.Green => UnityEngine.Color.green,
            ColorState.Blue => UnityEngine.Color.blue,
            ColorState.Yellow => UnityEngine.Color.yellow,
            ColorState.Magenta => UnityEngine.Color.magenta,
            ColorState.Cyan => UnityEngine.Color.cyan,
            ColorState.White => UnityEngine.Color.white,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}