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
using SharpConfig;
using System.Diagnostics;


namespace Scriptmanager
{
    public partial class Form1 : MaterialForm
    {
        const string INIFILE = "conf.ini";
        const string BOLNAME = "BoL Studio.exe";
        const string DLLNAME = "agent.dll";

        string apiSearchChampion = "http://www.bol-tools.com/api/search/champion/";
        string currentPath;
        string bolPath;
        Version version;

        bool debug = true;
        bool moveScript = true;

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
            tbLogs.Clear();

            writeLog("Loaded on " + DateTime.Now.ToShortDateString());

            // get app path
            currentPath = Path.GetDirectoryName(Application.ExecutablePath);
            writeLog("App path: " + currentPath);

            // check version for auto-update
            version = Assembly.GetEntryAssembly().GetName().Version;
            writeLog("App version: " + version);

            // check settings
            debug = cbDebug.Checked;
            
            // conf file
            // https://github.com/cemdervis/SharpConfig/blob/master/Example/Program.cs

            #region Start and load ini file

            if (File.Exists(INIFILE))
            {
                // test purpose
                writeLog("Found ini file");
                Configuration conf = Configuration.LoadFromFile(INIFILE);
                bolPath = conf["Path"]["bolPath"].StringValue;
                if ((File.Exists(bolPath + "/"+ BOLNAME) && File.Exists(bolPath + "/"+ DLLNAME))|| File.Exists(bolPath + "/agent.txt"))
                {
                    // ready to go
                    writeLog("Found files, seems we are ready.");
                }
                else
                {
                    writeLog("BoL Studio or agent.dll not found.");
                    MessageBox.Show("Impossible to find folder. Error N07F01D3R.\n Application will now exit.", "An error occured");
                    Application.Exit();
                }
            }
            else
            {
                // ask bolPath
                writeLog("New start, searching BoL folder");

                openFileBol.Filter = "BoL Studio|*.exe|DLL Agent|*.dll|All file|*.*";
                openFileBol.Title = "Please, select your BoL Studio exe or the agent.dll";

                DialogResult result = openFileBol.ShowDialog();
                if(result == DialogResult.OK)
                {
                    bolPath = Path.GetDirectoryName(openFileBol.FileName);

                    if((File.Exists(bolPath + "/"+BOLNAME) && File.Exists(bolPath + "/"+DLLNAME)) || File.Exists(bolPath + "/agent.txt"))
                    {
                        // we got some good things
                        Configuration conf = new Configuration();
                        conf["Path"]["bolPath"].StringValue = bolPath;
                        conf["constant"]["bolName"].StringValue = BOLNAME;
                        conf["constant"]["dllName"].StringValue = DLLNAME;
                        conf["debug"]["debug"].BoolValue = false;
                        conf.SaveToFile(INIFILE);
                    }
                    else
                    {
                        writeLog("BoL Studio or agent.dll not found.");
                        MessageBox.Show("Impossible to find folder. Error N07F01D3R.\n Application will now exit.", "An error occured");
                        Application.Exit();
                    }
                }
            }

            #endregion

            // Go get all campions
            writeLog("Loading champions for bol-tools API");
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
            grid_champions.Rows.Clear();

            if (this.cboChampionsList.SelectedValue == "-1")
            {
                writeLog("Default value, wrong champion");
                return;
            }

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
            DataGridViewRow row = grid_champions.Rows[e.RowIndex];
            if (cellIndex == 2)
            {
                writeLog("Click on link, open browser...");
                string forumUrl = row.Cells[2].Value.ToString();
                // TODO: check it cause it bugs 
                Process.Start(forumUrl);
            }

            if (cellIndex == 3)
            {
                // TODO: Fix link (doesn't open browser to forum link)
                // better get good script else drama will cum
                string downloadUrl = row.Cells[4].Value.ToString();
                string scriptTitle = row.Cells[0].Value.ToString();

                writeLog("Clicked button, downloading file");
                using( var client = new WebClient())
                {
                    client.DownloadFile(downloadUrl, scriptTitle+".lua");
                }
                writeLog("File Downloaded");
                postDownload(scriptTitle + ".lua");
                Console.WriteLine(downloadUrl);
            }
        }

        #region Private Custom Methods

        // after download, some check and move file to Script/ folder
        private void postDownload(string scriptFileName)
        {
            if(moveScript)
            {
                try
                {
                    writeLog("Moving file");
                    string fromPath = Path.GetFullPath(scriptFileName);
                    string toPath = Path.GetFullPath(bolPath+"/Scripts/"+ scriptFileName);
                    writeLog("From: "+ fromPath);
                    writeLog("To: "+ toPath);
                    if(File.Exists(toPath))
                    {
                        writeLog("Script already exist, erase it.");
                        File.Delete(toPath);
                    }
                    File.Move(fromPath, toPath);
                }
                catch (Exception ex)
                {
                    writeLog("Moving file failed.");
                    writeLog(ex.ToString());
                    MessageBox.Show("A wild error appear, I'm so scared. Error N01M0V310F01D3R.", "An error occured");
                }
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

        private void writeLog(string messageLog, bool force = false)
        {
            if (!debug && !force) return;

            tbLogs.Text += "[" + DateTime.Now.ToShortDateString() +"] ";
            tbLogs.Text += messageLog;
            tbLogs.Text += Environment.NewLine;
        }

        #endregion

        private void cbDebug_CheckedChanged(object sender, EventArgs e)
        {
            debug = cbDebug.Checked;
            writeLog("Debug set to"+cbDebug.Checked.ToString(), true);
        }

        private void cbMoveScripts_CheckedChanged(object sender, EventArgs e)
        {
            moveScript = cbMoveScripts.Checked;
            writeLog("Move Script set to" + cbMoveScripts.Checked.ToString(), true);
        }
    }
}
