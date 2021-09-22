namespace Compiler_Construction
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBox_inputfile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_semantic = new System.Windows.Forms.Button();
            this.button_syntax = new System.Windows.Forms.Button();
            this.button_laxical = new System.Windows.Forms.Button();
            this.button_file = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_inputfile
            // 
            this.textBox_inputfile.Location = new System.Drawing.Point(152, 53);
            this.textBox_inputfile.Multiline = true;
            this.textBox_inputfile.Name = "textBox_inputfile";
            this.textBox_inputfile.Size = new System.Drawing.Size(467, 49);
            this.textBox_inputfile.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Input FileName():";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // button_semantic
            // 
            this.button_semantic.Image = global::Compiler_Construction.Properties.Resources.table_icon_128243;
            this.button_semantic.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_semantic.Location = new System.Drawing.Point(433, 135);
            this.button_semantic.Name = "button_semantic";
            this.button_semantic.Size = new System.Drawing.Size(268, 89);
            this.button_semantic.TabIndex = 5;
            this.button_semantic.Text = "          Generate Symbol Table";
            this.button_semantic.UseVisualStyleBackColor = true;
            this.button_semantic.Click += new System.EventHandler(this.button_semantic_Click);
            // 
            // button_syntax
            // 
            this.button_syntax.Image = global::Compiler_Construction.Properties.Resources.code_icon_129141;
            this.button_syntax.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_syntax.Location = new System.Drawing.Point(152, 179);
            this.button_syntax.Name = "button_syntax";
            this.button_syntax.Size = new System.Drawing.Size(240, 73);
            this.button_syntax.TabIndex = 4;
            this.button_syntax.Text = "                Check Syntax Error";
            this.button_syntax.UseVisualStyleBackColor = true;
            this.button_syntax.Click += new System.EventHandler(this.button_syntax_Click);
            // 
            // button_laxical
            // 
            this.button_laxical.Image = global::Compiler_Construction.Properties.Resources.code_coding_programming_browser_icon_124774;
            this.button_laxical.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_laxical.Location = new System.Drawing.Point(152, 115);
            this.button_laxical.Name = "button_laxical";
            this.button_laxical.Size = new System.Drawing.Size(240, 58);
            this.button_laxical.TabIndex = 2;
            this.button_laxical.Text = "         Generate Token";
            this.button_laxical.UseVisualStyleBackColor = true;
            this.button_laxical.Click += new System.EventHandler(this.button_laxical_Click);
            // 
            // button_file
            // 
            this.button_file.Image = global::Compiler_Construction.Properties.Resources.browse_icon_149519;
            this.button_file.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_file.Location = new System.Drawing.Point(663, 53);
            this.button_file.Name = "button_file";
            this.button_file.Size = new System.Drawing.Size(157, 49);
            this.button_file.TabIndex = 0;
            this.button_file.Tag = "";
            this.button_file.Text = "     Browse File";
            this.button_file.UseVisualStyleBackColor = true;
            this.button_file.Click += new System.EventHandler(this.button_file_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(71, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "PHASE I:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(71, 207);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "PHASE II:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(525, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "PHASE III:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(830, 269);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_semantic);
            this.Controls.Add(this.button_syntax);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_laxical);
            this.Controls.Add(this.textBox_inputfile);
            this.Controls.Add(this.button_file);
            this.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Tag = "";
            this.Text = "MINI COMPILER BY GROUP 5";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_file;
        private System.Windows.Forms.TextBox textBox_inputfile;
        private System.Windows.Forms.Button button_laxical;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_syntax;
        private System.Windows.Forms.Button button_semantic;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

