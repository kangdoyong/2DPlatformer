using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _followTarget;

    // ī�޶� �ʱ� ��ġ���� ��ŭ ������������ ���� Vector
    // ī�޶��� ���� ��ġ������ ���� ���� ������Ʈ�� �ʱ� ��ġ���� ���ϴ�.
    Vector2 cameraMoveSinceStart => (Vector2)_camera.transform.position - startingPosition;

    // _followTarget���κ��� Z �Ÿ��� ��ŭ ������������ ���� ����
    // ���� ������Ʈ�� Position.Z - _followTarget�� Position.Z = (BG1) -1.2 (BG2) -0.6 (BG1) -0.24
    float zDistanceFromTarget => transform.position.z - _followTarget.transform.position.z;


    float clippingPlane => _camera.nearClipPlane;
    //(_camera.transform.position.z + zDistanceFromTarget) > 0 ? _camera.farClipPlane : _camera.nearClipPlane;

    // zDistanceFromTarget�� clippingPlane���� ���� ��
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    // ���� ������Ʈ�� �ʱ� ��ġ��
    Vector2 startingPosition = Vector2.zero;
    // ���� ������Ʈ�� �ʱ� Z �� ��ġ��
    float startingZ = 0f;

    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        // �ʱ� ��ġ������ ī�޶� ������ Vector ���� ���Ѵ�.
        Vector2 newPosition = startingPosition + cameraMoveSinceStart / parallaxFactor;

        transform.position = new Vector3(-newPosition.x, newPosition.y, startingZ);
    }


}