using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankPanel : BasePanel<RankPanel>
{
    //关联控件对象
    public CustomGUIButton btnClose;

    //因为控件较多 拖的话工作量太大 直接偷懒用代码
    private List<CustomGUILabel> labName = new List<CustomGUILabel>();
    private List<CustomGUILabel> labScore = new List<CustomGUILabel>();
    private List<CustomGUILabel> labTime = new List<CustomGUILabel>();
    void Start()
    {
        for(int i = 1; i <= 6; i++)
        {
            //小知识：找子对象的子对象 可以通过 / 来区分父子关系
            labName.Add(this.transform.Find("Name/labName" + i).GetComponent<CustomGUILabel>());
            labScore.Add(this.transform.Find("Score/labScore" + i).GetComponent<CustomGUILabel>());
            labTime.Add(this.transform.Find("Time/labTime" + i).GetComponent<CustomGUILabel>());
        }
        //处理事件监听逻辑
        btnClose.clickEvent += () =>
        {
            Hide();
            BeginPanel.Instance.Show();
        };
        //测试
        //GameDataMgr.Instance.AddRankInfo("测试数据", 100, 8432);
        Hide();
    }

    public void UpdatePanelInfo()
    {
        base.Show();
        UpdatePanelInfo();
    }

    void Update()
    {
        //处理根据排行榜数据 更新面板
        //获取GameDataMgr中的排行榜列表 用于在这里更新即可
        //得数据
        List<RankInfo> list = GameDataMgr.Instance.rankData.list;
        //根据列表更新面板
        for (int i = 0; i < list.Count; i++)
        {
            //名字
            labName[i].content.text = list[i].name;
            //分数
            labScore[i].content.text = list[i].score.ToString();
            //时间 存储得时间单位时秒
            //把秒转换成时分秒
            int time = (int)list[i].time;
            labTime[i].content.text = "";
            //得到 几个小时
            if (time / 3600 > 0) labTime[i].content.text += time / 3600 + "时";
            if (time % 3600 / 60 > 0 || labTime[i].content.text != "") labTime[i].content.text += time % 3600 / 60 + "分";
            labTime[i].content.text += time % 60 + "秒";
        }
    }
}
