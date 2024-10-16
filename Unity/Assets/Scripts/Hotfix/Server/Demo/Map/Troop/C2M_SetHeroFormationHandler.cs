namespace ET.Server
{
    [MessageLocationHandler(SceneType.Map)]
    [FriendOfAttribute(typeof (ET.Troop))]
    public class C2M_SetHeroFormationHandler: MessageLocationHandler<Unit, C2M_SetHeroFormation, M2C_SetHeroFormation>
    {
        protected override async ETTask Run(Unit unit, C2M_SetHeroFormation request, M2C_SetHeroFormation response)
        {
            
            Log.Debug($"C2M_SetHeroFormation ");
            
            TroopComponent troopComponent = unit.GetComponent<TroopComponent>();

            long troopId = request.TroopId;

            int index = request.Index;

            long heroCardId = request.HeroCardId;

            Troop troop = troopComponent.GetChild<Troop>(troopId);

            troop.HeroCardIds[index] = heroCardId;

            await ETTask.CompletedTask;
        }
    }
}