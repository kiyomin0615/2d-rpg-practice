using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : MonoBehaviour
{
    RectTransform rectTransform;
    Entity entityComponent;
    Slider slider;

    void Awake()
    {
        entityComponent = GetComponentInParent<Entity>();
        rectTransform = GetComponent<RectTransform>();
        slider = GetComponentInChildren<Slider>();
    }

    void Start()
    {
        entityComponent.onFlip += FlipUI;
        entityComponent.stats.onHPChanged += UpdateHPBar;

        UpdateHPBar();
    }

    void FlipUI()
    {
        rectTransform.Rotate(0, 180, 0);
    }

    void OnDisable()
    {
        entityComponent.onFlip -= FlipUI;
    }

    void UpdateHPBar()
    {
        slider.maxValue = entityComponent.stats.CalculateMaxHP();
        slider.value = entityComponent.stats.currentHP;
    }
}
