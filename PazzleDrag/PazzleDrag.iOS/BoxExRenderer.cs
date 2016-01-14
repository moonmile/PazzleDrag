using PazzleDrag;
using PazzleDrag.iOS;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Foundation;
using UIKit;

[assembly: ExportRenderer(typeof(BoxViewEx), typeof(BoxExRenderer))]
namespace PazzleDrag.iOS
{
    class BoxExRenderer : BoxRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
        {
            base.OnElementChanged(e);
        }
        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            UITouch touch = touches.AnyObject as UITouch;
            var el = this.Element as BoxViewEx;
            el.OnManipulationStarted(el, new ManipulationStartedRoutedEventArgs());
        }
        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);
            UITouch touch = touches.AnyObject as UITouch;
            var newPoint = touch.LocationInView(this);
            var previousPoint = touch.PreviousLocationInView(this);
            nfloat dx = newPoint.X - previousPoint.X;
            nfloat dy = newPoint.Y - previousPoint.Y;
            var el = this.Element as BoxViewEx;
            el.OnManipulationDelta(el, new ManipulationDeltaRoutedEventArgs(el, dx, dy));
        }
        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);
            var el = this.Element as BoxViewEx;
            el.OnManipulationCompleted(el, new ManipulationCompletedRoutedEventArgs());
        }
    }
}
