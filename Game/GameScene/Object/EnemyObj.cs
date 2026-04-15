using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyObj : TankBaseObj
{
    //1.要让坦克在两个点之间来回移动
    //当前的目标点
    public Transform targetPos;
    //随机用的点 外面去关联
    public Transform[] randomPos;

    //2.坦克要一直盯着自己的目标
    public Transform lookAtTarget;

    //3.当目标到达一定范围内 间隔一段时间 攻击目标
    //开火距离 当小于该值时 就会主动攻击
    public float fireDis = 5;
    //攻击间隔时间
    public float fireOffsetTime;
    private float nowTime = 0;

    //开火点
    public Transform[] shootPos;
    //子弹预设体
    public GameObject bulletObj;

    //血条的图外面关联
    public Texture maxHpBK;
    public Texture hpBK;

    //之所以没有new 是因为是结构体 可以不用new 直接在下面赋值
    private Rect maxHpRect;
    private Rect hpRect;

    //显示血条计时用时间
    private float showTime;

    void Start()
    {
        RandomPos();
    }

    private float hpUIScale;

    // Update is called once per frame
    void Update()
    {
        #region 多个点之间的随机移动逻辑
        //看向目标点
        this.transform.LookAt(targetPos);
        //不停的移动
        this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        //知识点 Vector3里有一个得到两个点距离的方法
        //当距离过小时 认为到达了目的地 重新随机一个点
        if(Vector3.Distance(this.transform.position,targetPos.position) <= 0.5f)
        {
            RandomPos();
        }
        #endregion

        #region 看向自己的目标
        if (lookAtTarget != null)
        {
            tankHead.LookAt(lookAtTarget);
            //当自己和目标对象的距离小于等于 配置的 开火距离时
            if(Vector3.Distance(this.transform.position,lookAtTarget.position) <= fireDis)
            {
                nowTime += Time.deltaTime;
                if (nowTime >= fireOffsetTime)
                {
                    Fire();
                    nowTime = 0;
                }
            }
        }
        #endregion

    }

    private void RandomPos()
    {
        if (randomPos.Length == 0)
            return;

        targetPos = randomPos[Random.Range(0,randomPos.Length)];
    }
    public override void Fire()
    {
        for(int i = 0; i < shootPos.Length; i++)
        {
            GameObject obj = Instantiate(bulletObj, shootPos[i].transform.position, shootPos[i].transform.rotation);
            //设置子弹拥有者
            BulletObj bullet = obj.GetComponent<BulletObj>();
            bullet.SetFather(this);
        }
    }
    public override void Die()
    {
        base.Die();
        //移动敌人死亡时要加分
        GamePanel.Instance.AddScore(10);
    }

    //在这里进行血条UI的绘制
    private void OnGUI()
    {
        showTime -= Time.deltaTime;
        if (showTime > 0)
        {
            //画血条
            //1.把敌人当前位置 转换成屏幕位置
            //摄像机提供了API 将世界坐标转换为屏幕坐标
            Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
            //根据远近使血条大小发生变化
            hpUIScale = 30 / screenPos.z;
            //2.屏幕位置转换为GUI位置
            //如何得到当前屏幕的分辨率的高
            screenPos.y = Screen.height - screenPos.y;
            //然后再绘制
            //底图
            maxHpRect.x = screenPos.x - 50;
            maxHpRect.y = screenPos.y - 60;
            maxHpRect.width = 100 * hpUIScale;
            maxHpRect.height = 15 * hpUIScale;
            //画底图
            GUI.DrawTexture(maxHpRect, maxHpBK);
            //血条
            hpRect.x = screenPos.x - 50;
            hpRect.y = screenPos.y - 60;
            //根据血量和最大血量的百分比 决定画多宽
            hpRect.width = (float)HP / maxHP * 100f * hpUIScale;
            hpRect.height = 15 * hpUIScale;
            //画血条
            GUI.DrawTexture(hpRect, hpBK);
        }
    }

    public override void Wound(TankBaseObj other)
    {
        base.Wound(other);
        //设置显示血条的时间
        showTime = 3;
    }
}
