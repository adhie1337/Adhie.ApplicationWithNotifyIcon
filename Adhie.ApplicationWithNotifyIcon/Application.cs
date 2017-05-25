namespace Adhie.ApplicationWithNotifyIcon
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    
    /// <summary>
    /// An <see cref="ApplicationContext"/> type that adds a <see cref="NotifyIcon"/> functionality.
    /// </summary>
    /// <typeparam name="TForm">The type of the main <see cref="Form"/> of the application.</typeparam>
    public class Application<TForm> : ApplicationContext where TForm : Form
    {
        private readonly Func<TForm> _formFactory;
        private readonly NotifyIcon _notifyIcon;
        private readonly ContextMenuStrip _contextMenu;
        private readonly ToolStripItem _toggle;
        private readonly ToolStripItem _exit;

        private TForm _mainWindow;
        
        private TForm MainWindow
        {
            get
            {
                if (this._mainWindow == null)
                {
                    this._mainWindow = _formFactory();
                    this._mainWindow.FormClosed += MainWindow_FormClosed;
                }

                return this._mainWindow;
            }
        }

        internal Application(Func<TForm> formFactory, string title, Icon icon)
            : base()
        {
            this._formFactory = formFactory;
            this._toggle = new ToolStripMenuItem();
            this._toggle.Size = new Size(220, 22);
            this._toggle.Text = "Toggle " + title;
            this._toggle.Click += new EventHandler(this.Toggle_Click);

            this._exit = new ToolStripMenuItem();
            this._exit.Size = new Size(220, 22);
            this._exit.Text = "Exit";
            this._exit.Click += new EventHandler(this.Exit_Click);

            this._contextMenu = new ContextMenuStrip();
            this._contextMenu.Items.AddRange(new ToolStripItem[] {
                this._toggle,
                this._exit});
            this._contextMenu.Size = new Size(221, 48);

            this._notifyIcon = new NotifyIcon();
            this._notifyIcon.ContextMenuStrip = this._contextMenu;
            this._notifyIcon.Icon = icon;
            this._notifyIcon.Text = title;
            this._notifyIcon.Visible = true;
            this._notifyIcon.MouseDoubleClick += new MouseEventHandler(this.NotifyIcon_MouseDoubleClick);
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Toggle_Click(sender, e);
        }

        private void Toggle_Click(object sender, EventArgs e)
        {
            if (this.MainWindow.WindowState == FormWindowState.Minimized)
            {
                this.MainWindow.Show();
                this.MainWindow.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.MainWindow.WindowState = FormWindowState.Minimized;
                this.MainWindow.Hide();
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            if (this._mainWindow != null)
            {
                this.MainWindow.Close();
            }

            this._notifyIcon.Dispose();
            this.ExitThread();
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            this._notifyIcon.Dispose();
            this.ExitThread();
        }
    }
}
