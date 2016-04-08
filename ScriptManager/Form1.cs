using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using ScriptManager.Models;
using Newtonsoft.Json;

namespace Scriptmanager
{
    public partial class Form1 : Form
    {
        string apiSearchChampion = "http://www.bol-tools.com/api/search/champion/";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Go get all campions

            var championsDataSource = new List<ComboBoxItem>();
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

            using( WebClient wc = new WebClient())
            {
                var jsonresult = wc.DownloadString(apiSearchChampion + selectedChampionkey);
                dynamic stuff = JsonConvert.DeserializeObject(jsonresult);
                
                foreach(var script in stuff)
                {
                    // title, author, forum, download
                    Script scriptObject = JsonConvert.DeserializeObject<Script>(script.ToString());
                    grid_champions.Rows.Add(scriptObject.Title, scriptObject.Author, scriptObject.ForumUrl, scriptObject.UpdateUrl);
                }
            }

            
        }

        private void grid_champions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine(sender.ToString());
            Console.WriteLine(e.ToString());
        }
    }
}
