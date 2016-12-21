using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;


namespace VideoCart
{
    public partial class Form1 : Form
    {
        private OleDbConnection connection = new OleDbConnection();

        public Form1()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Shilpaja\Desktop\project\VideoDatabase.accdb;Persist Security Info=False;";
            // above file path needs to be changed
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to exit?", "Message", MessageBoxButtons.YesNoCancel))

            {
                this.Close();
            }
        }

        private void btn_addVideo_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (!string.IsNullOrEmpty(txt_VideoId.Text) && !string.IsNullOrEmpty(txt_title.Text) && !string.IsNullOrEmpty(txt_genre.Text))
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    command.CommandText = "insert into Video (VideoID,Title,Genre) values(" + txt_VideoId.Text + ",'" + txt_title.Text + "','" + txt_genre.Text + "')";
                    command.ExecuteNonQuery();
                    MessageBox.Show("Video data saved");
                    this.txt_VideoId.Clear();
                    this.txt_title.Clear();
                    this.txt_genre.Clear();
                    OleDbCommand cmd = new OleDbCommand(" select Genre from Video",connection);
                    OleDbDataReader reader = cmd.ExecuteReader();
                    cb_genre.Items.Clear();
                    while (reader.Read())
                    {
                        cb_genre.Items.Add(reader[0].ToString());
                    }
                    connection.Close();
                }
                else
                {
                    MessageBox.Show("enter all fields");
                    this.txt_VideoId.Clear();
                    this.txt_title.Clear();
                    this.txt_genre.Clear();
                }
             
                
               
            }
           catch
           {
                MessageBox.Show(" Video Data entered is too long,invalid or already exists. Enter values again");
                this.txt_VideoId.Clear();
                this.txt_title.Clear();
                this.txt_genre.Clear();
                connection.Close();
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
           

        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                if (!string.IsNullOrEmpty(tb_videoid.Text) && string.IsNullOrEmpty(tb_title.Text))
                {
                    command.CommandText = "select * from Video where VideoID=" + tb_videoid.Text + "";
                    OleDbDataReader reader = command.ExecuteReader();
                    int count = 0;
                    while (reader.Read())
                    {
                        count = count + 1;
                        tb_title.Text = reader["Title"].ToString();
                    }
                    if (count == 1)
                    {
                        MessageBox.Show(" Video Data Found");
                        listBox_result.Items.Add(this.tb_title.Text);
                        this.tb_title.Clear();
                        this.tb_videoid.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Video Data not found. Enter values again");
                        this.tb_title.Clear();
                        this.tb_videoid.Clear();

                    }
                    connection.Close();
                }
                else
                {
                    command.CommandText = "select * from Video where Title='" + tb_title.Text + "'";
                    OleDbDataReader reader = command.ExecuteReader();
                    int count = 0;
                    while (reader.Read())
                    {
                        count = count + 1;
                    }
                    if (count == 1)
                    {
                        MessageBox.Show(" Result Found");
                        listBox_result.Items.Add(this.tb_title.Text);
                        this.tb_title.Clear();
                        this.tb_videoid.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Result not found");
                        this.tb_title.Clear();
                        this.tb_videoid.Clear();

                    }
                    connection.Close();            
                }
            }
            catch
            {
                MessageBox.Show("Invalid video Entry. Enter values again");
                this.tb_title.Clear();
                this.tb_videoid.Clear();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            string query = " select Genre from Video";
            command.CommandText = query;
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                cb_genre.Items.Add(reader[0].ToString());
            }
            connection.Close();
        }

        private void cb_genre_SelectedIndexChanged(object sender, EventArgs e)
        {
           
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = " select * from Video where Genre='" + cb_genre.Text + "'";
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    tb_videoid.Text = reader["VideoID"].ToString();
                    tb_title.Text = reader["Title"].ToString();
                }
                connection.Close();
           
        }

        private void btn_cart_Click(object sender, EventArgs e)
        {
            int c = listBox_result.Items.Count - 1;
            for (int i = c; i >= 0; i--)
            {
                if (listBox_result.GetSelected(i))
                {
                    listBox_cart.Items.Add(listBox_result.Items[i]);
                    listBox_result.Items.RemoveAt(i);
                }
            }
        }

        private void btn_empty_Click(object sender, EventArgs e)
        {
            listBox_cart.Items.Clear();
        }

        private void btn_checkout_Click(object sender, EventArgs e)
        {
            int k = listBox_cart.Items.Count * 2;
            listBox_cart.Items.Clear();
            listBox_cart.Items.Clear();
            MessageBox.Show("your total amount is $" + k);
        }
       

        private void videoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fIleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to exit?", "Message", MessageBoxButtons.YesNoCancel))

            {
                this.Close();
            }
        }


        private void addToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            tabPage1.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Word.Application application = new Microsoft.Office.Interop.Word.Application();
            Document document = application.Documents.Open("C:\\Users\\Shilpaja\\Desktop\\Read me.doc");
            // above file path needs to be changed

        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.txt_VideoId.Clear();
            this.txt_title.Clear();
            this.txt_genre.Clear();
            this.tb_title.Clear();
            this.tb_videoid.Clear();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            string query = " delete from Video";
            command.CommandText = query;
            command.ExecuteNonQuery();
            connection.Close();
            cb_genre.Items.Clear();
        }
    }
}
