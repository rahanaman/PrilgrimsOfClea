using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSceneView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _playerDeckButtonText;
    [SerializeField] GameObject _handCardPanel;
    [SerializeField] TextMeshProUGUI _costText;
    [SerializeField] TextMeshProUGUI _mana;

    private void Awake()
    {
        EventManager.SetPlayerDeckNum += SetPlayerDeckNum;
        EventManager.SetCost += SetCost;
        EventManager.SetMana += SetMana;

    }

    private void OnDestroy()
    {
        EventManager.SetPlayerDeckNum -= SetPlayerDeckNum;
        EventManager.SetCost -= SetCost;
        EventManager.SetMana -= SetMana;
    }

    private void SetMana(int value)
    {
        _mana.text = value.ToString();
    }
    private void SetPlayerDeckNum(string value)
    {
        _playerDeckButtonText.text = value;
    }

    private void SetCost(int currentCost, int maxCost)
    {
        _costText.text = currentCost.ToString()+'/'+maxCost.ToString();
    }





}
