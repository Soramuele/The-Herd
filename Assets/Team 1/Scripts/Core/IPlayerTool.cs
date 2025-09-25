using UnityEngine;

namespace Core 
{
    /// <summary>
    /// Interface for every tool that can be used by character(weapon, whistle, etc.).
    /// </summary>
    public interface IPlayerTool
    {
        /// <summary>
        /// Logic of tool main usage.
        /// </summary>
        public abstract void MainUsage();

        /// <summary>
        /// Logic of tool reload.
        /// </summary>
        public abstract void Reload();

        /// <summary>
        /// Logic of tool secondary usage.
        /// </summary>
        public abstract void SecondaryUsage();
    }
}