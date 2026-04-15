using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TankBaseObj : MonoBehaviour
{
    public int atk;
    public int def;
    public int maxHP;
    public int HP;

    //所有坦克都有炮台相关
    public Transform tankHead;

    public float moveSpeed = 10;
    public float roundSpeed = 100;
    public float headRoundSpeed = 100;

    //死亡特效 关联对应预设体 死亡的时候 动态创建出来 设置位置即可
    public GameObject dieEff;

    /// <summary>
    /// 开火抽象方法 子类重写开火方法
    /// </summary>
    public abstract void Fire();

    /// <summary>
    /// 被别人攻击 造成自己受伤
    /// </summary>
    /// <param name="other"></param>
    public virtual void Wound(TankBaseObj other)
    {
        int dmg = other.atk - this.def;
        if (dmg <= 0) return;
        //如果伤害大于0就应该减血
        HP -= dmg;
        //判断如果血量小于等于0 就应该死亡
        if(this.HP <= 0)
        {
            this.HP = 0;
            this.Die();
        }
    }

    /// <summary>
    /// 死亡行为 当自己血量小于等于0时 就应该死亡
    /// </summary>
    public virtual void Die()
    {
        //对象死亡 就是在场景上移除该对象
        Destroy(this.gameObject);
        //死亡的时候 可能所有的坦克都应该播放一个对应的特效
        if(dieEff!= null)
        {
            //实例化对象时 顺便把位置和角度都一起设置了
            GameObject effObj = Instantiate(dieEff, this.transform.position, this.transform.rotation);
            //由于该特效对象身上直接关联了音效 所以可以在此处 把音效播放相关也控制了
            AudioSource audioSource = effObj.GetComponent<AudioSource>();
            //根据音乐数据 设置音效大小
            audioSource.volume = GameDataMgr.Instance.musicData.soundValue;
            //是否播放
            audioSource.mute = !GameDataMgr.Instance.musicData.isOpenSound;
            //避免没有勾选PlayOnAwake
            audioSource.Play();
        }
    }
}
