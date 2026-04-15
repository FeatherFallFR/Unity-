using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BKMusic : MonoBehaviour
{
    private static BKMusic instance;

    public static BKMusic Instance => instance;

    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
        //得到自己依附的游戏对象上挂载的音频源脚本
        audioSource = GetComponent<AudioSource>();
        //初始化时 把大小和开关进行设置
        ChangeValue(GameDataMgr.Instance.musicData.bkValue);
        ChangeOpen(GameDataMgr.Instance.musicData.isOpenBK);
    }
    /// <summary>
    /// 改变音乐大小
    /// </summary>
    /// <param name="value"></param>
    public void ChangeValue(float value)
    {
        audioSource.volume = value;
    }

    /// <summary>
    /// 开关背景音乐
    /// </summary>
    /// <param name="isOpen"></param>
    public void ChangeOpen(bool isOpen)
    {
        //开启是不静音 没开启就是静音
        audioSource.mute = !isOpen;
    }
}
