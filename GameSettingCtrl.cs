using UnityEngine;
using UnityEngine.UI;

public enum LanguageType
{
    English,
    Chinese,
}
public class GameSettingCtrl : MonoBehaviour
{
    public Button audioBtn;
    public Sprite audioMuteSp;
    public Sprite audioDefSp;

    public Slider audioSlider;

    public Toggle[] lanTogs;

    public Button confirmBtn;

    private bool isAudioMuted = false;

    public static LanguageType languageType;

    public Transform root;
    private void Start()
    {
        audioSlider.onValueChanged.AddListener(SetAudioVolume);

        audioBtn.onClick.AddListener(ToggleAudio);

        confirmBtn.onClick.AddListener(() => SetActive(false));

        foreach (var toggle in lanTogs)
        {
            toggle.onValueChanged.AddListener(delegate { SetLanguage(toggle); });
        }
    }

    private void SetAudioVolume(float volume)
    {
        Debug.Log("SetAudioVolume" + volume);
        AudioListener.volume = volume;
        if (volume <= 0)
        {
            SetAudioEnable(true);
        }
        else
        {
            SetAudioEnable(false);
        }
    }

    private void ToggleAudio()
    {
        isAudioMuted = !isAudioMuted;
        SetAudioEnable(isAudioMuted);
    }

    private void SetAudioEnable(bool enable)
    {
        Debug.Log("ToggleAudio" + enable);
        AudioListener.pause = enable;
        audioBtn.image.sprite = enable ? audioMuteSp : audioDefSp;
        isAudioMuted = enable;
    }

    private void SetLanguage(Toggle selectedToggle)
    {
        if (selectedToggle.isOn)
        {
            int index = System.Array.IndexOf(lanTogs, selectedToggle);
            languageType = (LanguageType)index;
            var objs = root.GetComponentsInChildren<LanguageMono>(true);
            for (int i = 0; i < objs.Length; i++)
            {
                objs[i].ChangeLanguage();
            }
            //Debug.Log("Selected language: " + index);
        }
    }

    public void SetActive(bool show)
    {
        gameObject.SetActive(show);
    }
}