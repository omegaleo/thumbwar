using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonEvents : MonoBehaviour
{
    [SerializeField] private Image selectedHighlight;
    [SerializeField] private Image selectedShadowHighlight;

    void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    private void Update()
    {
        if(EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            if(selectedHighlight != null && selectedShadowHighlight != null)
            {
                selectedHighlight.enabled = true;
                selectedShadowHighlight.enabled = true;
            }
        }
        else
        {
            if (selectedHighlight != null && selectedShadowHighlight != null)
            {
                selectedHighlight.enabled = false;
                selectedShadowHighlight.enabled = false;
            }
        }
    }

    public void StartGame()
    {
        if (GameMaster.instance.sfx != null)
        {
            GameMaster.instance.sfx.clip = GameMaster.instance.btnClick;
            GameMaster.instance.sfx.loop = false;
            GameMaster.instance.sfx.Play();
        }

        ChangeScene(GameMaster.instance.NewGameScene);
        GameMaster.instance.StartGame();
    }

    public void Credits()
    {
        if (GameMaster.instance.sfx != null)
        {
            GameMaster.instance.sfx.clip = GameMaster.instance.btnClick;
            GameMaster.instance.sfx.loop = false;
            GameMaster.instance.sfx.Play();
        }
        //ChangeScene(GameMaster.instance.CreditsScene);
    }

    public void Options()
    {
        if (GameMaster.instance.sfx != null)
        {
            GameMaster.instance.sfx.clip = GameMaster.instance.btnClick;
            GameMaster.instance.sfx.loop = false;
            GameMaster.instance.sfx.Play();
        }
        GameMaster.instance.ToggleOptions();
    }

    public void Exit()
    {
        if (GameMaster.instance.sfx != null)
        {
            GameMaster.instance.sfx.clip = GameMaster.instance.btnClick;
            GameMaster.instance.sfx.loop = false;
            GameMaster.instance.sfx.Play();
        }
        Application.Quit();
    }
}
