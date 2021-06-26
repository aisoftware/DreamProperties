using Android.Content;
using DreamProperties.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(Entry), typeof(SimpleEntryRenderer))]
namespace DreamProperties.Droid.Renderers
{
    public class SimpleEntryRenderer: EntryRenderer
    {
        public SimpleEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                Control.Background = null;
            }
        }
    }
}