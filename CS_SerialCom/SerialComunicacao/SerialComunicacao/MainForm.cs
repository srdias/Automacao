/*
 * Created by SharpDevelop.
 * User: Adriano
 * Date: 28/7/2013
 * Time: 15:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SerialComunicacao
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		string RxString;
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			
			
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void ButtonStartClick(object sender, EventArgs e)
		{
			serialPort1.PortName = "COM5";
			serialPort1.BaudRate = 9600;
			
			serialPort1.Open();
			if (serialPort1.IsOpen)
			{
				buttonStart.Enabled = false;
				buttonStop.Enabled = true;
				textBox1.ReadOnly = false;
			}
		}
		
		void ButtonStopClick(object sender, EventArgs e)
		{
			if (serialPort1.IsOpen)
			{
				serialPort1.Close();
				buttonStart.Enabled = true;
				buttonStop.Enabled = false;
				textBox1.ReadOnly = true;
			}
		}
		
		void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			if (serialPort1.IsOpen) serialPort1.Close();
		}
		
		void TextBox1KeyPress(object sender, KeyPressEventArgs e)
		{
			// If the port is closed, don't try to send a character.

			if(!serialPort1.IsOpen) return;
			
			// If the port is Open, declare a char[] array with one element.
			char[] buff = new char[1];
			
			// Load element 0 with the key character.

			buff[0] = e.KeyChar;
			
			// Send the one character buffer.
			serialPort1.Write(buff, 0, 1);
			
			// Set the KeyPress event as handled so the character won't
			// display locally. If you want it to display, omit the next line.
			e.Handled = true;
		}
		
		private void DisplayText(object sender, EventArgs e)
		{
			textBox1.AppendText(RxString);
		}
		
		void SerialPort1DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
		{
			RxString = serialPort1.ReadExisting();
			this.Invoke(new EventHandler(DisplayText));
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			serialPort1.Write(textBox2.Text);
		}
	}
}
