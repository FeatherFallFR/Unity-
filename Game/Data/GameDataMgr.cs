using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏数据管理类 一个单例模式
/// </summary>
public class GameDataMgr
{
    private static GameDataMgr instance = new GameDataMgr();
    public static GameDataMgr Instance => instance;

    //音乐数据对象
    public MusicData musicData;
    //排行榜数据对象
    public RankList rankData;

    private GameDataMgr()
    {
        //可以初始化游戏数据
        musicData = PlayerPrefsDataManager.Instance.LoadData(typeof(MusicData), "Music") as MusicData;
        //如果第一次进游戏没有音乐数据 那么所有的数据要么是false要么是0
        if( !musicData.notFirst )
        {
            musicData.notFirst = true;
            musicData.isOpenBK = true;
            musicData.isOpenSound = true;
            musicData.bkValue = 1;
            musicData.soundValue = 1;
            PlayerPrefsDataManager.Instance.SaveData(musicData, "Music");
        }

        //初始化读取排行榜
        rankData = PlayerPrefsDataManager.Instance.LoadData(typeof(RankList),"Rank") as RankList;
        
    }

    //提供一些API给外部 方便数据的改变存储

    //提供一个 在排行榜中添加数据得方法
    public void AddRankInfo(string name, int score, float time)
    {
        rankData.list.Add(new RankInfo(name,score,time));
        //排序
        rankData.list.Sort((a, b) => a.score + a.time < b.score + b.time ? -1 : 1);
        //排序过后移除多余的数据
        for (int i = rankData.list.Count - 1; i >= 6; i--)
        {
            //从尾部往前遍历 移除每一条
            rankData.list.RemoveAt(i);
        }
        PlayerPrefsDataManager.Instance.SaveData(rankData,"Rank");
    }

    //开启或者关闭背景音乐
    public void OpenOrClsoeBKMusic(bool isOpen)
    {
        musicData.isOpenBK = isOpen;
        
        //在这里控制场景上的背景音乐开关
        BKMusic.Instance.ChangeOpen(isOpen);

        //存储改变后的数据
        PlayerPrefsDataManager.Instance.SaveData(musicData,"Music");
    }
    //开启或者关闭音效
    public void OpenOrClsoeSound(bool isOpen)
    {
        musicData.isOpenSound = isOpen;
        //存储改变后的数据
        PlayerPrefsDataManager.Instance.SaveData(musicData, "Music");
    }
    //改变背景音乐大小
    public void ChangeBKValue(float value)
    {
        musicData.bkValue = value;

        //在这里控制场景上的背景音乐大小
        BKMusic.Instance.ChangeValue(value);

        //存储改变后的数据
        PlayerPrefsDataManager.Instance.SaveData(musicData, "Music");
    }
    //改变音效大小
    public void ChangeSoundVAlue(float value)
    {
        musicData.soundValue = value;
        //存储改变后的数据
        PlayerPrefsDataManager.Instance.SaveData(musicData, "Music");
    }
}
