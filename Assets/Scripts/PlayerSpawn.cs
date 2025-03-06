using System;
using Sirenix.OdinInspector;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

//スポーンと呼んでるけど実質こっちがプレイヤーでしょ
//基本はこっちからplayerオブジェクト生成して発射する仕組み
public class PlayerSpawn : MonoBehaviour
{
    private Camera _camera; //マウスのワールド位置取るためカメラの参照をとっとく
    private bool _shot = false; //発射後操作を一旦中止の判定用
    [DoNotSerialize] public Rigidbody2D playerRb; //プレイヤー発射のためのRigidbody参照
    public static PlayerSpawn Instance { get; private set; } //シングルトン

    [SerializeField, Required, TabGroup("参照")]
    private GameObject playerPrefab;

    [Required, SerializeField, TabGroup("参照")]
    private Image aimCursor; //マウスの場所のカーソル

    [Required, SerializeField, TabGroup("参照")]
    private LineRenderer aimLine; //予測線

    [SerializeField, Range(0.1f, 0.3f), TabGroup("数値")]
    private float lineWidth = 0.3f; //予測線の太さ

    [SerializeField, Range(5, 20), TabGroup("数値")]
    private float launchSpeed = 5f; //プレイヤーの発射速度

    //プレイヤーの状態を監視するReactiveProperty
    public ReactiveProperty<bool> playerAlive = new ReactiveProperty<bool>(true);

    private void Awake()
    {
        //シングルトン処理
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //説明しなくていいかなこれ
        _camera = Camera.main;
        aimCursor.enabled = true;
        aimLine.enabled = true;

        //プレイヤーが死んだら生成する
        playerAlive.Where(_ => playerAlive.Value == false).Subscribe(_ => SpawnPlayer()).AddTo(this);
        //まだ発射していない状態だったら予測線とカーソルの処理する
        Observable.EveryUpdate().Where(_ => !_shot).Subscribe(_ => Aim()).AddTo(this);
        //マウス左クリックしたら発射
        Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0) && !_shot).Subscribe(_ => Shoot()).AddTo(this);
    }

    private void SpawnPlayer()
    {
        //自分の場所にプレイヤー生成
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
        //プレイヤーが死んだらfalseになるからリセット
        aimCursor.enabled = true;
        aimLine.enabled = true;
        //まだ発射してない
        _shot = false;
    }

    private void Aim()
    {
        //マウス位置
        Vector2 mousePosition = Input.mousePosition;
        //UIスペースだからカーソルの位置＝マウスの位置
        aimCursor.transform.position = mousePosition;
        //予測線処理
        DrawLine(mousePosition);
    }

    private void DrawLine(Vector2 mousePosition)
    {
        //向きだけとって、壁ぶつかったら予測線を止まる
        //そのためのRaycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position,
            (Vector2)_camera.ScreenToWorldPoint(mousePosition) - (Vector2)transform.position, Mathf.Infinity,
            LayerMask.GetMask("Wall"));
        //LineRendererの点は２つ
        aimLine.positionCount = 2;
        //自身の位置
        aimLine.SetPosition(0, transform.position);
        //Raycastの結果があればそこまで、なければマウスの位置まで
        aimLine.SetPosition(1, hit.collider ? hit.point : (Vector2)_camera.ScreenToWorldPoint(mousePosition));
        //線の太さ
        aimLine.startWidth = lineWidth;
        aimLine.endWidth = lineWidth;
    }

    private void Shoot()
    {
        //プレイヤーを発射
        playerRb.linearVelocity =
            (Vector2)(_camera.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized * launchSpeed;
        _shot = true;
        //カーソルと予測線を消す
        aimCursor.enabled = false;
        aimLine.enabled = false;
    }
}