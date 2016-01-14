using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using PazzleDrag;
using PazzleDrag.Droid;

[assembly: ExportRenderer(typeof(BoxViewEx), typeof(BoxExRenderer))]
namespace PazzleDrag.Droid
{
    class BoxExRenderer : BoxRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
        {
            base.OnElementChanged(e);
            this.Touch += BoxExRenderer_Touch;
        }

        float _gx, _gy; // 初期の相対値
        float _ox, _oy; // 前回の絶対位置
        /// <summary>
        /// タッチイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoxExRenderer_Touch(object sender, TouchEventArgs e)
        {
            var box = sender as Android.Views.View;
            var el = this.Element as BoxViewEx;
            switch (e.Event.Action)
            {
                case MotionEventActions.Down:
                    // 初期の相対値を保存
                    _gx = e.Event.GetX();
                    _gy = e.Event.GetY();
                    el.OnManipulationStarted(el, new ManipulationStartedRoutedEventArgs());
                    break;
                case MotionEventActions.Move:
                    // 移動距離を計算
                    float dx = e.Event.RawX - _ox;
                    float dy = e.Event.RawY - _oy;
                    // 移動
                    // TODO: 誤差で少しずれるが実用上問題ない
                    // setPos((int)box.Left + (int)dx, (int)box.Top + (int)dy);
                    // OnManipulationDelta(sender, new ManipulationDeltaRoutedEventArgs(sender, (int)dx, (int)dy));
                    // System.Diagnostics.Debug.WriteLine("move: Raw{0} {1} dx:{2} {3}", e.Event.RawX, e.Event.RawY, dx, dy);

                    // コールバック呼び出し
                    // TODO: delta 方式なのか誤差が大きい
                    el.OnManipulationDelta(el, new ManipulationDeltaRoutedEventArgs(sender, dx, dy));
                    break;
                case MotionEventActions.Up:
                    el.OnManipulationCompleted(el, new ManipulationCompletedRoutedEventArgs());
                    break;
            }
            // 現在の絶対位置を保存
            _ox = e.Event.RawX;
            _oy = e.Event.RawY;
        }
    }
}