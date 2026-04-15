using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitPanel : BasePanel<QuitPanel>
{
    public CustomGUIButton btnQuit;
    public CustomGUIButton btnContinue;
    public CustomGUIButton btnClose;
    void Start()
    {
        btnQuit.clickEvent += () =>
        {
            //回到主界面
            SceneManager.LoadScene("BeginScene");
        };

        //继续游戏和×都是关闭当前面板
        btnContinue.clickEvent += () =>
        {
            Hide();
        };
        btnClose.clickEvent += () =>
        {
            Hide();
        };
        Hide();
    }
    public override void Hide()
    {
        base.Hide();
        Time.timeScale = 1;
    }
}
