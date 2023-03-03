using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneStartController : MonoBehaviour
{
    [SerializeField] private GameObject Panel;
    [SerializeField] private GameObject _����Button;
    [SerializeField] private List<GameObject> _����Button;
    [SerializeField] private bool _isClick;

    void Start()
    {
        EventManager.SetSelection += DataManager.SetSelctionData;
        EventManager.SetCGID += SetCGID;
        EventManager.SetID += SetID;
        _����Button.GetComponent<Button>().onClick.AddListener(OnClick����Button);
        _isClick = false;
    }

    private void OnDestroy()
    {
        EventManager.SetSelection -= DataManager.SetSelctionData;
        EventManager.SetCGID -= SetCGID;
        EventManager.SetID -= SetID;
    }

    private void OnClick����Button()
    {
        if (!_isClick)
        {
            Panel.SetActive(false);
        }

    }

    private void SetCGID(GameObject value1)
    {
        int index;
        if (!_isClick)
        {
            if (value1 == null)
            {
                index = 0;
            }
            else
            {
                index = _����Button.IndexOf(value1) + 1;
            }
            EventManager.CallOnSelectionCG(index);
        }
    }
    private void SetID(GameObject value1)
    {
        if (!_isClick)
        {
            _isClick = true;
            int index = _����Button.IndexOf(value1) + 1;
            for(int i=0; i<3; i++)
            {
                _����Button[i].SetActive(false);
            }
            _����Button.SetActive(false);
            EventManager.CallOnSelection(index);
            
        }
            
    }





}
