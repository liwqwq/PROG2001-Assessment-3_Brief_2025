using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCtrl : MonoBehaviour
{
    public Button StartBtn;
        public Button SetBtn;
            public Button HelpBtn;
                public Button ExitBtn;

public GameSettingCtrl gameSettingCtrl;

public GameHelpCtrl gameHelpCtrl;

public ExitCtrl exitCtrl;
    void Start()
    {
        gameSettingCtrl.SetActive(false);
         gameHelpCtrl.SetActive(false);
         exitCtrl.SetActive(false);
         StartBtn.onClick.AddListener(StartGame);
          SetBtn.onClick.AddListener(() => gameSettingCtrl.SetActive(true));
           HelpBtn.onClick.AddListener(() => gameHelpCtrl.SetActive(true));
            ExitBtn.onClick.AddListener(() => exitCtrl.SetActive(true));
    }

    void StartGame()
    {
        print("开始游戏");
    }

}
