using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CustomGUIRoot : MonoBehaviour
{
    //用于存储 子对象 所有GUI控件的容器
    private CustomGUIControl[] allControls;
    void Start()
    {
    }

    //统一绘制子对象的内容
    private void OnGUI()
    {
        //这句代码浪费性能 因为每次GUI都会来获取所有的 控件对应的脚本
        allControls = this.GetComponentsInChildren<CustomGUIControl>();
        //遍历每一个控件 让其执行绘制
        for(int i = 0; i < allControls.Length; i++)
        {
            allControls[i].DrawGUI();
        }
    }
}
