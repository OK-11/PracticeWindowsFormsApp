using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    internal class CustomTextBox : TextBox
    {
        private int _number;
        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }

        public CustomTextBox()
        {
            this.Click += customTextBox_Click;
        }
        private void customTextBox_Click(object sender, EventArgs e)
        {
            List<CustomTextBox> customTextBoxs = new List<CustomTextBox>();
            foreach (Control control in this.Parent.Controls)
            {
                if (control is CustomTextBox customTextBox)
                {
                    customTextBoxs.Add(customTextBox);
                }
            }
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
            CustomTextBox customTextBoxTop = (CustomTextBox)sender;
            foreach (CustomTextBox customTextBox in customTextBoxsOrder)
            {
                this.Parent.Controls.SetChildIndex(customTextBox, 0);
            }
            this.Parent.Controls.SetChildIndex(customTextBoxTop, 0);
        }
    }
}
