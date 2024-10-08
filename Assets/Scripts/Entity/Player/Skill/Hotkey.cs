using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hotkey : MonoBehaviour
{
    KeyCode hotkey;
    Blackhole blackhole;
    Transform enemyTransform;
    TextMeshProUGUI textComponent;
    SpriteRenderer spriteRenderer;

    public void Setup(KeyCode key, Transform enemyTransform, Blackhole blackhole) {
        this.hotkey = key;
        this.enemyTransform = enemyTransform;
        this.blackhole = blackhole;
        this.textComponent = GetComponentInChildren<TextMeshProUGUI>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.textComponent.text = hotkey.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(hotkey)) {
            blackhole.AddEnemyToList(enemyTransform);
            textComponent.color = Color.clear;
            spriteRenderer.color = Color.clear;
        }
    }


}
