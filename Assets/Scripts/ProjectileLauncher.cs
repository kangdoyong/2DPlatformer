using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    // 생성할 게임 오브젝트
    public GameObject projectilePrefab;
    // 게임 오브젝트를 생성할 위치
    public Transform launchPoint;

    public void FirePorjectile()
    {
        // 화살 오브젝트를 생성한다.
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, projectilePrefab.transform.rotation);

        // 원래 Scale 값을 받아온다.
        Vector3 originScale = projectile.transform.localScale;

        // 플레이어가 오른쪽으로 바라보는지 왼쪽을 바라보는지 
        float value = transform.localScale.x > 0 ? 1 : -1;

        projectile.transform.localScale = new Vector3
        {
            x = originScale.x * value,
            y = originScale.y * value,
            z = originScale.z
        };
    }
}
