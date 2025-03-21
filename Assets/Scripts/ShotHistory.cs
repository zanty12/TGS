using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ShotHistory : MonoBehaviour
{
    [SerializeField] private List<Vector3> shotHistory = new List<Vector3>();
    [SerializeField] private float lineWidth = 0.1f;
    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    public void AddPoint(Vector3 point)
    {
        shotHistory.Add(point);
    }

    public void DrawHistory()
    {
        //clear the line renderer
        _lineRenderer.positionCount = 0;
        _lineRenderer.positionCount = shotHistory.Count;
        _lineRenderer.startWidth = lineWidth;
        _lineRenderer.endWidth = lineWidth;
        for (int i = 0; i < shotHistory.Count; i++)
        {
            _lineRenderer.SetPosition(i, shotHistory[i]);
        }
    }

    public void ClearHistory()
    {
        shotHistory.Clear();
        _lineRenderer.positionCount = 0;
    }

    public void SetLineColor(Color color)
    {
        _lineRenderer.startColor = color;
        _lineRenderer.endColor = color;
    }
}
