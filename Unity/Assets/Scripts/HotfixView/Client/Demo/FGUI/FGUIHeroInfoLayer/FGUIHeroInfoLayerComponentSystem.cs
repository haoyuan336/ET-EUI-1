/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using Spine.Unity;
using UnityEditor.Media;
using UnityEngine;
using UnityEngine.AI;
using WeChatWASM;

namespace ET.Client
{
    [FriendOf(typeof(FGUIHeroInfoLayerComponent))]
    [FriendOf(typeof(UIBaseWindow))]
    public static class FGUIHeroInfoLayerComponentSystem
    {
        public static void RegisterUIEvent(this FGUIHeroInfoLayerComponent self)
        {
            self.View.UpLevelButton.AddListenerAsync(self, self.OnUpLevelButtonClick);

            self.View.CloseButton.SetListener(self.OnCloseButtonClick);
        }

        public static void OnCloseButtonClick(this FGUIHeroInfoLayerComponent self)
        {
            Log.Debug("on close button click");

            EventSystem.Instance.Publish(self.Root(), new PopLayer());
        }

        public static void ShowWindow(this FGUIHeroInfoLayerComponent self, Entity contextData = null)
        {
            HeroCard heroCard = contextData as HeroCard;

            self.SetHeroInfo(heroCard);
        }

        public static void HideWindow(this FGUIHeroInfoLayerComponent self)
        {
        }

        public static void SetHeroInfo(this FGUIHeroInfoLayerComponent self, HeroCard heroCard)
        {
            self.ShowHeroSpine(heroCard);

            self.View.AttackBarComponent.SetInfo(WordBarType.Attack, heroCard);

            self.View.HPBarComponent.SetInfo(WordBarType.Hp, heroCard);

            self.View.Level.SetVar("Level", heroCard.Level.ToString()).FlushVars();
        }

        private static async ETTask OnUpLevelButtonClick(this FGUIHeroInfoLayerComponent self)
        {
            await HeroCardHelper.UpLevel(self.HeroCard);

            await ETTask.CompletedTask;
        }

        private static void ShowHeroSpine(this FGUIHeroInfoLayerComponent self, HeroCard heroCard)
        {
            self.HeroCard = heroCard;

            if (self.HeroSpine != null)
            {
                return;
            }

            GlobalComponent globalComponent = self.Root().GetComponent<GlobalComponent>();

            string prefabName = heroCard.Config.PrefabName;

            GameObject prefab = globalComponent.ReferenceCollector.Get<GameObject>(prefabName);

            GameObject gameObject = GameObject.Instantiate(prefab);

            self.HeroSpine = gameObject;

            NavMeshAgent navMeshAgent = gameObject.GetComponent<NavMeshAgent>();

            GameObject.Destroy(navMeshAgent);

            self.View.HeroLoader.SetWrapTarget(gameObject, false, 100, 100);

            gameObject.transform.localScale = Vector3.one * 100;

            gameObject.transform.position += Vector3.forward;

            SkeletonAnimation skeletonAnimation = gameObject.transform.GetChild(0).GetComponent<SkeletonAnimation>();

            skeletonAnimation.state.SetAnimation(0, "idle", true);
        }
    }
}