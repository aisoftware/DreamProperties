using System;
using CoreGraphics;
using DreamProperties.Common.CustomControls;
using DreamProperties.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ShadowFrame), typeof(ShadowFrameRenderer))]
namespace DreamProperties.iOS
{
    public class ShadowFrameRenderer: FrameRenderer
    {
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            base.LayoutSubviews();
            this.Layer.ShadowColor = UIColor.DarkGray.CGColor;
            this.Layer.ShadowOpacity = 0.2f;
            this.Layer.ShadowRadius = 8.0f;
            this.Layer.ShadowOffset = new CGSize(0, 5);
            this.Layer.ShadowPath = UIBezierPath.FromRect(Layer.Bounds).CGPath;
            this.Layer.MasksToBounds = false;
        }
    }
}
