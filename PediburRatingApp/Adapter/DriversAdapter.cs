using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using PediburRatingApp.DataModels;
using System.Collections.Generic;

namespace PediburRatingApp.Adapter
{
    class DriversAdapter : RecyclerView.Adapter
    {
        public event EventHandler<DriversAdapterClickEventArgs> ItemClick;
        public event EventHandler<DriversAdapterClickEventArgs> ItemLongClick;
       /* public event EventHandler<DriversAdapterClickEventArgs> SubmitRateClick;*/
        List<Drivers> Items;

        public DriversAdapter(List<Drivers>Data)
        {
            Items = Data;
            
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.DriversListLayout, parent, false);


            var vh = new DriversAdapterViewHolder(itemView, OnClick, OnLongClick /*OnSubmitClick*/);
            return vh;
        }

        
        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            
            var holder = viewHolder as DriversAdapterViewHolder;
            //holder.TextView.Text = items[position];
            holder.ID = Items[position].ID;
            holder.NameText.Text = Items[position].Name;
            holder.LocationText.Text = Items[position].Location;
            holder.BodyNumText.Text = Items[position].BodyNum;
        }

        public override int ItemCount => Items.Count;

        void OnClick(DriversAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(DriversAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);
      /*  void OnSubmitClick(DriversAdapterClickEventArgs args) => SubmitRateClick?.Invoke(this, args);*/

    }

    public class DriversAdapterViewHolder : RecyclerView.ViewHolder
    {
         public TextView NameText { get; set; }
         public TextView LocationText { get; set; }
         public TextView BodyNumText { get; set; }

        public String ID { get; set; }
     /*   public Button SubmitRateClickButton { get; set; }*/

        public DriversAdapterViewHolder(View itemView, Action<DriversAdapterClickEventArgs> clickListener,
                            Action<DriversAdapterClickEventArgs> longClickListener/* Action<DriversAdapterClickEventArgs> SubmitRateClickListener*/) : base(itemView)
        {
            NameText = (TextView)itemView.FindViewById(Resource.Id.Name);
            LocationText = (TextView)itemView.FindViewById(Resource.Id.Location);
            BodyNumText = (TextView)itemView.FindViewById(Resource.Id.BodyNum);
        //    SubmitRateClickButton = (Button)itemView.FindViewById(Resource.Id.subButton);
            itemView.Click += (sender, e) => clickListener(new DriversAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new DriversAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
           // SubmitRateClickButton.Click += (sender, e) => SubmitRateClickListener(new DriversAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }
   
    public class DriversAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}