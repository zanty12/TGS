using System;
using Sirenix.OdinInspector;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    protected ColorState colorState = ColorState.White;
    private ShotHistory _shotHistory;


    private void Start()
    {
        _shotHistory = ShotHistoryManager.Instance.CreateShotHistory();
        _shotHistory.AddPoint(transform.position);
        _shotHistory.gameObject.SetActive(true);

    }

    public void UpdateRayCast(Vector2 startPos, Vector2 direction)
    {
        //keep circle casting until circle cast hit a kill object
        //reflect circle cast along normal of the hit object
        //if circle cast hit a wall, reflect circle cast along normal of the hit wall
        //if circle cast hit a kill object, destroy the object and stop circle casting
        RaycastHit2D hit = Physics2D.Raycast(startPos, direction, Mathf.Infinity, LayerMask.GetMask("Wall"));
        if (hit)
        {
            _shotHistory.AddPoint(hit.point);
            if (hit.collider.CompareTag("Kill"))
            {
                _shotHistory.DrawHistory();
                return;
            }

            //if hit object inherits ITriggerObject, call the trigger method
            if (hit.collider.TryGetComponent(out ITriggerObject triggerObject))
            {
                triggerObject.OnHit();
            }

            //Destroy(hit.collider.gameObject);
            Vector2 newPos = hit.point + hit.normal * 0.5f;
            UpdateRayCast(newPos, Vector2.Reflect(direction, hit.normal));
        }
    }

    public void ClearHistory()
    {
        _shotHistory.ClearHistory();
        _shotHistory.AddPoint(transform.position);
    }

    public void SetColor(ColorState color)
    {
        colorState = color;
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

        if (color1 == ColorState.Red && color2 == ColorState.Blue ||
            color1 == ColorState.Blue && color2 == ColorState.Red)
        {
            return ColorState.Magenta;
        }

        if (color1 == ColorState.Red && color2 == ColorState.Green ||
            color1 == ColorState.Green && color2 == ColorState.Red)
        {
            return ColorState.Yellow;
        }

        if (color1 == ColorState.Blue && color2 == ColorState.Green ||
            color1 == ColorState.Green && color2 == ColorState.Blue)
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