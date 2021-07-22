using System;
using AuthenticationServices;
using DreamProperties.Common.CustomControls;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace DreamProperties.iOS.Renderers
{
    public class AppleSignInButtonRenderer : ViewRenderer<AppleSignInButton, UIView>
    {
        public static ASAuthorizationAppleIdButtonType ButtonType { get; set; } = ASAuthorizationAppleIdButtonType.Default;

        ASAuthorizationAppleIdButton button;

        protected override void OnElementChanged(ElementChangedEventArgs<AppleSignInButton> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (button == null)
                {
                    button = (ASAuthorizationAppleIdButton)CreateNativeControl();

                    SetNativeControl(button);
                }
            }
        }

        protected override UIView CreateNativeControl()
        {
            return new ASAuthorizationAppleIdButton(ButtonType, GetButtonStyle());
        }

        ASAuthorizationAppleIdButtonStyle GetButtonStyle()
        {
            switch (Element.ButtonStyle)
            {
                case AppleSignInButtonStyle.Black:
                    return ASAuthorizationAppleIdButtonStyle.Black;
                case AppleSignInButtonStyle.White:
                    return ASAuthorizationAppleIdButtonStyle.White;
                case AppleSignInButtonStyle.WhiteOutline:
                    return ASAuthorizationAppleIdButtonStyle.WhiteOutline;
            }

            return ASAuthorizationAppleIdButtonStyle.Black;
        }
    }
}
