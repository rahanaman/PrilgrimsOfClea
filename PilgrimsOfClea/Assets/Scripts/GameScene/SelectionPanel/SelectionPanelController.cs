using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionPanelController : MonoBehaviour
{

    [SerializeField] GameObject _backgroundPanel;
    [SerializeField] List<Button> _selectionButton;
    private GameSceneManager _gameSceneManager = new GameSceneManager();
    private bool _isSelected;
    private IEnumerator _routine;
    // Start is called before the first frame update
    private void OnEnable()
    {
        for (int i = 0; i < _selectionButton.Count; i++)
        {
            int index = i;
            _selectionButton[i].onClick.AddListener(() => OnClickSelectionButton(index));
        }
        _isSelected = false;
        _routine = SelectionRoutine();
        StartCoroutine(_routine);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnClickSelectionButton(int value) //  ������ ���� �� ���� ���� - Save �ϴ� ����
    {
        var data = DataManager.CurrentStateData[value];

        DataBase.State state = (DataBase.State)data;
        if (state == DataBase.State.None)
        {
            Debug.Log("None");
            return;
        } // ����� ������ if�� ������ ��
        List<int> stateData =_gameSceneManager.SetStateData(state); // ������� Data ����
        DataManager.SaveCurrentState(state, stateData);
        _isSelected = true;


    }
    private void CheckSelection() // Selection Routine �����ϰ� ���� ������ �� ����
    {
        var data = DataManager.CurrentStateData;
        int num = data.Count;
        for (int i = 0; i < num; i++)
        {
            if ((DataBase.State)data[i] == DataBase.State.None)
            {
                _selectionButton[i].gameObject.SetActive(false);
            }
            else
            {
                _selectionButton[i].gameObject.SetActive(true);
                _selectionButton[i].gameObject.GetComponent<SelectionButtonView>().SetImage((int)data[i]);// ������ ��� ��ȣ������
            }
        }

    }

    private IEnumerator SelectionRoutine()
    {
        CheckSelection(); // ������ ����, �� �ҷ�����
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        _backgroundPanel.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        Vector3 speed = new Vector3(-700f, 0, 0);
        while (_backgroundPanel.GetComponent<RectTransform>().localPosition.x > -1919.99f)
        {
            yield return waitForEndOfFrame;

            _backgroundPanel.GetComponent<RectTransform>().localPosition += speed * Time.deltaTime;
        }
        yield return new WaitUntil(() => _isSelected);
        _isSelected = false;
        EventManager.CallOnFade();
        gameObject.SetActive(false);


    } // ���� ��ƾ
}
