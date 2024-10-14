using System;
using ET.Server;

namespace ET
{
    [MessageHandler(SceneType.UnitCache)]

    public class Other2UnitCache_DeleteUnitHandler: MessageHandler<Scene, Other2UnitCache_DeleteUnit, UnitCache2Other_DeleteUnit>
    {
        protected override async ETTask Run(Scene scene, Other2UnitCache_DeleteUnit request, UnitCache2Other_DeleteUnit response)
        {
            Log.Debug("Other2UnitCache_DeleteUnitHandler");

            UnitCacheComponent unitCacheComponent = scene.GetComponent<UnitCacheComponent>();

            unitCacheComponent.Delete(request.UnitId);
            
            await ETTask.CompletedTask;
        }
    }
}