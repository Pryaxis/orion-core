namespace Orion.Networking.Packets.Misc {
    /// <summary>
    /// Specifies the type of action in a <see cref="MiscActionPacket"/>.
    /// </summary>
    public enum MiscAction : byte {
        /// <summary>
        /// Spawn skeletron.
        /// </summary>
        SpawnSkeletron = 1,

        /// <summary>
        /// Play the whoopie cushion sound.
        /// </summary>
        WhoopieCushion = 2,

        /// <summary>
        /// Use the sundial.
        /// </summary>
        UseSundial = 3,
            
        /// <summary>
        /// Create mimic smoke.
        /// </summary>
        CreateMimicSmoke = 4,
    }
}
