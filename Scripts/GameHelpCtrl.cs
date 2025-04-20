using UnityEngine;
using UnityEngine.UI;

public class GameHelpCtrl : MonoBehaviour
{
    public Button confirmBtn;

    private void Start()
    {
        confirmBtn.onClick.AddListener(() => SetActive(false));
    }

    public void SetActive(bool show)
    {
        gameObject.SetActive(show);
    }
}