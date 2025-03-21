using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

//スポーンと呼んでるけど実質こっちがプレイヤーでしょ
//基本はこっちからplayerオブジェクト生成して発射する仕組み
public class PlayerSpawn : MonoBehaviour
{
    private Camera _camera; //マウスのワールド位置取るためカメラの参照をとっとく

    public static PlayerSpawn Instance { get; private set; } //シングルトン

    [Required, SerializeField, BoxGroup("参照")]
    private PlayerController player;

    [Required, SerializeField, BoxGroup("参照")]
    private Image aimCursor; //マウスの場所のカーソル

    [Required, SerializeField, BoxGroup("参照")]
    private LineRenderer aimLine; //予測線

    [SerializeField, Range(0.1f, 0.3f), BoxGroup("数値")]
    private float lineWidth = 0.3f; //予測線の太さ

    [Required, SerializeField, BoxGroup("参照")]
    private GameObject hitEffect;

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

        //まだ発射していない状態だったら予測線とカーソルの処理する
        Observable.EveryUpdate().Where(_ => GameManager.Instance.gameState == GameState.Shoot)
            .Subscribe(_ => Aim()).AddTo(this);
        //マウス左クリックしたら発射
        Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButtonDown(0) && GameManager.Instance.gameState == GameState.Shoot)
            .Subscribe(_ => Shoot()).AddTo(this);
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
        player.ClearHistory();
        //clear history on all ITriggerObject
        foreach (var triggerObject in FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                     .OfType<ITriggerObject>())
        {
            triggerObject.Reset();
        }

        player.UpdateRayCast(transform.position,
            ((Vector2)_camera.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position).normalized, PlayerController.reflectMax, hitEffect);
    }
}