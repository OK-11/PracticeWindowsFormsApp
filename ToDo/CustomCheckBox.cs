using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace ToDo
{
    internal class CustomCheckBox : CheckBox
    {
        private int _id;
        public int Id
        {
            get { return _id; }
        }

        private int _deadline;
        public int Deadline
        {
            get { return _deadline; }
            set { _deadline = value; }
        }

        private int _priority;
        public int Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        private CustomTextBox _customTextBox;
        public CustomTextBox CustomTextBox
        {
            get { return _customTextBox; }
            set { _customTextBox = value; }  
        }

        public CustomCheckBox(int id,string title,int deadline,int priority,string text)
        {
            _id = id;
            this.Text = title; 
            _deadline = deadline;
            _priority = priority;
            CustomTextBox customTextBox = new CustomTextBox();
            _customTextBox = customTextBox;
            _customTextBox.Tag = this;
            _customTextBox.Multiline = true;
            _customTextBox.Size = new Size(400, 200);
            _customTextBox.Text = "■" + title + Environment.NewLine + text;
            this.Tag = this;
            this.Width = 230;
            this.CheckedChanged += toggleTextBox;
        }

        private void toggleTextBox(object sender, EventArgs e)
        {
            if(this.Checked)
            {
                List<CustomTextBox> customTextBoxsCheck = new List<CustomTextBox>();
                foreach (Control control in this.Parent.Controls)
                {
                    if (control is CustomTextBox customTextBox)
                    {
                        customTextBoxsCheck.Add(customTextBox);
                    }
                }
                if(customTextBoxsCheck.Count >= 5)
                {
                    MessageBox.Show("チェックできるのは5つまでです", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Checked = false;
                }
                else
                {
                    this.Parent.Controls.Add(this._customTextBox);
                    List<CustomTextBox> customTextBoxs = new List<CustomTextBox>();
                    TextBox memoTextBox = null;
                    Button updateButton = null;
                    Button addTaskButton = null;
                    foreach (Control control in this.Parent.Controls)
                    {
                        if (control is CustomTextBox customTextBox)
                        {
                            customTextBoxs.Add(customTextBox);
                        }
                        else if (control.Name == "memoTextBox")
                        {
                            memoTextBox = (TextBox)control;
                        }
                        else if (control.Name == "updateButton")
                        {
                            updateButton = (Button)control;
                        }
                        else if(control.Name == "addTaskButton")
                        {
                            addTaskButton = (Button)control;
                        }
                        else if (control.Tag == this && control is TextBox)
                        {
                            TextBox deadlineTextBox = (TextBox)control;
                            deadlineTextBox.ReadOnly = false;
                        }
                        else if (control.Tag == this && control is ListBox)
                        {
                            ListBox priorityListBox = (ListBox)control;
                            priorityListBox.SelectionMode = SelectionMode.One;
                            priorityListBox.Items.Clear();
                            priorityListBox.Items.Add("高");
                            priorityListBox.Items.Add("中");
                            priorityListBox.Items.Add("低");
                            if (this.Priority == 3)
                            {
                                priorityListBox.SelectedIndex = 0;
                            }
                            else if (this.Priority == 2)
                            {
                                priorityListBox.SelectedIndex = 1;
                            }
                            else if (this.Priority == 1)
                            {
                                priorityListBox.SelectedIndex = 2;
                            }
                        }

                    }
                    _customTextBox.Number = customTextBoxs.Count - 1;
                    List<CustomTextBox> customTextBoxsOrder = new List<CustomTextBox>();
                    int order = 0;
                    foreach (CustomTextBox aaa in customTextBoxs)
                    {
                        foreach (CustomTextBox customTextBox in customTextBoxs)
                        {
                            if (customTextBox.Number == order)
                            {
                                customTextBoxsOrder.Add(customTextBox);
                            }
                        }
                        order++;
                    }
                    CustomTextBox customTextBoxLast = null;
                    foreach (CustomTextBox customTextBox in customTextBoxsOrder)
                    {
                        if (customTextBoxLast == null)
                        {
                            customTextBox.Location = new Point(memoTextBox.Location.X, memoTextBox.Location.Y + 30);
                        }
                        else
                        {
                            customTextBox.Location = new Point(customTextBoxLast.Location.X, customTextBoxLast.Location.Y + 30);
                        }
                        this.Parent.Controls.SetChildIndex(customTextBox, 0);
                        customTextBoxLast = customTextBox;
                    }
                    if (updateButton==null)
                    {
                        Button button = new Button();
                        button.Name = "updateButton";
                        button.Location = new Point(addTaskButton.Location.X- button.Width, addTaskButton.Location.Y);
                        button.Text = "更新";
                        button.Size = new Size(70, 30);
                        button.Click += updateButton_Click;
                        this.Parent.Controls.Add(button);
                    }

                }
            }
            else
            {
                this.Parent.Controls.Remove(this._customTextBox);
                List<CustomTextBox> customTextBoxs = new List<CustomTextBox>();
                TextBox memoTextBox = null;
                Button updateButton = null;
                foreach (Control control in this.Parent.Controls)
                {
                    if (control is CustomTextBox customTextBox)
                    {
                        customTextBoxs.Add(customTextBox);
                    }
                    else if (control.Name == "memoTextBox")
                    {
                        memoTextBox = (TextBox)control;
                    }
                    else if (control.Name == "updateButton")
                    {
                        updateButton = (Button)control;
                    }
                    else if (control.Tag == this && control is TextBox)
                    {
                        TextBox deadlineTextBox = (TextBox)control;
                        deadlineTextBox.ReadOnly = true;
                    }
                    else if (control.Tag == this && control is ListBox)
                    {
                        ListBox priorityListBox = (ListBox)control;
                        priorityListBox.SelectionMode = SelectionMode.None;
                        priorityListBox.Items.Clear();
                        if (this.Priority == 3)
                        {
                            priorityListBox.Items.Add("高");
                        }
                        else if (this.Priority == 2)
                        {
                            priorityListBox.Items.Add("中");
                        }
                        else if (this.Priority == 1)
                        {
                            priorityListBox.Items.Add("低");
                        }
                    }
                }
                if (customTextBoxs.Count!=0)
                {
                    List<CustomTextBox> customTextBoxsOrder = new List<CustomTextBox>();
                    int order = 0;
                    while (order < 5)
                    {
                        foreach (CustomTextBox customTextBox in customTextBoxs)
                        {
                            if (customTextBox.Number == order)
                            {
                                customTextBoxsOrder.Add(customTextBox);
                            }
                        }
                        order++;
                    }
                    CustomTextBox customTextBoxLast = null;
                    int set = 0;
                    foreach (CustomTextBox customTextBox in customTextBoxsOrder)
                    {
                        if (customTextBoxLast == null)
                        {
                            customTextBox.Location = new Point(memoTextBox.Location.X, memoTextBox.Location.Y + 30);
                        }
                        else
                        {
                            customTextBox.Location = new Point(customTextBoxLast.Location.X, customTextBoxLast.Location.Y + 30);
                        }
                        customTextBoxLast = customTextBox;
                        customTextBox.Number = set;
                        set++;
                    }
                }
                else
                {
                    this.Parent.Controls.Remove(updateButton);
                }
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("全ての「メモ」「期限」「優先度」が更新されます", "注意", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            if (result == DialogResult.OK)
            {
                string query = @"UPDATE Tasks
                                 SET 
                                     Deadline = {deadline},
                                     Priority = {priority},
                                     Text = '{text}'
                                 WHERE Id = {id};";

                List<CustomCheckBox> customCheckBoxs = new List<CustomCheckBox>();
                foreach (Control control in ((Button)sender).Parent.Controls)
                {
                    if (control is CustomCheckBox customCheckBox && customCheckBox.Checked == true)
                    {
                        customCheckBoxs.Add(customCheckBox);
                    }
                }
                foreach (CustomCheckBox customCheckBox in customCheckBoxs)
                {
                    TextBox deadlineTextBox = null;
                    ListBox priorityListBox = null;
                    foreach (Control control in customCheckBox.Parent.Controls)
                    {
                        if (control.Tag == customCheckBox && control is TextBox)
                        {
                            deadlineTextBox = (TextBox)control;
                        }
                        else if (control.Tag == customCheckBox && control is ListBox)
                        {
                            priorityListBox = (ListBox)control;
                        }
                    }
                    string id = customCheckBox.Id.ToString();
                    string[] deadlineTextBoxArray = deadlineTextBox.Text.Split("/");
                    string deadline = string.Join("", deadlineTextBoxArray);
                    customCheckBox.Deadline = int.Parse(deadline);
                    string priority = null;
                    if (priorityListBox.SelectedItem == "高")
                    {
                        priority = "3";
                    }
                    else if (priorityListBox.SelectedItem == "中")
                    {
                        priority = "2";
                    }
                    else if (priorityListBox.SelectedItem == "低")
                    {
                        priority = "1";
                    }
                    customCheckBox.Priority = int.Parse(priority);
                    string[] texts = customCheckBox.CustomTextBox.Text.Split(Environment.NewLine);
                    string text = "";
                    for (int i = 1; i < texts.Length; i++)
                    {
                        if (i == texts.Length - 1)
                        {
                            text += texts[i];
                        }
                        else
                        {
                            text += texts[i] + Environment.NewLine;
                        }
                    }
                    using (var connection = new SQLiteConnection("Data Source=db/task.db;"))
                    {
                        try
                        {
                            connection.Open();
                            string newQuery = query.Replace("{id}", id).Replace("{deadline}", deadline).Replace("{priority}", priority).Replace("{text}", text);
                            using (var command = new SQLiteCommand(newQuery, connection))
                            {
                                command.ExecuteNonQuery();
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                ((Form1)((Button)sender).Parent).reloadTasksDB();
            }
            else if (result == DialogResult.Cancel)
            {

            }
            
        }
    }
}



