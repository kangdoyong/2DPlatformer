using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI healthText;
    private Canvas gameCanvas;

    private void Awake()
    {
        gameCanvas = GetComponent<Canvas>();
    }

    private void OnEnable()
    {
        HealthEvents.characterDamaged += CharacterDamaged;
        HealthEvents.characterHealed += CharacterHealed;
    }

    private void OnDisable()
    {
        HealthEvents.characterDamaged -= CharacterDamaged;
        HealthEvents.characterHealed -= CharacterHealed;
    }
    private void CharacterDamaged(GameObject character, int damage)
    {
        Debug.Log("CharacterHealed : " + damage);

        // Damage Text  持失

        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TextMeshProUGUI test = Instantiate<TextMeshProUGUI>
            (damageText,spawnPosition,Quaternion.identity, gameCanvas.transform);

    }

    private void CharacterHealed(GameObject character, int restore)
    {
        Debug.Log("CharacterDamaged : " + restore);

        // Helath Text  持失

        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TextMeshProUGUI test = Instantiate<TextMeshProUGUI>
            (damageText, spawnPosition, Quaternion.identity, gameCanvas.transform);

    }



}
