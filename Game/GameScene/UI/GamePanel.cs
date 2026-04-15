using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : BasePanel<GamePanel>
{
    //获取控件 关联场景上的控件对象

    public CustomGUILabel labScore;
    public CustomGUILabel labTime;
    public CustomGUIButton btnQuit;
    public CustomGUIButton btnSetting;
    public CustomGUITexture texHP;

    //用于记录玩家的当前分数
    [HideInInspector]
    public int nowScore = 0;

    //血条控件宽
    public float HPWidth = 300;

    [HideInInspector]
    public float nowTime = 0;
    private int time;

    void Start()
    {
        //监听界面上的一些控件操作
        btnSetting.clickEvent += () =>
        { 
            SettingPanel.Instance.Show();
            //改变时间缩放值为0 使时间停止
            Time.timeScale = 0;
        };
        btnQuit.clickEvent += () =>
        {
            //返回开始界面
            //弹出一个确定退出的按钮
            QuitPanel.Instance.Show();
            //改变时间缩放值为0 使时间停止
            Time.timeScale = 0;
        };
    }
    private void Update()
    {
        //通过帧间隔时间 进行累加 会比较准确
        nowTime += Time.deltaTime;
        //把秒转换成时分秒
        time = (int)nowTime;
        labTime.content.text = "";
        if (time / 3600 > 0) labTime.content.text += time / 3600 + "时";
        if (time % 3600 / 60 > 0 || labTime.content.text != "") labTime.content.text += time % 3600 / 60 + "分";
        labTime.content.text += time % 60 + "秒";
    }

    /// <summary>
    /// 提供给外部的加分方法
    /// </summary>
    /// <param name="score"></param>
    public void AddScore(int score)
    {
        nowScore += score;
        //更新界面显示
        labScore.content.text = nowScore.ToString();
    }
    
    /// <summary>
    /// 更新血条的方法
    /// </summary>
    /// <param name="MaxHP"></param>
    /// <param name="HP"></param>
    public void UpdateHP(int MaxHP,int HP)
    {
        texHP.guiPos.width = (float)HP / MaxHP * HPWidth;
    }
}
