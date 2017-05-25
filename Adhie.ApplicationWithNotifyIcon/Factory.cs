using System;
using System.Drawing;
using System.Windows.Forms;

namespace Adhie.ApplicationWithNotifyIcon
{
    /// <summary>
    /// A factory class that creates <see cref="Application{TForm}"/> instances.
    /// </summary>
    public static class ApplicationFactory
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Application{TForm}"/> class. Sets up the necessary controls,
        /// like the notify icon and it's context menu, with the toggle and exit menu items.
        /// </summary>
        /// <typeparam name="TForm">The type of the main form.</typeparam>
        /// <param name="factory">Factory function that creates the form instance.</param>
        /// <param name="title">Title of the application.</param>
        /// <param name="icon">Icon of the application.</param>
        /// <returns>The created instance of the application.</returns>
        public static Application<TForm> Create<TForm>(Func<TForm> factory, string title, Icon icon) where TForm : Form
        {
            return new Application<TForm>(factory, title, icon);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Application{TForm}"/> class. Sets up the necessary controls,
        /// like the notify icon and it's context menu, with the toggle and exit menu items.
        /// </summary>
        /// <typeparam name="TForm">The type of the main form.</typeparam>
        /// <param name="title">Title of the application.</param>
        /// <param name="icon">Icon of the application.</param>
        /// <returns>The created instance of the application.</returns>
        public static Application<TForm> Create<TForm>(string title, Icon icon) where TForm : Form, new()
        {
            return new Application<TForm>(() => new TForm(), title, icon);
        }
    }
}
