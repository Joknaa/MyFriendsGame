using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Assertions;
using Object = UnityEngine.Object;
#if UNITY_2020_2_OR_NEWER
using AssetImportContext = UnityEditor.AssetImporters.AssetImportContext;
using ScriptedImporter = UnityEditor.AssetImporters.ScriptedImporter;

#else
using AssetImportContext = UnityEditor.Experimental.AssetImporters.AssetImportContext;
using ScriptedImporter = UnityEditor.Experimental.AssetImporters.ScriptedImporter;
#endif

namespace SuperTiled2Unity.Editor {
    public abstract class SuperImporter : ScriptedImporter {
        [SerializeField] private List<string> m_MissingFiles = new List<string>();

        [SerializeField] private List<string> m_Errors = new List<string>();

        [SerializeField] private List<string> m_Warnings = new List<string>();

        [SerializeField] private List<string> m_MissingSortingLayers = new List<string>();

        [SerializeField] private List<string> m_MissingLayers = new List<string>();

        [SerializeField] private List<string> m_MissingTags = new List<string>();

        // Keep track of our importer version so that we may handle converions from old imports
        [SerializeField] private int m_ImporterVersion;

        // Keep track of loaded database objects by type
        private readonly Dictionary<KeyValuePair<string, Type>, Object> m_CachedDatabase = new Dictionary<KeyValuePair<string, Type>, Object>();

        // For tracking assets and dependencies imported by SuperTiled2Unity
        private SuperAsset m_SuperAsset;
        public IEnumerable<string> MissingFiles => m_MissingFiles;
        public IEnumerable<string> Errors => m_Errors;
        public IEnumerable<string> Warnings => m_Warnings;
        public IEnumerable<string> MissingSortingLayers => m_MissingSortingLayers;
        public IEnumerable<string> MissingLayers => m_MissingLayers;
        public IEnumerable<string> MissingTags => m_MissingTags;

        public int ImporterVersion {
            get => m_ImporterVersion;
            protected set => m_ImporterVersion = value;
        }

        protected AssetImportContext AssetImportContext { get; private set; }

        public sealed override void OnImportAsset(AssetImportContext ctx) {
            m_CachedDatabase.Clear();
            m_MissingFiles.Clear();
            m_Errors.Clear();
            m_Warnings.Clear();
            m_MissingSortingLayers.Clear();
            m_MissingLayers.Clear();
            m_MissingTags.Clear();
            m_SuperAsset = null;
            AssetImportContext = ctx;

#if UNITY_2018_3_OR_NEWER
            try {
                InternalOnImportAsset();
                InternalOnImportAssetCompleted();
            }
            catch (TiledException tiled) {
                // Exceptions that SuperTiled2Unity is aware of
                // These are the kind of errors a user should be able to fix
                m_Errors.Add(tiled.Message);
            }
            catch (XmlException xml) {
                // Xml exceptions are common if the Tiled data file somehow becomes corrupted
                m_Errors.Add("Asset file may contained corrupted XML data. Trying opening in Tiled Map Editor to resolve.");
                m_Errors.Add(xml.Message);
            }
            catch (Exception ex) {
                // Last-chance collection of unknown errors while importing
                // This should be reported for bug fixing
                m_Errors.Add("Unknown error encountered. Please report as bug. Stack track is in the console output.");
                m_Errors.Add(ex.Message);
                Debug.LogErrorFormat("Unknown error of type {0}: {1}\nStack Trace:\n{2}", ex.GetType(), ex.Message, ex.StackTrace);
            }
#else
            ReportUnityVersionError();
#endif
        }

        public T RequestAssetAtPath<T>(string path) where T : Object {
            Assert.IsNotNull(m_SuperAsset, "Must be a SuperAsset type if we are requesting dependencies");

            // Is the asset in our cache?
            path = path.SanitizePath();
            var key = new KeyValuePair<string, Type>(path.ToLower(), typeof(T));
            Object cachedObject;

            if (m_CachedDatabase.TryGetValue(key, out cachedObject)) return cachedObject as T;

            // The path is either relative to our asset path or is absolute
            using (new ChDir(assetPath)) {
                path = Path.GetFullPath(path);
                if (AssetPath.TryAbsoluteToAsset(ref path)) {
                    // Keep track that the the asset is a dependency
                    m_SuperAsset.AddDependency(AssetImportContext, path);

                    // In most cases our dependency is already known by the AssetDatabase
                    var asset = AssetDatabase.LoadAssetAtPath<T>(path);
                    if (asset != null) {
                        // Add the asset to our cache for next time it is requested
                        m_CachedDatabase[key] = asset;
                        return asset;
                    }

                    ReportMissingFile(path);
                }
            }

            return null;
        }

        public void ReportMissingFile(string path) {
            m_MissingFiles.Add(path);
        }

        public void ReportError(string fmt, params object[] args) {
            var error = string.Format(fmt, args);
            m_Errors.Add(error);
        }

        public void ReportWarning(string fmt, params object[] args) {
            var warning = string.Format(fmt, args);
            m_Warnings.Add(warning);
        }

        public string GetReportHeader() {
            return string.Format("SuperTiled2Unity version: {0}, Unity version: {1}", SuperTiled2Unity_Config.Version, Application.unityVersion);
        }

        public bool CheckSortingLayerName(string sortName) {
            if (!sortName.Equals("Default", StringComparison.OrdinalIgnoreCase) && SortingLayer.NameToID(sortName) == 0) {
                if (!m_MissingSortingLayers.Contains(sortName))
                    //Debug.LogWarningFormat("Sorting layer name '{0}' not found in Tag Manager. Tiled map layers and objects may be drawn out of order.", sortName);
                    m_MissingSortingLayers.Add(sortName);

                return false;
            }

            return true;
        }

        public bool CheckLayerName(string layerName) {
            if (LayerMask.NameToLayer(layerName) == -1) {
                if (!m_MissingLayers.Contains(layerName))
                    //Debug.LogWarningFormat("Layer name '{0}' not found in Tag Manager. Colliders may not work as expected", layerName);
                    m_MissingLayers.Add(layerName);

                return false;
            }

            return true;
        }

        public bool CheckTagName(string tagName) {
            if (!InternalEditorUtility.tags.Contains(tagName)) {
                if (!m_MissingTags.Contains(tagName)) m_MissingTags.Add(tagName);

                return false;
            }

            return true;
        }

        protected void AddSuperAsset<T>() where T : SuperAsset {
            m_SuperAsset = ScriptableObject.CreateInstance<T>();
            m_SuperAsset.name = Path.GetFileNameWithoutExtension(assetPath);
            AssetImportContext.AddObjectToAsset("_superAsset", m_SuperAsset);
        }

        protected abstract void InternalOnImportAsset();
        protected abstract void InternalOnImportAssetCompleted();

        private void ReportUnityVersionError() {
            var error = SuperTiled2Unity_Config.GetVersionError();
            ReportError(error);
            Debug.LogError(error);
        }
    }
}