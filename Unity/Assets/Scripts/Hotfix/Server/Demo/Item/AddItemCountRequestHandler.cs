namespace ET.Server
{
    [FriendOfAttribute(typeof (ET.ItemComponent))]
    [MessageLocationHandler(SceneType.Map)]
    public class AddItemCountRequestHandler: MessageLocationHandler<Unit, C2M_AddItemCountRequest, M2C_AddItemCountResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_AddItemCountRequest request, M2C_AddItemCountResponse response)
        {
            bool checkSign = SignHelper.CheckSign(request);

            Log.Debug($"check sign {checkSign}");

            if (!checkSign)
            {
                response.Error = ErrorCode.Sign_Error;

                return;
            }

            ItemComponent itemComponent = unit.GetComponent<ItemComponent>();

            if (itemComponent == null)
            {
                itemComponent = unit.AddComponent<ItemComponent>();
            }

            int itemConfigId = request.ItemConfigId;

            int itemCount = request.ItemCount;

            int count = itemComponent.Items[itemConfigId.ToString()];

            count += itemCount;

            itemComponent.Items[itemConfigId.ToString()] = count;

            response.ItemCount = count;

            await ETTask.CompletedTask;
        }
    }
}