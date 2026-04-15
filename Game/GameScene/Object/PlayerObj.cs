using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObj : TankBaseObj
{
    //当前装备的武器
    public WeaponObj nowWeapon;

    //武器父对象位置
    public Transform weaponPos;

    void Update()
    {
        //1.ws控制前进后退
        //知识点
        //1.Transform 位移
        //2.Input 轴向输入检测
        this.transform.Translate(Input.GetAxis("Vertical") * Vector3.forward * moveSpeed * Time.deltaTime);

        //2.ad控制旋转
        //知识点
        //1.Transform 旋转
        //2.Input 轴向输入检测
        this.transform.Rotate(Input.GetAxis("Horizontal") * Vector3.up *  roundSpeed * Time.deltaTime);

        //3.鼠标左右移动 控制炮台旋转
        //知识点
        //1.Transform 旋转
        //2.Input 鼠标输入检测
        tankHead.transform.Rotate(Input.GetAxis("Mouse X") * Vector3.up * headRoundSpeed * Time.deltaTime);

        //4.开火
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }
    //重写父类中的行为 玩家可能会存在特殊处理

    public override void Fire()
    {
        if (nowWeapon != null)
        {
            nowWeapon.Fire();
        }
    }
    public override void Die()
    {
        //这里不执行父类对的死亡 因为玩家坦克挂载着主摄像机 如果执行父类的死亡
        //就会把玩家坦克连同摄像机一起移除 会出问题
        //base.Die()
        //处理死亡逻辑
        Time.timeScale = 0;
        LosePanel.Instance.Show();
    }

    public override void Wound(TankBaseObj other)
    {
        base.Wound(other);
        //更新主面板血条
        GamePanel.Instance.UpdateHP(this.maxHP, this.HP);
    }

    /// <summary>
    /// 切换武器
    /// </summary>
    /// <param name="obj"></param>
    public void ChangeWeapon(GameObject weapon)
    {
        //删除当前拥有的武器
        if (nowWeapon != null)
        {
            Destroy(nowWeapon.gameObject);
            nowWeapon = null;
        }

        //切换武器
        //创建出武器 设置其父对象 并且保证其缩放正确
        GameObject weaponObj = Instantiate(weapon, this.weaponPos, false);
        nowWeapon = weaponObj.GetComponent<WeaponObj>();
        //设置武器拥有者
        nowWeapon.SetFather(this);
    }
}
