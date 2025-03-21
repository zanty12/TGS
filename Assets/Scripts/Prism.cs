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

    public void OnHit(PlayerController playerController)
    {
        player1.SetColor(COLORSTATE.Red);
        player2.SetColor(COLORSTATE.Green);
        player3.SetColor(COLORSTATE.Blue);

        player1.UpdateRayCast(launchPoint1.position, launchPoint1.up, PlayerController.reflectMax);
        player2.UpdateRayCast(launchPoint2.position, launchPoint2.up, PlayerController.reflectMax);
        player3.UpdateRayCast(launchPoint3.position, launchPoint3.up, PlayerController.reflectMax);
    }

    public void Reset()
    {
        player1.ClearHistory();
        player2.ClearHistory();
        player3.ClearHistory();
    }
}