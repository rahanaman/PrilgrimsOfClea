using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneController : MonoBehaviour
{
    [SerializeField] private Button _����Button;
    [SerializeField] private Button _����Button;
    [SerializeField] private Button _����Button;
    [SerializeField] private Button _����Button;
    [SerializeField] private GameObject _����Panel;
    [SerializeField] private GameObject _����Panel;
    [SerializeField] private GameObject _����Panel;
    [SerializeField] private GameObject _����Panel;
    void Start()
    {
        _����Button.onClick.AddListener(OnClick����);
    }

    private void OnClick����()
    {
        _����Panel.SetActive(true);
    }


}
