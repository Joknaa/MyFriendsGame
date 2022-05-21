using System;

namespace SuperTiled2Unity.Editor {
    public struct CollisionClipperKey : IEquatable<CollisionClipperKey> {
        public CollisionClipperKey(int layerId, string layerName, bool isTrigger) {
            LayerId = layerId;
            LayerName = layerName;
            IsTrigger = isTrigger;
        }

        public int LayerId { get; }

        public string LayerName { get; }

        public bool IsTrigger { get; }

        public override int GetHashCode() {
            var result = LayerId.GetHashCode();
            result = (result * 397) ^ LayerName.GetHashCode();
            result = (result * 397) ^ IsTrigger.GetHashCode();
            return result;
        }

        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType()) return false;
            return Equals((CollisionClipperKey) obj);
        }

        public bool Equals(CollisionClipperKey other) {
            return other.LayerId.Equals(LayerId) &&
                   other.LayerName.Equals(LayerName, StringComparison.OrdinalIgnoreCase) &&
                   other.IsTrigger.Equals(IsTrigger);
        }
    }
}