namespace ET.Server
{
    [MessageLocationHandler(SceneType.Map)]
    [FriendOfAttribute(typeof (ET.Troop))]
    public class C2M_UnSetHeroFromationHandler: MessageLocationHandler<Unit, C2M_UnSetHeroFromation, M2C_UnSetHeroFormation>
    {
        protected override async ETTask Run(Unit unit, C2M_UnSetHeroFromation request, M2C_UnSetHeroFormation response)
        {
            Log.Debug("C2M_UnSetHeroFromation");

            TroopComponent troopComponent = unit.GetComponent<TroopComponent>();

            long troopId = request.TroopId;

            int index = request.Index;

            Troop troop = troopComponent.GetChild<Troop>(troopId);

            troop.HeroCardIds[index] = 0;

            await ETTask.CompletedTask;
        }
    }
}