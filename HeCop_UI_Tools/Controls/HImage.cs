using HecopUI_Winforms.Enums;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace HecopUI_Winforms.Controls
{
    [ToolboxBitmap(typeof(Image))]
    public partial class HImage : Control
    {
        public HImage()
        {
            SetStyle(GetAppResources.SetControlStyles(), true);
            DoubleBuffered = true;
            Size = new Size(100, 100);
            Paint += HImage_Paint;
        }

        public enum ImageSizeMode
        {
            Custom, Zoom, Fill
        }

        private ImageSizeMode ISM = ImageSizeMode.Zoom;
        public ImageSizeMode SetImageSizeMode
        {
            get { return ISM; }
            set
            {
                ISM = value; Invalidate();
            }
        }

        private Size IS = new Size(150, 150);
        public Size ImageSize
        {
            get { return IS; }
            set { IS = value; Invalidate(); }
        }



        private ShapeType ShapeType = ShapeType.Rectangle;
        public ShapeType HShapeType
        {
            get { return ShapeType; }
            set
            {
                ShapeType = value; Invalidate();
            }
        }

        private void HImage_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            GetAppResources.GetControlGraphicsEffect(g);
            try
            {
                AnimateImage();
                int SStart = 0; int SEnd = 0;
                int SWi = 0; int SHi = 0;

                switch (SetImageSizeMode)
                {
                    case ImageSizeMode.Zoom:
                        if (Width <= BI.Width)
                        {
                            SWi = Width;
                            if (SEnd != 0)
                                SEnd = Height / 2 - BI.Height / 2;
                            if (SStart != 0)
                                SStart = Width / 2 - BI.Height / 2;
                        }
                        if (Width > BI.Width)
                        {
                            SWi = BI.Width;
                            SStart = Width / 2 - BI.Height / 2;
                            SEnd = Height / 2 - BI.Height / 2;
                            if (SEnd < 0) SEnd = 0;
                        }
                        if (Height <= BI.Height)
                        {
                            SHi = Height;
                            if (SStart != 0)
                                SStart = Width / 2 - BI.Height / 2;
                            if (SEnd != 0)
                                SEnd = Height / 2 - BI.Height / 2;
                        }
                        if (Height > BI.Height)
                        {
                            SHi = BI.Height;
                            SStart = Width / 2 - BI.Height / 2;
                            SEnd = Height / 2 - BI.Height / 2;
                            if (SStart < 0) SStart = 0;
                        }

                        break;
                    case ImageSizeMode.Custom:
                        SStart = Width / 2 - IS.Width / 2;
                        SEnd = Height / 2 - IS.Height / 2;
                        SWi = IS.Width; SHi = IS.Height;
                        break;
                    case ImageSizeMode.Fill:
                        SWi = Width; SHi = Height;
                        break;

                }
                //Get the next frame ready for rendering.
                ImageAnimator.UpdateFrames();
                GraphicsPath path = new GraphicsPath();
                //g.SetClip(path);
                //g.DrawImage(BI, new Rectangle(SStart, SEnd, SWi, SHi));
                g.DrawImage(CropCircle(BI, path), new Rectangle(SStart, SEnd, SWi, SHi));
            }

            catch { }
        }

        private Bitmap CropCircle(Image img, GraphicsPath gp)
        {
            var roundedImage = new Bitmap(img.Width, img.Height, img.PixelFormat);
            roundedImage.MakeTransparent();
            using (var g = Graphics.FromImage(roundedImage))
            {
                GetAppResources.GetControlGraphicsEffect(g);
                Brush brush = new TextureBrush(img);
                switch (HShapeType)
                {
                    case ShapeType.Rectangle:
                        gp.AddRectangle(new RectangleF(0, 0, img.Width, img.Height));
                        break;
                    case ShapeType.Circular:
                        gp.AddEllipse(0, 0, img.Width, img.Height);
                        break;
                }
                g.FillPath(brush, gp);
            }
            return roundedImage;
        }

        bool currentlyAnimating = false;
        private void OnFrameChanged(object o, EventArgs e)
        {

            this.Invalidate();
        }

        private Image BI;
        public Image Image
        {
            get { return BI; }
            set
            {
                BI = value; Invalidate();
            }
        }

        public void AnimateImage()
        {

            if (!currentlyAnimating)
            {

                //Begin the animation only once.
                ImageAnimator.Animate(BI, new EventHandler(this.OnFrameChanged));
                currentlyAnimating = true;
            }
        }
    }
}
