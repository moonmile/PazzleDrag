using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PazzleDrag
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private List<BoxViewEx> boxes;
        private Rectangle boxBlank ;    // 空き位置

        /// <summary>
        /// 初期化ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clickInit(object sender, EventArgs e)
        {
            var cols = new List<Color>()
            {
                Color.Red,
                Color.Navy,
                Color.Yellow,
                Color.Pink,
                Color.Purple,
            };
            var rnd = new Random();

            // キャンバスに Box を25個並べる
            boxes = new List<BoxViewEx>();
            for (int i = 0; i < 25; i++) {
                var box = new BoxViewEx();
                boxes.Add(box);
                canvas.Children.Add(box);
                int w = 60;
                int h = 60;

                int x = (i % 5) * (w + 10) + 30;
                int y = (i / 5) * (h + 10) + 30;

                var rect = new Rectangle(new Point(x, y), new Size(w, h));
                box.LayoutTo(rect);
                box.BackgroundColor = cols[ rnd.Next(cols.Count)];
                box.ManipulationDelta += Box_ManipulationDelta;
                box.ManipulationCompleted += Box_ManipulationCompleted;
                box.ManipulationStarted += Box_ManipulationStarted;
            }
        }

        /// <summary>
        /// ボックスの移動開始
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        private void Box_ManipulationStarted(object sender , EventArgs arg2)
        {
            var box = sender as BoxViewEx;
            /// 移動開始位置を覚えておく
            boxBlank = box.Bounds;
        }


        private bool _flag = false;
        /// <summary>
        /// box の移動中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Box_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            // box の中心が他のboxに入ったときに入れ替える
            var box1 = sender as BoxViewEx;
            double x1 = box1.Bounds.Left + box1.Bounds.Width / 2;
            double y1 = box1.Bounds.Top + box1.Bounds.Height / 2;

            // 1.中心が他のboxに入っていないか調べる
            try {
                var box2 = boxes
                    .Where(x => x != box1)
                    .SingleOrDefault<BoxViewEx>(x => x.Bounds.Contains(x1, y1));
                if (box2 == null) return;

                if (_flag == true) return;
                _flag = true;
                // 2.中心に入っていれば、空位置へ移動する
                var b = boxBlank;
                boxBlank = box2.Bounds;
                box2.LayoutTo(b).ContinueWith((_) => { _flag = false; });
            }catch
            {
                return;
            }

        }
        /// <summary>
        /// タップを離したとき
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        private void Box_ManipulationCompleted(object sender, EventArgs arg2)
        {
            // 移動中の box は空きに収まるように移動させる
            var box = sender as BoxViewEx;
            box.LayoutTo(boxBlank);
            boxBlank = new Rectangle(); // 空にしておく
        }
    }
}
