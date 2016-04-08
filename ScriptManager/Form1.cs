using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using ScriptManager.Models;
using Newtonsoft.Json;
using MaterialSkin.Controls;
using MaterialSkin;
using System.Reflection;

namespace Scriptmanager
{
    public partial class Form1 : MaterialForm
    {
        string apiSearchChampion = "http://www.bol-tools.com/api/search/champion/";
        string currentPath;
        string bolPath;
        Version version;

        public Form1()
        {
            InitializeComponent();

            // TODO: Merge all DLL into the exe.
            // Choose ILMerge or Costura.Fody ( https://github.com/Fody/Costura )
            // http://stackoverflow.com/questions/10137937/merge-dll-into-exe
            // http://stackoverflow.com/questions/12307602/understanding-ilmerge-how-to-pack-an-executable-with-all-its-associated-dlls
            // http://stackoverflow.com/questions/25578362/combine-net-external-dlls-to-a-single-executable-file

            // Skin to get a non instant eyes-bleeding GUI
            // https://github.com/IgnaceMaes/MaterialSkin
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // get app path
            currentPath = Path.GetDirectoryName(Application.ExecutablePath);

            // check version for auto-update
            version = Assembly.GetEntryAssembly().GetName().Version;

            // check app properties
            bolPath = ScriptManager.Properties.Settings.Default["bolFolderPath"].ToString();
            if(bolPath.Length < 1)
            {
                DialogResult result = openFileBol.ShowDialog();
                if (result == DialogResult.OK)
                {
                    bolPath = Path.GetDirectoryName(openFileBol.FileName);
                    ScriptManager.Properties.Settings.Default["bolFolderPath"] = bolPath;
                    ScriptManager.Properties.Settings.Default.Save();
                }
            }

            // Go get all campions
            var championsDataSource = new List<ComboBoxItem>();
            championsDataSource.Add(new ComboBoxItem("Select a champion", "-1"));

            championsDataSource.Add(new ComboBoxItem("Garen", "MonkeyKing"));
            championsDataSource.Add(new ComboBoxItem("Taric", "Taric"));
            championsDataSource.Add(new ComboBoxItem("Wukong", "Garen"));

            
            // set display & value, readonly
            this.cboChampionsList.DataSource = championsDataSource;
            this.cboChampionsList.DisplayMember = "Name";
            this.cboChampionsList.ValueMember = "Value";
        }

        private void cboChampionsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedChampionkey = this.cboChampionsList.SelectedValue;
            var url = apiSearchChampion + selectedChampionkey;

            var scriptsList = getScriptsListFromurl(url);
            foreach (var script in scriptsList)
            {
                grid_champions.Rows.Add(script.Title, script.Author, script.ForumUrl, "Download", script.UpdateUrl);
            }
        }

        private void grid_champions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int cellIndex = e.ColumnIndex;
            // cell[3] is download button
            if (cellIndex == 3)
            {
                // TODO: Fix link (doesn't open browser to forum link)
                // better get good script else drama will cum
                DataGridViewRow row = grid_champions.Rows[e.RowIndex];
                string downloadUrl = row.Cells[4].Value.ToString();
                string scriptTitle = row.Cells[0].Value.ToString();

                using( var client = new WebClient())
                {
                    client.DownloadFile(downloadUrl, scriptTitle+".lua");
                    postDownload(scriptTitle + ".lua");
                }

                Console.WriteLine(downloadUrl);
            }
        }

        #region Private Custom Methods

        // after download, some check and move file to Script/ folder
        private void postDownload(string scriptFileName)
        {
            if (File.Exists(scriptFileName))
            {
                // which file better search? ("BoL Studio.exe" + "agent.dll"?)
                string bolDllPath = Path.GetFullPath(Path.Combine(Application.StartupPath, @"../")) + "agent.dll";
                if (File.Exists(bolDllPath))
                {
                    // we got BoL base folder
                    string bolScriptsPath = Path.GetFullPath(Path.Combine(bolDllPath, @"/Scripts/"));
                    File.Move(Path.GetFullPath(scriptFileName), bolScriptsPath);
                }
                else
                {
                    // TODO
                    // Error, but better search or ask the dir
                    MessageBox.Show("Please, put this application in a folder, which need to be in your BoL folder.", "heeey :(] ");
                    
                }
            }
            else
            {
                MessageBox.Show("It seems the downloaded file ran aways :(", "Damn it !!");
            }
        }

        private List<Script> getScriptsListFromurl(string url)
        {
            var scriptList = new List<Script>();

            using (WebClient wc = new WebClient())
            {
                var jsonresult = wc.DownloadString(url);
                dynamic stuff = JsonConvert.DeserializeObject(jsonresult);

                foreach (var script in stuff)
                {
                    // title, author, forum, download
                    Script scriptObject = JsonConvert.DeserializeObject<Script>(script.ToString());
                    scriptList.Add(scriptObject);
                }
            }
            return scriptList;
        }

        private List<Script> getChampionsListFromurl(string url)
        {
            var championList = new List<Script>();

            using (WebClient wc = new WebClient())
            {
                var jsonresult = wc.DownloadString(url);
                dynamic stuff = JsonConvert.DeserializeObject(jsonresult);

                foreach (var champ in stuff)
                {
                    // title, author, forum, download
                    Script scriptObject = JsonConvert.DeserializeObject<Champion>(champ.ToString());
                    championList.Add(scriptObject);
                }
            }
            return championList;
        }

        #endregion
    }
}
