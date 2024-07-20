using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _followTarget;

    // 카메라가 초기 위치에서 얼마큼 움직였는지에 대한 Vector
    // 카메라의 현재 위치값에서 현재 게임 오브젝트의 초기 위치값을 뺍니다.
    Vector2 cameraMoveSinceStart => (Vector2)_camera.transform.position - startingPosition;

    // _followTarget으로부터 Z 거리가 얼마큼 떨어졌는지에 대한 변수
    // 현재 오브젝트의 Position.Z - _followTarget의 Position.Z = (BG1) -1.2 (BG2) -0.6 (BG1) -0.24
    float zDistanceFromTarget => transform.position.z - _followTarget.transform.position.z;


    float clippingPlane => _camera.nearClipPlane;
    //(_camera.transform.position.z + zDistanceFromTarget) > 0 ? _camera.farClipPlane : _camera.nearClipPlane;

    // zDistanceFromTarget을 clippingPlane으로 나눈 값
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    // 현재 오브젝트의 초기 위치값
    Vector2 startingPosition = Vector2.zero;
    // 현재 오브젝트의 초기 Z 축 위치값
    float startingZ = 0f;

    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        // 초기 위치값에서 카메라가 움직임 Vector 값을 더한다.
        Vector2 newPosition = startingPosition + cameraMoveSinceStart / parallaxFactor;

        transform.position = new Vector3(-newPosition.x, newPosition.y, startingZ);
    }


}