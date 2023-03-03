using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneController : MonoBehaviour
{
    [SerializeField] private Button _종료Button;
    [SerializeField] private Button _설정Button;
    [SerializeField] private Button _사전Button;
    [SerializeField] private Button _시작Button;
    [SerializeField] private GameObject _시작Panel;
    [SerializeField] private GameObject _사전Panel;
    [SerializeField] private GameObject _종료Panel;
    [SerializeField] private GameObject _설정Panel;
    void Start()
    {
        _시작Button.onClick.AddListener(OnClick시작);
    }

    private void OnClick시작()
    {
        _시작Panel.SetActive(true);
    }


}
