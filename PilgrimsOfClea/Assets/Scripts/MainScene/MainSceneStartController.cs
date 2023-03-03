using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneStartController : MonoBehaviour
{
    [SerializeField] private GameObject Panel;
    [SerializeField] private GameObject _종료Button;
    [SerializeField] private List<GameObject> _선택Button;
    [SerializeField] private bool _isClick;

    void Start()
    {
        EventManager.SetSelection += DataManager.SetSelctionData;
        EventManager.SetCGID += SetCGID;
        EventManager.SetID += SetID;
        _종료Button.GetComponent<Button>().onClick.AddListener(OnClick종료Button);
        _isClick = false;
    }

    private void OnDestroy()
    {
        EventManager.SetSelection -= DataManager.SetSelctionData;
        EventManager.SetCGID -= SetCGID;
        EventManager.SetID -= SetID;
    }

    private void OnClick종료Button()
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
                index = _선택Button.IndexOf(value1) + 1;
            }
            EventManager.CallOnSelectionCG(index);
        }
    }
    private void SetID(GameObject value1)
    {
        if (!_isClick)
        {
            _isClick = true;
            int index = _선택Button.IndexOf(value1) + 1;
            for(int i=0; i<3; i++)
            {
                _선택Button[i].SetActive(false);
            }
            _종료Button.SetActive(false);
            EventManager.CallOnSelection(index);
            
        }
            
    }





}
