using System;

using Android.Views;
using Android.Support.V7.Widget;

namespace OnMenu.Droid
{
    /// <summary>
    /// Basic RecycleViewAdapter to inherit
    /// </summary>
    public class BaseRecycleViewAdapter : RecyclerView.Adapter
    {
        /// <summary>
        /// Itemclick handler
        /// </summary>
        public event EventHandler<RecyclerClickEventArgs> ItemClick;
        /// <summary>
        /// Itemlongclick handler
        /// </summary>
        public event EventHandler<RecyclerClickEventArgs> ItemLongClick;

        /// <summary>
        /// When implemented, it performs the actions when the viewholder is bound
        /// </summary>
        /// <param name="holder">The viewholder</param>
        /// <param name="position">The position</param>
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// When implemented, it performs the actions when the viewholder is created
        /// </summary>
        /// <param name="parent">The parent viewgroup</param>
        /// <param name="viewType">The viewtype</param>
        /// <returns></returns>
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Implement to return the itemcount
        /// </summary>
        public override int ItemCount
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// On click event
        /// </summary>
        /// <param name="args">The event arguments</param>
        protected void OnClick(RecyclerClickEventArgs args) => ItemClick?.Invoke(this, args);
        /// <summary>
        /// On long click event
        /// </summary>
        /// <param name="args">The event arguments</param>
        protected void OnLongClick(RecyclerClickEventArgs args) => ItemLongClick?.Invoke(this, args);
    }
}
