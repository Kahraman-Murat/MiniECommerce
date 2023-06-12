using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Csv_Prüfer
{
    public partial class Form1 : Form
    {
        string selectedPath = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] filesWithPaths = Directory.GetFiles(fbd.SelectedPath);
                    listBox1.Items.Clear();
                    foreach (var fileWithPath in filesWithPaths)
                    {
                        // Get File Name
                        //string filePath = @"C:\Users\john\Documents\example.txt";
                        //string fileName = Path.GetFileName(filePath);

                        // Get File Name Without Extension
                        //Path.GetFileNameWithoutExtension("C:\file.txt");

                        // Get File Extension
                        //string myFilePath = @"C:\MyFile.txt";
                        //string ext = Path.GetExtension(myFilePath);

                        selectedPath = Path.GetDirectoryName(fileWithPath);
                        string fileName = Path.GetFileName(fileWithPath);
                        string ext = Path.GetExtension(fileWithPath);
                        

                        if (ext.Equals(".csv"))
                        {
                            label1.Text = selectedPath;
                            listBox1.Items.Add(fileName);
                            listBox1.SelectedIndex = 0;
                            listBox1_Click(sender, e);
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Taner Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "csv files (*.csv)|*.csv",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                selectedPath = Path.GetDirectoryName(openFileDialog1.FileName);
                string fileName = Path.GetFileName(openFileDialog1.FileName);
                
                label1.Text = selectedPath;
                listBox1.Items.Clear();
                listBox1.Items.Add(fileName);
                listBox1.SelectedIndex =0;
                listBox1_Click(sender, e);
                //Csv_Reader(label1.Text);
            }
        }

        public void SetDataGridView(DataTable dTable)
        {
            dataGridView1.DataSource = dTable;

            foreach (DataGridViewColumn dTableCol in dataGridView1.Columns)
            {
                dTableCol.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public void Csv_Reader(string csvFilePath)
        {
            using (var reader = new StreamReader(csvFilePath))
            {
                DataTable dt = new DataTable();

                bool isFirstRow = true;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var cellContents = line.Split(';');

                    if (isFirstRow)
                    {
                        foreach (var cellContent in cellContents)
                        {
                            dt.Columns.Add(cellContent);
                        }

                        isFirstRow = false;
                    }
                    else
                    {
                        dt.Rows.Add(cellContents);
                    }
                }

                SetDataGridView(dt);

                Cell_Analyzer();
            }
        }

        public void Cell_Analyzer()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    float minLimit = 0, maxLimit = 0;
                    string colName = dataGridView1.Columns[i].Name.ToString();
                    if (colName.Equals("Reception")) { minLimit = 50; maxLimit = 450; }
                    else if (colName.Equals("Charge")) { minLimit = 0; maxLimit = 100; }
                    else if (colName.Equals("Flow")) { minLimit = 0; maxLimit = 4000; }

                    float cellValue = float.Parse(row.Cells[i].Value.ToString().Replace(".", ","));
                    if (cellValue < minLimit || cellValue > maxLimit)
                    {
                        row.Cells[i].Style.BackColor = Color.Red;
                    }

                    //string colName = dataGridView1.Columns[i].Name.ToString();
                    //if (colName.Equals("Reception"))
                    //{
                    //    float rcptn = float.Parse(row.Cells[i].Value.ToString().Replace(".", ","));
                    //    if (rcptn < 50 || rcptn > 450)
                    //    {
                    //        row.Cells[i].Style.BackColor = Color.Red;
                    //    }
                    //}
                    //else if (colName.Equals("Charge"))
                    //{
                    //    float chrg = float.Parse(row.Cells[i].Value.ToString().Replace(".", ","));
                    //    if (chrg < 0 || chrg > 100)
                    //    {
                    //        row.Cells[i].Style.BackColor = Color.Red;
                    //    }
                    //}
                    //else if (colName.Equals("Flow"))
                    //{
                    //    float flw = float.Parse(row.Cells[i].Value.ToString().Replace(".", ","));
                    //    if (flw < 0 || flw > 4000)
                    //    {
                    //        row.Cells[i].Style.BackColor = Color.Red;
                    //    }
                    //}
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            //dialog.InitialDirectory = "C:\\Users";
            //dialog.IsFolderPicker = true;
            //if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            //{
            //    MessageBox.Show("You selected: " + dialog.FileName);
            //}
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;
            
            Csv_Reader(selectedPath + listBox1.Items[listBox1.SelectedIndex].ToString());
        }
    }
}
