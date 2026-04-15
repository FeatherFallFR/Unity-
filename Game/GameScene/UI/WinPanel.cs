using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class WinPanel : BasePanel<WinPanel>
{
    //关联控件
    public CustomGUIInput inputInfo;
    public CustomGUIButton btnSure;

    void Start()
    {
        btnSure.clickEvent += () =>
        {
            //取消游戏暂停
            Time.timeScale = 1;
            //把数据记录到排行榜中
            GameDataMgr.Instance.AddRankInfo(inputInfo.content.text, GamePanel.Instance.nowScore, GamePanel.Instance.nowTime);
            //接着返回开始界面
            SceneManager.LoadScene("BeginScene");
        };
        Hide();
    }
}
