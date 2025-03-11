using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ShotHistory : MonoBehaviour
{
    [SerializeField] private List<Vector3> shotHistory = new List<Vector3>();
    [SerializeField] private float lineWidth = 0.1f;
    private LineRenderer _lineRenderer;

    public static ShotHistory Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _lineRenderer = GetComponent<LineRenderer>();
    }
    public void AddPoint(Vector3 point)
    {
        shotHistory.Add(point);
    }

    public void DrawHistory()
    {
        _lineRenderer.positionCount = shotHistory.Count+1;
        _lineRenderer.startWidth = lineWidth;
        _lineRenderer.endWidth = lineWidth;
        //set the first point to the player position
        _lineRenderer.SetPosition(0, PlayerSpawn.Instance.transform.position);
        for (int i = 0; i < shotHistory.Count; i++)
        {
            _lineRenderer.SetPosition(i+1, shotHistory[i]);
        }
    }

    public void ClearHistory()
    {
        shotHistory.Clear();
    }
}
