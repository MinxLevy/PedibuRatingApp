using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using PediburRatingApp.DataModels;
using Android.Graphics;

namespace PediburRatingApp.Adapter
{
    class DriversAdapter : RecyclerView.Adapter
    {
        public event EventHandler<DriversAdapterClickEventArgs> ItemClick;
        public event EventHandler<DriversAdapterClickEventArgs> ItemLongClick;

        List<Drivers>Items;
        public DriversAdapter(List<Drivers>Data)
        {
            Items = Data;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.DriversListLayout, parent, false);
                
            var vh = new DriversAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var holder = viewHolder as DriversAdapterViewHolder;
            //holder.TextView.Text = items[position];
            holder.Name.Text = Items[position].Name;
            holder.Location.Text = Items[position].Location;
            holder.BodyNum.Text = Items[position].BodyNum;
         

        }

        public override int ItemCount => Items.Count;

        void OnClick(DriversAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(DriversAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

        public class DriversAdapterViewHolder : RecyclerView.ViewHolder
        {
            //public TextView TextView { get; set; }
            public TextView Name { get; set; }
            public TextView BodyNum { get; set; }
            public TextView Location { get; set; }

            public DriversAdapterViewHolder(View itemView, Action<DriversAdapterClickEventArgs> clickListener,
                                Action<DriversAdapterClickEventArgs> longClickListener) : base(itemView)
            {
                //TextView = v;
                Name = (TextView)itemView.FindViewById(Resource.Id.Name);
                Location = (TextView)itemView.FindViewById(Resource.Id.Location);
                BodyNum = (TextView)itemView.FindViewById(Resource.Id.BodyNum);


                itemView.Click += (sender, e) => clickListener(new DriversAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
                itemView.LongClick += (sender, e) => longClickListener(new DriversAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            }
        }

    }



    public class DriversAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }

}