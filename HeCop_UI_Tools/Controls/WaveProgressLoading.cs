using System.Drawing;
using System.Windows.Forms;

namespace HecopUI_Winforms.Controls
{
    public class WaveProgressLoading : Control
    {
        private int waveCount;
        private int waveWidth;
        private int waveHeight;
        private int progress;

        public WaveProgressLoading()
        {
            waveCount = 5;
            waveWidth = 20;
            waveHeight = 10;
            progress = 50;
            DoubleBuffered = true;
        }

        public int WaveCount
        {
            get { return waveCount; }
            set
            {
                if (value > 0)
                {
                    waveCount = value;
                    Invalidate();
                }
            }
        }

        public int WaveWidth
        {
            get { return waveWidth; }
            set
            {
                if (value > 0)
                {
                    waveWidth = value;
                    Invalidate();
                }
            }
        }

        public int WaveHeight
        {
            get { return waveHeight; }
            set
            {
                if (value > 0)
                {
                    waveHeight = value;
                    Invalidate();
                }
            }
        }

        public int Progress
        {
            get { return progress; }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    progress = value;
                    Invalidate();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            int width = Width;
            int height = Height;
            int progressWidth = (int)(width * ((double)progress / 100));
            int x = 0;
            int y = height / 2;

            using (Brush brush = new SolidBrush(ForeColor))
            {
                for (int i = 0; i < waveCount; i++)
                {
                    Rectangle rect = new Rectangle(x, y - (waveHeight / 2), width - waveWidth, waveHeight);
                    e.Graphics.FillRectangle(brush, rect);
                    x += waveWidth;
                }
            }

            /*
            using (Brush brush = new SolidBrush(BackColor))
            {
                Rectangle rect = new Rectangle(x, 0, width - progressWidth, height);
                e.Graphics.FillRectangle(brush, rect);
            }*/
        }
    }
}
