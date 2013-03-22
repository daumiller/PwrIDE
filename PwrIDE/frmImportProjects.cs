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
  public partial class frmImportProjects : Form
  {
    //========================================================================
    private List<Project> prjs;
    private SymInst       inst;
    //========================================================================
    public frmImportProjects()
    {
      InitializeComponent();
      treeSyms.Nodes.Clear();
      for (int i = 0; i < Config.Servers.Count; i++)
      {
        TreeNode serv = new TreeNode(Config.Servers[i].Alias);
        serv.ImageIndex = serv.SelectedImageIndex = 0;
        for (int c = 0; c < Config.Servers[i].Syms.Count; c++)
        {
          TreeNode sym = new TreeNode("Sym " + Config.Servers[i].Syms[c].SymDir);
          sym.ImageIndex = sym.SelectedImageIndex = 1;
          serv.Nodes.Add(sym);
        }
        serv.Expand();
        treeSyms.Nodes.Add(serv);
      }
    }
    //========================================================================
    private void treeSyms_AfterSelect(object sender, TreeViewEventArgs e)
    {
      lstProjects.Items.Clear();
      btnOkay.Enabled = false;
      btnlList.Enabled = true;
    }
    //------------------------------------------------------------------------
    private void lstProjects_ItemChecked(object sender, ItemCheckedEventArgs e)
    {
      int checks = 0;
      for(int i=0; i<lstProjects.Items.Count; i++)
        if(lstProjects.Items[i].Checked)
          checks++;

      if(checks > 0)
        btnOkay.Enabled = true;
      else
        btnOkay.Enabled = false;
    }
    //------------------------------------------------------------------------
    private void lstProjects_MouseClick(object sender, MouseEventArgs e)
    {
      if(e.Button == MouseButtons.Right)
        mnuList.Show(MousePosition);
    }
    //========================================================================
    private void frmImportProjects_KeyDown(object sender, KeyEventArgs e)
    {
      if(e.KeyCode == Keys.Escape)
        DialogResult = DialogResult.Cancel;
    }
    //========================================================================
    private void btnCancel_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
    }
    //========================================================================
    private void btnOkay_Click(object sender, EventArgs e)
    {
      //first load existing projects
      //(this was a bug, where Importing to a not-previously-connected Sym would just overwrite the existing projects file)
      inst.ProjectsLoad();

      for(int i=0; i<lstProjects.Items.Count; i++)
        if(lstProjects.Items[i].Checked)
          inst.ProjectAdd(prjs[i]);
      try
      {
        inst.ProjectsSave();
      }
      catch(Exception ex)
      {
        MessageBox.Show("Error Re-Saving Projects File After Import\nError: "+ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      Util.MainForm.Explorer.ProjectsImported(inst);
      DialogResult = DialogResult.OK;
    }
    //========================================================================
    private void btnlList_Click(object sender, EventArgs e)
    {
      if (treeSyms.SelectedNode.SelectedImageIndex != 1) //!Sym
      {
        MessageBox.Show("Please Select a Sym to List Projects");
        return;
      }

      inst = SymFromNode(treeSyms.SelectedNode);
      if(Util.TrySymConnect(inst))
      {
        try
        {
          prjs = inst.ProjectsImport();
          if(prjs == null)
          {
            MessageBox.Show("No Projects or Unable to Open Projects File", "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.None);
            return;
          }
          if (prjs.Count == 0)
          {
            MessageBox.Show("No Projects Found on Sym", "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.None);
            return;
          }
          for(int i=0; i<prjs.Count; i++)
          {
            lstProjects.Items.Add(new ListViewItem(new string[]{prjs[i].Name, prjs[i].Files.Count.ToString()}, 2));
          }
        }
        catch(Exception ex)
        {
          MessageBox.Show("Error Loading Projects File\nError: "+ex.Message, "PwrIDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
    }
    //========================================================================
    private SymInst SymFromNode(TreeNode tn)
    {
      string symDir = tn.Text.Substring(4);
      for(int i=0; i<Config.Servers.Count; i++)
      {
        if(Config.Servers[i].Alias == tn.Parent.Text)
        {
          for(int c=0; c<Config.Servers[i].Syms.Count; c++)
            if(Config.Servers[i].Syms[c].SymDir == symDir)
              return Config.Servers[i].Syms[c];
        }
      }
      throw new Exception("Project Sym Not Found in Config.Servers\nThis Shouldn't Happen");
    }
    //========================================================================
    private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
    {
      for(int i=0; i<lstProjects.Items.Count; i++)
        lstProjects.Items[i].Checked = true;
    }
    //------------------------------------------------------------------------
    private void deselectAllToolStripMenuItem_Click(object sender, EventArgs e)
    {
      for(int i=0; i<lstProjects.Items.Count; i++)
        lstProjects.Items[i].Checked = false;
    }
    //------------------------------------------------------------------------
    private void invertSelectionToolStripMenuItem_Click(object sender, EventArgs e)
    {
      for(int i=0; i<lstProjects.Items.Count; i++)
        lstProjects.Items[i].Checked = !lstProjects.Items[i].Checked;
    }
    //========================================================================
  }
}
