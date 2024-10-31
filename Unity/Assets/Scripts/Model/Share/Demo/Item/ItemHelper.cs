using System.Collections.Generic;
using ET.Client;

namespace ET
{
    public static class ItemHelper
    {
#if UNITY

        public static async ETTask<int> AddItemCount(Scene scene, int itemConfigId, int addCount)
        {
            C2M_AddItemCountRequest request = C2M_AddItemCountRequest.Create();

            request.ItemConfigId = itemConfigId;

            request.ItemCount = addCount;

            M2C_AddItemCountResponse response = await scene.GetComponent<ClientSenderComponent>().Call(request) as M2C_AddItemCountResponse;

            if (response.Error == ErrorCode.ERR_Success)
            {
                Unit unit = UnitHelper.GetMyUnit(scene);

                ItemComponent itemComponent = unit.GetComponent<ItemComponent>();

                itemComponent.Items[itemConfigId.ToString()] = response.ItemCount;

                EventSystem.Instance.Publish(scene, new UpdateItemCount()
                {
                    Key = itemConfigId.ToString(), Count = response.ItemCount
                });
            }

            return response.Error;
        }
#endif
    }
}