using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public class SettingPanel : BasePanel<SettingPanel>
{
    //申明成员变量 关联控件
    public CustomGUISlider sliderMusic;
    public CustomGUISlider sliderSound;

    public CustomGUIToggle togMusic;
    public CustomGUIToggle togSound;

    public CustomGUIButton btnClose;

    void Start()
    {
        //监听对应的事件 处理逻辑
        //处理音乐的变化
        sliderMusic.changeValue += (value) => GameDataMgr.Instance.ChangeBKValue(value);
        //处理音效的变化
        sliderSound.changeValue += (value) => GameDataMgr.Instance.ChangeSoundVAlue(value);
        //处理音乐的开关
        togMusic.changeValue += (value) => GameDataMgr.Instance.OpenOrClsoeBKMusic(value);
        //处理音效的开关
        togSound.changeValue += (value) => GameDataMgr.Instance.OpenOrClsoeSound(value);

        btnClose.clickEvent += () =>
        {
            //点击后隐藏自己
            Hide();

            //判断当前所在场景
            if (SceneManager.GetActiveScene().name == "BeginScene")
            {
                //显示开始面板
                BeginPanel.Instance.Show();
            }
        };
        Hide();
    }

    //过呢据数据更新面板
    public void UpdatePanelInfo()
    {
        //面板上的信息都是根据音效数据更新的
        MusicData data = GameDataMgr.Instance.musicData;

        //设置面板内容
        sliderMusic.nowValue = data.bkValue;
        sliderSound.nowValue = data.soundValue;
        togMusic.isSel = data.isOpenBK;
        togSound.isSel = data.isOpenSound;
    }

    public override void Show()
    {
        base.Show();
        //每次显示面板时 顺便把面板上的内容都更新了
        UpdatePanelInfo();
    }
    public override void Hide()
    {
        base.Hide();
        Time.timeScale = 1;
    }
}
