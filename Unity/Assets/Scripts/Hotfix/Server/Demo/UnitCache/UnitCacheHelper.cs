using System;
using System.Collections.Generic;
using System.Linq;

namespace ET.Server
{
    public static class UnitCacheHelper
    {
        /// <summary>
        /// 保存或者更新玩家缓存
        /// </summary>
        /// <param name="self"></param>
        /// <param name="zone"></param>
        /// <typeparam name="T"></typeparam>
        public static async ETTask AddOrUpdateUnitCache<T>(this T self, int zone) where T : Entity, IUnitCache
        {
            Other2UnitCache_AddOrUpdateUnit message = Other2UnitCache_AddOrUpdateUnit.Create();
            // { UnitId = self.Id, };
            message.UnitId = self.Id;

            message.EntityTypes.Add(typeof (T).FullName);

            message.EntityBytes.Add(MongoHelper.Serialize(self));

            StartSceneConfig startSceneConfig =
                    StartSceneConfigCategory.Instance.GetBySceneName(zone, SceneType.UnitCache.ToString() + $"{zone}");

            UnitCache2Other_AddOrUpdateUnit addOrUpdateUnit =
                    await self.Root().GetComponent<MessageSender>().Call(startSceneConfig.ActorId, message) as UnitCache2Other_AddOrUpdateUnit;
        }

        /// <summary>
        /// 获取玩家缓存
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="zone"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static async ETTask<Unit> GetUnitCache(Scene scene, int zone, long unitId)
        {
            StartSceneConfig startSceneConfig =
                    StartSceneConfigCategory.Instance.GetBySceneName(zone, SceneType.UnitCache.ToString() + $"{zone}");

            ET.ActorId actorId = startSceneConfig.ActorId;

            Log.Debug($"get unit cache config id {startSceneConfig.Id}");

            Other2UnitCache_GetUnit message = Other2UnitCache_GetUnit.Create();

            message.UnitId = unitId;

            UnitCache2Other_GetUnit queryUnit = await scene.Root().GetComponent<MessageSender>().Call(actorId, message) as UnitCache2Other_GetUnit;

            if (queryUnit.Error != ErrorCode.ERR_Success || queryUnit.EntityList.Count <= 0)
            {
                return null;
            }

            string unitFullName = typeof (Unit).FullName;

            int indexOf = queryUnit.ComponentNameList.IndexOf(unitFullName);

            Unit unit = queryUnit.EntityList[indexOf] as Unit;

            if (unit == null)
            {
                return null;
            }

            scene.GetComponent<UnitComponent>().AddChild(unit);

            foreach (Entity entity in queryUnit.EntityList)
            {
                if (entity == null || entity is Unit)
                {
                    continue;
                }

                unit.AddComponent(entity);
            }

            return unit;
        }

        /// <summary>
        /// 获取玩家组件缓存
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="zone"></param>
        /// <param name="unitId"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async ETTask<T> GetUnitComponentCache<T>(Scene scene, int zone, long unitId) where T : Entity, IUnitCache
        {
            Other2UnitCache_GetUnit message = Other2UnitCache_GetUnit.Create();

            message.UnitId = unitId;

            message.ComponentNameList.Add(typeof (T).Name);

            StartSceneConfig startSceneConfig =
                    StartSceneConfigCategory.Instance.GetBySceneName(zone, SceneType.UnitCache.ToString() + $"{zone}");

            ET.ActorId actorId = startSceneConfig.ActorId;

            UnitCache2Other_GetUnit queryUnit = await scene.Root().GetComponent<MessageSender>().Call(actorId, message) as UnitCache2Other_GetUnit;

            if (queryUnit.Error == ErrorCode.ERR_Success && queryUnit.EntityList.Count > 0)
            {
                return queryUnit.EntityList[0] as T;
            }

            return null;
        }

        /// <summary>
        /// 删除玩家缓存
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="zone"></param>
        /// <param name="unitId"></param>
        public static async ETTask DeleteUnitCache(Scene scene, int zone, long unitId)
        {
            Other2UnitCache_DeleteUnit message = Other2UnitCache_DeleteUnit.Create();

            message.UnitId = unitId;

            ActorId actorId = StartSceneConfigCategory.Instance.GetBySceneName(zone, SceneType.UnitCache + $"{zone}")
                    .ActorId;

            await scene.Root().GetComponent<MessageSender>().Call(actorId, message);
        }

        /// <summary>
        /// 保存Unit及Unit身上组件到缓存服及数据库中
        /// </summary>
        /// <param name="unit"></param>
        public static void AddOrUpdateUnitAllCache(Unit unit)
        {
            Other2UnitCache_AddOrUpdateUnit message = Other2UnitCache_AddOrUpdateUnit.Create();

            message.UnitId = unit.Id;

            message.EntityTypes.Add(unit.GetType().FullName);

            message.EntityBytes.Add(MongoHelper.Serialize(unit));

            foreach ((long key, Entity entity) in unit.Components)
            {
                Type type = entity.GetType();

                if (!typeof (IUnitCache).IsAssignableFrom(type))
                {
                    continue;
                }

                message.EntityTypes.Add(type.FullName);

                message.EntityBytes.Add(MongoHelper.Serialize(entity));
            }

            // MessageHelper.CallActor(StartSceneConfigCategory.Instance.GetUnitCacheConfig(unit.Id).InstanceId, message).Coroutine();

            ActorId actorId = StartSceneConfigCategory.Instance.GetBySceneName(unit.Zone(), SceneType.UnitCache.ToString() + $"{unit.Zone()}")
                    .ActorId;

            unit.Root().GetComponent<MessageSender>().Call(actorId, message).Coroutine();
        }
    }
}