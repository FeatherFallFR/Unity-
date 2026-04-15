using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_PropType
{
    //加属性的类型
    Atk,
    Def,
    MaxHP,
    Hp
}

public class PropReward : MonoBehaviour
{
    public E_PropType type = E_PropType.Atk;
    public GameObject getEff;

    //默认添加的值 
    public int changeValue = 2;

    private void OnTriggerEnter(Collider other)
    {
        //玩家才能获取属性奖励
        if (other.CompareTag("Player"))
        {
            //得到对应的玩家脚本
            PlayerObj player = other.GetComponent<PlayerObj>();
            //根据类型加属性
            switch (type)
            {
                case E_PropType.Atk:
                    player.atk += changeValue;
                    break;
                case E_PropType.Def:
                    player.def += changeValue;
                    break;
                case E_PropType.MaxHP:
                    player.maxHP += changeValue;
                    player.HP += changeValue;
                    if(player.HP > player.maxHP)
                        player.HP = player.maxHP;
                    //更新血条
                    GamePanel.Instance.UpdateHP(player.maxHP,player.HP);
                    break;
                case E_PropType.Hp:
                    player.HP += changeValue;
                    //不能超过上限
                    if(player.HP > player.maxHP)
                        player.HP = player.maxHP;
                    //更新血条
                    GamePanel.Instance.UpdateHP(player.maxHP, player.HP);
                    break;
            }
            
            GameObject eff = Instantiate(getEff, this.transform.position, this.transform.rotation);
            AudioSource audioSource = eff.GetComponent<AudioSource>();
            audioSource.volume = GameDataMgr.Instance.musicData.soundValue;
            audioSource.mute = !GameDataMgr.Instance.musicData.isOpenSound;
            
            Destroy(this.gameObject);
        }
    }
}
