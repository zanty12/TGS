using Unity.VisualScripting;
using UnityEngine;

public class Prism : MonoBehaviour, ITriggerObject
{
    [SerializeField] private Transform launchPoint1;
    [SerializeField] private Transform launchPoint2;
    [SerializeField] private Transform launchPoint3;
    [SerializeField] private GameObject playerPrefab;
   
    public void OnHit()
    {
        PlayerController player1 = launchPoint1.GetComponent<PlayerController>();
        PlayerController player2 = launchPoint2.GetComponent<PlayerController>();
        PlayerController player3 = launchPoint3.GetComponent<PlayerController>();

        player1.SetColor(PlayerController.ColorState.Red);
        player2.SetColor(PlayerController.ColorState.Green);
        player3.SetColor(PlayerController.ColorState.Blue);

        player1.UpdateRayCast(launchPoint1.position, launchPoint1.up);
        player2.UpdateRayCast(launchPoint2.position, launchPoint2.up);
        player3.UpdateRayCast(launchPoint3.position, launchPoint3.up);
    }
}
