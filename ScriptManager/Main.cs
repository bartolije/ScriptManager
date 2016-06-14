using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using Microsoft.Win32;
using System.Threading;


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
        string onlineVersionUrl = "https://raw.githubusercontent.com/bartolije/version/master/lazytool.txt";
        string bolPath;
        string downloadFileName;
        Version version;

        bool debug = true;
        bool moveScript = true;
        bool replaceScript = true;
        string culture = getCurrentCulture();

        Stopwatch sw = new Stopwatch();

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
            writeLog("App path: " + Path.GetDirectoryName(Application.ExecutablePath));

            // check version for auto-update
            version = Assembly.GetEntryAssembly().GetName().Version;
            Version onlineVersion = getLatestVersion();

            writeLog("App version: " + version);
            writeLog("Online version: " + onlineVersion);
            if(version < onlineVersion)
            {
                MessageBox.Show("A new version is available online.\nTo avoig bugs or disfunctions, please, download it.\n\n Application will now exit.", "A new version is available");
                writeLog("new version available: Exit");
                System.Diagnostics.Process.Start("http://www.forum.botoflegends.com/topic/94198-");
                Application.Exit();
            }

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
            fillLanguageCombobox();
            fillListScripts();
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
                            MessageBox.Show("Script already exist, but you unticked replace option.\n Abort operation.", "An error occured");
                            writeLog("Nope. User doesn't want. Abort.");
                            File.Delete(fromPath);
                            return;
                        }
                        File.Delete(toPath);
                    }
                    File.Move(fromPath, toPath);
                }
                catch
                {
                    writeLog("Moving file failed.");
                    MessageBox.Show("Impossible to move file.", "An error occured");
                }
            }
        }

        private void fillListScripts()
        {
            // Fill list from /Scripts folder
            foreach (var script in getScriptsInFolder(Path.GetFullPath(bolPath + "/Scripts")))
            {
                listScriptsLoaded.Items.Add(Path.GetFileName(script.ToString()));
            }

            // Fill list from /NotScripts folder
            if (!Directory.Exists(Path.GetFullPath(bolPath + "/NotScripts")))
            {
                Directory.CreateDirectory(Path.GetFullPath(bolPath + "/NotScripts"));
                writeLog("created NotScripts dir");
            }
            foreach (var script in getScriptsInFolder(Path.GetFullPath(bolPath + "/NotScripts")))
            {
                listScriptsNotLoaded.Items.Add(Path.GetFileName(script.ToString()));
            }
        }

        private List<string> getScriptsInFolder(string folderPath)
        {
            writeLog("get scripts folder => " + folderPath);
            List<string> scriptList = new List<string>();

            string[] files = Directory.GetFiles(folderPath, "*.lua");
            foreach(var file in files)
            {
                scriptList.Add(file);
            }
            return scriptList;
        }

        private void moveFile(string fromPath, string toPath)
        {
            try
            {
                writeLog("moveFile trigger");
                writeLog("From: " + fromPath);
                writeLog("To: " + toPath);
                if (File.Exists(toPath))
                {
                    writeLog("Script already exist, erase it.");
                    if (!replaceScript)
                    {
                        MessageBox.Show("Script already exist, but you unticked replace option.\n Abort operation.", "An error occured");
                        writeLog("Nope. User doesn't want. Abort.");
                        File.Delete(fromPath);
                        return;
                    }
                    File.Delete(toPath);
                }
                File.Move(fromPath, toPath);
            }
            catch
            {
                writeLog("Moving file failed.");
                MessageBox.Show("Impossible to move file.", "An error occured");
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

            tbLogs.Text += "[" + DateTime.Now +"] ";
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
                        writeLog("BoL Studio or agent.dll not found. Error N07F01D3R.");
                        MessageBox.Show("Impossible to find folder.", "An error occured");
                    }
                }else {
                    writeLog("User cancelled file pick.");
                    Application.Exit();
                }
            }while(!isValidPath);
        }

        #region Config file management

        private void createFreshConfigFile()
        {
            Configuration conf = new Configuration();
            conf["Path"]["bolPath"].StringValue = bolPath;
            conf["Settings"]["replace"].BoolValue = replaceScript;
            conf["Settings"]["move"].BoolValue = moveScript;
            conf["Settings"]["language"].StringValue = culture;
            conf["Constant"]["bolName"].StringValue = BOLNAME;
            conf["Constant"]["dllName"].StringValue = DLLNAME;
            conf["Debug"]["debug"].BoolValue = true;
            Section section = new Section("Scripts");
            conf.Add(section);
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
                catch
                {
                    writeLog("fail load settings.");
                    MessageBox.Show("Impossible to load settings. ", "An error occured");
                }
            }
            else
            {
                writeLog("BoL Studio or agent.dll not found.");
                MessageBox.Show("Impossible to find folder.", "An error occured");
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

        #endregion

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

        private void fillLanguageCombobox()
        {
            writeLog("Loading language for translation");
            var languageDataSource = new List<ComboBoxItem>();
            languageDataSource.Add(new ComboBoxItem("Select a category", "default"));
            languageDataSource.Add(new ComboBoxItem("French", "fr"));
            languageDataSource.Add(new ComboBoxItem("English", "en"));
            languageDataSource.Add(new ComboBoxItem("German", "de"));
            languageDataSource.Add(new ComboBoxItem("Chinese", "ch"));

            // set display & value, readonly
            cboLanguage.DataSource = languageDataSource;
            cboLanguage.DisplayMember = "Name";
            cboLanguage.ValueMember = "Value";
        }

        private Version getLatestVersion()
        {
            Version onlineVersion;

            using(WebClient wc = new WebClient())
            {
                string ver = wc.DownloadString(onlineVersionUrl);
                onlineVersion = new Version(ver);
            }
            return onlineVersion;
        }

        private static string getBrowserPath()
        {
            string browser = string.Empty;
            RegistryKey key = null;

            try
            {

                key = Registry.CurrentUser.OpenSubKey(@"HTTP\shell\open\command", false);

                if (key == null)
                {
                    key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http", false);
                }

                if (key != null)
                {
                    browser = key.GetValue(null).ToString().ToLower().Replace("\"", "");
                    if(!browser.EndsWith("exe"))
                    {
                        browser = browser.Substring(0, browser.LastIndexOf(".exe") + 4);
                    }
                    key.Close();
                }
                
            }catch
            {
                return string.Empty;
            }
            return browser;
        }

        private static void startBrower(string url)
        {
            string browerPath = getBrowserPath();
            if (browerPath == string.Empty) browerPath = "iexplore";

            Process process = new Process();
            process.StartInfo = new ProcessStartInfo(browerPath);
            process.StartInfo.Arguments = "\"" + url + "\"";
            process.Start();
        }

        private void checkScriptsLoadedListCount()
        {
            int listCount = 0;
            foreach(var item in listScriptsLoaded.Items)
            {
                listCount++;
            }

            if (listCount > 5)
            {
                writeLog("user got more than 5 scripts");
                MessageBox.Show("hey!! Just to be anoying, you got more than 5 scripts", "Here again");
            }
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
                grid_champions.Rows.Add(script.Title, script.IsPaid, script.Author, script.ForumUrl, "Download", script.UpdateUrl);
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
                grid_champions.Rows.Add(script.Title, script.IsPaid, script.Author, script.ForumUrl, "Download", script.UpdateUrl);
            }
        }

        private void grid_champions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int cellIndex = e.ColumnIndex;
            DataGridViewRow row = grid_champions.Rows[e.RowIndex];
            if (cellIndex == 3)
            {
                writeLog("Click on link, open browser...");
                string forumUrl = row.Cells[3].Value.ToString();
                // TODO: wrong value here
                startBrower(forumUrl);
            }

            if (cellIndex == 4)
            {
                // better get good script else drama will cum
                string downloadUrl = row.Cells[5].Value.ToString();
                string scriptTitle = row.Cells[0].Value.ToString();

                writeLog("Clicked button, downloading file");

                Uri url = new Uri(downloadUrl);
                writeLog("download from url: " + url.ToString());

                sw.Start();

                using (var client = new WebClient())
                {
                    client.DownloadFileCompleted += new AsyncCompletedEventHandler(downloadComplet);
                    client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(downloadProgress);
                    lDownload.Text = "Downloading...";
                    downloadFileName = scriptTitle + ".lua";
                    client.DownloadFileAsync(url, downloadFileName);
                }
            }
        }

        private void downloadProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            downloadBar.Value = e.ProgressPercentage;

            string speed = string.Format("{0} kb/s", (e.BytesReceived / 1024d / sw.Elapsed.TotalSeconds).ToString("0.00"));
            string perc = e.ProgressPercentage.ToString() + "%";
            lDownloadDetails.Text = string.Format("[{0}] {1} Kb's / {2} Kb's - {3}", perc, (e.BytesReceived/1024d).ToString("0.00"), (e.TotalBytesToReceive/1024d).ToString("0.00"), speed);
        }

        private void downloadComplet(object sender, AsyncCompletedEventArgs e)
        {
            sw.Reset();
            if (e.Cancelled)
            {
                // download cancel
                writeLog("download cancelled");
            }
            else
            {
                // download complet
                writeLog("download complete");
                lDownloadDetails.Text = "Download complete";

                // Moved here cause we need end download
                writeLog("File Downloaded");
                postDownload(downloadFileName);
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

        private void cboLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            // changing language
            writeStringSettingsToConf("Settings", "language", cboLanguage.SelectedValue.ToString());
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cboLanguage.SelectedValue.ToString());
            this.InitializeComponent();
        }

        private void btnMoveToNotLoaded_Click(object sender, EventArgs e)
        {
            // move selected to notloaded
            // => Move from /Scripts to /NoScripts
            var listScript = new List<string>();
            foreach(var item in listScriptsLoaded.SelectedItems)
            {
                string fromPath = Path.GetFullPath(bolPath + "/Scripts/" + item.ToString());
                string toPath = Path.GetFullPath(bolPath + "/NotScripts/" + item.ToString());
                writeLog("items from: " + fromPath);
                writeLog("items to: " + toPath);
                moveFile(fromPath, toPath);

                listScript.Add(item.ToString());
            }

            // after foreach, remove/add items to proper list
            foreach(var script in listScript)
            {
                listScriptsLoaded.Items.Remove(script);
                listScriptsNotLoaded.Items.Add(script);
            }

            checkScriptsLoadedListCount();
        }

        private void btnMoveToLoaded_Click(object sender, EventArgs e)
        {
            // move selected to loaded
            // move selected to notloaded
            // => Move from /Scripts to /NoScripts
            var listScript = new List<string>();
            foreach (var item in listScriptsNotLoaded.SelectedItems)
            {
                string fromPath = Path.GetFullPath(bolPath + "/NotScripts/" + item.ToString());
                string toPath = Path.GetFullPath(bolPath + "/Scripts/" + item.ToString());
                writeLog("items from: " + fromPath);
                writeLog("items to: " + toPath);
                moveFile(fromPath, toPath);

                listScript.Add(item.ToString());
            }

            // after foreach, remove/add items to proper list
            foreach (var script in listScript)
            {
                listScriptsNotLoaded.Items.Remove(script);
                listScriptsLoaded.Items.Add(script);
            }

            checkScriptsLoadedListCount();
        }

        #endregion



    }
}
