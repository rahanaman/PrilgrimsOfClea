using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPrizePanelController : MonoBehaviour
{
    [SerializeField] Button _exitButton;
    private void OnEnable()
    {
        _exitButton.onClick.AddListener(OnClickExitButton);
    }
    private void OnDisable()
    {
        _exitButton?.onClick.RemoveAllListeners();
    }

    private void OnClickExitButton()
    {
        EventManager.CallOnClosePrizeCard(false);
    }
    public void SetPrizePanel(List<int> card)
    {

        int n = card.Count;
        Vector2 pos = new Vector2(-350, -50);
        for (int i = 0; i < n; i++)
        {
            GameObject prize = Instantiate(DataLoader.CardPref[card[i]], gameObject.transform);
            prize.GetComponent<CardController>().SetCard(DataBase.CardList[card[i]]);
            prize.SetActive(true);
            prize.transform.localPosition = pos;
            pos.x += 400;
        }

    }
}
