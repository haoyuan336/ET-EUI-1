using FairyGUI;
using UnityEngine;

namespace ET.Client
{
    public class HPBarComponent : Entity, IAwake<GameObject>, IUpdate, IDestroy
    {
        public FGUIHPProgressItemCellComponent CellComponent = null;

        public long OutTime;

        public GameObject GameObject;

        public Controller ColorState;
    }
}