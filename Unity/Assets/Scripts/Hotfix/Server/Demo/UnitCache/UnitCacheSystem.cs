using System;
using System.Collections.Generic;
using ET.Server;

namespace ET
{
    [EntitySystem]
    [FriendOfAttribute(typeof (ET.UnitCache))]
    public class UnitCacheDestroySystem: DestroySystem<UnitCache>
    {
        protected override void Destroy(UnitCache self)
        {
            foreach (Entity entity in self.CacheCompoenntsDictionary.Values)
            {
                entity.Dispose();
            }

            self.CacheCompoenntsDictionary.Clear();
            self.key = null;
        }
    }

    [FriendOf(typeof (UnitCache))]
    public static class UnitCacheSystem
    {
        public static void AddOrUpdate(this UnitCache self, Entity entity)
        {
            if (entity == null)
            {
                return;
            }

            Log.Debug($"AddOrUpdate entity not null");

            if (self.CacheCompoenntsDictionary.TryGetValue(entity.Id, out EntityRef<Entity> oldEntityRef))
            {
                Log.Debug(" 存在old entity ");
                Entity oldEntity = oldEntityRef;

                if (entity != oldEntity)
                {
                    Log.Debug("对象相等");
                    oldEntity.Dispose();
                }

                Log.Debug("移除旧的entity");

                self.CacheCompoenntsDictionary.Remove(entity.Id);
            }

            Log.Debug("添加Cinderella");
            self.CacheCompoenntsDictionary.Add(entity.Id, entity);
        }

        public static async ETTask<Entity> Get(this UnitCache self, long unitId)
        {
            Log.Debug($"UnitCache  ");
            Log.Debug($"CacheCompoenntsDictionaryGet ");
            try
            {
                if (!self.CacheCompoenntsDictionary.TryGetValue(unitId, out EntityRef<Entity> entityRef))
                {
                    Log.Debug($"不存在 component {self.key} ");
                    DBManagerComponent dbManagerComponent = self.Root().GetComponent<DBManagerComponent>();

                    int zone = self.Zone();

                    Log.Debug($"unit id  {unitId} {self.key}");

                    Entity entity = await dbManagerComponent.GetZoneDB(zone).Query<Entity>(unitId, self.key);

                    Log.Debug($" await entity is null {entity == null}");

                    if (entity != null)
                    {
                        Log.Debug($"entity {entity.Children.Count}");
                    }

                    // Entity entity = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Query<Entity>(unitId, self.key);
                    if (entity != null)
                    {
                        self.AddOrUpdate(entity);
                    }
                    else
                    {
                        Log.Debug("entity is null");
                    }

                    return entity;
                }
                else
                {
                    Log.Debug("存在component");
                }

                return entityRef;
            }
            catch (Exception e)
            {
                Log.Debug($"e {e}");
                return null;
            }
        }

        public static void Delete(this UnitCache self, long id)
        {
            if (self.CacheCompoenntsDictionary.TryGetValue(id, out EntityRef<Entity> entityRef))
            {
                Entity entity = entityRef;

                entity.Dispose();

                self.CacheCompoenntsDictionary.Remove(id);
            }
        }
    }
}