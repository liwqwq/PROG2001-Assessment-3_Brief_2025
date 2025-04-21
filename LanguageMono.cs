using UnityEngine;
using UnityEngine.UI;

public class LanguageMono : MonoBehaviour
{
    public bool isObj = false;
    public GameObject[] mObjs;
    public string[] mStrs;

    public void ChangeLanguage()
    {
        int index = (int)GameSettingCtrl.languageType;
        if (isObj)
        {
            for (int i = 0; i < mObjs.Length; i++)
            {
                mObjs[i].SetActive(index == i);
            }
        }
        else
        {
            var txt = GetComponent<Text>();
            txt.text = mStrs[index];
        }
    }
}