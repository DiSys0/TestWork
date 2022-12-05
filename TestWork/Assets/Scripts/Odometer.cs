using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Odometer : MonoBehaviour
{
    private Animator _animator;
    private Text _text;
    private string _newValue;
    private void Start()
    {
        EventsManager.ChangeOdometer += ChangeValue;
        _animator = GetComponent<Animator>();
        _text = GetComponent<Text>();
    }

    private void ChangeValue(float value)
    {
        _newValue = value.ToString();
        _animator.SetTrigger("Change");
    }
    public void GetValue()
    {
        _text.text = _newValue;
    }
    
    private void OnDestroy()
    {
        EventsManager.ChangeOdometer -= ChangeValue;
    }
}
