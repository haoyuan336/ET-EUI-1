using FairyGUI;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace ET.Client
{
    public class HPBarComponent : Entity, IAwake, IUpdate, IDestroy
    {
        public FGUIHPProgressItemCellComponent CellComponent = null;

        public long OutTime;

        public Controller ColorState;
    }
}