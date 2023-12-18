using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace HecopUI_Winforms.Controls
{
    [ToolboxBitmap(typeof(HProgressBar), "Bitmaps.Progress.bmp")]
    public partial class HProgressBar : Control
    {

        int interval = 50;
        public int Interval
        {
            get { return interval; }
            set
            {
                interval = value;
                tmrIndi.Interval = value;
                Invalidate();
            }
        }

        int locx = 0;

        public HProgressBar()
        {
            SetStyle(GetAppResources.SetControlStyles(), true);
            ForeColor = Color.White;
            BorderColor = Color.Silver;
            tmrIndi = new System.Windows.Forms.Timer();
            tmrIndi.Tick += TmrIndi_Tick1;
            //tmrIndi.Interval = Interval;
        }

        protected override void OnCreateControl()
        {
            if (base.IsHandleCreated)
                // if (!DesignMode)
                tmrIndi.Start();
            base.OnCreateControl();
        }

        private void TmrIndi_Tick1(object sender, EventArgs e)
        {
            switch (animationMode)
            {
                case Enums.ProgressAnimationMode.Indeterminate:
                    if (locx < Width) locx += 10;
                    if (locx > Width)
                    {
                        locx = 0 - (int)((AnV - MV) * (Width) / MAV);
                    }
                    break;
                case Enums.ProgressAnimationMode.Value:
                    if (AnV != PV)
                    {
                        if (AnV > PV)
                        {
                            PV += 1;
                        }
                        if (AnV < PV) PV -= 1;
                    }
                    else tmrIndi.Stop();
                    break;

            }

            Invalidate();
        }

        private System.Windows.Forms.Timer tmrIndi;

        protected override void OnTextChanged(EventArgs e)
        {
            Invalidate();
            base.OnTextChanged(e);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            Invalidate();
            base.OnFontChanged(e);
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            Invalidate();
            base.OnForeColorChanged(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            RectangleF recPro = new RectangleF(0.5f, 0.5f, Width - 1, Height - 1);
            RectangleF recf = new RectangleF(0, 0, Width - 0.5f, Height);
            GetAppResources.GetControlGraphicsEffect(e.Graphics);
            using (Bitmap bitm = new Bitmap(Width, Height))
            using (Graphics g = Graphics.FromImage(bitm))
            using (GraphicsPath GP = HecopUI_Winforms.DrawHelper.GetRoundPath(recf, Radius, 0))
            using (LinearGradientBrush LB = new LinearGradientBrush(recPro, BPC1, BPC2, Linear))
            using (LinearGradientBrush LB1 = new LinearGradientBrush(recPro, PC1, PC2, Linear))
            using (GraphicsPath GPV = AnimationMode == Enums.ProgressAnimationMode.None ? DrawHelper.GetRoundPath(recPro, Ra, 0) :
            AnimationMode == Enums.ProgressAnimationMode.Value ? DrawHelper.GetRoundPath(recPro, Ra, 0) : DrawHelper.GetRoundPath(new RectangleF(locx, 0.5f, 30 + locx, Height - 1), Radius, 0))
            {
                GetAppResources.GetControlGraphicsEffect(g);
                g.FillPath(LB, GP);
                switch (AnimationMode)
                {
                    case Enums.ProgressAnimationMode.None:
                        recPro.Width = (float)((AnV - MV) * (Width) / MAV);
                        if (AnV != 0) g.FillPath(LB1, GPV);
                        break;
                    case Enums.ProgressAnimationMode.Value:
                        recPro.Width = (float)((PV - MV) * (Width) / MAV);
                        if (AnV != 0) g.FillPath(LB1, GPV);
                        break;
                    case Enums.ProgressAnimationMode.Indeterminate:
                        g.FillPath(LB1, GPV);
                        break;
                }

                if (BT != 0)
                {
                    using (Pen pen = new Pen(new SolidBrush(BC), BT))
                    {
                        pen.Alignment = PenAlignment.Inset;
                        g.DrawPath(pen, HecopUI_Winforms.DrawHelper.GetRoundPath(recf, Radius, BorderWidth));
                    }
                }
                e.Graphics.FillPath(new TextureBrush(bitm), GP);

            }
            base.OnPaint(e);
        }

        private LinearGradientMode Linear = LinearGradientMode.Horizontal;
        public LinearGradientMode GradientMode
        {
            get { return Linear; }
            set
            {
                Linear = value; Invalidate();
            }
        }

        int AnV = 0;
        public int ProgressValue
        {
            get { return AnV; }
            set
            {
                if (value > MAV)
                {
                    AnV = MAV;
                }
                if (value < MV)
                {
                    AnV = MV;
                }
                if (value >= MV || value <= MAV) AnV = value;

                Invalidate();
            }
        }

        private Enums.ProgressAnimationMode animationMode = Enums.ProgressAnimationMode.Value;
        public Enums.ProgressAnimationMode AnimationMode
        {
            get { return animationMode; }
            set
            {
                animationMode = value;
                locx = 0;
                if (animationMode != Enums.ProgressAnimationMode.None)
                {
                    if (IsHandleCreated)
                        //if(!DesignMode)
                        tmrIndi.Start();
                }
                Invalidate();

            }
        }

        private int Ra = 2;
        private int BT = 1;

        private int MAV = 100;
        private int MV = 0;
        private int PV = 0;

        private Color BC = GetAppResources.GetBorderProgressBarColor();

        private Color BPC2 = GetAppResources.GetBaseProgressBarColor();
        private Color PC2 = GetAppResources.GetProgressBarColor();


        public Color ProgressColor2
        {
            get { return PC2; }
            set
            {
                PC2 = value; Invalidate();
            }
        }

        public Color BaseProgressColor2
        {
            get { return BPC2; }
            set
            {
                BPC2 = value; Invalidate();
            }
        }

        private Color BPC1 = GetAppResources.GetBaseProgressBarColor();
        private Color PC1 = GetAppResources.GetProgressBarColor();

        public Color ProgressColor1
        {
            get { return PC1; }
            set
            {
                PC1 = value; Invalidate();
            }
        }

        public Color BaseProgressColor1
        {
            get { return BPC1; }
            set
            {
                BPC1 = value; Invalidate();
            }
        }

        public Color BorderColor
        {
            get { return BC; }
            set
            {
                BC = value; Invalidate();
            }
        }

        public int Radius
        {
            get { return Ra; }
            set
            {
                Ra = value; Invalidate();
            }
        }

        public int BorderWidth
        {
            get { return BT; }
            set
            {
                BT = value; Invalidate();
            }
        }

        public int MinimumValue
        {
            get { return MV; }
            set
            {
                if (value < 0) MV = 0;
                else MV = value; Invalidate();
            }
        }

        public int MaximumValue
        {
            get { return MAV; }
            set
            {
                if (value < AnV) MAV = AnV;
                else MAV = value; Invalidate();
            }
        }
    }
}
