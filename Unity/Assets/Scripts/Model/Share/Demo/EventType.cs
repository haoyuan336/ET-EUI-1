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

    public struct HideEnemyObject
    {
        public Enemy Enemy;
    }
    public struct ShowEnemyObject
    {
        public Enemy Enemy;
    }

    public struct SpawnOneEnemy
    {
        public Unit Unit;

        public EnemySpawnPos EnemySpawnPos;
    }

    public struct ShowEnemySpawnPos
    {
        public Unit Unit;

        public MapConfig MapConfig;
    }

    public struct HideEnemySpawnPos
    {
        public Unit Unit;

        public MapConfig MapConfig;
    }

    public struct InitMapManager
    {
        public Unit Unit;
    }

    public struct UpdateTroop
    {
        public Unit Unit;

        public Troop Troop;
    }

    public struct StartFight
    {
        public Unit Unit;
    }

    public struct DiffuseHero
    {
        public Unit Unit;
    }

    public struct CreateHeroObject
    {
        public Unit Unit;

        public HeroCard HeroCard;

        public int Index;
    }

    public struct DisposeHeroObject
    {
        public HeroCard HeroCard;
    }

    public struct CreateHeroObjects
    {
        public Unit Unit;
    }

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