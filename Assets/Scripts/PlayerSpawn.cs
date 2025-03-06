using System;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField, Required] private GameObject playerPrefab;

    private Camera _camera;
    private bool _shot = false;
    public Rigidbody2D playerRb;
    [Required, SerializeField] private Image aimCursor;
    [Required, SerializeField] private LineRenderer aimLine;
    [SerializeField, Range(0.1f, 0.3f)] private float lineWidth = 0.3f;
    [SerializeField, Range(5, 20)] private float speed = 5f;

    public ReactiveProperty<bool> playerAlive = new ReactiveProperty<bool>(true);

    private void Awake()
    {
        _camera = Camera.main;
        aimCursor.enabled = true;
        aimLine.enabled = true;

        playerAlive.Where(_ =>playerAlive.Value == false).Subscribe(_ => SpawnPlayer()).AddTo(this);
        Observable.EveryUpdate().Where(_ => !_shot).Subscribe(_ => Aim()).AddTo(this);
        Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0) && !_shot).Subscribe(_ => Shoot()).AddTo(this);
    }

    private void SpawnPlayer()
    {
        //spawn player at mouse position
        Instantiate(playerPrefab, transform.position, Quaternion.identity);

        aimCursor.enabled = true;
        aimLine.enabled = true;
        _shot = false;
    }

    private void Aim()
    {
        //Get mouse position in screen space
        Vector2 mousePosition = Input.mousePosition;
        //attach the image to mouse position
        aimCursor.transform.position = mousePosition;
        DrawLine(mousePosition);
    }

    private void DrawLine(Vector2 mousePosition)
    {
        //draw line from player towards mouse direction, stopping when the line hits a wall
        RaycastHit2D hit = Physics2D.Raycast(transform.position,
            (Vector2)_camera.ScreenToWorldPoint(mousePosition) - (Vector2)transform.position, Mathf.Infinity,
            LayerMask.GetMask("Wall"));
        aimLine.positionCount = 2;
        //set the first point of the line
        aimLine.SetPosition(0, transform.position);
        //set the last point of the line
        aimLine.SetPosition(1, hit.collider ? hit.point : (Vector2)_camera.ScreenToWorldPoint(mousePosition));

        //set line width
        aimLine.startWidth = lineWidth;
        aimLine.endWidth = lineWidth;
    }

    private void Shoot()
    {
        //shoot self in the direction of the mouse
        playerRb.linearVelocity =
            (Vector2)(_camera.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized * speed;
        _shot = true;
        //disable the aim cursor and line
        aimCursor.enabled = false;
        aimLine.enabled = false;
    }
}