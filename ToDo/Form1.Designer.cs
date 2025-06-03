namespace ToDo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            addTaskButton = new Button();
            addTaskLabel = new Label();
            addTaskTextBox = new TextBox();
            taskLabel = new Label();
            menuStrip1 = new MenuStrip();
            ファイルToolStripMenuItem = new ToolStripMenuItem();
            保存ToolStripMenuItem = new ToolStripMenuItem();
            memoLabel = new Label();
            memoTextBox = new TextBox();
            label1 = new Label();
            priorityListBox = new ListBox();
            label2 = new Label();
            deadlineTextBox = new TextBox();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // addTaskButton
            // 
            addTaskButton.Location = new Point(362, 169);
            addTaskButton.Name = "addTaskButton";
            addTaskButton.Size = new Size(74, 29);
            addTaskButton.TabIndex = 0;
            addTaskButton.Text = "追加";
            addTaskButton.UseVisualStyleBackColor = true;
            addTaskButton.Click += addTaskButton_Click;
            // 
            // addTaskLabel
            // 
            addTaskLabel.AutoSize = true;
            addTaskLabel.Location = new Point(40, 50);
            addTaskLabel.Name = "addTaskLabel";
            addTaskLabel.Size = new Size(57, 20);
            addTaskLabel.TabIndex = 1;
            addTaskLabel.Text = "タスク名";
            // 
            // addTaskTextBox
            // 
            addTaskTextBox.Location = new Point(36, 73);
            addTaskTextBox.Name = "addTaskTextBox";
            addTaskTextBox.Size = new Size(400, 27);
            addTaskTextBox.TabIndex = 2;
            // 
            // taskLabel
            // 
            taskLabel.AutoSize = true;
            taskLabel.Location = new Point(570, 50);
            taskLabel.Name = "taskLabel";
            taskLabel.Size = new Size(42, 20);
            taskLabel.TabIndex = 3;
            taskLabel.Text = "タスク";
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { ファイルToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1099, 28);
            menuStrip1.TabIndex = 4;
            menuStrip1.Text = "menuStrip1";
            // 
            // ファイルToolStripMenuItem
            // 
            ファイルToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 保存ToolStripMenuItem });
            ファイルToolStripMenuItem.Name = "ファイルToolStripMenuItem";
            ファイルToolStripMenuItem.Size = new Size(65, 24);
            ファイルToolStripMenuItem.Text = "ファイル";
            // 
            // 保存ToolStripMenuItem
            // 
            保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            保存ToolStripMenuItem.Size = new Size(122, 26);
            保存ToolStripMenuItem.Text = "保存";
            // 
            // memoLabel
            // 
            memoLabel.AutoSize = true;
            memoLabel.Location = new Point(36, 178);
            memoLabel.Name = "memoLabel";
            memoLabel.Size = new Size(57, 20);
            memoLabel.TabIndex = 5;
            memoLabel.Text = "メモ書き";
            // 
            // memoTextBox
            // 
            memoTextBox.Location = new Point(36, 204);
            memoTextBox.Multiline = true;
            memoTextBox.Name = "memoTextBox";
            memoTextBox.PlaceholderText = "メモ";
            memoTextBox.Size = new Size(400, 200);
            memoTextBox.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(275, 110);
            label1.Name = "label1";
            label1.Size = new Size(54, 20);
            label1.TabIndex = 7;
            label1.Text = "優先度";
            // 
            // priorityListBox
            // 
            priorityListBox.FormattingEnabled = true;
            priorityListBox.Location = new Point(275, 136);
            priorityListBox.Name = "priorityListBox";
            priorityListBox.Size = new Size(161, 24);
            priorityListBox.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(40, 110);
            label2.Name = "label2";
            label2.Size = new Size(39, 20);
            label2.TabIndex = 9;
            label2.Text = "期限";
            // 
            // deadlineTextBox
            // 
            deadlineTextBox.Location = new Point(36, 133);
            deadlineTextBox.MaxLength = 10;
            deadlineTextBox.Name = "deadlineTextBox";
            deadlineTextBox.PlaceholderText = "2025/02/03";
            deadlineTextBox.Size = new Size(208, 27);
            deadlineTextBox.TabIndex = 10;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1099, 484);
            Controls.Add(deadlineTextBox);
            Controls.Add(label2);
            Controls.Add(priorityListBox);
            Controls.Add(label1);
            Controls.Add(memoTextBox);
            Controls.Add(memoLabel);
            Controls.Add(taskLabel);
            Controls.Add(addTaskTextBox);
            Controls.Add(addTaskLabel);
            Controls.Add(addTaskButton);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "ToDo";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button addTaskButton;
        private Label addTaskLabel;
        private TextBox addTaskTextBox;
        private Label taskLabel;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem ファイルToolStripMenuItem;
        private ToolStripMenuItem 保存ToolStripMenuItem;
        private Label memoLabel;
        private TextBox memoTextBox;
        private Label label1;
        private ListBox priorityListBox;
        private Label label2;
        private TextBox deadlineTextBox;
    }
}
