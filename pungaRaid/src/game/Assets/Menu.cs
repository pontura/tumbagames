using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour {

    public GameObject canvas;
    public GameObject confirmCanvas;
    public Text inputText;
    public GameObject WrongCanvas;

	void Start () {
        canvas.SetActive(false);
        confirmCanvas.SetActive(false);
        WrongCanvas.SetActive(false);

        if (Data.Instance.MainMenuPopupOn)
        {
            Init();
            Data.Instance.MainMenuPopupOn = false;
        }
	}
    
    public void Init()
    {
        canvas.SetActive(true);
    }

    public void Privacy()
    {
        Events.OnSoundFX("buttonPress");
        Data.Instance.LoadLevel("07_PrivacyPolicy");
        Data.Instance.MainMenuPopupOn = true;
    }

    public void Reset()
    {
        Events.OnSoundFX("buttonPress");
        confirmCanvas.SetActive(true);
       // Close();
    }

    public void Close()
    {
        canvas.SetActive(false);
        Events.OnSoundFX("backPress");
    }
    public void Confirm()
    {
        Events.OnSoundFX("buttonPress");
        confirmCanvas.SetActive(true);
        Close();
    }
    public void CloseConfirm()
    {
        Events.OnSoundFX("buttonPress");
        confirmCanvas.SetActive(false);
    }
    public void ConfirmReset()
    {
        Events.OnSoundFX("buttonPress");
        if (inputText.text == "56")
        {
            inputText.text = "";
            Data.Instance.GetComponent<UserData>().Reset();
            confirmCanvas.SetActive(false);
            Close();
        }
        else
        {
            WrongCanvas.SetActive(true);
        }
    }
    public void CloseWrong()
    {
        Events.OnSoundFX("buttonPress");
        WrongCanvas.SetActive(false);
    }
}
