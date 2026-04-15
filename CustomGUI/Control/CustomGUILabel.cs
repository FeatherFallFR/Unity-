using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGUILabel : CustomGUIControl
{
    protected override void StyleOnDraw()
    {
        GUI.Label(guiPos.Pos,content,style);
    }

    protected override void StyleOffDraw()
    {
        GUI.Label(guiPos.Pos,content);
    }
}
