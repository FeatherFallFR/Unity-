using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTower : TankBaseObj
{
    //自动旋转已经实现

    //间隔开火
    // 应该有一个间隔时间
    public float fireOffsetTime = 1;
    //记录累加时间 用于间隔开火判断
    private float nowTime = 0;

    // 发射位置
    public Transform[] shootPos;

    // 子弹预设体关联
    public GameObject bulletObj;

    void Update()
    {
        //不停累加时间并记录下来 当时间超过间隔时间时就开火
        nowTime += Time.deltaTime;
        if (nowTime >= fireOffsetTime)
        {
            Fire();
            nowTime = 0;
        }
    }
    public override void Fire()
    {
        for(int i = 0; i < shootPos.Length; i++)
        {
            //发射子弹
            GameObject obj = GameObject.Instantiate(bulletObj, shootPos[i].transform.position, shootPos[i].transform.rotation);
            //设置子弹的拥有者 方便后面进行属性计算
            BulletObj bullet = obj.GetComponent<BulletObj>();
            bullet.SetFather(this);
        }
    }

    public override void Wound(TankBaseObj other)
    {
        //直接什么都不写 
        //目的是让该炮塔无敌
    }
}
