using System;
using System.Collections.Generic;

namespace ET.Server
{
    [MessageSessionHandler(SceneType.Gate)]
    [FriendOfAttribute(typeof (ET.Server.Player))]
    public class C2G_LoginGateHandler: MessageSessionHandler<C2G_LoginGate, G2C_LoginGate>
    {
        protected override async ETTask Run(Session session, C2G_LoginGate request, G2C_LoginGate response)
        {
            Log.Debug($"C2G_LoginGate session {session.Id}");

            Scene root = session.Root();

            string account = root.GetComponent<GateSessionKeyComponent>().Get(request.Key);

            if (account == null)
            {
                response.Error = ErrorCore.ERR_ConnectGateKeyError;

                response.Message = "Gate key验证失败!";

                return;
            }

            session.RemoveComponent<SessionAcceptTimeoutComponent>();

            int zoneConfigId = request.ZoneConfigId;

            long roleId = await this.GetCurrentRoleId(session.Root(), account, zoneConfigId);

            PlayerComponent playerComponent = root.GetComponent<PlayerComponent>();

            Player player = playerComponent.GetByAccount(account);

            PlayerSessionComponent playerSessionComponent = null;

            // if (player == null || player.GetComponent<PlayerRoomComponent>() == null)
            // {
            //     if (player != null && !player.IsDisposed)
            //     {
            //         playerComponent.Remove(player);
            //         
            //         player?.Dispose();
            //     }
            //
            //     player = playerComponent.AddChildWithId<Player, string>(roleId, account);
            //
            //     player.ZoneConfigId = zoneConfigId;
            //
            //     playerComponent.Add(player);
            //
            //     playerSessionComponent = player.AddComponent<PlayerSessionComponent>();
            //
            //     playerSessionComponent.AddComponent<MailBoxComponent, MailBoxType>(MailBoxType.GateSession);
            //
            //     await playerSessionComponent.AddLocation(LocationType.GateSession);
            //
            //     player.AddComponent<MailBoxComponent, MailBoxType>(MailBoxType.UnOrderedMessage);
            //
            //     await player.AddLocation(LocationType.Player);
            // }

            if (player == null)
            {
                player = playerComponent.AddChildWithId<Player, string>(roleId, account);

                playerComponent.Add(player);

                playerSessionComponent = player.AddComponent<PlayerSessionComponent>();

                playerSessionComponent.AddComponent<MailBoxComponent, MailBoxType>(MailBoxType.GateSession);

                await playerSessionComponent.AddLocation(LocationType.GateSession);

                player.AddComponent<MailBoxComponent, MailBoxType>(MailBoxType.UnOrderedMessage);
                
                await player.AddLocation(LocationType.Player);
            }
            else
            {
                playerSessionComponent = player.GetComponent<PlayerSessionComponent>();
            }

            player.ZoneConfigId = zoneConfigId;

            session.AddComponent<SessionPlayerComponent>().Player = player;

            playerSessionComponent.Session = session;

            player.ZoneConfigId = zoneConfigId;

            response.PlayerId = player.Id;

            Log.Debug($"player id {player.Id}");

            await ETTask.CompletedTask;
        }

        private static async ETTask CheckRoom(Player player, Session session)
        {
            Fiber fiber = player.Fiber();
            await fiber.WaitFrameFinish();

            G2Room_Reconnect g2RoomReconnect = G2Room_Reconnect.Create();
            g2RoomReconnect.PlayerId = player.Id;
            using Room2G_Reconnect room2GateReconnect = await fiber.Root.GetComponent<MessageSender>().Call(
                player.GetComponent<PlayerRoomComponent>().RoomActorId,
                g2RoomReconnect) as Room2G_Reconnect;
            G2C_Reconnect g2CReconnect = G2C_Reconnect.Create();
            g2CReconnect.StartTime = room2GateReconnect.StartTime;
            g2CReconnect.Frame = room2GateReconnect.Frame;
            g2CReconnect.UnitInfos.AddRange(room2GateReconnect.UnitInfos);
            session.Send(g2CReconnect);

            session.AddComponent<SessionPlayerComponent>().Player = player;
            player.GetComponent<PlayerSessionComponent>().Session = session;
        }

        private async ETTask<long> GetCurrentRoleId(Scene scene, string account, int zoneConfigId)
        {
            MessageSender messageSender = scene.Root().GetComponent<MessageSender>();

            StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetRealSceneConfig();

            G2R_GetServerCurrentRoleIdRequest request = G2R_GetServerCurrentRoleIdRequest.Create();

            request.Account = account;

            request.ZoneConfigId = zoneConfigId;

            R2G_GetServerCurrentRoleIdResponse response =
                    await messageSender.Call(startSceneConfig.ActorId, request) as R2G_GetServerCurrentRoleIdResponse;

            return response.RoleId;
        }
    }
}