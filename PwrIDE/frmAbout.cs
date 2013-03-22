using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PwrIDE
{
  public partial class frmAbout : Form
  {
    //========================================================================
    public frmAbout()
    {
      InitializeComponent();
    }
    //========================================================================
    private void lnkPwrPlus_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start("http://www.PwrIDE.org");
    }
    //------------------------------------------------------------------------
    private void lnkSourceForge_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start("http://www.sourceforge.net");
    }
    //------------------------------------------------------------------------
    private void lnkSharpDevelop_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start("http://www.icsharpcode.net");
    }
    //------------------------------------------------------------------------
    private void lnkWeifenLuo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start("http://sf.net/projects/dockpanelsuite");
    }
    //------------------------------------------------------------------------
    private void lnkMarkJames_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start("http://www.famfamfam.com");
    }
    //------------------------------------------------------------------------
    private void lnkHaikuInc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start("http://www.haiku-inc.org");
    }
    //========================================================================
  }
}
