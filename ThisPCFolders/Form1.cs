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
        RegistryKey documentsKey;
        RegistryKey picturesKey;
        RegistryKey videosKey;
        RegistryKey downloadsKey;
        RegistryKey musicKey;
        RegistryKey desktopKey;

        string baseName = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FolderDescriptions\";
        string baseName2 = @"\PropertyBag";

        string documentsID = @"{f42ee2d3-909f-4907-8871-4c22fc0bf756}";
        string picturesID = @"{0ddd015d-b06c-45d5-8c4c-f59713854639}";
        string videosID = @"{35286a68-3c57-41a1-bbb1-0eae73d76c95}";
        string downloadsID = @"{7d83ee9b-2244-4e70-b1f5-5393042af1e4}";
        string musicID = @"{a0c69a99-21c8-4671-8703-7934162fcf1d}";
        string desktopID = @"{B4BFCC3A-DB2C-424C-B029-7FE99A87C641}";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Check what OS this is, and move appropriate functions.

            if (Environment.Is64BitOperatingSystem)
            {
                documentsKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                picturesKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                videosKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                downloadsKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                musicKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                desktopKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            }
            else
            {
                documentsKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
                picturesKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
                videosKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
                downloadsKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
                musicKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
                desktopKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            }

            string docVal = documentsKey.OpenSubKey(baseName+documentsID+baseName2, true).GetValue("ThisPCPolicy").ToString();
            string picVal = picturesKey.OpenSubKey(baseName + picturesID + baseName2, true).GetValue("ThisPCPolicy").ToString();
            string vidVal = videosKey.OpenSubKey(baseName + videosID + baseName2, true).GetValue("ThisPCPolicy").ToString();
            string dowVal = downloadsKey.OpenSubKey(baseName + downloadsID + baseName2, true).GetValue("ThisPCPolicy").ToString();
            string musVal = musicKey.OpenSubKey(baseName + musicID + baseName2, true).GetValue("ThisPCPolicy").ToString();
            string desVal;
            try
            {
                desVal = desktopKey.OpenSubKey(baseName + desktopID + baseName2, true).GetValue("ThisPCPolicy").ToString();
            }
            catch
            {
                Console.WriteLine("Key doesn't exist... Creating!");
                desktopKey.OpenSubKey(baseName + desktopID + baseName2, true).SetValue("ThisPCPolicy", "Show");
                desktopKey.Close();
                desVal = desktopKey.OpenSubKey(baseName + desktopID + baseName2, true).GetValue("ThisPCPolicy").ToString();
            }

            String[] thisData = new[]
            {
                docVal,
                picVal,
                vidVal,
                dowVal,
                musVal,
                desVal
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
                if (thisData[i] == "Show")
                {
                    boxes[i].Checked = true;
                }
                else
                {
                    boxes[i].Checked = false;
                }
            }
        }

        public class SpaceSavingClass
        {
            public CheckBox checkBox { get; set; }
            public RegistryKey regKey { get; set; }
            public String stringInfo { get; set; }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            CheckBox[] boxes = new[]
            {
                docBox,
                picBox,
                vidBox,
                downBox,
                musBox,
                deskBox
            };
            RegistryKey[] multiKeys = new[]
            {
                documentsKey,
                picturesKey,
                videosKey,
                downloadsKey,
                musicKey,
                desktopKey
            };
            String[] finalArray = new[]
            {
                documentsID,
                picturesID,
                videosID,
                downloadsID,
                musicID,
                desktopID
            };

            for(int i = 0; i < 6; i++)
            {
                if (boxes[i].Checked == true)
                {
                    // Write to registry that we should show this box
                    multiKeys[i].OpenSubKey(baseName + finalArray[i] + baseName2, true).SetValue("ThisPCPolicy", "Show");
                    multiKeys[i].Close();
                }
                else
                {
                    // Write to registry that we should hide this box
                    multiKeys[i].OpenSubKey(baseName + finalArray[i] + baseName2, true).SetValue("ThisPCPolicy", "Hide");
                    multiKeys[i].Close();
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
