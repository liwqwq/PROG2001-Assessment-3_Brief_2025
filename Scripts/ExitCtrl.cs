using UnityEngine;
using UnityEngine.UI;

public class ExitCtrl : MonoBehaviour
{
    public Button confirmBtn;
    public Button cancelBtn;

    private void Start()
    {
        confirmBtn.onClick.AddListener(Application.Quit);
        cancelBtn.onClick.AddListener(() => SetActive(false));
    }

    public void SetActive(bool show)
    {
        gameObject.SetActive(show);
    }
}