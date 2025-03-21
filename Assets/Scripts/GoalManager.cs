using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    [SerializeField] private List<Goal> goalList;
    public ReactiveProperty<bool> isClear = new ReactiveProperty<bool>(false);
    private void Awake()
    {
        foreach (var slot in goalList)
        {
            slot.isGoal.Subscribe(_ =>
            {
                if (goalList.TrueForAll(s => s.isGoal.Value))
                {
                    isClear.Value = true;
                }
            });
        }
    }

    [Button("ScanGoal")]
    private void ScanGoal()
    {
        goalList = new List<Goal>(FindObjectsByType<Goal>(FindObjectsSortMode.None));
    }
}
