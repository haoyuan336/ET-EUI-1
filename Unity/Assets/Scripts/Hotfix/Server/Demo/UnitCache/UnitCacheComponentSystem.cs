using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json.Nodes;

namespace ET.Server
{
    [FriendOf(typeof (UnitCache))]
    [FriendOfAttribute(typeof (ET.UnitCacheComponent))]
    public class UnitCacheComponentAwakeSystem: AwakeSystem<UnitCacheComponent>
    {
        protected override void Awake(UnitCacheComponent self)
        {
            self.UnitCacheKeyList.Clear();

            foreach (Type type in CodeTypes.Instance.GetTypes().Values)
            {
                if (type != typeof (IUnitCache) && typeof (IUnitCache).IsAssignableFrom(type))
                {
                    self.UnitCacheKeyList.Add(type.FullName);
                }
            }

            foreach (string key in self.UnitCacheKeyList)
            {
                UnitCache unitCache = self.AddChild<UnitCache>();
                unitCache.key = key;
                self.UnitCaches.Add(key, unitCache);
            }
        }
    }

    [EntitySystem]
    [FriendOfAttribute(typeof (ET.UnitCacheComponent))]
    public class UnitCacheComponentDestroySystem: DestroySystem<UnitCacheComponent>
    {
        protected override void Destroy(UnitCacheComponent self)
        {
            foreach (var unitCache in self.UnitCaches.Values)
            {
                UnitCache cache = (UnitCache)unitCache;

                cache?.Dispose();
            }

            self.UnitCaches.Clear();
        }
    }

    [FriendOf(typeof (UnitCache))]
    [FriendOf(typeof (UnitCacheComponent))]
    public static class UnitCacheComponentSystem
    {
        public static async ETTask<Entity> Get(this UnitCacheComponent self, long unitId, string key)
        {
            try
            {
                Log.Debug("unit cache component");
                if (!self.UnitCaches.TryGetValue(key, out EntityRef<UnitCache> unitCache))
                {
                    UnitCache cache = self.AddChild<UnitCache>();

                    cache.key = key;

                    self.UnitCaches.Add(key, cache);

                    return await cache.Get(unitId);
                }
                else
                {
                    Log.Debug($"存在 key {key}  ");

                    UnitCache cache = null;

                    cache = unitCache;

                    Log.Debug($"cache {cache == null}");

                    if (cache == null)
                    {
                        Log.Debug("cache is null");
                    }

                    Entity entity = await cache.Get(unitId);

                    return entity;
                }
            }
            catch (Exception e)
            {
                Log.Debug($"error {e}");

                return null;
            }
        }

        // public static async ETTask<T> Get<T>(this UnitCacheComponent self, long unitId) where T : Entity
        // {
        //     string key = typeof (T).Name;
        //
        //     UnitCache unitCache;
        //
        //     if (!self.UnitCaches.TryGetValue(key, out EntityRef<UnitCache> unitCacheRef))
        //     {
        //         unitCache = self.AddChild<UnitCache>();
        //
        //         unitCache.key = key;
        //
        //         self.UnitCaches.Add(key, unitCache);
        //     }
        //
        //     unitCache = unitCacheRef;
        //
        //     return await unitCache.Get(unitId) as T;
        // }

        public static async ETTask AddOrUpdate(this UnitCacheComponent self, long id, ListComponent<Entity> entityList)
        {
            Log.Debug($"UnitCacheComponent AddOrUpdate {entityList.Count}");

            using (ListComponent<Entity> list = ListComponent<Entity>.Create())
            {
                foreach (Entity entity in entityList)
                {
                    string key = entity.GetType().FullName ?? entity.GetType().Name;

                    Log.Debug($"key {key}");

                    UnitCache cache = null;

                    if (!self.UnitCaches.TryGetValue(key, out EntityRef<UnitCache> unitCacheRef))
                    {
                        cache = self.AddChild<UnitCache>();

                        cache.key = key;

                        self.UnitCaches.Add(key, cache);
                    }
                    else
                    {
                        cache = unitCacheRef;
                    }

                    cache.AddOrUpdate(entity);

                    list.Add(entity);
                }

                Log.Debug($"list count {list.Count}");

                if (list.Count > 0)
                {
                    // await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Save(id, list);
                    DBManagerComponent dbManagerComponent = self.Root().GetComponent<DBManagerComponent>();

                    await dbManagerComponent.GetZoneDB(self.Zone()).Save(id, list);
                }
            }
        }

        public static void Delete(this UnitCacheComponent self, long unitId)
        {
            foreach (UnitCache cache in self.UnitCaches.Values)
            {
                cache.Delete(unitId);
            }
        }
    }
}