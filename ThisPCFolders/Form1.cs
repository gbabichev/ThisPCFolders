using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ThisPCFolders
{
    public partial class Form1 : Form
    {
        string W10baseName = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FolderDescriptions\";
        string W10baseName2 = @"\PropertyBag";

        string W8baseName = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\";


        /* -- Registry ID's --
        -- Windows 10 --
        Documents : {f42ee2d3-909f-4907-8871-4c22fc0bf756}
        Pictures : {0ddd015d-b06c-45d5-8c4c-f59713854639}
        Videos : {35286a68-3c57-41a1-bbb1-0eae73d76c95}
        Downloads : {7d83ee9b-2244-4e70-b1f5-5393042af1e4}
        Music : {a0c69a99-21c8-4671-8703-7934162fcf1d}
        Desktop : {B4BFCC3A-DB2C-424C-B029-7FE99A87C641}

        -- Windows 8, 8.1, Server 2012, Server 2012 R2 -- 
        Documents : {A8CDFF1C-4878-43be-B5FD-F8091C1C60D0}
        Pictures : {3ADD1653-EB32-4cb0-BBD7-DFA0ABB5ACCA}
        Videos : {A0953C92-50DC-43bf-BE83-3742FED03C9C}
        Downloads : {374DE290-123F-4565-9164-39C4925E467B}
        Music : {1CF1260C-4DD0-4ebb-811F-33C572699FDE}
        Desktop : {B4BFCC3A-DB2C-424C-B029-7FE99A87C641}
        */

        public Form1()
        {

            InitializeComponent();
        }

        public string CheckOSVer()
        {
            // Check if Win10 or Server 2012 (R2)
            var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
            string productName = (string)reg.GetValue("ProductName");
            if (productName.StartsWith("Windows 10") == true)
            {
                return "Windows 10";
            }
            if (productName.StartsWith("Windows Server 2012") == true)
            {
                return "Windows Server 2012";
            }
            return "Error";
            //return productName;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Check what OS this is, and move appropriate functions.
            if (CheckOSVer() == "Windows 10")
            {
                LoadFolderInfoWindows10();
            }
            if (CheckOSVer() == "Windows Server 2012")
            {
                LoadFolderInfoServer2012();
            }
            if (CheckOSVer() == "Error")
            {
                MessageBox.Show("Sorry, your operating system is not supported", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }
        
        public void LoadFolderInfoWindows10()
        {
            // Worker for Windows 10 & Server 2012
            RegistryKey currentWorkingKey;
            // Check if OS is 64 bit or not, and load the appropriate regkey.
            if (Environment.Is64BitOperatingSystem)
                currentWorkingKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            else
                currentWorkingKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);

            string[] registryIDs = new string[]
            {
                @"{f42ee2d3-909f-4907-8871-4c22fc0bf756}",
                @"{0ddd015d-b06c-45d5-8c4c-f59713854639}",
                @"{35286a68-3c57-41a1-bbb1-0eae73d76c95}",
                @"{7d83ee9b-2244-4e70-b1f5-5393042af1e4}",
                @"{a0c69a99-21c8-4671-8703-7934162fcf1d}",
                @"{B4BFCC3A-DB2C-424C-B029-7FE99A87C641}"
            };

            CheckBox[] boxes = new[]
            {
                docBox,
                picBox,
                vidBox,
                downBox,
                musBox,
                deskBox
            };

            for (int i = 0; i < 6; i++)
            {
                string readRegistryValue;
                // Check the regkey. If the value is blank, create it.
                try
                {
                    readRegistryValue = currentWorkingKey.OpenSubKey(W10baseName + registryIDs[i] + W10baseName2, true).GetValue("ThisPCPolicy").ToString();
                }
                catch
                {
                    currentWorkingKey.OpenSubKey(W10baseName + registryIDs[i] + W10baseName2, true).SetValue("ThisPCPolicy", "Show");
                    currentWorkingKey.Close();
                    readRegistryValue = currentWorkingKey.OpenSubKey(W10baseName + registryIDs[i] + W10baseName2, true).GetValue("ThisPCPolicy").ToString();
                }
                // If the value is Show, then check the box in the GUI
                if (readRegistryValue == "Show")
                    boxes[i].Checked = true;
                else
                    boxes[i].Checked = false;
            }
        }

        public void LoadFolderInfoServer2012()
        {
            RegistryKey currentWorkingKey;
            // Check if OS is 64 bit or not, and load the appropriate regkey setting.
            if (Environment.Is64BitOperatingSystem)
                currentWorkingKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            else
                currentWorkingKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            string[] registryIDs = new[]
            {
                @"{A8CDFF1C-4878-43be-B5FD-F8091C1C60D0}",
                @"{3ADD1653-EB32-4cb0-BBD7-DFA0ABB5ACCA}",
                @"{A0953C92-50DC-43bf-BE83-3742FED03C9C}",
                @"{374DE290-123F-4565-9164-39C4925E467B}",
                @"{1CF1260C-4DD0-4ebb-811F-33C572699FDE}",
                @"{B4BFCC3A-DB2C-424C-B029-7FE99A87C641}"
            };

            CheckBox[] boxes = new[]
            {
                docBox,
                picBox,
                vidBox,
                downBox,
                musBox,
                deskBox
            };

            for (int i = 0; i < 6; i++)
            {
                // Check the regkey. If the value is blank, create it
                RegistryKey tempKey = currentWorkingKey.OpenSubKey(W8baseName + registryIDs[i], true);
                if (tempKey == null)
                {
                    boxes[i].Checked = false;
                }
                else
                {
                    boxes[i].Checked = true;
                }
            }
        }

        // GUI Elements

        public void btn_Save_Click(object sender, EventArgs e)
        {
            RegistryKey currentWorkingKey;
            // Check if OS is 64 bit or not, and load the appropriate regkey.
            if (Environment.Is64BitOperatingSystem)
                currentWorkingKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            else
                currentWorkingKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);

            CheckBox[] boxes = new[]
            {
                docBox,
                picBox,
                vidBox,
                downBox,
                musBox,
                deskBox
            };

            if (CheckOSVer() == "Windows 10")
            {
                // Windows 10, Server 2016 Method
                string[] registryIDs = new string[]
                {
                    @"{f42ee2d3-909f-4907-8871-4c22fc0bf756}",
                    @"{0ddd015d-b06c-45d5-8c4c-f59713854639}",
                    @"{35286a68-3c57-41a1-bbb1-0eae73d76c95}",
                    @"{7d83ee9b-2244-4e70-b1f5-5393042af1e4}",
                    @"{a0c69a99-21c8-4671-8703-7934162fcf1d}",
                    @"{B4BFCC3A-DB2C-424C-B029-7FE99A87C641}"
                };
                for (int i = 0; i < 6; i++)
                {
                    if (boxes[i].Checked == true)
                    {
                        //Console.WriteLine(multiKeys[i].ToString());
                        // Write to registry that we should show this box
                        currentWorkingKey.OpenSubKey(W10baseName + registryIDs[i] + W10baseName2, true).SetValue("ThisPCPolicy", "Show");
                        currentWorkingKey.Close();
                    }
                    else
                    {
                        // Write to registry that we should hide this box
                        currentWorkingKey.OpenSubKey(W10baseName + registryIDs[i] + W10baseName2, true).SetValue("ThisPCPolicy", "Hide");
                        currentWorkingKey.Close();
                    }
                }
                // Windows 10 Contains keys from 8/8.1/Server2012(R2)
                // Therefore, just in case I will delete those values
                string[] registryIDs2 = new[]
{
                    @"{A8CDFF1C-4878-43be-B5FD-F8091C1C60D0}",
                    @"{3ADD1653-EB32-4cb0-BBD7-DFA0ABB5ACCA}",
                    @"{A0953C92-50DC-43bf-BE83-3742FED03C9C}",
                    @"{374DE290-123F-4565-9164-39C4925E467B}",
                    @"{1CF1260C-4DD0-4ebb-811F-33C572699FDE}",
                    @"{B4BFCC3A-DB2C-424C-B029-7FE99A87C641}"
                };
                for (int i = 0; i < 6; i++)
                {
                    RegistryKey tempKey = currentWorkingKey.OpenSubKey(W8baseName, true);
                    if (boxes[i].Checked == true)
                    {
                        tempKey.CreateSubKey(registryIDs2[i]);
                    }
                    else
                    {
                        // Throwing a try block in here. The way this work is to delete the key, but 
                        // what if the key wasn't present to begin with? Should be safe to do.
                        try
                        {
                            tempKey.DeleteSubKey(registryIDs2[i]);
                        }
                        catch { }
                    }
                    tempKey.Close();
                }
            }
            if (CheckOSVer() == "Windows Server 2012")
            {
                // Windows 8, Server 2012 (R2) Method
                string[] registryIDs = new[]
                {
                    @"{A8CDFF1C-4878-43be-B5FD-F8091C1C60D0}",
                    @"{3ADD1653-EB32-4cb0-BBD7-DFA0ABB5ACCA}",
                    @"{A0953C92-50DC-43bf-BE83-3742FED03C9C}",
                    @"{374DE290-123F-4565-9164-39C4925E467B}",
                    @"{1CF1260C-4DD0-4ebb-811F-33C572699FDE}",
                    @"{B4BFCC3A-DB2C-424C-B029-7FE99A87C641}"
                };
                for (int i = 0; i < 6; i++)
                {
                    RegistryKey tempKey = currentWorkingKey.OpenSubKey(W8baseName, true);
                    if (boxes[i].Checked == true)
                    {
                        tempKey.CreateSubKey(registryIDs[i]);
                    }
                    else
                    {
                        // Throwing a try block in here. The way this work is to delete the key, but 
                        // what if the key wasn't present to begin with? Should be safe to do.
                        try
                        {
                            tempKey.DeleteSubKey(registryIDs[i]);
                        }
                        catch{}
                    }
                    tempKey.Close();
                }
            }
        }

        private void btn_help_Click(object sender, EventArgs e)
        {
            Form helppageView = new helpPage();
            helppageView.ShowDialog();
        }
    }
}
