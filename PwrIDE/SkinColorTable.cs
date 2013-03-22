using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms.VisualStyles;
using Microsoft.Win32;
using System.IO;

namespace PwrIDE
{
    class SkinColorTable : ProfessionalColorTable
    {
        public override Color ToolStripGradientBegin  { get { return Color.FromArgb(252, 252, 255); } }
        public override Color ToolStripGradientMiddle { get { return Color.FromArgb(212, 219, 237); } }
        public override Color ToolStripGradientEnd    { get { return Color.FromArgb(225, 230, 246); } }
        public override Color ToolStripBorder         { get { return Color.FromArgb(182, 188, 204); } }

        public override Color MenuStripGradientBegin  { get { return Color.FromArgb(252, 252, 255); } }
        public override Color MenuStripGradientEnd    { get { return Color.FromArgb(225, 230, 246); } }
        public override Color MenuBorder              { get { return Color.FromArgb(182, 188, 204); } }
    }
}
