using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
#if UNITY
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

    public struct MoveUnitPos
    {
        public Vector2 Vector2;
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