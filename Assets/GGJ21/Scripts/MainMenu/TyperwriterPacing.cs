﻿using System.Collections;
using Febucci.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class TyperwriterPacing : MonoBehaviour
{
    public TypewriterByCharacter line1, line2, tagline, pressAnyButton;
    public GameObject faderImage;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        line1.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        line2.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        tagline.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        pressAnyButton.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        InputSystem.onEvent += InputEvent;
    }

    private void InputEvent(InputEventPtr eventPtr, InputDevice device)
    {
        if (!eventPtr.IsA<StateEvent>() && !eventPtr.IsA<DeltaStateEvent>())
        {
            return;
        }

        var controls = device.allControls;
        var buttonPressPoint = InputSystem.settings.defaultButtonPressPoint;
        foreach (var t in controls)
        {
            var control = t as ButtonControl;
            if (control == null || control.synthetic || control.noisy)
                continue;
            if (!control.ReadValueFromEvent(eventPtr, out var value) || !(value >= buttonPressPoint)) continue;
            InputSystem.onEvent -= InputEvent;
            StartCoroutine(FadeScene());
            break;
        }
    }

    private IEnumerator FadeScene()
    {
        faderImage.SetActive(true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("SampleScene");
    }
    
}