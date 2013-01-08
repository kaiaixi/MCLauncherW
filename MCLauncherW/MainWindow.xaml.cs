﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace MCLauncherW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private String javaVM;
        private String playerName;
        private String playerPswd;
        private String mcPath;
        private bool x64mode;
        private long memory;
        private bool pswdEnabled;

        public MainWindow()
        {
            InitializeComponent();
            refreshSettings();
            playerNameTextField.Text = playerName;
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.playerName = playerNameTextField.Text;
            Properties.Settings.Default.Save();

            Process p = new Process();
            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = javaVM;
            String memString = "-Xms" + memory + "m";
            p.StartInfo.Arguments = memString;
            String path = mcPath.Substring(0, mcPath.Length - 13);
            String cp = " -cp \"" + path + "minecraft.jar;" + path + "lwjgl.jar;" + path + "lwjgl_util.jar;" + path + "jinput.jar\"";
            p.StartInfo.Arguments += cp;
            String dcp = " -Djava.library.path=\"" + path + "natives\"";
            p.StartInfo.Arguments += dcp;
            p.StartInfo.Arguments += " net.minecraft.client.Minecraft";
            p.StartInfo.Arguments += " " + playerNameTextField.Text;

            try
            {
                p.Start();
            }
            catch (Exception)
            {

            }
            // Do not wait for the child process to exit before
            // reading to the end of its redirected stream.
            // p.WaitForExit();
            // Read the output stream first and then wait.
            //string output = p.StandardOutput.ReadToEnd();
            Application.Current.Shutdown();
        }

        private void preference_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Preference prefer = new Preference();
            prefer.setParent(this);
            prefer.Show();
        }

        public void refreshSettings()
        {
            javaVM = Properties.Settings.Default.javaVM;
            playerName = Properties.Settings.Default.playerName;
            playerPswd = Properties.Settings.Default.playerPswd;
            x64mode = Properties.Settings.Default.x64mode;
            memory = Properties.Settings.Default.memory;
            pswdEnabled = Properties.Settings.Default.pswdEnabled;
            mcPath = Properties.Settings.Default.mcPath;

        }
    }
}
