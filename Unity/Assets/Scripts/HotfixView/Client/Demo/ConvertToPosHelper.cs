using FairyGUI;
using UnityEngine;

namespace ET.Client
{
    public static class ConvertToPosHelper
    {
        public static Vector2 ConvertToPos(Vector3 startPos)
        {
            //3D视图下的位置，转化到屏幕上之后的位置
            Vector3 pos = Camera.main.WorldToScreenPoint(startPos);
            //Unity初始位置在左下角，FGUI在左上角，所以需要取反
            pos.y = Screen.height - pos.y;
            //
            Vector2 pt = GRoot.inst.GlobalToLocal(pos);

            return pt;
        }
    }
}