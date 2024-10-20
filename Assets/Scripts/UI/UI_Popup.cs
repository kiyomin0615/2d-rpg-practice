using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : MonoBehaviour
{
    [SerializeField] GameObject characterPanelUI;
    [SerializeField] GameObject skillPanelUI;
    [SerializeField] GameObject craftPanelUI;
    [SerializeField] GameObject optionsPanelUI;

    void Start()
    {
        ActivateUIPanel(null);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchToTargetUIPanel(characterPanelUI);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            SwitchToTargetUIPanel(skillPanelUI);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            SwitchToTargetUIPanel(craftPanelUI);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            SwitchToTargetUIPanel(optionsPanelUI);
        }
    }

    public void ActivateUIPanel(GameObject panel)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        if (panel != null)
        {
            panel.SetActive(true);
        }
    }

    public void SwitchToTargetUIPanel(GameObject targetPanel)
    {
        if (targetPanel != null && targetPanel.activeSelf)
        {
            targetPanel.SetActive(false);
            return;
        }

        ActivateUIPanel(targetPanel);
    }
}
