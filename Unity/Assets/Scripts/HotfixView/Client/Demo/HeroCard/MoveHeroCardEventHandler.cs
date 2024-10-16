// using UnityEngine;
//
// namespace ET.Client
// {
//     [Event(SceneType.Current)]
//     public class MoveHeroCardEventHandler : AEvent<Scene, MoveHeroCard>
//     {
//         protected override async ETTask Run(Scene scene, MoveHeroCard a)
//         {
//             HeroCard heroCard = a.HeroCard;
//
//             Vector2 speed = a.MoveSpeed;
//
//             float power = a.Power;
//
//             Log.Debug($"speed {speed} {heroCard == null}");
//
//             if (heroCard == null)
//             {
//                 return;
//             }
//
//             Log.Debug("move hero card");
//
//             await ETTask.CompletedTask;
//         }
//     }
// }