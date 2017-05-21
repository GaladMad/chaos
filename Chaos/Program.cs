﻿using System;
using System.Windows.Forms;
using Chaos.Misc;
using Chaos.Properties;

namespace Chaos
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Cursor.Current = CreateCursorFromStream.CreateCursor(Resources.Normal);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormStart());
//<<<<<<< master
            // Application.Run(new Form1());
//=======
            // Application.Run(new GameForm());
//>>>>>>> master
        }
    }
}