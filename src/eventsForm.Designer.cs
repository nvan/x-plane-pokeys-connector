
namespace nvan.PoKeysConnector
{
    partial class eventsForm
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
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.dataRefLabel = new System.Windows.Forms.Label();
            this.dataRefTextBox = new System.Windows.Forms.TextBox();
            this.inputRadio = new System.Windows.Forms.RadioButton();
            this.outputRadio = new System.Windows.Forms.RadioButton();
            this.pinTypeSelector = new System.Windows.Forms.ComboBox();
            this.pinTypeLabel = new System.Windows.Forms.Label();
            this.pinLabel = new System.Windows.Forms.Label();
            this.pinSelector = new System.Windows.Forms.ComboBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.syncCheckBox = new System.Windows.Forms.CheckBox();
            this.readValueLabel = new System.Windows.Forms.Label();
            this.readValueTextBox = new System.Windows.Forms.TextBox();
            this.writeValueTextBox = new System.Windows.Forms.TextBox();
            this.writeValueLabel = new System.Windows.Forms.Label();
            this.writeMaxLabel = new System.Windows.Forms.Label();
            this.writeMaxTextBox = new System.Windows.Forms.TextBox();
            this.readMaxTextBox = new System.Windows.Forms.TextBox();
            this.readMaxLabel = new System.Windows.Forms.Label();
            this.roundValue = new System.Windows.Forms.CheckBox();
            this.pin2Label = new System.Windows.Forms.Label();
            this.pin2Selector = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(12, 25);
            this.nameTextBox.MaxLength = 30;
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(106, 20);
            this.nameTextBox.TabIndex = 0;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(9, 9);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(35, 13);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "Name";
            // 
            // dataRefLabel
            // 
            this.dataRefLabel.AutoSize = true;
            this.dataRefLabel.Location = new System.Drawing.Point(121, 9);
            this.dataRefLabel.Name = "dataRefLabel";
            this.dataRefLabel.Size = new System.Drawing.Size(47, 13);
            this.dataRefLabel.TabIndex = 2;
            this.dataRefLabel.Text = "DataRef";
            // 
            // dataRefTextBox
            // 
            this.dataRefTextBox.Location = new System.Drawing.Point(124, 25);
            this.dataRefTextBox.Name = "dataRefTextBox";
            this.dataRefTextBox.Size = new System.Drawing.Size(297, 20);
            this.dataRefTextBox.TabIndex = 1;
            // 
            // inputRadio
            // 
            this.inputRadio.AutoSize = true;
            this.inputRadio.Location = new System.Drawing.Point(12, 51);
            this.inputRadio.Name = "inputRadio";
            this.inputRadio.Size = new System.Drawing.Size(49, 17);
            this.inputRadio.TabIndex = 2;
            this.inputRadio.TabStop = true;
            this.inputRadio.Text = "Input";
            this.inputRadio.UseVisualStyleBackColor = true;
            // 
            // outputRadio
            // 
            this.outputRadio.AutoSize = true;
            this.outputRadio.Location = new System.Drawing.Point(61, 51);
            this.outputRadio.Name = "outputRadio";
            this.outputRadio.Size = new System.Drawing.Size(57, 17);
            this.outputRadio.TabIndex = 3;
            this.outputRadio.TabStop = true;
            this.outputRadio.Text = "Output";
            this.outputRadio.UseVisualStyleBackColor = true;
            // 
            // pinTypeSelector
            // 
            this.pinTypeSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pinTypeSelector.FormattingEnabled = true;
            this.pinTypeSelector.Items.AddRange(new object[] {
            "PIN",
            "POEXTBUS",
            "ANALOG",
            "ENCODER",
            "DUAL"});
            this.pinTypeSelector.Location = new System.Drawing.Point(12, 95);
            this.pinTypeSelector.Name = "pinTypeSelector";
            this.pinTypeSelector.Size = new System.Drawing.Size(106, 21);
            this.pinTypeSelector.TabIndex = 4;
            this.pinTypeSelector.SelectedIndexChanged += new System.EventHandler(this.pinTypeSelector_SelectedIndexChanged);
            // 
            // pinTypeLabel
            // 
            this.pinTypeLabel.AutoSize = true;
            this.pinTypeLabel.Location = new System.Drawing.Point(9, 79);
            this.pinTypeLabel.Name = "pinTypeLabel";
            this.pinTypeLabel.Size = new System.Drawing.Size(49, 13);
            this.pinTypeLabel.TabIndex = 7;
            this.pinTypeLabel.Text = "Pin Type";
            // 
            // pinLabel
            // 
            this.pinLabel.AutoSize = true;
            this.pinLabel.Location = new System.Drawing.Point(121, 79);
            this.pinLabel.Name = "pinLabel";
            this.pinLabel.Size = new System.Drawing.Size(22, 13);
            this.pinLabel.TabIndex = 8;
            this.pinLabel.Text = "Pin";
            // 
            // pinSelector
            // 
            this.pinSelector.FormattingEnabled = true;
            this.pinSelector.Location = new System.Drawing.Point(124, 95);
            this.pinSelector.Name = "pinSelector";
            this.pinSelector.Size = new System.Drawing.Size(62, 21);
            this.pinSelector.TabIndex = 5;
            // 
            // saveButton
            // 
            this.saveButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.saveButton.Location = new System.Drawing.Point(346, 160);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 12;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // syncCheckBox
            // 
            this.syncCheckBox.AutoSize = true;
            this.syncCheckBox.Location = new System.Drawing.Point(12, 160);
            this.syncCheckBox.Name = "syncCheckBox";
            this.syncCheckBox.Size = new System.Drawing.Size(108, 17);
            this.syncCheckBox.TabIndex = 8;
            this.syncCheckBox.Text = "Sync on Connect";
            this.syncCheckBox.UseVisualStyleBackColor = true;
            // 
            // readValueLabel
            // 
            this.readValueLabel.AutoSize = true;
            this.readValueLabel.Location = new System.Drawing.Point(189, 79);
            this.readValueLabel.Name = "readValueLabel";
            this.readValueLabel.Size = new System.Drawing.Size(63, 13);
            this.readValueLabel.TabIndex = 12;
            this.readValueLabel.Text = "Read Value";
            // 
            // readValueTextBox
            // 
            this.readValueTextBox.Location = new System.Drawing.Point(192, 95);
            this.readValueTextBox.Name = "readValueTextBox";
            this.readValueTextBox.Size = new System.Drawing.Size(112, 20);
            this.readValueTextBox.TabIndex = 6;
            // 
            // writeValueTextBox
            // 
            this.writeValueTextBox.Location = new System.Drawing.Point(310, 95);
            this.writeValueTextBox.Name = "writeValueTextBox";
            this.writeValueTextBox.Size = new System.Drawing.Size(111, 20);
            this.writeValueTextBox.TabIndex = 7;
            // 
            // writeValueLabel
            // 
            this.writeValueLabel.AutoSize = true;
            this.writeValueLabel.Location = new System.Drawing.Point(307, 79);
            this.writeValueLabel.Name = "writeValueLabel";
            this.writeValueLabel.Size = new System.Drawing.Size(62, 13);
            this.writeValueLabel.TabIndex = 15;
            this.writeValueLabel.Text = "Write Value";
            // 
            // writeMaxLabel
            // 
            this.writeMaxLabel.AutoSize = true;
            this.writeMaxLabel.Location = new System.Drawing.Point(307, 118);
            this.writeMaxLabel.Name = "writeMaxLabel";
            this.writeMaxLabel.Size = new System.Drawing.Size(85, 13);
            this.writeMaxLabel.TabIndex = 21;
            this.writeMaxLabel.Text = "Write Max Value";
            // 
            // writeMaxTextBox
            // 
            this.writeMaxTextBox.Location = new System.Drawing.Point(310, 134);
            this.writeMaxTextBox.Name = "writeMaxTextBox";
            this.writeMaxTextBox.Size = new System.Drawing.Size(111, 20);
            this.writeMaxTextBox.TabIndex = 10;
            // 
            // readMaxTextBox
            // 
            this.readMaxTextBox.Location = new System.Drawing.Point(192, 134);
            this.readMaxTextBox.Name = "readMaxTextBox";
            this.readMaxTextBox.Size = new System.Drawing.Size(112, 20);
            this.readMaxTextBox.TabIndex = 9;
            // 
            // readMaxLabel
            // 
            this.readMaxLabel.AutoSize = true;
            this.readMaxLabel.Location = new System.Drawing.Point(189, 118);
            this.readMaxLabel.Name = "readMaxLabel";
            this.readMaxLabel.Size = new System.Drawing.Size(86, 13);
            this.readMaxLabel.TabIndex = 20;
            this.readMaxLabel.Text = "Read Max Value";
            // 
            // roundValue
            // 
            this.roundValue.AutoSize = true;
            this.roundValue.Location = new System.Drawing.Point(192, 160);
            this.roundValue.Name = "roundValue";
            this.roundValue.Size = new System.Drawing.Size(111, 17);
            this.roundValue.TabIndex = 11;
            this.roundValue.Text = "Round Float to Int";
            this.roundValue.UseVisualStyleBackColor = true;
            // 
            // pin2Label
            // 
            this.pin2Label.AutoSize = true;
            this.pin2Label.Location = new System.Drawing.Point(121, 119);
            this.pin2Label.Name = "pin2Label";
            this.pin2Label.Size = new System.Drawing.Size(31, 13);
            this.pin2Label.TabIndex = 22;
            this.pin2Label.Text = "Pin 2";
            // 
            // pin2Selector
            // 
            this.pin2Selector.FormattingEnabled = true;
            this.pin2Selector.Location = new System.Drawing.Point(124, 133);
            this.pin2Selector.Name = "pin2Selector";
            this.pin2Selector.Size = new System.Drawing.Size(62, 21);
            this.pin2Selector.TabIndex = 23;
            // 
            // eventsForm
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 194);
            this.Controls.Add(this.pin2Selector);
            this.Controls.Add(this.pin2Label);
            this.Controls.Add(this.roundValue);
            this.Controls.Add(this.writeMaxLabel);
            this.Controls.Add(this.writeMaxTextBox);
            this.Controls.Add(this.readMaxTextBox);
            this.Controls.Add(this.readMaxLabel);
            this.Controls.Add(this.writeValueLabel);
            this.Controls.Add(this.writeValueTextBox);
            this.Controls.Add(this.readValueTextBox);
            this.Controls.Add(this.readValueLabel);
            this.Controls.Add(this.syncCheckBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.pinSelector);
            this.Controls.Add(this.pinLabel);
            this.Controls.Add(this.pinTypeLabel);
            this.Controls.Add(this.pinTypeSelector);
            this.Controls.Add(this.outputRadio);
            this.Controls.Add(this.inputRadio);
            this.Controls.Add(this.dataRefTextBox);
            this.Controls.Add(this.dataRefLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.nameTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "eventsForm";
            this.Text = "Event";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label dataRefLabel;
        private System.Windows.Forms.TextBox dataRefTextBox;
        private System.Windows.Forms.RadioButton inputRadio;
        private System.Windows.Forms.RadioButton outputRadio;
        private System.Windows.Forms.ComboBox pinTypeSelector;
        private System.Windows.Forms.Label pinTypeLabel;
        private System.Windows.Forms.Label pinLabel;
        private System.Windows.Forms.ComboBox pinSelector;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.CheckBox syncCheckBox;
        private System.Windows.Forms.Label readValueLabel;
        private System.Windows.Forms.TextBox readValueTextBox;
        private System.Windows.Forms.TextBox writeValueTextBox;
        private System.Windows.Forms.Label writeValueLabel;
        private System.Windows.Forms.Label writeMaxLabel;
        private System.Windows.Forms.TextBox writeMaxTextBox;
        private System.Windows.Forms.TextBox readMaxTextBox;
        private System.Windows.Forms.Label readMaxLabel;
        private System.Windows.Forms.CheckBox roundValue;
        private System.Windows.Forms.Label pin2Label;
        private System.Windows.Forms.ComboBox pin2Selector;
    }
}