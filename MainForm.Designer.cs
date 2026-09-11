namespace ConvertAndMergeDocumentsToPdf
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelTitle;

        private System.Windows.Forms.Panel panelDropArea;
        private System.Windows.Forms.Label labelDropText1;
        private System.Windows.Forms.Label labelDropText2;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDialog;

        private System.Windows.Forms.Label labelSelectedFiles;
        private System.Windows.Forms.ListBox listBoxFiles;
        private System.Windows.Forms.Button buttonRemove;

        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.panelHeader = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();

            this.panelDropArea = new System.Windows.Forms.Panel();
            this.labelDropText1 = new System.Windows.Forms.Label();
            this.labelDropText2 = new System.Windows.Forms.Label();
            this.buttonBrowse = new System.Windows.Forms.Button();

            this.labelSelectedFiles = new System.Windows.Forms.Label();
            this.listBoxFiles = new System.Windows.Forms.ListBox();
            this.buttonRemove = new System.Windows.Forms.Button();

            this.buttonGenerate = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();

            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();

            this.panelHeader.SuspendLayout();
            this.panelDropArea.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // panelHeader
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Height = 50;
            this.panelHeader.Controls.Add(this.labelTitle);

            // labelTitle
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(13, 110, 253);
            this.labelTitle.Text = "Convert And Merge Documents To Pdf";
            this.labelTitle.Location = new System.Drawing.Point(20, 12);

            // panelDropArea
            this.panelDropArea.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.panelDropArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDropArea.Location = new System.Drawing.Point(20, 70);
            this.panelDropArea.Size = new System.Drawing.Size(540, 130);
            this.panelDropArea.Controls.Add(this.labelDropText1);
            this.panelDropArea.Controls.Add(this.labelDropText2);
            this.panelDropArea.Controls.Add(this.buttonBrowse);
            this.panelDropArea.AllowDrop = true;
            this.panelDropArea.DragEnter += new System.Windows.Forms.DragEventHandler(this.PanelDropArea_DragEnter);
            this.panelDropArea.DragDrop += new System.Windows.Forms.DragEventHandler(this.PanelDropArea_DragDrop);

            // labelDropText1
            this.labelDropText1.AutoSize = true;
            this.labelDropText1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelDropText1.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.labelDropText1.Text = "Drag & Drop files here";
            this.labelDropText1.Location = new System.Drawing.Point(210, 25);

            // labelDropText2
            this.labelDropText2.AutoSize = true;
            this.labelDropText2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelDropText2.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.labelDropText2.Text = "or";
            this.labelDropText2.Location = new System.Drawing.Point(260, 50);

            // buttonBrowse
            this.buttonBrowse.BackColor = System.Drawing.Color.FromArgb(13, 110, 253);
            this.buttonBrowse.ForeColor = System.Drawing.Color.White;
            this.buttonBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBrowse.FlatAppearance.BorderSize = 0;
            this.buttonBrowse.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonBrowse.Text = "Browse Files";
            this.buttonBrowse.Size = new System.Drawing.Size(160, 34);
            this.buttonBrowse.Location = new System.Drawing.Point(190, 84);
            this.buttonBrowse.Click += new System.EventHandler(this.ButtonBrowse_Click);

            // labelSelectedFiles
            this.labelSelectedFiles.AutoSize = true;
            this.labelSelectedFiles.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.labelSelectedFiles.Text = "Selected Files";
            this.labelSelectedFiles.Location = new System.Drawing.Point(20, 215);

            // listBoxFiles
            this.listBoxFiles.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.listBoxFiles.Location = new System.Drawing.Point(20, 245);
            this.listBoxFiles.Size = new System.Drawing.Size(420, 150);
            this.listBoxFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxFiles.HorizontalScrollbar = true;

            // buttonRemove
            this.buttonRemove.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.buttonRemove.ForeColor = System.Drawing.Color.White;
            this.buttonRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRemove.FlatAppearance.BorderSize = 0;
            this.buttonRemove.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.Size = new System.Drawing.Size(120, 34);
            this.buttonRemove.Location = new System.Drawing.Point(450, 245);
            this.buttonRemove.Click += new System.EventHandler(this.ButtonRemove_Click);

            // buttonGenerate
            this.buttonGenerate.BackColor = System.Drawing.Color.FromArgb(25, 135, 84);
            this.buttonGenerate.ForeColor = System.Drawing.Color.White;
            this.buttonGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGenerate.FlatAppearance.BorderSize = 0;
            this.buttonGenerate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.buttonGenerate.Text = "Generate PDF";
            this.buttonGenerate.Size = new System.Drawing.Size(540, 42);
            this.buttonGenerate.Location = new System.Drawing.Point(20, 415);
            this.buttonGenerate.Click += new System.EventHandler(this.ButtonGenerate_Click);

            // openFileDialog
            this.openFileDialog.Multiselect = true;
            this.openFileDialog.Title = "Select files to merge";
            this.openFileDialog.Filter =
                "Supported Files|*.pdf;*.doc;*.docx;*.dot;*.dotx;*.rtf;*.xls;*.xlsx;*.ppt;*.pptx;" +
                "*.jpg;*.jpeg;*.png;*.html;*.htm;*.md;*.xps";

            // saveFileDialog
            this.saveFileDialog.Title = "Save merged PDF";
            this.saveFileDialog.Filter = "PDF Files|*.pdf";
            this.saveFileDialog.FileName = "MergedDocument.pdf";

            // statusStrip
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.statusLabel });
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusLabel.Text = "Ready";

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 500);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Merge Documents to PDF - WinForms";
            this.MinimumSize = new System.Drawing.Size(600, 540);
            this.BackColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelDropArea);
            this.Controls.Add(this.labelSelectedFiles);
            this.Controls.Add(this.listBoxFiles);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.statusStrip);

            this.panelHeader.ResumeLayout(false);
            this.panelDropArea.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
