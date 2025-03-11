using Unity.VisualScripting;
using UnityEngine;

public class Prism : MonoBehaviour
{
    [SerializeField] private Transform launchPoint1;
    [SerializeField] private Transform launchPoint2;
    [SerializeField] private GameObject playerPrefab;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);

        var player1Rb = Instantiate(playerPrefab, launchPoint1.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        var player2Rb = Instantiate(playerPrefab, launchPoint2.position, Quaternion.identity).GetComponent<Rigidbody2D>();

        // shoot the players along the y-axis of the launch points
        player1Rb.linearVelocity = launchPoint1.up * 20;
        player2Rb.linearVelocity = launchPoint2.up * 20;
    }
}
