using System;

namespace ET.Server
{
    [MessageLocationHandler(SceneType.Map)]
    public class G2M_SessionDisconnectHandler: MessageLocationHandler<Unit, G2M_SessionDisconnect>
    {
        protected override async ETTask Run(Unit unit, G2M_SessionDisconnect message)
        {
            Log.Debug($"g2m session disconnect  {unit.Id}");

            Scene root = unit.Root();

            UnitComponent unitComponent = root.GetComponent<UnitComponent>();

            Log.Debug($"unit component child count {unitComponent.Children.Count}");

            foreach (var kv in unitComponent.Children)
            {
                Log.Debug($"kv key {kv.Key}");
            }

            UnitDBSaveComponent unitDbSaveComponent = unit.GetComponent<UnitDBSaveComponent>();
            
            unitDbSaveComponent.SaveChange();

            try
            {
                unitComponent.RemoveChild(unit.Id);
            }
            catch (Exception e)
            {
                Log.Error($"unit disponse {e}");
            }

            foreach (var kv in unitComponent.Children)
            {
                Log.Debug($"kv key {kv.Key}");
            }

            await ETTask.CompletedTask;
        }
    }
}