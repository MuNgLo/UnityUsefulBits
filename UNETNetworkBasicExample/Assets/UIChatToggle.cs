using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChatToggle : MonoBehaviour
{
    public InputField input;
    public KeyCode toggleButton;
    private void Update()
    {
        if (Input.GetKeyDown(toggleButton))
        {
            input.gameObject.SetActive(!input.gameObject.activeSelf);
        }
    }
    private void OnEnable()
    {
        input.Select();
    }
}
