using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using BitcoinBrainWalletSweeper.Properties;

namespace BitcoinBrainWalletSweeper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
             // Set constants and dataset
             const string comma = ",";
             const string tablename = "Data";
             var dataset = new DataSet();
             // Use openFileDialog like before
             using (var openFileDialog1 = new OpenFileDialog
             {
                 
                 FilterIndex = 1
             })
             {
                 // Check file opened is valid
                 if (openFileDialog1.ShowDialog() == DialogResult.OK)
                 {
                     var filename = openFileDialog1.FileName;
                     // Open file and read contents
                     var sr = new StreamReader(filename);
                     File.ReadAllText(openFileDialog1.FileName);
                     // Add table and columns to dataset
                     dataset.Tables.Add(tablename);
                     dataset.Tables[tablename].Columns.Add("Password");
                     var allData = sr.ReadToEnd();
                     var rows = allData.Split(Environment.NewLine.ToCharArray());
                     // Loop through rows
                     for (var index = 0; index < rows.Length; index++)
                     {
                         var t = rows[index];
                         var r = t.Replace("\"", string.Empty); // Remove "
                         var items = r.Split(comma.ToCharArray()); // Split on CSV
                         foreach (var item in items)
                         {
                             dataset.Tables[tablename].Rows.Add(item); // Add to dataset table        
                         }
                           
                     }
                     var crypto = new Crypto();
                     foreach (var row in dataset.Tables[tablename].Rows)
                     {
                         var privateKeyHex = crypto.ComputeHash(row.ToString(), new SHA256CryptoServiceProvider());
                     }
                     if (dgvResults != null)
                        if (dataset.Tables != null) dgvResults.DataSource = dataset.Tables[0].DefaultView; // Populate datagrid
                          
 
                 }
                 else
                 {
                     // Invalid file or non selected
                     MessageBox.Show(Resources.Form1_btnOpenFile_Click_No_file_or_invalid_file);
                 }

            }
        }
    }
}
