using System;
using System.Windows.Forms;
using SuperheroDatabase.UI;

namespace SuperheroDatabase
{
    /// <summary>
    /// Main entry point for the Superhero Database application
    /// PRG2782 Project 2025 - One Kick Heroes Academy
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SuperheroesForm());
        }
    }
}
