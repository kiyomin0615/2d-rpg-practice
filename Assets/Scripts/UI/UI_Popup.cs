using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : MonoBehaviour, ISaveManager
{
    [SerializeField] GameObject characterPanelUI;
    [SerializeField] GameObject skillPanelUI;
    [SerializeField] GameObject craftPanelUI;
    [SerializeField] GameObject optionsPanelUI;

    [SerializeField] UI_VolumeSlider[] volumeSliders;

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

        AudioManager.instance.PlaySFX(7);

        ActivateUIPanel(targetPanel);
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.volumeSettingDict.Clear();
        foreach (UI_VolumeSlider volumeSlider in volumeSliders)
        {
            gameData.volumeSettingDict.Add(volumeSlider.param, volumeSlider.slider.value);
        }
    }

    public void LoadData(GameData gameData)
    {
        foreach (KeyValuePair<string, float> pair in gameData.volumeSettingDict)
        {
            foreach (UI_VolumeSlider volumeSlider in volumeSliders)
            {
                if (volumeSlider.param == pair.Key)
                {
                    volumeSlider.SetSliderValueOnGameLoaded(pair.Value);
                }
            }
        }

    }
}
