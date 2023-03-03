using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _enemySprite;
    [SerializeField] private TextMeshProUGUI _enemyHpText;
    [SerializeField] private Image _enemyHpBar;
    [SerializeField] private GameObject _enemyDefence;
    [SerializeField] private Animator _enemyAnim;
    [SerializeField] private TextMeshProUGUI _enemyDefenceText;
    private readonly int HitMotion = Animator.StringToHash("IsHit");

    public void SetEnemyUI(EnemyData data, Sprite sprite)
    {
        _enemySprite.sprite = sprite;
        SetEnemyHp(data.EnemyHP, data.EnemyHP);
    }

    public void SetEnemyHp(int currentHP, int maxHP)
    {
        _enemyHpBar.fillAmount = (float)currentHP / maxHP;
        _enemyHpText.text = currentHP.ToString() + "/" + maxHP.ToString();
    }

    public void SetEnemyDefence(int value)
    {
        if (value <= 0)
        {
            _enemyDefence.SetActive(false);
        }
        else
        {
            _enemyDefence.SetActive(true);
            _enemyDefenceText.text = value.ToString();
        }
    }

    public void SetEnemyAnim(int value1)
    {
        switch (value1)
        {
            case 1:
                _enemyAnim.SetTrigger(HitMotion);
                break;
        }

        //공격 피격 죽음
    }
}
