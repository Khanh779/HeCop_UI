using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace HecopUI_Winforms.Controls
{
    [ToolboxBitmap(typeof(Panel))]
    public partial class HRadiusPanel : Panel
    {
        public HRadiusPanel()
        {
            SetStyle(GetAppResources.SetControlStyles(), true);
            BackColor = Color.Transparent;
            Paint += HRadiusPanel_Paint;
        }

        private Color panelColor1 = Color.LightGray;
        public Color PanelColor1
        {
            get { return panelColor1; }
            set
            {
                panelColor1 = value; Invalidate();
            }
        }

        private Color panelColor2 = Color.PeachPuff;
        public Color PanelColor2
        {
            get { return panelColor2; }
            set
            {
                panelColor2 = value; Invalidate();
            }
        }

        private Color borderColor = Color.Transparent;
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value; Invalidate();
            }
        }

        private float bordw = 2;
        public float BorderThickness
        {
            get { return bordw; }
            set
            {
                bordw = value; Invalidate();
            }
        }

        private float Ra = 2;
        public float Radius
        {
            get { return Ra; }
            set
            {
                Ra = value; Invalidate();
            }
        }

        private bool roundTopLeft = true;
        private bool roundTopRight = true;
        private bool roundBottomLeft = true;
        private bool roundBottomRight = true;
        public bool RoundTopLeft
        {
            get => roundTopLeft;
            set
            {
                roundTopLeft = value; Invalidate();
            }
        }
        public bool RoundTopRight
        {
            get => roundTopRight;
            set
            {
                roundTopRight = value; Invalidate();
            }
        }

        public bool RoundBottomLeft
        {
            get => roundBottomLeft;
            set
            {
                roundBottomLeft = value; Invalidate();
            }
        }
        public bool RoundBottomRight
        {
            get => roundBottomRight;
            set
            {
                roundBottomRight = value; Invalidate();
            }
        }

        private LinearGradientMode LinearGradient = LinearGradientMode.Vertical;
        public LinearGradientMode GradientMode
        {
            get { return LinearGradient; }
            set
            {
                LinearGradient = value; Invalidate();
            }
        }


        private Padding shadowPadding = new Padding(0, 0, 0, 0);
        public Padding ShadowPadding
        {
            get { return shadowPadding; }
            set
            {
                shadowPadding = value; Invalidate();
            }
        }

        private Color shadownColor = Color.FromArgb(100, 0, 0, 0);
        public Color ShadowColor
        {
            get { return shadownColor; }
            set
            {
                shadownColor = value; Invalidate();
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

        private void HRadiusPanel_Paint(object sender, PaintEventArgs e)
        {
            using (GraphicsPath gp = DrawHelper.GetRoundPath(new RectangleF(ShadowPadding.Left,
     ShadowPadding.Top, Width - ShadowPadding.Right - ShadowPadding.Left, Height - ShadowPadding.Bottom - ShadowPadding.Top), Radius))
            using (LinearGradientBrush LB = new LinearGradientBrush(gp.GetBounds(), panelColor1, panelColor2, LinearGradient))
            using (Bitmap Shado = HecopUI_Winforms.Ultils.DropShadow.Create(gp, ShadowColor, shadowRad))
            {
                Shado.MakeTransparent();
                // Tạo một đối tượng Graphics từ Bitmap để vẽ shadow
                using (Graphics shadowGraphics = Graphics.FromImage(Shado))
                {
                    GetAppResources.GetControlGraphicsEffect(e.Graphics);
                    GetAppResources.GetControlGraphicsEffect(shadowGraphics);
                    // Vẽ shadow
                    shadowGraphics.FillPath(LB, gp);
                    shadowGraphics.DrawPath(new Pen(borderColor, bordw) { Alignment = PenAlignment.Inset }, gp);
                }

                // Vẽ shadow lên đối tượng Graphics chính
                using (var a = new TextureBrush(Shado))
                    e.Graphics.FillPath(a, gp);
            }



        }
    }
}
