using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneStartView : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Image _backgroundDark;
    private Color _backgroundDarkColor;

    void Start()
    {
        _backgroundDarkColor = _backgroundDark.color;
        _backgroundDarkColor.a = 0.5f;
        _backgroundDark.color = _backgroundDarkColor;
        EventManager.SetSelectionCG += SetSelectionCGUI;
        EventManager.SetSelection += SelectCharacter;
    }

    private void OnDestroy()
    {
        EventManager.SetSelectionCG -= SetSelectionCGUI;
        EventManager.SetSelection -= SelectCharacter;
    }

    private void SetSelectionCGUI(int value)
    {
        if (_background != null)
        {
            _background.sprite = DataBase.StartSceneBackgroundCG[value];
        }
    }

    private void SelectCharacter(int value)
    {
        EventManager.CallOnSoundID((DataBase.SoundID)value, 0);
        StartCoroutine(SceneFade(value));
    }
    private void SceneChange()
    {
        SceneManager.LoadScene("GameScene");
    }

    IEnumerator SceneFade(int value)
    {
        float deltaAlpha = 3f; ;
        
        WaitForEndOfFrame frame = new WaitForEndOfFrame();
        //yield return frame;
        while (_backgroundDarkColor.a > 0f)
        {
            _backgroundDarkColor.a -= Time.deltaTime * deltaAlpha;
            _backgroundDark.color = _backgroundDarkColor;
            yield return frame;
        }
        yield return new WaitForSeconds(3.5f);
        SceneChange();
    }



}
