using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginPanel : BasePanel<BeginPanel>
{
    //首先 申明公共成员变量来关联 各个控件
    public CustomGUIButton btnBegin;
    public CustomGUIButton btnSetting;
    public CustomGUIButton btnRank;
    public CustomGUIButton btnQuit;
    void Start()
    {
        //目的是方便控制坦克的头部转向
        Cursor.lockState = CursorLockMode.Confined;
        //监听一次按钮点击过后要做什么
        btnBegin.clickEvent += () => 
        {
            //切换场景
            SceneManager.LoadScene("GameScene");
        };
        btnSetting.clickEvent += () =>
        {
            //隐藏自己 避免穿透
            Hide();
            //打开设置面板
            SettingPanel.Instance.Show();
        };
        btnRank.clickEvent += () =>
        {
            //打开排行榜面板
            RankPanel.Instance.Show();
            //避免穿透 隐藏自己
            Hide();
        };
        btnQuit.clickEvent += () =>
        {
            
            //退出游戏
            Application.Quit();
        };
    }
}
