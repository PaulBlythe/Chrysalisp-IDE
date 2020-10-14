using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

using ChrysaEditor.Pages;
using ChrysaEditor.Forms;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Runtime.InteropServices;

namespace ChrysaEditor
{
    public partial class Form1 : Form
    {

        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        public Form1()
        {
            InitializeComponent();

            Settings.Settings.Load();

            TreeNode root = new TreeNode("Chrysalisp");
            treeView1.Nodes.Add(root);
            if (Settings.Settings.HostPath != "")
                SetupTreeview(Settings.Settings.HostPath, root);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        #region draw mode code
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.Graphics.DrawString(this.tabControl1.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + 2, e.Bounds.Top + 4);
            e.DrawFocusRectangle();
        }
        #endregion

        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            //Looping through the controls.
            for (int i = 0; i < this.tabControl1.TabPages.Count; i++)
            {
                Rectangle r = tabControl1.GetTabRect(i);
                //Getting the position of the "x" mark.
                Rectangle closeButton = new Rectangle(r.Right - 19, r.Top + 3, 12, 12);
                if (closeButton.Contains(e.Location))
                {

                    this.tabControl1.TabPages.RemoveAt(i);

                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        #region Setup

        
        void SetupTreeview(String d, TreeNode node)
        {
            DirectoryInfo files = new DirectoryInfo(d);
            var dirs = files.EnumerateDirectories();
            foreach(DirectoryInfo f in dirs)
            {
                if (f.Name != ".git")
                {
                    TreeNode t = new TreeNode(f.Name);
                    t.Tag = null;
                    t.Name = f.Name;
                    node.Nodes.Add(t);

                    SetupTreeview(f.FullName, t);
                }
            }
            var src = files.EnumerateFiles();
            foreach (FileInfo fi in src)
            {
                switch (fi.Extension)
                {
                    case ".inc":
                    case ".vp":
                    case ".lisp":
                        {
                            TreeNode t = new TreeNode(fi.Name);
                            t.Tag = fi.FullName;
                            node.Nodes.Add(t);
                        }
                        break;

                }
                
            }
        }
        #endregion


        private void chrysalispLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                Settings.Settings.HostPath = fbd.SelectedPath;

                treeView1.Nodes.Clear();
                TreeNode root = new TreeNode("Chrysalisp");
                treeView1.Nodes.Add(root);
                Settings.Settings.Dirty = true;

                SetupTreeview(Settings.Settings.HostPath, root);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Settings.Save();
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            TreeNode t = treeView1.SelectedNode;
            if (t!=null)
            {
                if (t.Tag != null)
                {
                    String s = t.Tag as String;

                    CodePage cp = new CodePage(s, tabControl1.ClientSize);
                    tabControl1.TabPages.Add(cp.hostpage);
                    tabControl1.Refresh();

                    Application.DoEvents();
                }
            }
        }

        /// <summary>
        /// New file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Save file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SaveAll();
        }

        /// <summary>
        /// Run chyrsalisp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            String cmd = Path.Combine(Settings.Settings.HostPath, "run.bat");
            RunProcessAsync(cmd);
        }

        /// <summary>
        /// Execute and wait for result
        /// </summary>
        /// <param name="command"></param>
        static void ExecuteCommand(string command)
        {

            ProcessStartInfo processInfo;
            Process process;

            processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            processInfo.CreateNoWindow = false;
            processInfo.UseShellExecute = false;
            processInfo.WorkingDirectory = Settings.Settings.HostPath;
            // *** Redirect the output ***
            processInfo.RedirectStandardError = false;
            processInfo.RedirectStandardOutput = false;

            process = Process.Start(processInfo);
            process.WaitForExit();

           
            process.Close();
        }


        /// <summary>
        /// Eexcute batch file async
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        static Task<int> RunProcessAsync(string command)
        {
            var tcs = new TaskCompletionSource<int>();

            var process = new Process
            {
                StartInfo = { FileName = "cmd.exe", Arguments = "/c " + command, WorkingDirectory = Settings.Settings.HostPath },
                EnableRaisingEvents = true
            };

            process.Exited += (sender, args) =>
            {
                tcs.SetResult(process.ExitCode);
                process.Dispose();
            };

            process.Start();

            return tcs.Task;
        }

        /// <summary>
        /// Run terminal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            String cmd = Path.Combine(Settings.Settings.HostPath, "run_tui.bat");
            RunProcessAsync(cmd);
        }

        #region Keyboard faking
        private void SendMakeAll(Process p)
        {
            var pointer = p.MainWindowHandle;

            SetForegroundWindow(pointer);
            SendKeys.Send("m");
            SendKeys.Send("a");
            SendKeys.Send("k");
            SendKeys.Send("e");
            SendKeys.Send(" ");
            SendKeys.Send("a");
            SendKeys.Send("l");
            SendKeys.Send("l");
            SendKeys.Send("\n");
        }
        private void SendMake(Process p)
        {
            var pointer = p.MainWindowHandle;

            SetForegroundWindow(pointer);
            SendKeys.Send("m");
            SendKeys.Send("a");
            SendKeys.Send("k");
            SendKeys.Send("e");
            SendKeys.Send("\n");
        }
        private void SendMakeDocs(Process p)
        {
            var pointer = p.MainWindowHandle;

            SetForegroundWindow(pointer);
            SendKeys.Send("m");
            SendKeys.Send("a");
            SendKeys.Send("k");
            SendKeys.Send("e");
            SendKeys.Send(" ");
            SendKeys.Send("d");
            SendKeys.Send("o");
            SendKeys.Send("c");
            SendKeys.Send("s");
            SendKeys.Send("\n");
        }
        private void SendMakeBoot(Process p)
        {
            var pointer = p.MainWindowHandle;

            SetForegroundWindow(pointer);
            SendKeys.Send("m");
            SendKeys.Send("a");
            SendKeys.Send("k");
            SendKeys.Send("e");
            SendKeys.Send(" ");
            SendKeys.Send("b");
            SendKeys.Send("o");
            SendKeys.Send("o");
            SendKeys.Send("t");
            SendKeys.Send("\n");
        }
        private void SendMakePlatforms(Process p)
        {
            var pointer = p.MainWindowHandle;

            SetForegroundWindow(pointer);
            SendKeys.Send("m");
            SendKeys.Send("a");
            SendKeys.Send("k");
            SendKeys.Send("e");
            SendKeys.Send(" ");
            SendKeys.Send("p");
            SendKeys.Send("l");
            SendKeys.Send("a");
            SendKeys.Send("t");
            SendKeys.Send("f");
            SendKeys.Send("o");
            SendKeys.Send("r");
            SendKeys.Send("m");
            SendKeys.Send("s");
            SendKeys.Send("\n");
        }
        private void SendMakeSyms(Process p)
        {
            var pointer = p.MainWindowHandle;

            SetForegroundWindow(pointer);
            SendKeys.Send("m");
            SendKeys.Send("a");
            SendKeys.Send("k");
            SendKeys.Send("e");
            SendKeys.Send(" ");
            SendKeys.Send("s");
            SendKeys.Send("y");
            SendKeys.Send("m");
            SendKeys.Send("s");
            SendKeys.Send("\n");
        }
        private void SendMakeAllPB(Process p)
        {
            var pointer = p.MainWindowHandle;

            SetForegroundWindow(pointer);
            SendKeys.Send("m");
            SendKeys.Send("a");
            SendKeys.Send("k");
            SendKeys.Send("e");
            SendKeys.Send(" ");
            SendKeys.Send("a");
            SendKeys.Send("l");
            SendKeys.Send("l");
            SendKeys.Send(" ");
            SendKeys.Send("p");
            SendKeys.Send("l");
            SendKeys.Send("a");
            SendKeys.Send("t");
            SendKeys.Send("f");
            SendKeys.Send("o");
            SendKeys.Send("r");
            SendKeys.Send("m");
            SendKeys.Send("s");
            SendKeys.Send(" ");
            SendKeys.Send("b");
            SendKeys.Send("o");
            SendKeys.Send("o");
            SendKeys.Send("t");
            SendKeys.Send("\n");
        }
        #endregion

        #region Help files
        /// <summary>
        /// Commands help
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void commandsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpPage hp = new HelpPage(@"docs/COMMANDS.md", tabControl1.ClientSize);
            tabControl1.TabPages.Add(hp.hostpage);
            tabControl1.Refresh();

            Application.DoEvents();
        }

        private void commsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpPage hp = new HelpPage(@"docs/COMMS.md", tabControl1.ClientSize);
            tabControl1.TabPages.Add(hp.hostpage);
            tabControl1.Refresh();

            Application.DoEvents();
        }

        private void readmeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpPage hp = new HelpPage(@"README.md", tabControl1.ClientSize);
            tabControl1.TabPages.Add(hp.hostpage);
            tabControl1.Refresh();

            Application.DoEvents();
        }

        private void diaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpPage hp = new HelpPage(@"docs/DIARY.md", tabControl1.ClientSize);
            tabControl1.TabPages.Add(hp.hostpage);
            tabControl1.Refresh();

            Application.DoEvents();
        }

        private void environmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpPage hp = new HelpPage(@"docs/ENVIRONMENT.md", tabControl1.ClientSize);
            tabControl1.TabPages.Add(hp.hostpage);
            tabControl1.Refresh();

            Application.DoEvents();
        }

        private void functionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpPage hp = new HelpPage(@"docs/FUNCTIONS.md", tabControl1.ClientSize);
            tabControl1.TabPages.Add(hp.hostpage);
            tabControl1.Refresh();

            Application.DoEvents();
        }

        private void introToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpPage hp = new HelpPage(@"docs/INTRO.md", tabControl1.ClientSize);
            tabControl1.TabPages.Add(hp.hostpage);
            tabControl1.Refresh();

            Application.DoEvents();
        }

        private void iterationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpPage hp = new HelpPage(@"docs/ITERATION.md", tabControl1.ClientSize);
            tabControl1.TabPages.Add(hp.hostpage);
            tabControl1.Refresh();

            Application.DoEvents();
        }

        private void lispToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            HelpPage hp = new HelpPage(@"docs/LISP.md", tabControl1.ClientSize);
            tabControl1.TabPages.Add(hp.hostpage);
            tabControl1.Refresh();

            Application.DoEvents();
        }

        private void structureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpPage hp = new HelpPage(@"docs/STRUCTURE.md", tabControl1.ClientSize);
            tabControl1.TabPages.Add(hp.hostpage);
            tabControl1.Refresh();

            Application.DoEvents();
        }

        private void syntaxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpPage hp = new HelpPage(@"docs/SYNTAX.md", tabControl1.ClientSize);
            tabControl1.TabPages.Add(hp.hostpage);
            tabControl1.Refresh();

            Application.DoEvents();
        }

        private void taosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpPage hp = new HelpPage(@"docs/TAOS.md", tabControl1.ClientSize);
            tabControl1.TabPages.Add(hp.hostpage);
            tabControl1.Refresh();

            Application.DoEvents();
        }

        private void terminalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpPage hp = new HelpPage(@"docs/TERMINAL.md", tabControl1.ClientSize);
            tabControl1.TabPages.Add(hp.hostpage);
            tabControl1.Refresh();

            Application.DoEvents();
        }

        private void vMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpPage hp = new HelpPage(@"docs/VM.md", tabControl1.ClientSize);
            tabControl1.TabPages.Add(hp.hostpage);
            tabControl1.Refresh();

            Application.DoEvents();
        }

        private void tODOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpPage hp = new HelpPage(@"docs/TODO.md", tabControl1.ClientSize);
            tabControl1.TabPages.Add(hp.hostpage);
            tabControl1.Refresh();

            Application.DoEvents();
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String cmd = Path.Combine(Settings.Settings.HostPath, "run.bat");
            ExecuteCommand(cmd);
        }

        private void lispToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpPage hp = new HelpPage("STATUS.md", tabControl1.ClientSize);
            tabControl1.TabPages.Add(hp.hostpage);
            tabControl1.Refresh();

            Application.DoEvents();
        }
        /// <summary>
        /// Classses help file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void classesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpPage hp = new HelpPage(@"docs\CLASSES.md", tabControl1.ClientSize);
            tabControl1.TabPages.Add(hp.hostpage);
            tabControl1.Refresh();

            Application.DoEvents();
        }

        /// <summary>
        /// Help file Assigment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void assigmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpPage hp = new HelpPage(@"docs\ASSIGNMENT.md", tabControl1.ClientSize);
            tabControl1.TabPages.Add(hp.hostpage);
            tabControl1.Refresh();

            Application.DoEvents();
        }

        #endregion

        /// <summary>
        /// New application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewApplication na = new NewApplication();
            if (na.ShowDialog() == DialogResult.OK)
            {
                String dir = Settings.Settings.HostPath + @"\apps\" + na.Result;
                String file = Path.Combine(dir, "app.lisp");
                bool valid = true;
                bool addtopupa = true;
                if (Directory.Exists(dir))
                {
                    if (File.Exists(file))
                    {
                        OverwriteConfirmation oc = new OverwriteConfirmation();
                        if (oc.ShowDialog() == DialogResult.Cancel)
                        {
                            valid = false;
                        }
                        addtopupa = false;
                    }

                }
                if (valid)
                {
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    
                    try
                    {
                        System.IO.TextWriter writeFile = new StreamWriter(file);
                        writeFile.Write(String.Format(Settings.Settings.BaseApp, na.Result));
                        writeFile.Flush();
                        writeFile.Close();
                        writeFile = null;

                        CodePage cp = new CodePage(file, tabControl1.ClientSize);
                        tabControl1.TabPages.Add(cp.hostpage);
                        tabControl1.Refresh();

                        if (addtopupa)
                        {
                            String pupa = @"C:\GitHub\ChrysaLisp\apps\login\Guest\pupa.inc";
                            String fp = File.ReadAllText(pupa);
                            fp = fp.Replace("\"whiteboard\"", "\"whiteboard\" \"" + na.Result + "\"");
                            writeFile = new StreamWriter(pupa);
                            writeFile.Write(fp);
                            writeFile.Flush();
                            writeFile.Close();
                            writeFile = null;
                        }
                        treeView1.Nodes.Clear();
                        TreeNode root = new TreeNode("Chrysalisp");
                        treeView1.Nodes.Add(root);
                        SetupTreeview(Settings.Settings.HostPath, root);
                        treeView1.Nodes.Find("apps", true)[0].Expand();
                        root.Expand();
                        treeView1.Invalidate();
                        Application.DoEvents();
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show("Failed to create new application " + ex.ToString());
                    }
                }
            }
        }

        #region Make commands
        /// <summary>
        /// Make command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            SaveAll();

            ProcessStartInfo processInfo;
            Process process;

            processInfo = new ProcessStartInfo("cmd.exe", "/c " + "run_tui.bat");
            processInfo.CreateNoWindow = false;
            processInfo.UseShellExecute = false;
            processInfo.WorkingDirectory = Settings.Settings.HostPath;
            processInfo.RedirectStandardError = false;
            processInfo.RedirectStandardOutput = false;
            processInfo.RedirectStandardInput = false;

            process = Process.Start(processInfo);

            Thread.Sleep(1000);
            SendMake(process);

            process.WaitForExit();
            process.Close();
        }

        /// <summary>
        /// Make docs 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            SaveAll();
            ProcessStartInfo processInfo;
            Process process;

            processInfo = new ProcessStartInfo("cmd.exe", "/c " + "run_tui.bat");
            processInfo.CreateNoWindow = false;
            processInfo.UseShellExecute = false;
            processInfo.WorkingDirectory = Settings.Settings.HostPath;
            processInfo.RedirectStandardError = false;
            processInfo.RedirectStandardOutput = false;
            processInfo.RedirectStandardInput = false;

            process = Process.Start(processInfo);

            Thread.Sleep(1000);
            SendMakeDocs(process);

            process.WaitForExit();
            process.Close();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            SaveAll();

            ProcessStartInfo processInfo;
            Process process;

            processInfo = new ProcessStartInfo("cmd.exe", "/c " + "run_tui.bat");
            processInfo.CreateNoWindow = false;
            processInfo.UseShellExecute = false;
            processInfo.WorkingDirectory = Settings.Settings.HostPath;
            processInfo.RedirectStandardError = false;
            processInfo.RedirectStandardOutput = false;
            processInfo.RedirectStandardInput = false;

            process = Process.Start(processInfo);

            Thread.Sleep(1000);
            SendMakeBoot(process);

            process.WaitForExit();
            process.Close();
        }

        /// <summary>
        /// Build all
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            SaveAll();
            ProcessStartInfo processInfo;
            Process process;

            processInfo = new ProcessStartInfo("cmd.exe", "/c " + "run_tui.bat");
            processInfo.CreateNoWindow = false;
            processInfo.UseShellExecute = false;
            processInfo.WorkingDirectory = Settings.Settings.HostPath;
            processInfo.RedirectStandardError = false;
            processInfo.RedirectStandardOutput = false;
            processInfo.RedirectStandardInput = false;

            process = Process.Start(processInfo);

            Thread.Sleep(1000);
            SendMakeAll(process);

            process.WaitForExit();
            process.Close();
        }
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            SaveAll();
            ProcessStartInfo processInfo;
            Process process;

            processInfo = new ProcessStartInfo("cmd.exe", "/c " + "run_tui.bat");
            processInfo.CreateNoWindow = false;
            processInfo.UseShellExecute = false;
            processInfo.WorkingDirectory = Settings.Settings.HostPath;
            processInfo.RedirectStandardError = false;
            processInfo.RedirectStandardOutput = false;
            processInfo.RedirectStandardInput = false;

            process = Process.Start(processInfo);

            Thread.Sleep(1000);
            SendMakePlatforms(process);

            process.WaitForExit();
            process.Close();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            SaveAll();
            ProcessStartInfo processInfo;
            Process process;

            processInfo = new ProcessStartInfo("cmd.exe", "/c " + "run_tui.bat");
            processInfo.CreateNoWindow = false;
            processInfo.UseShellExecute = false;
            processInfo.WorkingDirectory = Settings.Settings.HostPath;
            processInfo.RedirectStandardError = false;
            processInfo.RedirectStandardOutput = false;
            processInfo.RedirectStandardInput = false;

            process = Process.Start(processInfo);

            Thread.Sleep(1000);
            SendMakeSyms(process);

            process.WaitForExit();
            process.Close();
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            SaveAll();
            ProcessStartInfo processInfo;
            Process process;

            processInfo = new ProcessStartInfo("cmd.exe", "/c " + "run_tui.bat");
            processInfo.CreateNoWindow = false;
            processInfo.UseShellExecute = false;
            processInfo.WorkingDirectory = Settings.Settings.HostPath;
            processInfo.RedirectStandardError = false;
            processInfo.RedirectStandardOutput = false;
            processInfo.RedirectStandardInput = false;

            process = Process.Start(processInfo);

            Thread.Sleep(1000);
            SendMakeAllPB(process);

            process.WaitForExit();
            process.Close();
        }

        #endregion


        #region Helpers
        void SaveAll()
        {
            foreach (TabPage page in tabControl1.TabPages )
            {
                Page p = (Page)page.Tag;
                if (p.Dirty)
                {
                    try
                    {
                        System.IO.TextWriter writeFile = new StreamWriter(p.FileName);
                        writeFile.Write(p.TextArea.Text);
                        writeFile.Flush();
                        writeFile.Close();
                        writeFile = null;
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }


        #endregion

       
    }
}
