namespace SuperTiled2Unity.Editor {
    // Helper struct to deal with Tile Id values having burned-in flip flags
    public struct TileIdMath {
        // Flip flags from tiled
        private const uint TiledHexagonal120Flag = 0x10000000;
        private const uint TiledDiagonalFlipFlag = 0x20000000;
        private const uint TiledVerticalFlipFlag = 0x40000000;
        private const uint TiledHorizontalFlipFlag = 0x80000000;

        public TileIdMath(uint importedTileId) {
            ImportedlTileId = importedTileId;

            FlipFlags = 0;
            FlipFlags |= HasHorizontalFlip ? FlipFlags.Horizontal : 0;
            FlipFlags |= HasVerticalFlip ? FlipFlags.Vertical : 0;
            FlipFlags |= HasDiagonalFlip ? FlipFlags.Diagonal : 0;
            FlipFlags |= HasHexagonal120Flip ? FlipFlags.Hexagonal120 : 0;
        }

        // The tileId with baked in flip flags
        public uint ImportedlTileId { get; }

        // Just the raw tileId (no flip flags)
        public int JustTileId => (int) (ImportedlTileId & ~(TiledHorizontalFlipFlag | TiledVerticalFlipFlag | TiledDiagonalFlipFlag | TiledHexagonal120Flag));

        public FlipFlags FlipFlags { get; }

        public bool HasHorizontalFlip => (ImportedlTileId & TiledHorizontalFlipFlag) != 0;

        public bool HasVerticalFlip => (ImportedlTileId & TiledVerticalFlipFlag) != 0;

        public bool HasDiagonalFlip => (ImportedlTileId & TiledDiagonalFlipFlag) != 0;

        public bool HasHexagonal120Flip => (ImportedlTileId & TiledHexagonal120Flag) != 0;

        public bool HasFlip => HasHorizontalFlip || HasVerticalFlip || HasDiagonalFlip || HasHexagonal120Flip;
    }
}