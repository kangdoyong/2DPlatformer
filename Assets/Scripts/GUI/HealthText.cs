using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    // �̵� �ӵ� �� ���� ����
    public Vector3 moveSpeed = new Vector3(0, 75, 0);
    // Fade ȿ�� ���� �ð�
    public float timeToFade = 1f;

    RectTransform textTransform;
    TextMeshProUGUI textMeshPro;

    private float timeEalpsed = 0;
    private Color startColor;

    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startColor = textMeshPro.color;
    }

    // Update is called once per frame
    void Update()
    {
        textTransform.position += moveSpeed * Time.deltaTime;

        timeEalpsed += Time.deltaTime;
        if(timeEalpsed < timeToFade)
        {
            float newAlpha = startColor.a * (1 - (timeEalpsed / timeToFade));
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
