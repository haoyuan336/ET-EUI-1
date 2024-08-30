using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using YooAsset;

namespace ET
{
    // 用于字符串转换，减少GC
    [FriendOf(typeof(ResourcesComponent))]
    public static class AssetBundleHelper
    {
        public static string StringToAB(this string value)
        {
            string result = $"Assets/Bundles/UI/Dlg/{value}.prefab";
            return result;
        }
    }

    /// <summary>
    /// 远端资源地址查询服务类
    /// </summary>
    public class RemoteServices : IRemoteServices
    {
        private readonly string _defaultHostServer;
        private readonly string _fallbackHostServer;

        public RemoteServices(string defaultHostServer, string fallbackHostServer)
        {
            _defaultHostServer = defaultHostServer;
            _fallbackHostServer = fallbackHostServer;
        }

        string IRemoteServices.GetRemoteMainURL(string fileName)
        {
            return $"{_defaultHostServer}/{fileName}";
        }

        string IRemoteServices.GetRemoteFallbackURL(string fileName)
        {
            return $"{_fallbackHostServer}/{fileName}";
        }
    }

    public class ResourcesComponent : Singleton<ResourcesComponent>, ISingletonAwake
    {
        public void Awake()
        {
            YooAssets.Initialize();
        }

        protected override void Destroy()
        {
            // YooAssets.Destroy();
        }

        public async ETTask CreatePackageAsync(string packageName, bool isDefault = false)
        {
            GlobalConfig globalConfig = Resources.Load<GlobalConfig>("GlobalConfig");

            EPlayMode ePlayMode = globalConfig.EPlayMode;

            Debug.Log($"e play mode {ePlayMode}");

            ResourcePackage package = YooAssets.CreatePackage(packageName);

            Debug.Log($"is default {isDefault}");

            if (isDefault)
            {
                YooAssets.SetDefaultPackage(package);
            }

            // 编辑器下的模拟模式
            switch (ePlayMode)
            {
                case EPlayMode.EditorSimulateMode:
                {
                    var buildPipeline = EDefaultBuildPipeline.BuiltinBuildPipeline;

                    var simulateBuildResult = EditorSimulateModeHelper.SimulateBuild(buildPipeline, "DefaultPackage");

                    var editorFileSystem = FileSystemParameters.CreateDefaultEditorFileSystemParameters(simulateBuildResult);

                    var initParameters = new EditorSimulateModeParameters();

                    initParameters.EditorFileSystemParameters = editorFileSystem;

                    InitializationOperation initializationOperation = package.InitializeAsync(initParameters);

                    await initializationOperation.Task;

                    if (initializationOperation.Status == EOperationStatus.Succeed)
                        Debug.Log("资源包初始化成功！");
                    else
                        Debug.LogError($"资源包初始化失败：{initializationOperation.Error}");

                    break;
                }
                case EPlayMode.OfflinePlayMode:
                {
                    OfflinePlayModeParameters createParameters = new();
                    await package.InitializeAsync(createParameters).Task;
                    break;
                }
                case EPlayMode.HostPlayMode:
                {
                    string defaultHostServer = GetHostServerURL();
                    string fallbackHostServer = GetHostServerURL();
                    HostPlayModeParameters createParameters = new();

                    // createParameters.

                    // createParameters.BuildinQueryServices = new GameQueryServices();
                    // createParameters.RemoteServices = new RemoteServices(defaultHostServer, fallbackHostServer);
                    await package.InitializeAsync(createParameters).Task;
                    break;
                }
                case EPlayMode.WebPlayMode:
                {
                    Debug.Log("web player mode");
                    var webFileSystem = FileSystemParameters.CreateDefaultWebFileSystemParameters();

                    var initParameters = new WebPlayModeParameters();

                    initParameters.WebFileSystemParameters = webFileSystem;

                    var initOperation = package.InitializeAsync(initParameters);

                    await initOperation.Task;

                    if (initOperation.Status == EOperationStatus.Succeed)
                        Debug.Log("资源包初始化成功！");
                    else
                        Debug.LogError($"资源包初始化失败：{initOperation.Error}");
                }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            RequestPackageVersionOperation requestPackageVersionOperation = package.RequestPackageVersionAsync();

            await requestPackageVersionOperation.Task;

            Debug.Log($"task state {requestPackageVersionOperation.PackageVersion}");

            UpdatePackageManifestOperation updatePackageManifestOperation =
                    package.UpdatePackageManifestAsync(requestPackageVersionOperation.PackageVersion);

            await updatePackageManifestOperation.Task;

            string version = package.GetPackageVersion();

            Debug.Log($"version {version}");

            AssetHandle handle = YooAssets.LoadAssetAsync<GameObject>("Cube");
            //
            await handle.Task;
            //
            Debug.Log($"handler {handle.Status}");

            GameObject prefab = handle.AssetObject as GameObject;

            GameObject gameObject = GameObject.Instantiate(prefab);
        }

        static string GetHostServerURL()
        {
            //string hostServerIP = "http://10.0.2.2"; //安卓模拟器地址
            string hostServerIP = "http://127.0.0.1";
            string appVersion = "v1.0";

#if UNITY_EDITOR
            if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.Android)
            {
                return $"{hostServerIP}/CDN/Android/{appVersion}";
            }
            else if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.iOS)
            {
                return $"{hostServerIP}/CDN/IPhone/{appVersion}";
            }
            else if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.WebGL)
            {
                return $"{hostServerIP}/CDN/WebGL/{appVersion}";
            }

            return $"{hostServerIP}/CDN/PC/{appVersion}";
#else
            if (Application.platform == RuntimePlatform.Android)
            {
                return $"{hostServerIP}/CDN/Android/{appVersion}";
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                return $"{hostServerIP}/CDN/IPhone/{appVersion}";
            }
            else if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                return $"{hostServerIP}/CDN/WebGL/{appVersion}";
            }

            return $"{hostServerIP}/CDN/PC/{appVersion}";
#endif
        }

        public void DestroyPackage(string packageName)
        {
            ResourcePackage package = YooAssets.GetPackage(packageName);
            // package.UnloadUnusedAssets();
            // package.UnloadAllAssetsAsync();
        }

        public T LoadAssetSync<T>(string location) where T : UnityEngine.Object
        {
            AssetHandle handle = YooAssets.LoadAssetSync<T>(location);
            T t = (T)handle.AssetObject;
            handle.Release();
            return t;
        }

        /// <summary>
        /// 主要用来加载dll config aotdll，因为这时候纤程还没创建，无法使用ResourcesLoaderComponent。
        /// 游戏中的资源应该使用ResourcesLoaderComponent来加载
        /// </summary>
        public async ETTask<T> LoadAssetAsync<T>(string location) where T : UnityEngine.Object
        {
            Log.Debug($"location {location}");
            AssetHandle handle = YooAssets.LoadAssetAsync<T>(location);
            await handle.Task;
            T t = (T)handle.AssetObject;
            handle.Release();
            return t;
        }

        /// <summary>
        /// 主要用来加载dll config aotdll，因为这时候纤程还没创建，无法使用ResourcesLoaderComponent。
        /// 游戏中的资源应该使用ResourcesLoaderComponent来加载
        /// </summary>
        public async ETTask<Dictionary<string, T>> LoadAllAssetsAsync<T>(string location) where T : UnityEngine.Object
        {
            AllAssetsHandle allAssetsOperationHandle = YooAssets.LoadAllAssetsAsync<T>(location);
            await allAssetsOperationHandle.Task;
            Dictionary<string, T> dictionary = new Dictionary<string, T>();
            foreach (UnityEngine.Object assetObj in allAssetsOperationHandle.AllAssetObjects)
            {
                T t = assetObj as T;
                dictionary.Add(t.name, t);
            }

            allAssetsOperationHandle.Release();
            return dictionary;
        }
    }
}