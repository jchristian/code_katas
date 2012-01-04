namespace UI.Views
{
    partial class ChatView
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
            this.uxChatBox = new System.Windows.Forms.TextBox();
            this.uxDialogLabel = new Infragistics.Win.Misc.UltraLabel();
            this.SuspendLayout();
            // 
            // uxChatBox
            // 
            this.uxChatBox.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxChatBox.Location = new System.Drawing.Point(4, 181);
            this.uxChatBox.Multiline = true;
            this.uxChatBox.Name = "uxChatBox";
            this.uxChatBox.Size = new System.Drawing.Size(276, 69);
            this.uxChatBox.TabIndex = 0;
            // 
            // uxDialogLabel
            // 
            this.uxDialogLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxDialogLabel.Location = new System.Drawing.Point(4, 13);
            this.uxDialogLabel.Name = "uxDialogLabel";
            this.uxDialogLabel.Size = new System.Drawing.Size(276, 162);
            this.uxDialogLabel.TabIndex = 1;
            this.uxDialogLabel.Text = "Dialog";
            this.uxDialogLabel.TextRenderingMode = Infragistics.Win.TextRenderingMode.GDI;
            // 
            // Chat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.uxDialogLabel);
            this.Controls.Add(this.uxChatBox);
            this.Name = "ChatView";
            this.Text = "Chat";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox uxChatBox;
        private Infragistics.Win.Misc.UltraLabel uxDialogLabel;
    }
}