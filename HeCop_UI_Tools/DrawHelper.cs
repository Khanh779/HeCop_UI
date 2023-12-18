namespace HecopUI_Winforms
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    /// <summary>
    /// Defines the <see cref="DrawHelper" />
    /// </summary>
    internal static class DrawHelper
    {

        public static void DrawBlurred(Graphics graphics, Color color, GraphicsPath graphicsPath, int max_alpha, int pen_width)
        {
            float tmp = max_alpha / pen_width;
            float actualAlpha = tmp;

            for (int tmp_width = pen_width; tmp_width > 0; tmp_width--)
            {
                Pen blurredPen = new Pen(Color.FromArgb((int)actualAlpha, color), tmp_width)
                {
                    StartCap = LineCap.Round,
                    EndCap = LineCap.Round
                };
                actualAlpha += tmp;

                graphics.DrawPath(blurredPen, graphicsPath);
            }
        }

        public static GraphicsPath GetRoundPath(RectangleF Rect, float radius, float width = 0)
        {
            //Fix radius to rect size
            radius = (int)Math.Max(
                     (Math.Min(radius,
                     Math.Min(Rect.Width, Rect.Height)) - width), 1) * 2;
            //radius *= 2;
            var r2 = (radius / 2f);
            var w2 = (width / 2f);
            GraphicsPath GraphPath = new GraphicsPath();

            if (radius != 0)
            {
                //Top-Left Arc
                GraphPath.AddArc(Rect.X + w2, Rect.Y + w2, radius, radius, 180, 90);

                //Top-Right Arc
                GraphPath.AddArc(Rect.X + Rect.Width - radius - w2, Rect.Y + w2, radius,
                                 radius, 270, 90);

                //Bottom-Right Arc
                GraphPath.AddArc(Rect.X + Rect.Width - w2 - radius,
                            Rect.Y + Rect.Height - w2 - radius, radius, radius, 0, 90);
                //Bottom-Left Arc
                GraphPath.AddArc(Rect.X + w2, Rect.Y - w2 + Rect.Height - radius, radius,
                                 radius, 90, 90);

                //Close line ( Left)           
                GraphPath.AddLine(Rect.X + w2, Rect.Y + Rect.Height - r2 - w2, Rect.X +
                w2, Rect.Y + r2 + w2);


            }
            if (radius == 0) GraphPath.AddRectangle(new RectangleF(Rect.X + w2, Rect.Y + w2, Rect.Width - w2, Rect.Height - w2));
            return GraphPath;
        }


        public static PathGradientBrush PathBrush(GraphicsPath path, RectangleF bound, Color color1, Color color2, bool InOut=true)
        {
            using (PathGradientBrush brush = new PathGradientBrush(path))
            {
                // Thiết lập màu trong suốt ở tâm của brush
                brush.CenterColor = InOut? color1:color2;
                // Thiết lập màu vùng xung quanh brush
                brush.SurroundColors = new Color[] { InOut? color2:color1 };
                return brush;
            }
        }


        /// <summary>
        /// Create path rounded edge rectangle border with different radius.
        /// </summary>
        /// <param name="bounds">Gets or sets rectangle of control. <seealso cref="Rectangle">bounds</seealso></param>
        /// <param name="radiusTopLeft">Gets or sets corner radius top left.</param>
        /// <param name="radiusTopRight">Gets or sets corner radius top right. </param>
        /// <param name="radiusBottomLeft">Gets or sets corner radius bottom left.</param>
        /// <param name="radiusBottomRight">Gets or sets corner radius bottom right. </param>
        /// <param name="borderSize">Gets or sets size of border corner. <para>By default the value is <see>0</see>.</para></param>
        /// <returns></returns>


        public static GraphicsPath RoundedRect(RectangleF bounds, int radiusTopLeft, int radiusTopRight, int radiusBottomLeft, int radiusBottomRight, float borderSize = 0)
        {
            int radius1 = radiusTopLeft;
            int radius2 = radiusTopRight;
            int radius3 = radiusBottomLeft;
            int radius4 = radiusBottomRight;
            float width = borderSize;

            radius1 *= 2; radius2 *= 2; radius3 *= 3; radius4 *= 2;

            float w2 = width / 2;

            int diameter1 = radius1 * 2;
            int diameter2 = radius2 * 2;
            int diameter3 = radius3 * 2;
            int diameter4 = radius4 * 2;

            RectangleF arc1 = new RectangleF(bounds.Location, new Size(diameter1, diameter1));
            RectangleF arc2 = new RectangleF(bounds.Location, new Size(diameter2, diameter2));
            RectangleF arc3 = new RectangleF(bounds.Location, new Size(diameter3, diameter3));
            RectangleF arc4 = new RectangleF(bounds.Location, new Size(diameter4, diameter4));
            GraphicsPath path = new GraphicsPath();
            arc1.X += w2;
            // top left arc  
            arc1.Y += (w2);
            if (radius1 == 0)
            {
                path.AddLine(arc1.Location, arc1.Location);
            }
            else
            {
                path.AddArc(arc1, 180, 90);
            }

            // top right arc  
            arc2.X = bounds.Right - diameter2 - w2;
            arc2.Y += (w2);
            if (radius2 == 0)
            {
                path.AddLine(arc2.Location, arc2.Location);
            }
            else
            {
                path.AddArc(arc2, 270, 90);
            }

            // bottom right arc  

            arc4.X = bounds.Right - diameter4 - w2;
            arc4.Y = bounds.Bottom - diameter4 - w2;

            if (radius4 == 0)
            {
                path.AddLine(arc4.Location, arc4.Location);
            }
            else
            {
                path.AddArc(arc4, 0, 90);
            }

            // bottom left arc 
            arc3.X = bounds.Right - diameter3 - w2;
            arc3.Y = bounds.Bottom - diameter3 - w2;
            arc3.X = bounds.Left + w2;
            if (radius3 == 0)
            {
                path.AddLine(arc3.Location, arc3.Location);
            }
            else
            {
                path.AddArc(arc3, 90, 90);
            }

            path.AddLine(new PointF(arc1.Location.X, arc1.Location.Y + radius1), arc3.Location);
            //path.ClearMarkers();
            //path.CloseFigure();

            return path;
        }

        /// <summary>
        /// The BlendColor
        /// </summary>
        /// <param name="backgroundColor">The backgroundColor<see cref="Color"/></param>
        /// <param name="frontColor">The frontColor<see cref="Color"/></param>
        /// <param name="blend">The blend<see cref="double"/></param>
        /// <returns>The <see cref="Color"/></returns>
        public static Color BlendColor(Color backgroundColor, Color frontColor, double blend)
        {
            var ratio = blend / 255d;
            var invRatio = 1d - ratio;
            var a = (int)((backgroundColor.A * invRatio) + (frontColor.A * ratio));
            var r = (int)((backgroundColor.R * invRatio) + (frontColor.R * ratio));
            var g = (int)((backgroundColor.G * invRatio) + (frontColor.G * ratio));
            var b = (int)((backgroundColor.B * invRatio) + (frontColor.B * ratio));
            return Color.FromArgb(a, r, g, b);
        }

        /// <summary>
        /// The BlendColor
        /// </summary>
        /// <param name="backgroundColor">The backgroundColor<see cref="Color"/></param>
        /// <param name="frontColor">The frontColor<see cref="Color"/></param>
        /// <returns>The <see cref="Color"/></returns>
        public static Color BlendColor(Color backgroundColor, Color frontColor)
        {
            return BlendColor(backgroundColor, frontColor, frontColor.A);
        }



        public static void DrawRoundShadow(Graphics g, Rectangle bounds)
        {
            using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(12, 0, 0, 0)))
            {
                g.FillEllipse(shadowBrush, new Rectangle(bounds.X - 2, bounds.Y - 1, bounds.Width + 4, bounds.Height + 6));
                g.FillEllipse(shadowBrush, new Rectangle(bounds.X - 1, bounds.Y - 1, bounds.Width + 2, bounds.Height + 4));
                g.FillEllipse(shadowBrush, new Rectangle(bounds.X - 0, bounds.Y - 0, bounds.Width + 0, bounds.Height + 2));
                g.FillEllipse(shadowBrush, new Rectangle(bounds.X - 0, bounds.Y + 2, bounds.Width + 0, bounds.Height + 0));
                g.FillEllipse(shadowBrush, new Rectangle(bounds.X - 0, bounds.Y + 1, bounds.Width + 0, bounds.Height + 0));
            }
        }

        public static void FillBackControl(Graphics graphics, RectangleF rectf, Brush brush, int radius, int border)
        {

            //graphics.FillPath(brush, GetAppResources.MyRectangle.GetFigurePath(rectf, radius));
        }



    }


}