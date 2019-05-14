namespace OnMenu.Droid
{
    /// <summary>
    /// Interface for visible fragments
    /// </summary>
    interface IFragmentVisible
    {
        /// <summary>
        /// Should implement to inform the fragment that it became visible and perform the necessary actions
        /// </summary>
        void BecameVisible();
    }
}
