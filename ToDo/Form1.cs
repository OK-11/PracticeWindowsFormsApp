using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Security.Policy;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;


namespace ToDo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            priorityListBox.Items.Add("優先度");
            priorityListBox.Items.Add("高");
            priorityListBox.Items.Add("中");
            priorityListBox.Items.Add("低");
            deadlineTextBox.TextChanged += addDeadlineTextBox_TextChanged;
            reloadTasksDB();
        }


        private void addTaskButton_Click(object sender, EventArgs e)
        {
            string createTableQuery = @"CREATE TABLE Tasks (
                                        Id INTEGER PRIMARY KEY,
                                        Title TEXT NOT NULL,
                                        Deadline INTEGER NOT NULL CHECK (LENGTH(Deadline) = 8),
                                        Priority INTEGER NOT NULL CHECK (Priority IN (1, 2, 3)),
                                        Text TEXT        
                                        );";

            string insertIntoQuery = @"INSERT INTO Tasks (Title , Deadline , Priority , Text )
                                       VALUES ('{title}' , {deadline} , {priority} ,'{text}');";

            string insertIntoQueryWithText = @"INSERT INTO Tasks (Title , Deadline , Priority , Text)
                                               VALUES ('{title}', {deadline} , {priority} ,'{text}');";

            if (addTaskTextBox.Text.Length == 0)
            {
                MessageBox.Show("タスク名を入力してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (checkDeadlineTextBox())
            {
                MessageBox.Show("締切日が正しく入力されていません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (priorityListBox.SelectedIndex == -1)
            {
                MessageBox.Show("優先度を選択してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                using (var connection = new SQLiteConnection("Data Source=db/task.db;"))
                {
                    try
                    {
                        connection.Open();
                        string newInsertIntoQuery;
                        string[] deadlineTextBoxArray = deadlineTextBox.Text.Split("/");
                        string deadline = string.Join("", deadlineTextBoxArray);
                        string priority = null;
                        if (priorityListBox.SelectedItem == "高")
                        {
                            priority = "3";
                        }
                        else if (priorityListBox.SelectedItem=="中")
                        {
                            priority = "2";
                        }
                        else if (priorityListBox.SelectedItem == "低")
                        {
                            priority = "1";
                        }

                        if (memoTextBox.Text.Length>0)
                        {
                            newInsertIntoQuery = insertIntoQuery.Replace("{title}", addTaskTextBox.Text).Replace("{deadline}", deadline).Replace("{priority}", priority).Replace("{text}", memoTextBox.Text);
                        }
                        else
                        {
                            newInsertIntoQuery = insertIntoQuery.Replace("{title}", addTaskTextBox.Text).Replace("{deadline}", deadline).Replace("{priority}", priority).Replace("{text}", "");
                        }

                        using (var insertCommand = new SQLiteCommand(newInsertIntoQuery, connection))
                        {
                            try
                            {
                                insertCommand.ExecuteNonQuery();
                            }
                            catch (SQLiteException ex)
                            {
                                if (ex.Message.Contains("no such table"))
                                {
                                    using (var createCommand = new SQLiteCommand(createTableQuery, connection))
                                    {
                                        createCommand.ExecuteNonQuery();
                                    }
                                    insertCommand.ExecuteNonQuery();
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                       
                    }
                }
                reloadTasksDB();
                addTaskTextBox.Text = "";
                memoTextBox.Text = "";
                deadlineTextBox.Text = "";
                priorityListBox.SelectedIndex = -1;
            }
        }

        private void removeCheckBox_Click(object sender, EventArgs e)
        {
            List<CustomCheckBox> customCheckBoxs = new List<CustomCheckBox>();
            foreach (Control control in this.Controls)
            {
                if (control is CustomCheckBox customCheckBox && customCheckBox.Checked == true)
                {
                    customCheckBoxs.Add(customCheckBox);
                }
            }

            string query = @"DELETE FROM Tasks
                             WHERE Id = {id}";

            using (var connection = new SQLiteConnection("Data Source=db/task.db;"))
            {
                try
                {
                    connection.Open();
                    foreach (CustomCheckBox customCheckBox in customCheckBoxs)
                    {
                        string newQuery = query.Replace("{id}", customCheckBox.Id.ToString());
                        using (var command = new SQLiteCommand(newQuery, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            reloadTasksDB();
        }

        public void reloadTasksDB()
        {
            string query = "SELECT * FROM Tasks;";

            List<Control> removeControls = new List<Control>();
            foreach (Control control in this.Controls)
            {
                if (control is CustomCheckBox customCheckBox)
                {
                    foreach (Control check in this.Controls)
                    {
                        if(check.Tag == customCheckBox)
                        {
                            removeControls.Add(check);
                        }
                    }
                }
                else if (control is Button button)
                {
                    if (button.Name == "taskButton")
                    {
                        removeControls.Add(button);
                    }
                    else if (button.Name == "updateButton")
                    {
                        removeControls.Add(button);
                    }
                }
            }
            foreach (Control control in removeControls)
            {
                this.Controls.Remove(control);
            }

            using (var connection = new SQLiteConnection("Data Source=db/task.db;"))
            {
                try
                {
                    connection.Open();
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        CustomCheckBox customCheckBoxLast = null;
                        SQLiteDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int id = int.Parse(reader["Id"].ToString());
                            string title = reader["Title"].ToString();
                            int deadline = int.Parse(reader["Deadline"].ToString());
                            int priority = int.Parse(reader["Priority"].ToString());
                            string text = reader["Text"].ToString();
                            CustomCheckBox customCheckBox = new CustomCheckBox(id,title,deadline,priority,text);
                            if (customCheckBoxLast == null)
                            {
                                customCheckBox.Location = new Point(taskLabel.Location.X, taskLabel.Location.Y + 30);
                            }
                            else
                            {
                                customCheckBox.Location = new Point(customCheckBoxLast.Location.X, customCheckBoxLast.Location.Y + 30);
                            }
                            TextBox deadlineTextBox = new TextBox();
                            deadlineTextBox.Tag = customCheckBox;
                            char[] deadlineArray = customCheckBox.Deadline.ToString().ToCharArray();
                            string deadlineTextBoxText = null;
                            for (int i=0; i<deadlineArray.Length; i++)
                            {
                                deadlineTextBoxText += deadlineArray[i];
                                if(i==3)
                                {
                                    deadlineTextBoxText += "/";
                                }
                                else if(i==5)
                                {
                                    deadlineTextBoxText += "/";
                                }
                            }
                            deadlineTextBox.Text = deadlineTextBoxText;
                            deadlineTextBox.Location = new Point(customCheckBox.Location.X + 230, customCheckBox.Location.Y);
                            deadlineTextBox.Width = 100;
                            deadlineTextBox.TextChanged += addDeadlineTextBox_TextChanged;
                            deadlineTextBox.MaxLength = 10;
                            deadlineTextBox.ReadOnly = true;
                            ListBox priorityListBox = new ListBox();
                            priorityListBox.Tag = customCheckBox;
                            if (customCheckBox.Priority == 3)
                            {
                                priorityListBox.Items.Add("高");
                            }
                            else if (customCheckBox.Priority == 2)
                            {
                                priorityListBox.Items.Add("中");
                            }
                            else if (customCheckBox.Priority == 1)
                            {
                                priorityListBox.Items.Add("低");
                            }
                            priorityListBox.Location = new Point(deadlineTextBox.Location.X + 120, deadlineTextBox.Location.Y);
                            priorityListBox.Size = new Size(60, 25);
                            priorityListBox.SelectionMode = SelectionMode.None;
                            this.Controls.Add(customCheckBox);
                            this.Controls.Add(deadlineTextBox);
                            this.Controls.Add(priorityListBox);
                            customCheckBoxLast = customCheckBox;
                        }
                        if (customCheckBoxLast != null)
                        {
                            Button button = new Button();
                            button.Text = "削除";
                            button.Location = new Point(customCheckBoxLast.Location.X, customCheckBoxLast.Location.Y + 30);
                            button.Size = new Size(75, 30);
                            button.Name = "taskButton";
                            button.Click += removeCheckBox_Click;
                            this.Controls.Add(button);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }


        private void addDeadlineTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox deadlineTextBox = (TextBox)sender;

            if (deadlineTextBox.Text.Length == 4 && int.TryParse(deadlineTextBox.Text, out _))
            {
                deadlineTextBox.Text = deadlineTextBox.Text + "/";
                deadlineTextBox.SelectionStart = 5;
            }
            else if (deadlineTextBox.Text.Length == 7)
            {
                string[] deadlineTextBoxArray = deadlineTextBox.Text.Split("/");
                if (deadlineTextBoxArray.Length == 2 && deadlineTextBoxArray[1].Length == 2 && int.TryParse(deadlineTextBoxArray[1], out _))
                {
                    deadlineTextBox.Text = deadlineTextBox.Text + "/";
                    deadlineTextBox.SelectionStart = 8;
                }
            }
        }

        private bool checkDeadlineTextBox()
        {
            string[] deadlineTextBoxArray = deadlineTextBox.Text.Split("/");
            if (deadlineTextBoxArray.Length==3)
            {
                int set = 0;
                for(int i=0;i<3;i++)
                {
                    if (i==0)
                    {
                        if (deadlineTextBoxArray[i].Length==4 && int.TryParse(deadlineTextBoxArray[i],out _))
                        {
                            set++;
                        }
                    }
                    else if (i==1)
                    {
                        if (deadlineTextBoxArray[i].Length == 2 && int.TryParse(deadlineTextBoxArray[i], out _))
                        {
                            set++;
                        }
                    }
                    else if (i==2)
                    {
                        if (deadlineTextBoxArray[i].Length == 2 && int.TryParse(deadlineTextBoxArray[i], out _))
                        {
                            set++;
                        }
                    }
                }
                if (set == 3)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
