﻿

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HecopUI_Winforms.SuperNotifyIcon
{
    public partial class SuperNotifyIcon : System.ComponentModel.Component
    {
        public NotifyIcon NotifyIcon { get; set; }

        private FormDrop _formDrop;


        private Animation Animation { get; set; }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool DestroyIcon(IntPtr hIcon);

        public SuperNotifyIcon()
        {
            NotifyIcon = new NotifyIcon();
            Animation = new Animation() { _parent = this };
        }

        private bool disposed = false;

        ~SuperNotifyIcon()
        {
            Dispose(false);
        }

        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // The finalise process no longer needs to be run for this
        }

        protected virtual new void Dispose(bool disposeManagedResources)
        {
            if (!disposed)
            {
                if (disposeManagedResources)
                {
                    try
                    {
                        NotifyIcon.Dispose();
                        _formDrop.Dispose();
                    }
                    catch { }
                }
                disposed = true;
            }
        }
    }
}
