using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] Button _����Button;
    [SerializeField] Button _playerDeckButton;
    [SerializeField] GameObject _cardPanel;
    [SerializeField] GameObject _battlePanel;
    [SerializeField] GameObject _selectionPanel;
    [SerializeField] Button _turnEndButton;
    [SerializeField] Button _unusedDeckButton;
    [SerializeField] Button _usedDeckButton;
    [SerializeField] List<Button> _selectionButton;
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _backgroundPanel;
    [SerializeField] Image _fade; // ����
    private bool _isSelected;
    private bool _isPlayerTurnEnd;
    private List<GameObject> _enemys = new List<GameObject>();
    private GameSceneManager _gameSceneManager = new GameSceneManager();
    private IEnumerator _battleRoutine;
    private IEnumerator _selectionRoutine;



    private void Start()
    {
        EventManager.CallOnPlayerHP(DataBase.PlayerMaxHP[DataManager.PlayerID], DataManager.PlayerCurrentHP);

        _����Button.onClick.AddListener(OnClick����Button);
        _playerDeckButton.onClick.AddListener(OnClickPlayerDeckButton);
        EventManager.CheckState += CheckCurrentState;
        EventManager.SetFade += SetFade;
        //��������� ���� ����
        EventManager.CallOnMana(DataManager.Mana);
        EventManager.CallOnPlayerDeckNum(DataManager.PlayerDeck.Count.ToString());
        DataManager.LoadDebug(); // ����׿� ī�� �߰�

        CheckCurrentState();


    }
    void CheckCurrentState() // ���̺� ���� �о����
    {
        _selectionRoutine = SelectionRoutine();
        switch (DataManager.CurrentState)
        {
            case DataBase.State.Selection:
                StartSelectionRoutine();
                break;
            case DataBase.State.Battle:
                StartBattleRoutine();
                break;
        }
    }

    private void StartSelectionRoutine()
    {
        _selectionPanel.SetActive(true);

    } // ���̺� ������ ���� �ҷ�����

    private void SetFade()
    {
        StartCoroutine(FadeOut());
        
        
    }
    private void StartBattleRoutine()
    {
        _battlePanel.SetActive(true);
        //StartCoroutine(_battleRoutine);
    } // ���̺� ������ ���� �ҷ�����

    private void OnDestroy()
    {
        EventManager.SetFade -= SetFade;
        EventManager.CheckState -= CheckCurrentState;
    } // �ݹ� ����
    
    private void OnClick����Button()
    {
        SceneManager.LoadScene("MainScene");

    } // ���� ��ư
    private void CheckSelection() // Selection Routine �����ϰ� ���� ������ �� ����
    {
        var data = DataManager.CurrentStateData;
        int num = data.Count;
        for (int i = 0; i < num; i++)
        {
            if((DataBase.State)data[i] == DataBase.State.None)
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



    private IEnumerator FadeOut()
    {
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        _fade.gameObject.SetActive(true);
        float alpha = 0f;
        float deltaAlpha = 0.005f;
        _fade.color = new Color(0, 0, 0, alpha);
        while (alpha < 1.0f)
        {
            yield return waitForEndOfFrame;
            alpha += deltaAlpha;
            _fade.color = new Color(0, 0, 0, alpha);
        }
        EventManager.CallOnCheckState();
        StartCoroutine(FadeIn());

    } // ȿ��
    private IEnumerator FadeIn()
    {
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        float alpha = 0f;
        float deltaAlpha = 0.005f;
        while (alpha > 0)
        {
            yield return waitForEndOfFrame;
            alpha -= deltaAlpha;
            _fade.color = new Color(0, 0, 0, alpha);
        }
        _fade.gameObject.SetActive(false);
    } //ȿ��


    private IEnumerator SelectionRoutine()
    {
        CheckSelection(); // ������ ����, �� �ҷ�����
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        _backgroundPanel.GetComponent<RectTransform>().localPosition = new Vector3(960, 0, 0);
        Vector3 speed = new Vector3(-700f, 0, 0);
        while (_backgroundPanel.GetComponent<RectTransform>().localPosition.x > -959.99f)
        {
            yield return waitForEndOfFrame;
            
            _backgroundPanel.GetComponent<RectTransform>().localPosition += speed * Time.deltaTime;
        }
        yield return new WaitUntil(() => _isSelected);
        _isSelected = false;
        yield return StartCoroutine(FadeOut());
        _selectionPanel.SetActive(false);
        yield return StartCoroutine(FadeIn());

        CheckCurrentState();

    } // ���� ��ƾ
    


    private void OnClickPlayerDeckButton()
    {
        _cardPanel.SetActive(true);
        GameManager.instance.PostCardState = GameManager.instance.CardState;
        GameManager.instance.CardState = DataBase.CardState.CardPanel;
        EventManager.CallOnCardList(DataManager.PlayerDeck);
    } // ��ü �� �ҷ�����








    private void CheckCurrentStated(GameObject panel)
    {
        panel.SetActive(false);
    }


}
