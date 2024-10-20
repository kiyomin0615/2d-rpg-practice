using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Scene : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    [SerializeField] Slider slider;
    [SerializeField] Image dashImage;
    [SerializeField] Image dashCooldownImage;

    float dashCooldown;
    float dashTimer;

    void Start()
    {
        if (playerStats != null)
            playerStats.onHPChanged += UpdateHPBar;

        dashCooldownImage.fillAmount = 0;
        dashTimer = 0;
        dashCooldown = SkillManager.instance.dashSkill.cooldown;
    }

    void Update()
    {
        UpdateDashTimer();

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (dashTimer <= 0)
            {
                dashTimer = dashCooldown;
                SetDashToCooldown();
            }
        }
    }

    void UpdateHPBar()
    {
        slider.maxValue = playerStats.CalculateMaxHP();
        slider.value = playerStats.currentHP;
    }

    void SetDashToCooldown()
    {
        dashCooldownImage.fillAmount = 1;
    }

    void UpdateDashTimer()
    {
        dashTimer -= Time.deltaTime;
        dashCooldownImage.fillAmount = Mathf.Clamp(dashTimer / dashCooldown, 0, 1);
    }
}
