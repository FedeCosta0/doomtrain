﻿namespace Doomtrain.Characters_Stats_Charts
{
    partial class CharSPR
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 400D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 800D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(2D, 1200D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(3D, 1600D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint5 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(4D, 2000D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint6 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(5D, 2500D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint7 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(6D, 3000D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint8 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(7D, 3500D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint9 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(8D, 4000D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint10 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(9D, 4500D);
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint11 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 9000D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint12 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 255D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint13 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(2D, 255D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint14 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(3D, 255D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint15 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(4D, 255D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint16 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(5D, 255D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint17 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(6D, 255D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint18 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(7D, 255D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint19 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(8D, 255D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint20 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(9D, 255D);
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.buttonSPRClose = new System.Windows.Forms.Button();
            this.chartSPR = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartSPR)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSPRClose
            // 
            this.buttonSPRClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonSPRClose.Location = new System.Drawing.Point(551, 413);
            this.buttonSPRClose.Name = "buttonSPRClose";
            this.buttonSPRClose.Size = new System.Drawing.Size(75, 23);
            this.buttonSPRClose.TabIndex = 15;
            this.buttonSPRClose.Text = "Close";
            this.buttonSPRClose.UseVisualStyleBackColor = true;
            this.buttonSPRClose.Click += new System.EventHandler(this.buttonSPRClose_Click);
            // 
            // chartSPR
            // 
            chartArea1.AxisX.Title = "Level";
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY.Title = "SPR";
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Segoe UI Semibold", 10F);
            chartArea1.Name = "ChartAreaSPR";
            this.chartSPR.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartSPR.Legends.Add(legend1);
            this.chartSPR.Location = new System.Drawing.Point(0, 0);
            this.chartSPR.Name = "chartSPR";
            series1.ChartArea = "ChartAreaSPR";
            series1.Legend = "Legend1";
            series1.Name = "Default";
            dataPoint1.AxisLabel = "";
            dataPoint2.AxisLabel = "";
            dataPoint3.AxisLabel = "";
            dataPoint4.AxisLabel = "";
            dataPoint5.AxisLabel = "";
            dataPoint6.AxisLabel = "";
            dataPoint7.AxisLabel = "";
            dataPoint8.AxisLabel = "";
            dataPoint9.AxisLabel = "";
            dataPoint10.AxisLabel = "";
            dataPoint10.Label = "";
            series1.Points.Add(dataPoint1);
            series1.Points.Add(dataPoint2);
            series1.Points.Add(dataPoint3);
            series1.Points.Add(dataPoint4);
            series1.Points.Add(dataPoint5);
            series1.Points.Add(dataPoint6);
            series1.Points.Add(dataPoint7);
            series1.Points.Add(dataPoint8);
            series1.Points.Add(dataPoint9);
            series1.Points.Add(dataPoint10);
            series2.ChartArea = "ChartAreaSPR";
            series2.Legend = "Legend1";
            series2.Name = "Current";
            dataPoint11.AxisLabel = "10";
            dataPoint11.IsValueShownAsLabel = false;
            dataPoint12.AxisLabel = "20";
            dataPoint13.AxisLabel = "30";
            dataPoint14.AxisLabel = "40";
            dataPoint15.AxisLabel = "50";
            dataPoint16.AxisLabel = "60";
            dataPoint17.AxisLabel = "70";
            dataPoint18.AxisLabel = "80";
            dataPoint19.AxisLabel = "90";
            dataPoint20.AxisLabel = "100";
            dataPoint20.IsValueShownAsLabel = false;
            dataPoint20.Label = "";
            series2.Points.Add(dataPoint11);
            series2.Points.Add(dataPoint12);
            series2.Points.Add(dataPoint13);
            series2.Points.Add(dataPoint14);
            series2.Points.Add(dataPoint15);
            series2.Points.Add(dataPoint16);
            series2.Points.Add(dataPoint17);
            series2.Points.Add(dataPoint18);
            series2.Points.Add(dataPoint19);
            series2.Points.Add(dataPoint20);
            this.chartSPR.Series.Add(series1);
            this.chartSPR.Series.Add(series2);
            this.chartSPR.Size = new System.Drawing.Size(640, 450);
            this.chartSPR.TabIndex = 14;
            title1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            title1.Name = "TitleSPR";
            title1.Text = "SPR CHART";
            this.chartSPR.Titles.Add(title1);
            this.chartSPR.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chartSPR_MouseDown);
            this.chartSPR.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chartSPR_MouseMove);
            this.chartSPR.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chartSPR_MouseUp);
            // 
            // CharSPR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonSPRClose;
            this.ClientSize = new System.Drawing.Size(638, 448);
            this.ControlBox = false;
            this.Controls.Add(this.buttonSPRClose);
            this.Controls.Add(this.chartSPR);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(640, 450);
            this.MinimumSize = new System.Drawing.Size(640, 450);
            this.Name = "CharSPR";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.chartSPR)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSPRClose;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSPR;
    }
}