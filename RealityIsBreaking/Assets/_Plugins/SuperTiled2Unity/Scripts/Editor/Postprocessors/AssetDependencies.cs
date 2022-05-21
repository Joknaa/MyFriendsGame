using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperTiled2Unity.Editor {
    public class AssetDependencies {
        private List<string> m_Dependencies = new List<string>();
        private readonly List<string> m_References = new List<string>();

        public AssetDependencies(string assetPath) {
            AssetPath = assetPath;
        }

        public string AssetPath { get; }
        public IEnumerable<string> Dependencies => m_Dependencies;
        public IEnumerable<string> References => m_References;

        public void AssignDependencies(IEnumerable<string> assetPaths) {
            m_Dependencies = assetPaths.ToList();
        }

        public void AddReference(string path) {
            if (!m_References.Contains(path, StringComparer.OrdinalIgnoreCase)) m_References.Add(path);
        }

        public void RemoveReference(string path) {
            m_References.RemoveAll(r => r.Equals(path, StringComparison.OrdinalIgnoreCase));
        }
    }
}