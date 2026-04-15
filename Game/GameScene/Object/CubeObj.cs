using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeObj : MonoBehaviour
{
    //奖励物品预设体关联
    public GameObject[] rewardObjs;

    //死亡预设体关联
    public GameObject dieEff;
    private void OnTriggerEnter(Collider other)
    {
        
        //1.打到自己的子弹应该销毁
        //第一步不用在这里写，只需要把箱子的tag改成Cube
        //之前已经在子弹逻辑中处理过

        //2.达到自己 应该处理 随机创建奖励的逻辑
        //随机一个数 来获取奖励
        int rangeInt = Random.Range(0, 100);
        //50%的几率创建一个奖励
        if(rangeInt %2 == 0)
        {
            //随机创建一个奖励预设体 在当前位置
            rangeInt = Random.Range(0,rewardObjs.Length);
            Instantiate(rewardObjs[rangeInt], this.transform.position, this.transform.rotation);
        }
        //创建预设体
        GameObject effObj = Instantiate(dieEff,this.transform.position, this.transform.rotation);
        //控制音效
        AudioSource source = effObj.GetComponent<AudioSource>();
        source.volume = GameDataMgr.Instance.musicData.soundValue;
        source.mute = !GameDataMgr.Instance.musicData.isOpenSound;

        Destroy(this.gameObject);
    }
}
