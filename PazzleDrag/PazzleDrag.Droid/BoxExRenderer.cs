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

        float _gx, _gy; // �����̑��Βl
        float _ox, _oy; // �O��̐�Έʒu
        /// <summary>
        /// �^�b�`�C�x���g
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
                    // �����̑��Βl��ۑ�
                    _gx = e.Event.GetX();
                    _gy = e.Event.GetY();
                    el.OnManipulationStarted(el, new ManipulationStartedRoutedEventArgs());
                    break;
                case MotionEventActions.Move:
                    // �ړ��������v�Z
                    float dx = e.Event.RawX - _ox;
                    float dy = e.Event.RawY - _oy;
                    // �ړ�
                    // TODO: �덷�ŏ�������邪���p����Ȃ�
                    // setPos((int)box.Left + (int)dx, (int)box.Top + (int)dy);
                    // OnManipulationDelta(sender, new ManipulationDeltaRoutedEventArgs(sender, (int)dx, (int)dy));
                    // System.Diagnostics.Debug.WriteLine("move: Raw{0} {1} dx:{2} {3}", e.Event.RawX, e.Event.RawY, dx, dy);

                    // �R�[���o�b�N�Ăяo��
                    // TODO: delta �����Ȃ̂��덷���傫��
                    el.OnManipulationDelta(el, new ManipulationDeltaRoutedEventArgs(sender, dx, dy));
                    break;
                case MotionEventActions.Up:
                    el.OnManipulationCompleted(el, new ManipulationCompletedRoutedEventArgs());
                    break;
            }
            // ���݂̐�Έʒu��ۑ�
            _ox = e.Event.RawX;
            _oy = e.Event.RawY;
        }
    }
}