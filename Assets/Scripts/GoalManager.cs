using Sirenix.OdinInspector;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    [ShowInInspector] private List<Goal> goalList = new List<Goal>();
    bool clear;
    private void Awake()
    {
        foreach (var slot in goalList)
        {
            slot.isGoal.Subscribe(_ =>
            {
                if (goalList.TrueForAll(s => s.isGoal.Value))
                {
                    isCompleted_.Value = true;
                }
            });
        }
    }
}
