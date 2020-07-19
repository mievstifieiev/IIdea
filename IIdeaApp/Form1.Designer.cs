namespace IIdeaApp
{
    partial class FormAuth
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
            this.helloLable = new System.Windows.Forms.Label();
            this.buttSignIn = new System.Windows.Forms.Button();
            this.buttLocal = new System.Windows.Forms.Button();
            this.butSignUp = new System.Windows.Forms.Button();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbPass = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // helloLable
            // 
            this.helloLable.AutoSize = true;
            this.helloLable.Location = new System.Drawing.Point(2, 9);
            this.helloLable.Name = "helloLable";
            this.helloLable.Size = new System.Drawing.Size(436, 40);
            this.helloLable.TabIndex = 0;
            this.helloLable.Text = "Войдите в свою учётную запись,\r\nчтобы работать над проектами, которые находятся в" +
    " облаке.\r\n";
            // 
            // buttSignIn
            // 
            this.buttSignIn.Location = new System.Drawing.Point(12, 142);
            this.buttSignIn.Name = "buttSignIn";
            this.buttSignIn.Size = new System.Drawing.Size(94, 29);
            this.buttSignIn.TabIndex = 1;
            this.buttSignIn.Text = "Sign In";
            this.buttSignIn.UseVisualStyleBackColor = true;
            this.buttSignIn.Click += new System.EventHandler(this.buttSignIn_Click);
            // 
            // buttLocal
            // 
            this.buttLocal.Location = new System.Drawing.Point(136, 206);
            this.buttLocal.Name = "buttLocal";
            this.buttLocal.Size = new System.Drawing.Size(94, 29);
            this.buttLocal.TabIndex = 2;
            this.buttLocal.Text = "Local work";
            this.buttLocal.UseVisualStyleBackColor = true;
            this.buttLocal.Click += new System.EventHandler(this.buttLocal_Click);
            // 
            // butSignUp
            // 
            this.butSignUp.Location = new System.Drawing.Point(12, 206);
            this.butSignUp.Name = "butSignUp";
            this.butSignUp.Size = new System.Drawing.Size(94, 29);
            this.butSignUp.TabIndex = 3;
            this.butSignUp.Text = "Sign Up";
            this.butSignUp.UseVisualStyleBackColor = true;
            this.butSignUp.Click += new System.EventHandler(this.butSignUp_Click);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(12, 66);
            this.tbName.Name = "tbName";
            this.tbName.PlaceholderText = "Name";
            this.tbName.Size = new System.Drawing.Size(384, 27);
            this.tbName.TabIndex = 4;
            // 
            // tbPass
            // 
            this.tbPass.Location = new System.Drawing.Point(12, 109);
            this.tbPass.Name = "tbPass";
            this.tbPass.PlaceholderText = "Password";
            this.tbPass.Size = new System.Drawing.Size(384, 27);
            this.tbPass.TabIndex = 5;
            // 
            // FormAuth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 290);
            this.Controls.Add(this.tbPass);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.butSignUp);
            this.Controls.Add(this.buttLocal);
            this.Controls.Add(this.buttSignIn);
            this.Controls.Add(this.helloLable);
            this.Name = "FormAuth";
            this.Text = "Authorization";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label helloLable;
        private System.Windows.Forms.Button buttSignIn;
        private System.Windows.Forms.Button buttLocal;
        private System.Windows.Forms.Button butSignUp;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbPass;
    }
}

