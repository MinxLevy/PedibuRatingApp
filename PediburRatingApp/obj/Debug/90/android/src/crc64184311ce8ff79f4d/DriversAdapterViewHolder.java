package crc64184311ce8ff79f4d;


public class DriversAdapterViewHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("PediburRatingApp.Adapter.DriversAdapterViewHolder, PediburRatingApp", DriversAdapterViewHolder.class, __md_methods);
	}


	public DriversAdapterViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == DriversAdapterViewHolder.class)
			mono.android.TypeManager.Activate ("PediburRatingApp.Adapter.DriversAdapterViewHolder, PediburRatingApp", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
