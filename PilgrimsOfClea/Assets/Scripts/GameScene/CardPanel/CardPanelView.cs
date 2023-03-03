using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPanelView : MonoBehaviour
{
    [SerializeField] private Button _종료Button;
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _panelRectTransform;
    private List<GameObject> _cardList = new List<GameObject>();
    void Awake()
    {
        EventManager.SetCardList += SetCardList;
        _종료Button.onClick.AddListener(OnClick종료Button);
    }

    private void OnDestroy()
    {
        EventManager.SetCardList -= SetCardList;
    }


    // Update is called once per frame
    private void OnClick종료Button()
    {
        _panel.SetActive(false);
    }
    public void SetCardList(List<int> value)
    {
        int n = value.Count;
        int k = Mathf.CeilToInt(n / 5f);
        _panelRectTransform.GetComponent<RectTransform>().sizeDelta = new Vector2(1920, 500 * k);
        Vector3 pos = new Vector3(270, -300, 0);
        for (int i = 0; i < n; i++)
        {
            var card = Instantiate(DataLoader.CardPref[value[i]], _panelRectTransform.transform);
            card.GetComponent<CardController>().SetCard(DataBase.CardList[value[i]]);
            _cardList.Add(card);
            card.transform.localPosition = pos;
            card.SetActive(true);
            
            if (pos.x == 1650)
            {
                pos.x = 270;
                pos.y -= 500;
            }
            else
            {
                pos.x += 345;
            }
        }
    }
    public void OnDisable()
    {
        var n = _cardList.Count;
        for (int i = 0; i < n; i++)
        {
            Destroy(_cardList[i]);
        }
       
    }



}
