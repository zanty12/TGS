using System;
using Sirenix.OdinInspector;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;
using static ColorState;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public COLORSTATE colorState = COLORSTATE.White;
    private ShotHistory _shotHistory;

    [SerializeField] public static readonly int reflectMax = 3;

    private void Start()
    {
        _shotHistory = ShotHistoryManager.Instance.CreateShotHistory();
        _shotHistory.AddPoint(transform.position);
        _shotHistory.gameObject.SetActive(true);
        _shotHistory.SetLineColor(StateToColor(colorState));
    }

    public void UpdateRayCast(Vector2 startPos, Vector2 direction, int reflect)
    {
        //keep circle casting until circle cast hit a kill object
        //reflect circle cast along normal of the hit object
        //if circle cast hit a wall, reflect circle cast along normal of the hit wall
        //if circle cast hit a kill object, destroy the object and stop circle casting
        if (reflect < 0)
        {
            _shotHistory.DrawHistory();
            return;
        }

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
                triggerObject.OnHit(this);
                _shotHistory.DrawHistory();
                return;
            }

            //Destroy(hit.collider.gameObject);
            Vector2 newPos = hit.point + hit.normal * 0.5f;
            UpdateRayCast(newPos, Vector2.Reflect(direction, hit.normal), reflect - 1);
        }
    }

    public void ClearHistory()
    {
        _shotHistory.ClearHistory();
        _shotHistory.AddPoint(transform.position);
    }

    public void SetColor(COLORSTATE color)
    {
        colorState = color;
        _shotHistory.SetLineColor(StateToColor(colorState));
    }
}