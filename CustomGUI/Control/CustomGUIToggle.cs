using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomGUIToggle : CustomGUIControl
{
    public event UnityAction<bool> changeValue;
    public bool isSel;
    private bool isOldSel;
    protected override void StyleOffDraw()
    {
        isSel = GUI.Toggle(guiPos.Pos, isSel, content);
        //只有变化时才告诉外部执行函数 没有必要一直告诉别人同一个值
        if (isOldSel != isSel)
        {
            changeValue?.Invoke(isSel);
            isOldSel = isSel;
        }
    }
    protected override void StyleOnDraw() 
    {
        isSel = GUI.Toggle(guiPos.Pos, isSel, content, style);
        if (isOldSel != isSel)
        {
            changeValue?.Invoke(isSel);
            isOldSel = isSel;
        }
    }
}
