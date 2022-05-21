using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace SuperTiled2Unity.Editor {
    public class RecursiveAssetDependencyTracker {
        private readonly HashSet<string> m_Dependencies = new HashSet<string>();
        private readonly string m_SourceAsset;
        private readonly HashSet<string> m_VisitedFiles = new HashSet<string>();

        public RecursiveAssetDependencyTracker(string asset) {
            m_SourceAsset = asset;
            ProcessFile(m_SourceAsset);
        }

        public List<string> Dependencies => m_Dependencies.ToList();

        private void ProcessFile(string assetPath) {
            if (!m_VisitedFiles.Contains(assetPath, StringComparer.OrdinalIgnoreCase)) {
                m_VisitedFiles.Add(assetPath);
                m_Dependencies.Add(assetPath);

                var super = AssetDatabase.LoadAssetAtPath<SuperAsset>(assetPath);
                if (super != null)
                    foreach (var path in super.AssetDependencies)
                        ProcessFile(path);
            }
        }
    }
}