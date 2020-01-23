using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderNumber : MonoBehaviour
{
    private Text textNumber;

    private void Start()
    {
        textNumber = GetComponent<Text>();
    }

    public void SetValue(float value)
    {
        textNumber.text = value.ToString();
    }
}
