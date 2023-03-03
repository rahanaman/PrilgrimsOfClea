using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneStartButtonController : MonoBehaviour
{
    [SerializeField] private Button _button;
    private void Start()
    {
        _button.onClick.AddListener(OnClick);
    }
    private void OnMouseEnter()
    {
        EventManager.CallOnCGID(_button.gameObject);


    }

    private void OnMouseExit()
    {
        EventManager.CallOnCGID();
    }

    private void OnClick()
    {
        
        EventManager.CallOnID(_button.gameObject);
    }
}
