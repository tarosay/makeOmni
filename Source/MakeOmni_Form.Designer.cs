namespace MakeOmni
{
    partial class MakeOmni_Form
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.pbxData = new System.Windows.Forms.PictureBox();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.pbxData)).BeginInit();
            this.SuspendLayout();
            // 
            // pbxData
            // 
            this.pbxData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxData.Location = new System.Drawing.Point(0, 0);
            this.pbxData.Name = "pbxData";
            this.pbxData.Size = new System.Drawing.Size(451, 319);
            this.pbxData.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxData.TabIndex = 0;
            this.pbxData.TabStop = false;
            // 
            // bgWorker
            // 
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            // 
            // MakeOmni_Form
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 319);
            this.Controls.Add(this.pbxData);
            this.Name = "MakeOmni_Form";
            this.Text = "全方位画像作成 【画像ファイルをドラッグしてください】";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MakeOmni_Form_FormClosed);
            this.Load += new System.EventHandler(this.MakeOmni_Form_Load);
            this.Shown += new System.EventHandler(this.MakeOmni_Form_Shown);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MakeOmni_Form_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MakeOmni_Form_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.pbxData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pbxData;
        private System.ComponentModel.BackgroundWorker bgWorker;
    }
}

