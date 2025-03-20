using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    [ShowInInspector] private Dictionary<GameObject,int> buildObjects;
    private GameObject _currentObject;
    private Camera _camera;

    private Image _objectCursor;


    void Awake()
    {
        //set the first object active

        _camera = Camera.main;

        //オブジェクト移動
        Observable.EveryUpdate().Where(_ => GameManager.Instance.gameState == GameState.Build)
            .Subscribe(_ => ObjectPointer()).AddTo(this);
        //クリックしたら置く
        Observable.EveryUpdate()
            .Where(_ => GameManager.Instance.gameState == GameState.Build && Input.GetMouseButtonDown(0))
            .Subscribe(_ => PlaceObject()).AddTo(this);
    }

    private void ObjectPointer()
    {
        //マウスの位置にオブジェクトを置く
        _objectCursor.transform.position = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }

    private void PlaceObject()
    {
        //マウスの位置にオブジェクトを置く
        _currentObject.transform.position = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }

    public void SetActiveIndex(int index)
    {

    }
}