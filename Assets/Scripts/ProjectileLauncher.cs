using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    // ������ ���� ������Ʈ
    public GameObject projectilePrefab;
    // ���� ������Ʈ�� ������ ��ġ
    public Transform launchPoint;

    public void FirePorjectile()
    {
        // ȭ�� ������Ʈ�� �����Ѵ�.
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, projectilePrefab.transform.rotation);

        // ���� Scale ���� �޾ƿ´�.
        Vector3 originScale = projectile.transform.localScale;

        // �÷��̾ ���������� �ٶ󺸴��� ������ �ٶ󺸴��� 
        float value = transform.localScale.x > 0 ? 1 : -1;

        projectile.transform.localScale = new Vector3
        {
            x = originScale.x * value,
            y = originScale.y * value,
            z = originScale.z
        };
    }
}
