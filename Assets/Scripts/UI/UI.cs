using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

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
}
