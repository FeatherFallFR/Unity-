using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletObj : MonoBehaviour
{
    //移动速度
    public float moveSpeed = 50;
    //设发射的子弹
    public TankBaseObj fatherObj;

    public GameObject effObj;

    void Start()
    {
        
    }

    void Update()
    {
        this.transform.Translate(Vector3.forward *  moveSpeed * Time.deltaTime); 
    }

    //和别人碰撞触发时
    private void OnTriggerEnter(Collider other)
    {
        //子弹射击到立方体 会爆炸
        //同样 子弹射击到 不同阵营的对象也应该爆炸
        if( other.CompareTag("Cube") ||
            other.CompareTag("Player") && fatherObj.CompareTag("Enemy") ||
            other.CompareTag("Enemy") && fatherObj.CompareTag("Player"))
        {
            //判断受伤
            //得到碰撞到的对象身上是否有坦克相关脚本 用里氏替换原则 通过父类去获取
            TankBaseObj obj = other.gameObject.GetComponent<TankBaseObj>();
            if (obj != null)
                obj.Wound(fatherObj);

            //当子弹销毁时 可以创建一个爆炸特效
            if(effObj != null)
            {
                //创建爆炸特效
                GameObject eff = Instantiate(effObj, this.transform.position, this.transform.rotation);
                //改音效的音量和开启状态
                AudioSource audioSource = eff.GetComponent<AudioSource>();
                //设置大小
                audioSource.volume = GameDataMgr.Instance.musicData.soundValue;
                //设置是否开启
                audioSource.mute = !GameDataMgr.Instance.musicData.isOpenSound;
            }
            Destroy(this.gameObject);
        }
    }

    public void SetFather(TankBaseObj obj)
    {
        fatherObj = obj;
    }
}
