using Unity.VisualScripting;
using UnityEngine;

public class Prism : MonoBehaviour, ITriggerObject
{
    [SerializeField] private Transform launchPoint1;
    [SerializeField] private Transform launchPoint2;
    [SerializeField] private Transform launchPoint3;
    [SerializeField] private GameObject playerPrefab;

    private PlayerController player1;
    private PlayerController player2;
    private PlayerController player3;

    private void Start()
    {
        player1 = launchPoint1.GetComponent<PlayerController>();
        player2 = launchPoint2.GetComponent<PlayerController>();
        player3 = launchPoint3.GetComponent<PlayerController>();
    }

    public void OnHit()
    {
        player1.SetColor(PlayerController.ColorState.Red);
        player2.SetColor(PlayerController.ColorState.Green);
        player3.SetColor(PlayerController.ColorState.Blue);

        player1.UpdateRayCast(launchPoint1.position, launchPoint1.up);
        player2.UpdateRayCast(launchPoint2.position, launchPoint2.up);
        player3.UpdateRayCast(launchPoint3.position, launchPoint3.up);
    }

    public void Reset()
    {
        player1.ClearHistory();
        player2.ClearHistory();
        player3.ClearHistory();
    }
}