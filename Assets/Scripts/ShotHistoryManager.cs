using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ShotHistoryManager : MonoBehaviour
{
    public static ShotHistoryManager Instance { get; private set; }
    [ShowInInspector] private List<ShotHistory> _shotHistory;
    [ShowInInspector] private List<ShotHistory> _prevShotHistory;

    [SerializeField] GameObject shotHistoryPrefab;

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

        _shotHistory = new List<ShotHistory>();
        _prevShotHistory = new List<ShotHistory>();
    }

    public ShotHistory CreateShotHistory()
    {
        GameObject shotHistory = Instantiate(shotHistoryPrefab, transform);
        ShotHistory shotHistoryComponent = shotHistory.GetComponent<ShotHistory>();
        _shotHistory.Add(shotHistoryComponent);
        return shotHistoryComponent;
    }

    public void ClearShotHistory()
    {
        foreach (var shotHistory in _prevShotHistory)
        {
            Destroy(shotHistory.gameObject);
        }

        _prevShotHistory.Clear();
    }

    public void SaveShotHistory()
    {
        foreach (var shotHistory in _shotHistory)
        {
            _prevShotHistory.Add(shotHistory);
        }
        _shotHistory.Clear();
    }

    public void DrawHistory()
    {
        foreach (var shotHistory in _prevShotHistory)
        {
            shotHistory.gameObject.SetActive(true);
            shotHistory.DrawHistory();
        }
    }

    public void HideShotHistory()
    {
        foreach (var shotHistory in _prevShotHistory)
        {
            shotHistory.gameObject.SetActive(false);
        }
    }
}