using PazzleDrag;
using PazzleDrag.WinPhone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

[assembly: ExportRenderer(typeof(BoxViewEx), typeof(BoxExRenderer))]
namespace PazzleDrag.WinPhone
{
    class BoxExRenderer : Xamarin.Forms.Platform.WinPhone.BoxViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
        {
            base.OnElementChanged(e);
            this.ManipulationDelta += BoxExRenderer_ManipulationDelta;
            this.ManipulationCompleted += BoxExRenderer_ManipulationCompleted;
            this.ManipulationStarted += BoxExRenderer_ManipulationStarted;
        }

        private void BoxExRenderer_ManipulationDelta(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
        {
            var el = this.Element as BoxViewEx;
            el.OnManipulationDelta(el, new PazzleDrag.ManipulationDeltaRoutedEventArgs(el, e.DeltaManipulation.Translation.X, e.DeltaManipulation.Translation.Y));
        }
        private void BoxExRenderer_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            var el = this.Element as BoxViewEx;
            el.OnManipulationStarted(el, new PazzleDrag.ManipulationStartedRoutedEventArgs());
        }

        private void BoxExRenderer_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            var el = this.Element as BoxViewEx;
            el.OnManipulationCompleted(el, new PazzleDrag.ManipulationCompletedRoutedEventArgs());
        }
    }
}
