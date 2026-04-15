using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : BasePanel<LosePanel>
{
    public CustomGUIButton btnBack;
    public CustomGUIButton btnRestart;

    void Start()
    {
        Time.timeScale = 0;
        btnBack.clickEvent += () =>
        {
            //取消暂停
            Time.timeScale = 1;
            //切换场景
            SceneManager.LoadScene("BeginScene");
        };
        btnRestart.clickEvent += () =>
        {
            Time.timeScale = 1;
            //再次切换到游戏场景 就可以达到 所有内容重新加载 从头开始的目的
            SceneManager.LoadScene("GameScene");
        };
        Hide();
    }
}
