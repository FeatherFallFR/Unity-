using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对齐方式枚举
/// </summary>

public enum E_Aligment_Type
{
    Up,
    Down,
    Left,
    Right,
    Center,
    Left_Up,
    Left_Down,
    Right_Up,
    Right_Down,
}

/// <summary>
/// 该类是用来表示位置 计算位置相关信息的 不需要继承mono
/// </summary>
[System.Serializable]
public class CustomGUIPos
{
    //主要是处理 控件位置相关内容
    //要完成 分辨率自适应的相关计算
    //该位置信息 会用来返回给外部 用于绘制控件
    //但是要对它进行 计算
    private Rect rpos = new Rect(0, 0, 100, 100);

    //屏幕九宫格对齐方式
    public E_Aligment_Type screen_Aligment_Type = E_Aligment_Type.Center;
    //控件中心对齐方式
    public E_Aligment_Type control_Center_Aligment_Type = E_Aligment_Type.Center;
    //偏移位置
    public Vector2 pos;
    //宽高
    public float width = 100;
    public float height = 50;

    //用于计算的 中心点 成员变量
    private Vector2 centerPos;
    //计算中心点偏移的方法
    private void CalcCenterPos()
    {
        switch (control_Center_Aligment_Type)
        {
            case E_Aligment_Type.Up:
                centerPos.x = -width / 2;
                centerPos.y = 0;
                break;
            case E_Aligment_Type.Down:
                centerPos.x = -width / 2;
                centerPos.y = -height;
                break;
            case E_Aligment_Type.Left:
                centerPos.x = 0;
                centerPos.y = -height / 2;
                break;
            case E_Aligment_Type.Right:
                centerPos.x = -width;
                centerPos.y = -height / 2;
                break;
            case E_Aligment_Type.Center:
                centerPos.x = -width / 2;
                centerPos.y = -height / 2;
                break;
            case E_Aligment_Type.Left_Up:
                centerPos.x = 0;
                centerPos.y = 0;
                break;
            case E_Aligment_Type.Left_Down:
                centerPos.x = 0;
                centerPos.y = -height;
                break;
            case E_Aligment_Type.Right_Up:
                centerPos.x = -width;
                centerPos.y = 0;
                break;
            case E_Aligment_Type.Right_Down:
                centerPos.x = -width;
                centerPos.y = -height;
                break;
        }
    }

    //计算最终相对坐标位置的方法
    private void CalcPos()
    {
        switch (screen_Aligment_Type)
        {
            case E_Aligment_Type.Up:
                rpos.x = Screen.width / 2 + centerPos.x + pos.x;
                rpos.y = 0 + centerPos.y + pos.y;
                break;
            case E_Aligment_Type.Down:
                rpos.x = Screen.width / 2 + centerPos.x + pos.x;
                rpos.y = Screen.height + centerPos.y - pos.y;
                break;
            case E_Aligment_Type.Left:
                rpos.x = 0 + centerPos.x + pos.x;
                rpos.y = Screen.height / 2 + centerPos.y + pos.y;
                break;
            case E_Aligment_Type.Right:
                rpos.x = Screen.width + centerPos.x - pos.x;
                rpos.y = Screen.height / 2 + centerPos.y + pos.y;
                break;
            case E_Aligment_Type.Center:
                rpos.x = Screen.width / 2 + centerPos.x + pos.x;
                rpos.y = Screen.height / 2 + centerPos.y + pos.y;
                break;
            case E_Aligment_Type.Left_Up:
                rpos.x = 0 + centerPos.x + pos.x;
                rpos.y = 0 + centerPos.y + pos.y;
                break;
            case E_Aligment_Type.Left_Down:
                rpos.x = 0 + centerPos.x + pos.x;
                rpos.y = Screen.height + centerPos.y - pos.y;
                break;
            case E_Aligment_Type.Right_Up:
                rpos.x = Screen.width + centerPos.x - pos.x;
                rpos.y = 0 + centerPos.y + pos.y;
                break;
            case E_Aligment_Type.Right_Down:
                rpos.x = Screen.width + centerPos.x - pos.x;
                rpos.y = Screen.height + centerPos.y - pos.y;
                break;
        }   
    }
    /// <summary>
    /// 得到最终绘制的位置 和 宽高
    /// </summary>
    public Rect Pos
    {
        get
        { 
            //进行计算中心点偏移
            CalcCenterPos();
            //计算相对屏幕坐标点
            CalcPos();
            //宽高直接赋值 返回给外部 别人直接使用来绘制控件
            rpos.width = width;
            rpos.height = height;
            return rpos;
        }
    }
}
