using DreamProperties.Common.CustomControls;
using DreamProperties.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SimpleEntry), typeof(SimpleEntryRenderer))]
namespace DreamProperties.iOS.Renderers
{
    public class SimpleEntryRenderer: EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Background = null;
                Control.BorderStyle = UITextBorderStyle.None;
            }
        }
    }
}