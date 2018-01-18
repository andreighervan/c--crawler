using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace ThePirateBayCrawlerBot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Search(string keyword)
        {
            string url = "https://thepiratebay.org/search/";
            url += keyword;
            url += "/0/99/0";
             HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
             HttpWebResponse response = (HttpWebResponse)request.GetResponse();
             StreamReader sr = new StreamReader(response.GetResponseStream());
             string response_text = sr.ReadToEnd();
             Regex r = new Regex("<a\\s+(?:[^>]*?\\s+)?class=\"detLink\"(.*?)\\</a>");
             MatchCollection ms = r.Matches(response_text);
             foreach(Match m in ms)
             {
                Regex extract_link_r = new Regex("<a\\s+(?:[^>]*?\\s+)?class=\"detLink\"(.*?)\\</a>");
                Match extract_link_m = extract_link_r.Match(m.Value);
                string link = extract_link_m.Value;
                link = link.Replace(@"<a href=", "");
                link=link.Replace(@"""","");
                link = link.Replace(@"class=detLink", "");
                link=Regex.Replace(link, @"title=[^a-z0-9\\s-]", "");
                link = link.Replace(@"</a>", "");
                link = "https://thepiratebay.org" + link;
                String title = "hi";
                ListViewItem item = new ListViewItem(new String[] { title, link });
                item.Tag = link;
                listView1.Items.Add(item);
              
            }
         
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Search(textBox1.Text);
        }

    }
}
