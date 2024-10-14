using System;
using System.Collections.Generic;

namespace ET.Server
{
    [MessageHandler(SceneType.UnitCache)]
    [FriendOfAttribute(typeof (ET.UnitCacheComponent))]
    public class Other2UnitCache_GetUnitHandler: MessageHandler<Scene, Other2UnitCache_GetUnit, UnitCache2Other_GetUnit>
    {
        protected override async ETTask Run(Scene scene, Other2UnitCache_GetUnit request, UnitCache2Other_GetUnit response)
        {
            Log.Debug($"Other2UnitCache_GetUnit id {request.UnitId} ");

            UnitCacheComponent unitCacheComponent = scene.GetComponent<UnitCacheComponent>();

            Dictionary<string, Entity> dictionary = new Dictionary<string, Entity>();

            foreach (var key in unitCacheComponent.UnitCacheKeyList)
            {
                Log.Debug($"foreach key {key}");
            }

            if (request.ComponentNameList.Count == 0)
            {
                string fullName = typeof (Unit).FullName;

                dictionary.Add(fullName, null);

                foreach (string s in unitCacheComponent.UnitCacheKeyList)
                {
                    dictionary.Add(s, null);
                }
            }
            else
            {
                foreach (string s in request.ComponentNameList)
                {
                    dictionary.Add(s, null);
                }
            }

            Log.Debug($"keys {dictionary.Keys.Count}");

            foreach (var key in dictionary.Keys)
            {
                Log.Debug($"get key {key} {request.UnitId} ");

                Entity entity = await unitCacheComponent.Get(request.UnitId, key);

                if (entity != null)
                {
                    Log.Debug($"entity child count {entity.Children.Count}");
                }

                dictionary[key] = entity;
            }

            foreach (var key in dictionary.Keys)
            {
                Log.Debug($"key {key}");

                Log.Debug($"value {dictionary[key] == null}");
            }

            response.ComponentNameList.AddRange(dictionary.Keys);
            response.EntityList.AddRange(dictionary.Values);

            await ETTask.CompletedTask;
        }
    }
}