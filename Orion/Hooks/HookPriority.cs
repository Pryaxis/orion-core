namespace Orion.Hooks {
    /// <summary>
    /// Specifies the priority of a hook handler.
    /// </summary>
    public enum HookPriority {
        /// <summary>
        /// Indicates that the hook handler should have the lowest priority.
        /// </summary>
        Lowest,

        /// <summary>
        /// Indicates that the hook handler should have low priority.
        /// </summary>
        Low,

        /// <summary>
        /// Indicates that the hook handler should have normal priority. This is the default priority.
        /// </summary>
        Normal,

        /// <summary>
        /// Indicates that the hook handler should have high priority.
        /// </summary>
        High,

        /// <summary>
        /// Indicates that the hook handler should have the highest priority.
        /// </summary>
        Highest,

        /// <summary>
        /// Indicates that the hook handler should always be run last.
        /// </summary>
        /// <remarks>
        /// Logging events is a good use case of this, as the handler will have access to the 'freshest' data.
        /// </remarks>
        Monitor
    }
}
