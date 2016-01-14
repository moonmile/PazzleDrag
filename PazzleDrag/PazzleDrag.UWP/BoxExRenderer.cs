using PazzleDrag;
using PazzleDrag.UWP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(BoxViewEx), typeof(BoxExRenderer))]
namespace PazzleDrag.UWP
{
    class BoxExRenderer : Xamarin.Forms.Platform.UWP.BoxViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
        {
            base.OnElementChanged(e);
            this.ManipulationDelta += BoxExRenderer_ManipulationDelta1;
            this.ManipulationCompleted += BoxExRenderer_ManipulationCompleted;
            this.ManipulationStarted += BoxExRenderer_ManipulationStarted;
            this.ManipulationMode = Windows.UI.Xaml.Input.ManipulationModes.All;
        }

        private void BoxExRenderer_ManipulationStarted(object sender, Windows.UI.Xaml.Input.ManipulationStartedRoutedEventArgs e)
        {
            var el = this.Element as BoxViewEx;
            el.OnManipulationStarted(el, new PazzleDrag.ManipulationStartedRoutedEventArgs());
        }

        private void BoxExRenderer_ManipulationCompleted(object sender, Windows.UI.Xaml.Input.ManipulationCompletedRoutedEventArgs e)
        {
            var el = this.Element as BoxViewEx;
            el.OnManipulationCompleted(el, new PazzleDrag.ManipulationCompletedRoutedEventArgs());
        }

        private void BoxExRenderer_ManipulationDelta1(object sender, Windows.UI.Xaml.Input.ManipulationDeltaRoutedEventArgs e)
        {
            var el = this.Element as BoxViewEx;
            el.OnManipulationDelta(el, new PazzleDrag.ManipulationDeltaRoutedEventArgs(el, e.Delta.Translation.X, e.Delta.Translation.Y));
        }
    }
}
