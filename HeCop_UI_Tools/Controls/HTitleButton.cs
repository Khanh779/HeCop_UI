using HecopUI_Winforms.Animations;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Brush = System.Drawing.Brush;
using Color = System.Drawing.Color;
using DashStyle = System.Drawing.Drawing2D.DashStyle;
using LinearGradientBrush = System.Drawing.Drawing2D.LinearGradientBrush;
using Pen = System.Drawing.Pen;

namespace HecopUI_Winforms.Controls
{
    [ToolboxBitmap(typeof(HTitleButton), "Bitmaps.Button.bmp")]
    public partial class HTitleButton : Control
    {

        /// <summary>
        ///   Gets or sets the text associated with this control.
        /// </summary>
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        public new string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value; Invalidate();
            }
        }
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

        private System.Drawing.Text.TextRenderingHint textRenderHint = GetAppResources.SetTextRender();
        public System.Drawing.Text.TextRenderingHint TextRenderHint
        {
            get { return textRenderHint; }
            set
            {
                textRenderHint = value; Invalidate();
            }
        }

        protected override void OnCreateControl()
        {
            Invalidate();
            base.OnCreateControl();
        }



        public HTitleButton()
        {
            SetStyle(GetAppResources.SetControlStyles(), true);
            DoubleBuffered = true;
            _animationManager = new AnimationManager(false)
            {
                Increment = 0.03,
                AnimationType = Animations.AnimationType.EaseOut
            };
            _animationManager.OnAnimationProgress += sender =>
            {
                Invalidate();
            };
            Size = new Size(111, 123);

            MouseDown += (sender, e) =>
            {
                butDo = true;
                if (ButtonDownColor1 == Color.Empty)
                {
                    ButtonDownColor1 = Color.FromArgb(ButtonColor1.R - 5, ButtonColor1.G - 5, ButtonColor1.B - 5);
                }
                _animationManager.StartNewAnimation(AnimationDirection.In, e.Location);
                Invalidate();
            };
            MouseUp += (sender, e) =>
            {
                butDo = false;
                Invalidate();
            };
            MouseEnter += (sender, e) =>
            {
                butHo = true;
                if (!DesignMode && AnimationMode == Enums.AnimationMode.ColorTransition)
                {
                    _animationManager.StartNewAnimation(AnimationDirection.In);
                }

                Invalidate();
            };
            MouseLeave += (sender, e) =>
            {
                butHo = false;
                if (!DesignMode && AnimationMode == Enums.AnimationMode.ColorTransition)
                {
                    _animationManager.StartNewAnimation(AnimationDirection.Out);
                }

                Invalidate();
            };

            BackColor = Color.Transparent;
            DoubleBuffered = true;

            ForeColor = Color.White;

        }


        public Enums.AnimationMode AnimationMode { get; set; } = Enums.AnimationMode.None;


        bool butHo;
        bool butDo;

        private int Ra = 2;
        public int Radius
        {
            get { return Ra; }
            set
            {
                Ra = value; Invalidate();
            }
        }

        private int BT = 0;
        public int BorderThickness
        {
            get { return BT; }
            set
            {
                BT = value; Invalidate();
            }
        }

        private Color BC = GetAppResources.GetColorNormal();
        public Color ButtonHoverColor1 { get; set; } = GetAppResources.GetHoverColor();
        public Color ButtonDownColor1 { get; set; } = GetAppResources.GetDownColor();
        public Color ButtonHoverColor2 { get; set; } = GetAppResources.GetHoverColor();
        public Color ButtonDownColor2 { get; set; } = GetAppResources.GetDownColor();

        public Color ButtonColor1
        {
            get { return BC; }
            set
            {
                BC = value;
              
                Invalidate();
            }
        }

        private Color btn2 = GetAppResources.GetColorNormal();

        public Color ButtonColor2
        {
            get { return btn2; }
            set
            {
                btn2 = value;
               
                Invalidate();
            }
        }

        private Color BDC = Color.Transparent;
        public Color BorderColor
        {
            get { return BDC; }
            set
            {
                BDC = value; Invalidate();
            }
        }

        private Image BI;
        public Image ButtonImage
        {
            get { return BI; }
            set
            {
                BI = value; Invalidate();

            }
        }



        private float IS = 5;
        public float ImageOffsetY
        {
            get { return IS; }
            set
            {
                IS = value; Invalidate();
            }
        }

        private Size ISi = new Size(50, 50);
        public Size ImageSize
        {
            get { return ISi; }
            set
            {
                ISi = value; Invalidate();
            }
        }

        private float TOY = 1;
        public float TextOffsetY
        {
            get { return TOY; }
            set
            {
                TOY = value; Invalidate();
            }
        }

        bool currentlyAnimating = false;
        private void OnFrameChanged(object o, EventArgs e)
        {

            this.Invalidate();
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


        StringFormat SF = new StringFormat();
        Pen pen;

        private int interval = 200;
        /// <summary>
        /// Set speed animation with value type milisecond to show animate
        /// </summary>
        public int Interval
        {
            get { return interval; }
            set
            {
                interval = value; Invalidate();
            }
        }


        public Color RippleColor { get; set; } = Color.Black;


        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
        }

        private Padding textPadding = new Padding(0, 0, 0, 0);
        public Padding TextPadding
        {
            get
            {
                return textPadding;
            }
            set
            {
                textPadding = value;
                if (value.Left < 0) textPadding.Left = 0;
                if (value.Top < 0) textPadding.Top = 0;
                if (value.Right < 0) textPadding.Right = 0;
                if (value.Bottom < 0) textPadding.Bottom = 0; Invalidate();
            }
        }





        public Color BorderHoverColor { get; set; } = Color.Transparent;
        public Color BorderDownColor { get; set; } = Color.Transparent;

        private Padding shadowPadding = new Padding(0, 0, 0, 0);
        public Padding ShadowPadding
        {
            get { return shadowPadding; }
            set
            {
                shadowPadding = value;
                if (value.Left < 0) shadowPadding.Left = 0;
                if (value.Top < 0) shadowPadding.Top = 0;
                if (value.Right < 0) shadowPadding.Right = 0;
                if (value.Bottom < 0) shadowPadding.Bottom = 0; Invalidate();
            }
        }

        private Color shadowColor = Color.FromArgb(60, 0, 0, 0);
        public Color ShadowColor
        {
            get { return shadowColor; }
            set
            {
                shadowColor = value; Invalidate();
            }
        }

        private int shadowRad = 5;
        public int ShadowRadius
        {
            get { return shadowRad; }
            set
            {
                shadowRad = value; Invalidate();
            }

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            float b = 0f;

            using (Bitmap bitmap = new Bitmap(Width, Height))
            using (Graphics g = Graphics.FromImage(bitmap))
            using (LinearGradientBrush LB1 = (AnimationMode == Enums.AnimationMode.Ripple ?
                new LinearGradientBrush(ClientRectangle, butHo ? ButtonHoverColor1 : ButtonColor1, butHo ? ButtonHoverColor2 : ButtonColor2, LB) :
                AnimationMode == Enums.AnimationMode.ColorTransition ?
                new LinearGradientBrush(ClientRectangle, butDo ? ButtonDownColor1 : butHo ? DrawHelper.BlendColor(ButtonColor1, ButtonHoverColor1, _animationManager.GetProgress()) : DrawHelper.BlendColor(ButtonColor1, ButtonHoverColor1, _animationManager.GetProgress()),
                butDo ? ButtonDownColor2 : butHo ? DrawHelper.BlendColor(ButtonColor2, ButtonHoverColor2, _animationManager.GetProgress()) : DrawHelper.BlendColor(ButtonColor2, ButtonHoverColor2, _animationManager.GetProgress()), LB) :
                new LinearGradientBrush(ClientRectangle, butDo ? ButtonDownColor1 : butHo ? ButtonHoverColor1 : ButtonColor1, butDo ? ButtonDownColor2 : butHo ? ButtonHoverColor2 : ButtonColor2, LB)))

            using (GraphicsPath GP = DrawHelper.GetRoundPath(new RectangleF(b + (shadowPadding.Left), b + (shadowPadding.Top), (Width - shadowPadding.Left) - (shadowPadding.Right), (Height - shadowPadding.Top) - (shadowPadding.Bottom)), Radius, BorderThickness))
            
            using (GraphicsPath SGP = DrawHelper.GetRoundPath(new RectangleF(b, b, Width, Height), Radius))
            using (GraphicsPath FillPa = DrawHelper.GetRoundPath(new RectangleF(b + (shadowPadding.Left), b + (shadowPadding.Top), (Width - shadowPadding.Left) - (shadowPadding.Right), (Height - shadowPadding.Top) - (shadowPadding.Bottom)), Radius))
            {
                if (ClipRegion == true && DesignMode == false)
                {
                    GetAppResources.MakeTransparent(this, g);
                    Region = new Region(DrawHelper.GetRoundPath(new RectangleF(0, 0, Width, Height), Radius - 2.5f));
                }
                g.TextRenderingHint = TextRenderHint;
                if (Ra != 0)
                {
                    GetAppResources.GetControlGraphicsEffect(g);
                    GetAppResources.GetControlGraphicsEffect(e.Graphics);
                }
                if (Ra == 0) g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                SF.Trimming = ST;
                SF.Alignment = StringAlignment.Center;
                SF.LineAlignment = StringAlignment.Near;
                pen = new Pen(new SolidBrush(butDo ? BorderDownColor : butHo ? BorderHoverColor : BDC), BT);
                pen.Alignment = PenAlignment.Inset;

                if (ShadowPadding.All != 0)
                    using (Bitmap Shado = HecopUI_Winforms.Ultils.DropShadow.Create(SGP, ShadowColor, shadowRad))
                    {
                        Shado.MakeTransparent();
                        if (ShadowColor.A != 0 || ShadowRadius != 0)
                            g.DrawImage(Shado, 0, 0, Width - 1, Height - 1);
                    }

                g.FillPath(LB1, FillPa);

                if (BT != 0) g.DrawPath(pen, GP);

                try
                {
                    AnimateImage();
                    ImageAnimator.UpdateFrames();
                    g.DrawImage(BI, new RectangleF(Width / 2 - ISi.Width / 2, IS, ISi.Width, ISi.Height));
                }
                catch { }
                if (Text != String.Empty)
                    g.DrawString(Text, Font, new SolidBrush(ForeColor), new RectangleF(2 + textPadding.Left, textPadding.Top + (IS + ISi.Height + TOY), Width - 2 - textPadding.Right - textPadding.Left, (this.Height) - (IS + ISi.Height + TOY) - textPadding.Bottom - textPadding.Top), SF);

                switch (AnimationMode)
                {
                    case Enums.AnimationMode.Ripple:
                        if (_animationManager.IsAnimating())
                        {

                            for (var i = 0; i < _animationManager.GetAnimationCount(); i++)
                            {
                                var animationValue = _animationManager.GetProgress(i);
                                var animationSource = _animationManager.GetSource(i);

                                using (Brush rippleBrush = new SolidBrush(Color.FromArgb((int)(101 - (animationValue * 100)), RippleColor)))
                                {
                                    var rippleSize = (int)(animationValue * (Math.Max(Width, Height)) * 3);
                                    g.FillEllipse(rippleBrush, new Rectangle(animationSource.X - rippleSize / 2, animationSource.Y - rippleSize / 2, rippleSize, rippleSize));
                                }
                            }
                        }
                        break;

                }

                if (foc)
                {
                    using (GraphicsPath gpf = DrawHelper.GetRoundPath(new RectangleF(b + (shadowPadding.Left), b + (shadowPadding.Top),
                    (Width - shadowPadding.Left) - (shadowPadding.Right),
                    (Height - shadowPadding.Top) - (shadowPadding.Bottom)), Radius, BorderThickness * 2 + 5))
                        g.DrawPath(new Pen(new SolidBrush(fbc), 1) { Alignment = PenAlignment.Inset, DashStyle = dashStyle }, gpf);
                }
                Brush br = new TextureBrush(bitmap);
                e.Graphics.FillPath(br, FillPa);
            }

            base.OnPaint(e);
        }


        bool foc = false;

        protected override void OnEnter(EventArgs e)
        {
            foc = true;
            Invalidate();
            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            foc = false; Invalidate();
            base.OnLeave(e);
        }

        Color fbc = Color.White;
        public Color FocusBorderColor
        {
            get { return fbc; }
            set { fbc = value; Invalidate(); }
        }

        private DashStyle dashStyle = DashStyle.Dot;
        public DashStyle DashStyle
        {
            get
            {
                return dashStyle;
            }
            set
            {
                dashStyle = value; Invalidate();
            }
        }

        public bool ClipRegion { get; set; } = false;

        private readonly AnimationManager _animationManager;

        private LinearGradientMode LB = LinearGradientMode.Vertical;
        public LinearGradientMode GradientMode
        {
            get { return LB; }
            set
            {
                LB = value; Invalidate();
            }
        }

        public int AlphaAnimated { get; set; } = 180;




        private StringTrimming ST = StringTrimming.EllipsisCharacter;
        public StringTrimming TextTrim
        {
            get { return ST; }
            set
            {
                ST = value; Invalidate();
            }
        }


    }
}
