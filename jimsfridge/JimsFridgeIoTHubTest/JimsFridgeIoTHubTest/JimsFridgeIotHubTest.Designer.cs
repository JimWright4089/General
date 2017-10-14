namespace JimsFridgeIoTHubTest
{
  partial class JimsFridgeIotHubTest
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JimsFridgeIotHubTest));
      this.tDisplay = new System.Windows.Forms.Timer(this.components);
      this.gbFridge = new System.Windows.Forms.GroupBox();
      this.lFridgeTemp = new System.Windows.Forms.Label();
      this.lFridgeDoor = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.gbFreezer = new System.Windows.Forms.GroupBox();
      this.lFreezerTemp = new System.Windows.Forms.Label();
      this.lFreezerDoor = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.lDoorTime = new System.Windows.Forms.Label();
      this.label10 = new System.Windows.Forms.Label();
      this.lLockoutTime = new System.Windows.Forms.Label();
      this.lBoredTime = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.lLastTweet = new System.Windows.Forms.Label();
      this.bTweet = new System.Windows.Forms.Button();
      this.gbFridge.SuspendLayout();
      this.gbFreezer.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tDisplay
      // 
      this.tDisplay.Tick += new System.EventHandler(this.tDisplay_Tick);
      // 
      // gbFridge
      // 
      this.gbFridge.Controls.Add(this.lFridgeTemp);
      this.gbFridge.Controls.Add(this.lFridgeDoor);
      this.gbFridge.Controls.Add(this.label3);
      this.gbFridge.Controls.Add(this.label4);
      this.gbFridge.Location = new System.Drawing.Point(12, 12);
      this.gbFridge.Name = "gbFridge";
      this.gbFridge.Size = new System.Drawing.Size(295, 81);
      this.gbFridge.TabIndex = 0;
      this.gbFridge.TabStop = false;
      this.gbFridge.Text = "Fridge";
      // 
      // lFridgeTemp
      // 
      this.lFridgeTemp.Location = new System.Drawing.Point(89, 46);
      this.lFridgeTemp.Name = "lFridgeTemp";
      this.lFridgeTemp.Size = new System.Drawing.Size(200, 24);
      this.lFridgeTemp.TabIndex = 1;
      this.lFridgeTemp.Text = "label1";
      // 
      // lFridgeDoor
      // 
      this.lFridgeDoor.Location = new System.Drawing.Point(89, 22);
      this.lFridgeDoor.Name = "lFridgeDoor";
      this.lFridgeDoor.Size = new System.Drawing.Size(200, 24);
      this.lFridgeDoor.TabIndex = 2;
      this.lFridgeDoor.Text = "label2";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(20, 22);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(55, 24);
      this.label3.TabIndex = 3;
      this.label3.Text = "Door:";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(20, 46);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(63, 24);
      this.label4.TabIndex = 4;
      this.label4.Text = "Temp:";
      // 
      // gbFreezer
      // 
      this.gbFreezer.Controls.Add(this.lFreezerTemp);
      this.gbFreezer.Controls.Add(this.lFreezerDoor);
      this.gbFreezer.Controls.Add(this.label5);
      this.gbFreezer.Controls.Add(this.label6);
      this.gbFreezer.Location = new System.Drawing.Point(12, 99);
      this.gbFreezer.Name = "gbFreezer";
      this.gbFreezer.Size = new System.Drawing.Size(295, 81);
      this.gbFreezer.TabIndex = 1;
      this.gbFreezer.TabStop = false;
      this.gbFreezer.Text = "Freezer";
      // 
      // lFreezerTemp
      // 
      this.lFreezerTemp.Location = new System.Drawing.Point(89, 46);
      this.lFreezerTemp.Name = "lFreezerTemp";
      this.lFreezerTemp.Size = new System.Drawing.Size(200, 24);
      this.lFreezerTemp.TabIndex = 1;
      this.lFreezerTemp.Text = "label1";
      // 
      // lFreezerDoor
      // 
      this.lFreezerDoor.Location = new System.Drawing.Point(89, 22);
      this.lFreezerDoor.Name = "lFreezerDoor";
      this.lFreezerDoor.Size = new System.Drawing.Size(200, 24);
      this.lFreezerDoor.TabIndex = 2;
      this.lFreezerDoor.Text = "label2";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(20, 22);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(55, 24);
      this.label5.TabIndex = 3;
      this.label5.Text = "Door:";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(20, 46);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(63, 24);
      this.label6.TabIndex = 4;
      this.label6.Text = "Temp:";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.lDoorTime);
      this.groupBox1.Controls.Add(this.label10);
      this.groupBox1.Controls.Add(this.lLockoutTime);
      this.groupBox1.Controls.Add(this.lBoredTime);
      this.groupBox1.Controls.Add(this.label7);
      this.groupBox1.Controls.Add(this.label8);
      this.groupBox1.Location = new System.Drawing.Point(12, 186);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(447, 105);
      this.groupBox1.TabIndex = 2;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Time";
      // 
      // lDoorTime
      // 
      this.lDoorTime.Location = new System.Drawing.Point(95, 70);
      this.lDoorTime.Name = "lDoorTime";
      this.lDoorTime.Size = new System.Drawing.Size(337, 24);
      this.lDoorTime.TabIndex = 5;
      this.lDoorTime.Text = "label9";
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(20, 70);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(55, 24);
      this.label10.TabIndex = 6;
      this.label10.Text = "Door:";
      // 
      // lLockoutTime
      // 
      this.lLockoutTime.Location = new System.Drawing.Point(95, 46);
      this.lLockoutTime.Name = "lLockoutTime";
      this.lLockoutTime.Size = new System.Drawing.Size(337, 24);
      this.lLockoutTime.TabIndex = 1;
      this.lLockoutTime.Text = "label1";
      // 
      // lBoredTime
      // 
      this.lBoredTime.Location = new System.Drawing.Point(95, 22);
      this.lBoredTime.Name = "lBoredTime";
      this.lBoredTime.Size = new System.Drawing.Size(337, 24);
      this.lBoredTime.TabIndex = 2;
      this.lBoredTime.Text = "label2";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(20, 22);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(62, 24);
      this.label7.TabIndex = 3;
      this.label7.Text = "Bored:";
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(20, 46);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(81, 24);
      this.label8.TabIndex = 4;
      this.label8.Text = "Lockout:";
      // 
      // lLastTweet
      // 
      this.lLastTweet.Location = new System.Drawing.Point(12, 294);
      this.lLastTweet.Name = "lLastTweet";
      this.lLastTweet.Size = new System.Drawing.Size(499, 91);
      this.lLastTweet.TabIndex = 3;
      this.lLastTweet.Text = "label2";
      // 
      // bTweet
      // 
      this.bTweet.Location = new System.Drawing.Point(323, 34);
      this.bTweet.Name = "bTweet";
      this.bTweet.Size = new System.Drawing.Size(136, 48);
      this.bTweet.TabIndex = 4;
      this.bTweet.Text = "Tweet";
      this.bTweet.UseVisualStyleBackColor = true;
      this.bTweet.Click += new System.EventHandler(this.bTweet_Click);
      // 
      // JimsFridgeIotHubTest
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(523, 394);
      this.Controls.Add(this.bTweet);
      this.Controls.Add(this.lLastTweet);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.gbFreezer);
      this.Controls.Add(this.gbFridge);
      this.Font = new System.Drawing.Font("Railway", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(6);
      this.Name = "JimsFridgeIotHubTest";
      this.Text = "Jims Fridge IoT Hub Test";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.JimsFridgeIotHubTest_FormClosing);
      this.gbFridge.ResumeLayout(false);
      this.gbFridge.PerformLayout();
      this.gbFreezer.ResumeLayout(false);
      this.gbFreezer.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Timer tDisplay;
    private System.Windows.Forms.GroupBox gbFridge;
    private System.Windows.Forms.Label lFridgeTemp;
    private System.Windows.Forms.Label lFridgeDoor;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.GroupBox gbFreezer;
    private System.Windows.Forms.Label lFreezerTemp;
    private System.Windows.Forms.Label lFreezerDoor;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label lDoorTime;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label lLockoutTime;
    private System.Windows.Forms.Label lBoredTime;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label lLastTweet;
    private System.Windows.Forms.Button bTweet;
  }
}

