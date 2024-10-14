/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Collections.Generic;
using ET.Server;
using FairyGUI;

namespace ET.Client
{
    [FriendOf(typeof(FGUILoginServerLayerComponent))]
    [FriendOf(typeof(UIBaseWindow))]
    public static class FGUILoginServerLayerComponentSystem
    {
        public static void RegisterUIEvent(this FGUILoginServerLayerComponent self)
        {
            self.View.LoginButton.AddListenerAsync(self, self.OnLoginButtonClick);
        }

        private static async ETTask OnLoginButtonClick(this FGUILoginServerLayerComponent self)
        {
            await LoginHelper.LoginGate(self.Root(), self.CurrentServerInfo.ZoneConfigId);
            
            await EnterMapHelper.EnterMapAsync(self.Root(), self.CurrentServerInfo.ZoneConfigId);

            self.Root().GetComponent<UIComponent>().CloseWindow(WindowID.LoginServerLayer);
        }

        public static void ShowWindow(this FGUILoginServerLayerComponent self, Entity contextData = null)
        {
            //首先拿到server 信息
            // ServerStateType serverStateType
            PlayerComponent playerComponent = self.Root().GetComponent<PlayerComponent>();

            RoleInfo roleInfo = self.GetLastLoginRole(playerComponent.RoleInfos);

            Log.Debug($"role info {roleInfo}");

            ServerInfo serverInfo = null;

            if (roleInfo == null)
            {
                serverInfo = self.GetLastStartServerInfo(playerComponent.ServerInfos);
            }
            else
            {
                serverInfo = playerComponent.ServerInfos.Find(a => a.ZoneConfigId == roleInfo.ZoneConfigId);
            }

            self.CurrentServerInfo = serverInfo;

            Log.Debug($"server info {serverInfo}");
            self.View.CurrentServerComponent.SetInfo(serverInfo);

            //首先找到最后一次登录的角色
        }

        private static ServerInfo GetLastStartServerInfo(this FGUILoginServerLayerComponent self, List<ServerInfo> serverInfos)
        {
            ServerInfo serverInfo = null;

            long lastTime = 0;

            foreach (var server in serverInfos)
            {
                if (server.StartTime > lastTime)
                {
                    lastTime = server.StartTime;

                    serverInfo = server;
                }
            }

            return serverInfo;
        }

        private static RoleInfo GetLastLoginRole(this FGUILoginServerLayerComponent self, List<RoleInfo> roleInfos)
        {
            if (roleInfos.Count == 0)
            {
                return null;
            }

            RoleInfo roleInfo = null;

            long lastLoginTime = 0;

            foreach (var role in roleInfos)
            {
                if (role.LastLoginStamp > lastLoginTime)
                {
                    lastLoginTime = role.LastLoginStamp;

                    roleInfo = role;
                }
            }

            return roleInfo;
        }

        public static void HideWindow(this FGUILoginServerLayerComponent self)
        {
        }
    }
}