using System.Collections.Generic;

#if UNITY
using UnityEngine;

#endif

namespace ET.Client
{
#if UNITY

    // public struct MoveHeroCard
    // {
    //     public HeroCard HeroCard;
    //
    //     public Vector2 MoveSpeed;
    //
    //     public float Power;
    // }

    // public struct EnterMapCollider
    // {
    //     public Unit Unit;
    //
    //     public string ColliderName;
    // }
    //
    // public struct ExitMapCollider
    // {
    //     public Unit Unit;
    //
    //     public string ColliderName;
    // }

    public struct MoveUnitToMainCity
    {
    }

    public struct CheckGameLoseLogic
    {
    }

    public struct PlayDamageText
    {
        public Vector3 StartPos;

        public string Text;
    }

    public struct MakeEnemySleep
    {
        public Enemy Enemy;
    }

    public struct MakeEnemyRunning
    {
        public Enemy Enemy;
    }

    public struct SpawnOneEnemy
    {
        public Unit Unit;

        public EnemySpawnPos EnemySpawnPos;
    }

    public struct BindEnemySpawnPos
    {
        public Unit Unit;

        public MapConfig MapConfig;
    }

    public struct UnBindEnemySpawnPos
    {
        public Unit Unit;

        public MapConfig MapConfig;
    }

    public struct InitMapManager
    {
        public Unit Unit;
    }

    public struct StartFight
    {
        public Unit Unit;
    }

    public struct DiffuseHero
    {
        public Unit Unit;
    }

    public struct PlayDeadAnim
    {
        public Entity Entity;
    }

    public struct CreateFightHero
    {
        public Unit Unit;

        public long HeroCardId;

        public int Index;
    }

    public struct DisposeHeroObject
    {
        public Unit Unit;

        public long CardId;
    }

    // public struct CreateHeroObjects
    // {
    //     public Unit Unit;
    //
    //     public List<long> CardIds;
    // }

    public struct ShowLayerById
    {
        public WindowID WindowID;
    }

    public struct CloseLayerById
    {
        public WindowID WindowID;
    }

    public struct HideLayerById
    {
        public WindowID WindowID;
    }

    public struct StartMoveUnitPos
    {
    }

    public struct MoveUnitPos
    {
        public Vector2 Vector2;

        public float Power;
    }

    public struct EndMoveUnitPos
    {
    }
#endif
    public struct ShowLoadingLayer
    {
        public bool IsShow;
    }

    public struct SceneChangeStart
    {
    }

    public struct SceneChangeFinish
    {
    }

    public struct AfterCreateClientScene
    {
    }

    public struct AfterCreateCurrentScene
    {
    }

    public struct AppStartInitFinish
    {
    }

    public struct LoginFinish
    {
    }

    public struct EnterMapFinish
    {
    }

    public struct AfterUnitCreate
    {
        public Unit Unit;
    }

    public struct ShowLoginLayer
    {
    }
}