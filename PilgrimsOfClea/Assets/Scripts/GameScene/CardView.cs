using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardView : MonoBehaviour
{
    [SerializeField] private Image _cardIcon;
    [SerializeField] private TextMeshProUGUI _cardCost;
    [SerializeField] private TextMeshProUGUI _cardName;
    [SerializeField] private TextMeshProUGUI _cardDesc;




    public void SetCardUI(CardData data, Sprite sprite)
    {
        _cardIcon.sprite = sprite;
        _cardCost.text = data.CardCost.ToString();
        _cardName.text = data.CardName;
        _cardDesc.text = data.CardDesc;

    }
}
