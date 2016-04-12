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
using System.Globalization;


namespace ScriptManager
{
    public partial class ScriptManagerForm : MaterialForm
    {
        // TODO: make string as ressources and load them depending of current computer language
 
        const string INIFILE = "conf.ini";
        const string BOLNAME = "BoL Studio.exe";
        const string DLLNAME = "agent.dll";

        string apiSearchChampion = "http://www.bol-tools.com/api/search/champion/";
        string apiSearchCategory = "http://www.bol-tools.com/api/search/category/";
        string currentPath;
        string bolPath;
        Version version;

        bool debug = true;
        bool moveScript = true;
        bool replaceScript = true;

        public ScriptManagerForm()
        {
            InitializeComponent();

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

            // check settings ==> Default
            debug = cbDebug.Checked;
            replaceScript = cbReplaceScript.Checked;
            moveScript = cbMoveScripts.Checked;
            
            // conf file
            // https://github.com/cemdervis/SharpConfig/blob/master/Example/Program.cs

            #region Start and load ini file

            if (File.Exists(INIFILE))
            {
                // test purpose
                writeLog("Found ini file");
                loadSettingsFromConfig();
            }
            else
            {
                // ask bolPath
                writeLog("New start, searching BoL folder");
                getBolPathFolder();
            }

            #endregion

            fillChampionCombobox();
            fillCategoryCombobox();
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
                        if(!replaceScript)
                        {
                            writeLog("Nope. User doesn't want. Abort.");
                            return;
                        }
                        File.Delete(toPath);
                    }
                    File.Move(fromPath, toPath);
                }
                catch (Exception ex)
                {
                    writeLog("Moving file failed.");
                    writeLog(ex.ToString());
                    MessageBox.Show("Impossible to move file.\n. Error N01M0V310F01D3R.", "An error occured");
                }
            }
        }

        private static List<Script> getScriptsListFromUrl(string url)
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

        private static List<Champion> getChampionsListFromUrl(string url)
        {
            var championList = new List<Champion>();

            using (WebClient wc = new WebClient())
            {
                var jsonresult = wc.DownloadString(url);
                dynamic stuff = JsonConvert.DeserializeObject(jsonresult);

                foreach (var champ in stuff)
                {
                    // title, author, forum, download
                    Champion scriptObject = JsonConvert.DeserializeObject<Champion>(champ.ToString());
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

        private void exportLogsFromCombo()
        {
            if(File.Exists("logs.txt"))
            {
                File.Delete("logs.txt");
            }

            var bytes = Encoding.UTF8.GetBytes(tbLogs.Text);
            using(var file = File.OpenWrite("logs.txt"))
            {
                file.Write(bytes, 0, bytes.Length);
            }
        }

        private void getBolPathFolder()
        {
            bool isValidPath = false;
            do {
                openFileBol.Filter = "BoL Studio|*.exe|DLL Agent|*.dll|All file|*.*";
                openFileBol.Title = "Please, select your BoL Studio exe or the agent.dll";

                DialogResult result = openFileBol.ShowDialog();
                if(result == DialogResult.OK)
                {
                    bolPath = Path.GetDirectoryName(openFileBol.FileName);

                    if((File.Exists(bolPath + "/"+BOLNAME) && File.Exists(bolPath + "/"+DLLNAME)) || File.Exists(bolPath + "/agent.txt"))
                    {
                        // we got some good things
                        createFreshConfigFile();
                        isValidPath = true;
                    }
                    else
                    {
                        writeLog("BoL Studio or agent.dll not found.");
                        MessageBox.Show("Impossible to find folder.\nError N07F01D3R.", "An error occured");
                    }
                }else {
                    writeLog("User cancelled file pick.");
                    Application.Exit();
                }
            }while(!isValidPath);
        }

        private void createFreshConfigFile()
        {
            Configuration conf = new Configuration();
            conf["Path"]["bolPath"].StringValue = bolPath;
            conf["Settings"]["replace"].BoolValue = replaceScript;
            conf["Settings"]["move"].BoolValue = moveScript;
            conf["Constant"]["bolName"].StringValue = BOLNAME;
            conf["Constant"]["dllName"].StringValue = DLLNAME;
            conf["Debug"]["debug"].BoolValue = true;
            conf.SaveToFile(INIFILE);
        }

        private void loadSettingsFromConfig()
        {
            Configuration conf = Configuration.LoadFromFile(INIFILE); 
            bolPath = Convert.ToString(conf["Path"]["bolPath"].StringValue);
            if ((File.Exists(bolPath + "/"+ BOLNAME) && File.Exists(bolPath + "/"+ DLLNAME))|| File.Exists(bolPath + "/agent.txt"))
            {
                // ready to go
                writeLog("Found files, seems we are ready.");

                // load config from ini file
                writeLog("Load settings from config.");
                try
                {
                    replaceScript = conf["Settings"]["replace"].BoolValue;
                    moveScript = conf["Settings"]["move"].BoolValue;
                }
                catch(Exception ex)
                {
                    writeLog("fail load settings.");
                    MessageBox.Show("Impossible to load settings.\nError L04DF411. ", "An error occured");
                }
            }
            else
            {
                writeLog("BoL Studio or agent.dll not found.");
                MessageBox.Show("Impossible to find folder.\nError N07F01D3R.", "An error occured");
                getBolPathFolder();
            }
        }

        private static void writeStringSettingsToConf(string section, string key, string value)
        {
            Configuration conf = Configuration.LoadFromFile(INIFILE); 
            conf[section][key].StringValue = value;
            conf.SaveToFile(INIFILE);
        }

        private static void writeBoolSettingsToConf(string section, string key, bool value)
        {
            Configuration conf = Configuration.LoadFromFile(INIFILE); 
            conf[section][key].BoolValue = value;
            conf.SaveToFile(INIFILE);
        }

        private static void writeIntSettingsToConf(string section, string key, int value)
        {
            Configuration conf = Configuration.LoadFromFile(INIFILE); 
            conf[section][key].IntValue = value;
            conf.SaveToFile(INIFILE);
        }

        private static string getCurrentCulture()
        {
            return CultureInfo.CurrentCulture.Name;
        }

        private void fillChampionCombobox()
        {
            // ===--- Fill cbo champions ---===
            // Go get all campions
            writeLog("Loading champions for bol-tools API");
            var championsDataSource = new List<ComboBoxItem>();
            championsDataSource.Add(new ComboBoxItem("Select a champion", "default"));

            var champions = getChampionsListFromUrl("http://bol-tools.com/api/list/champions").OrderBy(c => c.Name);
            foreach (var champion in champions)
            {
                championsDataSource.Add(new ComboBoxItem(champion.Name, champion.Key));
            }

            // set display & value, readonly
            cboChampionsList.DataSource = championsDataSource;
            cboChampionsList.DisplayMember = "Name";
            cboChampionsList.ValueMember = "Value";
        }

        private void fillCategoryCombobox()
        {
            // ===--- Fill cbo category ---===
            // Go get all campions
            writeLog("Loading category for bol-tools API");
            var categoriesDataSource = new List<ComboBoxItem>();
            categoriesDataSource.Add(new ComboBoxItem("Select a category", "default"));
            categoriesDataSource.Add(new ComboBoxItem("OrbWalker", "orbwalk"));
            categoriesDataSource.Add(new ComboBoxItem("Awareness", "awareness"));
            categoriesDataSource.Add(new ComboBoxItem("Evade", "evade"));
            categoriesDataSource.Add(new ComboBoxItem("Bots", "bot"));

            // set display & value, readonly
            cboCategoryList.DataSource = categoriesDataSource;
            cboCategoryList.DisplayMember = "Name";
            cboCategoryList.ValueMember = "Value";
        }

        #endregion

        #region GUI events

        private void cboChampionsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            grid_champions.Rows.Clear();

            if (cboChampionsList.SelectedValue.ToString() == "default")
            {
                writeLog("Default value, wrong champion");
                return;
            }

            var selectedChampionkey = this.cboChampionsList.SelectedValue;
            var url = apiSearchChampion + selectedChampionkey;

            var scriptsList = getScriptsListFromUrl(url);
            foreach (var script in scriptsList)
            {
                grid_champions.Rows.Add(script.Title, script.Author, script.ForumUrl, "Download", script.UpdateUrl);
            }
        }

        private void cboCategoryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            grid_champions.Rows.Clear();

            if (cboCategoryList.SelectedValue.ToString() == "default")
            {
                writeLog("Default value, wrong category");
                return;
            }

            var selectedCategoryKey = cboCategoryList.SelectedValue;
            var url = apiSearchCategory + selectedCategoryKey;

            var scriptsList = getScriptsListFromUrl(url);
            foreach (var script in scriptsList)
            {
                grid_champions.Rows.Add(script.Title, script.Author, script.ForumUrl, "Download", script.UpdateUrl);
            }
        }

        private void grid_champions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int cellIndex = e.ColumnIndex;
            DataGridViewRow row = grid_champions.Rows[e.RowIndex];
            if (cellIndex == 21)
            {
                writeLog("Click on link, open browser...");
                string forumUrl = row.Cells[2].Value.ToString();
                // TODO: check it cause it bugs 
                Process.Start(forumUrl);
            }

            if (cellIndex == 3)
            {
                // better get good script else drama will cum
                string downloadUrl = row.Cells[4].Value.ToString();
                string scriptTitle = row.Cells[0].Value.ToString();

                writeLog("Clicked button, downloading file");
                using (var client = new WebClient())
                {
                    client.DownloadFile(downloadUrl, scriptTitle + ".lua");
                }
                writeLog("File Downloaded");
                postDownload(scriptTitle + ".lua");
            }
        }

        private void cbDebug_CheckedChanged(object sender, EventArgs e)
        {
            debug = cbDebug.Checked;
            writeLog("Debug set to"+cbDebug.Checked.ToString(), true);
            writeBoolSettingsToConf("Debug", "debug", debug);
        }

        private void cbMoveScripts_CheckedChanged(object sender, EventArgs e)
        {
            moveScript = cbMoveScripts.Checked;
            writeLog("Move Script set to" + cbMoveScripts.Checked.ToString(), true);
            writeBoolSettingsToConf("Settings", "move", moveScript);
        }

        private void cbReplaceScript_CheckedChanged(object sender, EventArgs e)
        {
            replaceScript = cbReplaceScript.Checked;
            writeLog("Replace Script set to" + cbReplaceScript.Checked.ToString(), true);
            writeBoolSettingsToConf("Settings", "replace", replaceScript);
        }

        private void ScriptManagerForm_Leave(object sender, EventArgs e)
        {
            // wrong one
        }

        private void ScriptManagerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Generate log file on leave (check settings)
            exportLogsFromCombo();
        }

        #endregion



        

    }
}
