using System.ComponentModel;
using System.Windows.Forms;

namespace HecopUI_Winforms.Controls
{
    public partial class HAnimateControl : Component
    {
        public HAnimateControl()
        {

        }

        private Control tar;
        /// <summary>
        /// Gets or sets control to show animation.
        /// </summary>
        public Control TargetControl
        {
            get { return tar; }
            set
            {
                tar = value;
                if (DesignMode == false)
                    HecopUI_Winforms.Win32.User32.AnimateWindow(TargetControl.Handle, Interval, AnimateMode);
                TargetControl.Invalidate();
                //System.Drawing.Design.UITypeEditorEditStyle.
                //EnumSetEditor

            }
        }

        /// <summary>
        /// Gets or sets animate mode to show animate effect of control.
        /// </summary>

        //[SettingsBindable(true)]
        public HecopUI_Winforms.Win32.User32.AnimateWindowFlags AnimateMode { get; set; } = HecopUI_Winforms.Win32.User32.AnimateWindowFlags.AW_BLEND;

        public int Interval { get; set; } = 100;
    }
}
