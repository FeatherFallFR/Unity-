using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGUIToggleGroup : MonoBehaviour
{
    public CustomGUIToggle[] toggles;

    //记录上一次为true的toggle
    public CustomGUIToggle frontTruTog;
    private void Start()
    {
        if(toggles.Length == 0)
        {
            return;
        }
        //通过遍历来为多个 多选框添加监听事件函数
        //在函数中做处理
        //当一个为true 其他变为false
        for (int i = 0; i < toggles.Length; i++)
        {
            CustomGUIToggle toggle = toggles[i];
            toggle.changeValue += (value) => 
            {
                //当传入的value为true时 需要把另外的变成false
                if (value)
                {
                    //意味着另外的要变成false
                    for(int j = 0; j < toggles.Length; j++)
                    {
                        //这里有闭包 toggle就是上一个函数中申明的变量
                        //改变了它的生命周期
                        if (toggles[j] != toggle)
                        {
                            toggles[j].isSel = false;
                        }
                    }
                    frontTruTog = toggle;
                }
                //判断当前变成false的Toggle是不是上一次为true的
                //如果是 就不应该让他变为false
                else if(frontTruTog == toggle)
                {
                    //强制改成true
                    toggle.isSel = true;
                }
            };
        }
    }
}
