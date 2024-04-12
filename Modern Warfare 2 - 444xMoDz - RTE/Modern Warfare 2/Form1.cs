using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PS3Lib;
using System.Threading;
using System.Text.RegularExpressions;

namespace Modern_Warfare_2
{
          #region Connection
    public partial class Form1 : Form
    {
        public static uint ProcessID;
        public static uint[] processIDs;
        public static string snresult;
        public static string Info;
        public static PS3TMAPI.ConnectStatus connectStatus;
        public static string Status;
        public static string MemStatus;
        private Random rand = new Random();
        public static PS3API PS3 = new PS3API();
        public static TMAPI DEX = new TMAPI();

        
        public Form1()
        {
            InitializeComponent();
        }
        private List<string> colorList = new List<string>();
        private Dictionary<string, Color> lblColors = new Dictionary<string, Color>();
        private void metroUserControl1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            lblColors.Add("^1", Color.Red);
            lblColors.Add("^2", Color.GreenYellow);
            lblColors.Add("^3", Color.Yellow);
            lblColors.Add("^4", Color.Blue);
            lblColors.Add("^5", Color.Cyan);
            lblColors.Add("^6", Color.DeepPink);
           
            


                   

            
        }
        private List<Label> Labels = new List<Label>();
        private void chromeForm1_Click(object sender, EventArgs e)
        {
            
        }

        private void spaceButton1_Click(object sender, EventArgs e)
        {

            try
            {
                if (PS3.GetCurrentAPI() == SelectAPI.TargetManager)
                {
                    PS3.ConnectTarget(0);

                    label5.Text = "DEX Connected ";
                    label5.ForeColor = Color.White;
                }
            }
            catch
            {
                spaceButton1.Text = "Error";

            }
            try
            {
                if (PS3.GetCurrentAPI() == SelectAPI.ControlConsole)
                {
                    PS3.ConnectTarget();
                    label5.Text = "CEX Connected ";
                    label5.ForeColor = Color.White;
                    PS3.CCAPI.Notify(CCAPI.NotifyIcon.FRIEND, ("Welcome CEX User"));
                    PS3.CCAPI.RingBuzzer(CCAPI.BuzzerMode.Single);

                }
            }
            catch
            {
                label5.Text = "Not Connected(Error at Connection)";
                label5.ForeColor = Color.DimGray;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void spaceButton2_Click(object sender, EventArgs e)
        {
           ﻿try
            {
                if (PS3.GetCurrentAPI() == SelectAPI.TargetManager)
                {

                    if (System.IO.File.Exists("C:/Program Files/SN Systems/PS3/bin/ps3debugger.exe"))
                    {
                        System.IO.File.Exists("C:/Program Files/SN Systems/PS3/bin/ps3debugger.exe");

                    }
                    else if (System.IO.File.Exists("C:/Program Files (x86)/SN Systems/PS3/bin/ps3debugger.exe"))
                    {
                        System.IO.File.Exists("C:/Program Files (x86)/SN Systems/PS3/bin/ps3debugger.exe");

                    }

                    else
                    {
                        DialogResult dialogResult = MessageBox.Show("Could not locate Debugger.\nPlease copy Debugger to the current directory\nC:/Program Files/SN Systems/PS3/bin/<here>", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                        if (dialogResult == DialogResult.OK)
                        {


                            Close();

                        }
                    }
                }
                PS3.AttachProcess();
                if (PS3.AttachProcess())
                    label4.Text = "Ps3 Attached";
                label4.ForeColor = Color.White;
                PS3.Extension.WriteInt32(0x10030000, 0x00724C38);
                PS3.Extension.WriteInt32(0x10030004, 0x00734BE8);
                PS3.Extension.WriteBool(0x01D0CE6C, false);
                PS3.CCAPI.Notify(CCAPI.NotifyIcon.FRIEND, ("Game Attached. Thanks for using HFH Mx444 MW2 Tool"));
                PS3.CCAPI.RingBuzzer(CCAPI.BuzzerMode.Double);


            }
            catch
            {
                MessageBox.Show("No game process found", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            PS3.ChangeAPI(SelectAPI.ControlConsole);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            PS3.ChangeAPI(SelectAPI.TargetManager);
        }

        private void spaceButton3_Click(object sender, EventArgs e)
        {
            if (spaceComboBox1.SelectedItem == "Cex")
            {

                PS3.CCAPI.ShutDown(0);
            }


            if (spaceComboBox1.SelectedItem == "Dex")
            {
                PS3TMAPI.PowerOff(0, true);
            
            }
        }

        private void spaceComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    #endregion

          #region MW2
          public class MW2
        {
           
           

            public class HudStruct
            {
                public static uint
                xOffset = 0x08,
                yOffset = 0x04,
                textOffset = 0x84,
                GlowColor = 0x8C,
                fontOffset = 0x28,
                fontSizeOffset = 0x14,
                colorOffset = 0x34,
                relativeOffset = 0x2c,
                widthOffset = 0x48,
                heightOffset = 0x44,
                shaderOffset = 0x4C,
                alignOffset = 0x30,
                flags = 0xA4,
                clientIndex = 0xA8;
            }

            public static void WritePowerPc(bool Active)
            {
                byte[] NewPPC = new byte[] { 0xF8, 0x21, 0xFF, 0x61, 0x7C, 0x08, 0x02, 0xA6, 0xF8, 0x01, 0x00, 0xB0, 0x3C, 0x60, 0x10, 0x03, 0x80, 0x63, 0x00, 0x00, 0x60, 0x62, 0x00, 0x00, 0x3C, 0x60, 0x10, 0x04, 0x80, 0x63, 0x00, 0x00, 0x2C, 0x03, 0x00, 0x00, 0x41, 0x82, 0x00, 0x28, 0x3C, 0x60, 0x10, 0x04, 0x80, 0x63, 0x00, 0x04, 0x3C, 0xA0, 0x10, 0x04, 0x38, 0x80, 0x00, 0x00, 0x30, 0xA5, 0x00, 0x10, 0x4B, 0xE8, 0xB2, 0x7D, 0x38, 0x60, 0x00, 0x00, 0x3C, 0x80, 0x10, 0x04, 0x90, 0x64, 0x00, 0x00, 0x3C, 0x60, 0x10, 0x05, 0x80, 0x63, 0x00, 0x00, 0x2C, 0x03, 0x00, 0x00, 0x41, 0x82, 0x00, 0x24, 0x3C, 0x60, 0x10, 0x05, 0x30, 0x63, 0x00, 0x10, 0x4B, 0xE2, 0xF9, 0x7D, 0x3C, 0x80, 0x10, 0x05, 0x90, 0x64, 0x00, 0x04, 0x38, 0x60, 0x00, 0x00, 0x3C, 0x80, 0x10, 0x05, 0x90, 0x64, 0x00, 0x00, 0x3C, 0x60, 0x10, 0x03, 0x80, 0x63, 0x00, 0x04, 0x60, 0x62, 0x00, 0x00, 0xE8, 0x01, 0x00, 0xB0, 0x7C, 0x08, 0x03, 0xA6, 0x38, 0x21, 0x00, 0xA0, 0x4E, 0x80, 0x00, 0x20 };
                byte[] RestorePPC = new byte[] { 0x81, 0x62, 0x92, 0x84, 0x7C, 0x08, 0x02, 0xA6, 0xF8, 0x21, 0xFF, 0x01, 0xFB, 0xE1, 0x00, 0xB8, 0xDB, 0x01, 0x00, 0xC0, 0x7C, 0x7F, 0x1B, 0x78, 0xDB, 0x21, 0x00, 0xC8, 0xDB, 0x41, 0x00, 0xD0, 0xDB, 0x61, 0x00, 0xD8, 0xDB, 0x81, 0x00, 0xE0, 0xDB, 0xA1, 0x00, 0xE8, 0xDB, 0xC1, 0x00, 0xF0, 0xDB, 0xE1, 0x00, 0xF8, 0xFB, 0x61, 0x00, 0x98, 0xFB, 0x81, 0x00, 0xA0, 0xFB, 0xA1, 0x00, 0xA8, 0xFB, 0xC1, 0x00, 0xB0, 0xF8, 0x01, 0x01, 0x10, 0x81, 0x2B, 0x00, 0x00, 0x88, 0x09, 0x00, 0x0C, 0x2F, 0x80, 0x00, 0x00, 0x40, 0x9E, 0x00, 0x64, 0x7C, 0x69, 0x1B, 0x78, 0xC0, 0x02, 0x92, 0x94, 0xC1, 0xA2, 0x92, 0x88, 0xD4, 0x09, 0x02, 0x40, 0xD0, 0x09, 0x00, 0x0C, 0xD1, 0xA9, 0x00, 0x04, 0xD0, 0x09, 0x00, 0x08, 0xE8, 0x01, 0x01, 0x10, 0xEB, 0x61, 0x00, 0x98, 0xEB, 0x81, 0x00, 0xA0, 0x7C, 0x08, 0x03, 0xA6, 0xEB, 0xA1, 0x00, 0xA8, 0xEB, 0xC1, 0x00, 0xB0, 0xEB, 0xE1, 0x00, 0xB8, 0xCB, 0x01, 0x00, 0xC0, 0xCB, 0x21, 0x00, 0xC8 };
                if (Active == true)    
                    PS3.SetMemory(0x0038EDE8, NewPPC); 
                else
                    PS3.SetMemory(0x0038EDE8, RestorePPC);
            }

            public static void SV_SendServerCommand(int clientIndex, string Command)
            {
                MW2.WritePowerPc(true);
                PS3.Extension.WriteString(0x10040010, Command);
               PS3.Extension.WriteInt32(0x10040004, clientIndex);
                PS3.Extension.WriteBool(0x10040003, true);
                bool isRunning;
                do { isRunning = DEX.Extension.ReadBool(0x10040003); } while (isRunning != false);
                MW2.WritePowerPc(false);
            }

            public static class HudAlloc
            {
                public static uint
                    IndexSlot = 50,
                    g_hudelem = 0x012E9858;

                public static bool
                    Start = true;
            }

            public static class HUDAlign
            {
                public static uint
                    RIGHT = 2,
                    CENTER = 5,
                    LEFT = 1;
            }

            public class HudTypes
            {
                public static uint
                    Text = 1,
                    Shader = 6,
                    Null = 0;
            }

            public class Material
            {
                public static uint
                    White = 1,
                    Black = 2,
                    Prestige0 = 0x1A,
                    Prestige1 = 0x1B,
                    Prestige2 = 0x1C,
                    Prestige3 = 0x1D,
                    Prestige4 = 0x1E,
                    Prestige5 = 0x1F,
                    Prestige6 = 0x20,
                    Prestige7 = 0x21,
                    Prestige8 = 0x22,
                    Prestige9 = 0x23,
                    Prestige10 = 0x24,
                    WhiteRectangle = 0x25,
                    NoMap = 0x29;
            }

            public static uint HudElemAlloc(bool Reset = false)
            {
                if (Reset == true)
                    HudAlloc.IndexSlot = 50;
                uint Output = HudAlloc.g_hudelem + (HudAlloc.IndexSlot * 0xB4);
                HudAlloc.IndexSlot++;
                return Output;
            }

            public static uint G_LocalizedString(string input)
            {
                uint StrIndex = 0;
                bool isRunning = true;
                MW2.WritePowerPc(true);
                PS3.Extension.WriteString(0x10050010, input);
                PS3.Extension.WriteBool(0x10050000 + 3, true);
                do { StrIndex = DEX.Extension.ReadUInt32(0x10050004); } while (StrIndex == 0);
               PS3.Extension.WriteUInt32(0x10050004, 0);
                do { isRunning = DEX.Extension.ReadBool(0x10050003); } while (isRunning != false);
                MW2.WritePowerPc(false);
                return StrIndex;
            }

            public static int RGB2INT(int r, int g, int b, int a)
            {
                byte[] newRGB = new byte[4];
                newRGB[0] = (byte)r;
                newRGB[1] = (byte)g;
                newRGB[2] = (byte)b;
                newRGB[3] = (byte)a;
                Array.Reverse(newRGB);
                return BitConverter.ToInt32(newRGB, 0);
            }
            public static void Level70(int ClientInt)
            {
                SV_SendServerCommand(ClientInt, "N 2056 206426 6525 7F 3760 09 4623 E803 3761 09 4627 F430 3762 02 4631 14 3763 02 4635 3C 3764 02 4639 0F 3765 02 4643 14 3766 02 4647 28 3767 02 4651 0A 3752 09 4591 E803 3753 09 4595 0F40 3754 02 4599 14 3755 02 4603 3C 3756 02 4607 0F 3757 02 4611 14 3758 02 4615 28 3759 02 4619 0A 3736 09 4527 E803");
            }

            public static void SetText(uint clientIndex, uint elem, uint text, uint font, float fontScale, float x, float y, uint alignText, uint align, int r = 255, int g = 255, int b = 255, int a = 255, int GlowR = 255, int GlowG = 0, int GlowB = 0, int GlowA = 0)
            {
               PS3.Extension.WriteInt32(elem, 0);
               PS3.Extension.WriteInt32(elem + HudStruct.flags, 1);
               PS3.Extension.WriteUInt32(elem + HudStruct.clientIndex, clientIndex);
               PS3.Extension.WriteUInt32(elem + HudStruct.textOffset, text);
               PS3.Extension.WriteUInt32(elem + HudStruct.relativeOffset, alignText);
               PS3.Extension.WriteUInt32(elem + HudStruct.relativeOffset - 4, 6);
               PS3.Extension.WriteUInt32(elem + HudStruct.fontOffset, font);
               PS3.Extension.WriteUInt32(elem + HudStruct.alignOffset, align);
               PS3.Extension.WriteInt16(elem + HudStruct.textOffset + 4, 0x4000);
               PS3.Extension.WriteFloat(elem + HudStruct.fontSizeOffset, fontScale);
               PS3.Extension.WriteFloat(elem + HudStruct.xOffset, x);
               PS3.Extension.WriteFloat(elem + HudStruct.yOffset, y);
               PS3.Extension.WriteInt32(elem + HudStruct.colorOffset, RGB2INT(r,g,b,a));
               PS3.Extension.WriteInt32(elem + HudStruct.GlowColor, RGB2INT(GlowR,GlowG,GlowB,GlowA));
            }
            public static void Vision(int client, string message)
            {
                
                SV_SendServerCommand(client, string.Concat("Q \"", message));
            }
            public static void SetShader(uint clientIndex, uint elem, uint shader, int width, int height, float x, float y, uint align, float sort = 0, int r = 255, int g = 255, int b = 255, int a = 255)
            {
                PS3.Extension.WriteInt32(elem, 0);
                PS3.Extension.WriteInt32(elem + HudStruct.flags, 1);
                PS3.Extension.WriteUInt32(elem + HudStruct.clientIndex, clientIndex);
                PS3.Extension.WriteUInt32(elem + HudStruct.shaderOffset, shader);
                PS3.Extension.WriteUInt32(elem + HudStruct.relativeOffset, 5);
                PS3.Extension.WriteUInt32(elem + HudStruct.relativeOffset - 4, 6);
                PS3.Extension.WriteInt32(elem + HudStruct.heightOffset, height);
                PS3.Extension.WriteInt32(elem + HudStruct.widthOffset, width);
                PS3.Extension.WriteUInt32(elem + HudStruct.alignOffset, align);
                PS3.Extension.WriteFloat(elem + HudStruct.xOffset, x);
                PS3.Extension.WriteFloat(elem + HudStruct.yOffset, y);
                PS3.Extension.WriteInt32(elem + HudStruct.colorOffset, RGB2INT(r, g, b, a));
                PS3.Extension.WriteFloat(elem + HudStruct.textOffset + 4, sort);
            }

            public static void SetElement(uint Element, uint HudTypes)
            {
                PS3.Extension.WriteUInt32(Element, HudTypes);
            }
            public static void RemovelocWarnings(int ClientInt)
            {
                SV_SendServerCommand(ClientInt, "v loc_warnings \"0\"");
                SV_SendServerCommand(ClientInt, "v loc_warningsAsErrors \"0\"");
            }
            public static void SetClientDvars(int client, string dvars)
            {
                RemovelocWarnings(client);
                SV_SendServerCommand(client, string.Concat("v ", dvars));
            }

            public static void iPrintln(int client, string message)
            {
                SV_SendServerCommand(client, "f \"" + message);
            }

            public static void iPrintlnBold(int client, string message)
            {
                SV_SendServerCommand(client, "c \"" + message);
            }
            public static void GiveDerankAll(int ClientInt)
            {
                SV_SendServerCommand(ClientInt, "v clanName \"{}{}\"");
                SV_SendServerCommand(ClientInt, "v motd \"-> You Have Been Deranked! ^0By The Host! <-\"");
                SV_SendServerCommand(ClientInt, "c \"^0You have been Deranked!\"");
                System.Threading.Thread.Sleep(300);
                SV_SendServerCommand(ClientInt, "v loc_warnings \"0\"");
                SV_SendServerCommand(ClientInt, "v loc_warningsAsErrors \"0\"");
                SV_SendServerCommand(ClientInt, "N 2056 000000 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000");
                SV_SendServerCommand(ClientInt, "N 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                SV_SendServerCommand(ClientInt, "N 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00");
                SV_SendServerCommand(ClientInt, "N 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000");
                SV_SendServerCommand(ClientInt, "N 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                SV_SendServerCommand(ClientInt, "N 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000");
                SV_SendServerCommand(ClientInt, "N 3737 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000");
                SV_SendServerCommand(ClientInt, "N 3775 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00");
                SV_SendServerCommand(ClientInt, "N 3787 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00");
                SV_SendServerCommand(ClientInt, "N 3792 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000");
                SV_SendServerCommand(ClientInt, "N 3747 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000");
                System.Threading.Thread.Sleep(100);
                SV_SendServerCommand(ClientInt, "N 3826 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000");
                SV_SendServerCommand(ClientInt, "N 3848 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 000000");
                SV_SendServerCommand(ClientInt, "N 3877 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                SV_SendServerCommand(ClientInt, "N 3812 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                SV_SendServerCommand(ClientInt, "N 3883 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                SV_SendServerCommand(ClientInt, "N 3909 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                SV_SendServerCommand(ClientInt, "N 3918 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                SV_SendServerCommand(ClientInt, "N 3934 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                SV_SendServerCommand(ClientInt, "N 3949 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000");
                SV_SendServerCommand(ClientInt, "N 3969 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00");
                System.Threading.Thread.Sleep(100);
                SV_SendServerCommand(ClientInt, "N 3989 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                SV_SendServerCommand(ClientInt, "N 4003 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                SV_SendServerCommand(ClientInt, "N 4013 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                SV_SendServerCommand(ClientInt, "N 4026 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                SV_SendServerCommand(ClientInt, "N 4046 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000000000000 0000 00000000 0000 0000 0000 0000ZZ0000 0000 00000");
                System.Threading.Thread.Sleep(100);
                SV_SendServerCommand(ClientInt, "N 6641 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                SV_SendServerCommand(ClientInt, "N 6644 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                SV_SendServerCommand(ClientInt, "N 6507 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                SV_SendServerCommand(ClientInt, "N 6651 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                SV_SendServerCommand(ClientInt, "N 6509 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                SV_SendServerCommand(ClientInt, "N 6656 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                SV_SendServerCommand(ClientInt, "N 6661 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                System.Threading.Thread.Sleep(0);
                SV_SendServerCommand(ClientInt, "N 6679 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                SV_SendServerCommand(ClientInt, "N 6633 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                SV_SendServerCommand(ClientInt, "N 6690 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                SV_SendServerCommand(ClientInt, "N 6701 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                SV_SendServerCommand(ClientInt, "N 6532 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                SV_SendServerCommand(ClientInt, "N 3850 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                SV_SendServerCommand(ClientInt, "N 3900 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                SV_SendServerCommand(ClientInt, "N 3950 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                SV_SendServerCommand(ClientInt, "N 4000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                SV_SendServerCommand(ClientInt, "N 4050 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                System.Threading.Thread.Sleep(100);
                SV_SendServerCommand(ClientInt, "v clanName \"{}{}\"");
                SV_SendServerCommand(ClientInt, "v motd \"-> You Have Been Deranked! ^0By The Host! <-\"");
                System.Threading.Thread.Sleep(100);
                SV_SendServerCommand(ClientInt, "c \"^0You have been Deranked!\"");
            }
           public static void On(int ClientInt)
                        {
                            MW2.SetClientDvars(ClientInt, "set SingleAimBot");
                            MW2.SetClientDvars(ClientInt, "set aim_autoaim_lerp 100");
                            MW2.SetClientDvars(ClientInt, "set aim_autoaim_region_height 480");
                            MW2.SetClientDvars(ClientInt, "set aim_autoaim_region_width 640");
                            MW2.SetClientDvars(ClientInt, "set aim_aimAssistRangeScale 2");
                            MW2.SetClientDvars(ClientInt, "set aim_autoAimRangeScale 2");
                            MW2.SetClientDvars(ClientInt, "set aim_slowdown_debug 0");
                            MW2.SetClientDvars(ClientInt, "set aim_slowdown_region_height 0");
                            MW2.SetClientDvars(ClientInt, "set aim_slowdown_region_width 0");
                            MW2.SetClientDvars(ClientInt, "set aim_lockon_enabled 1");
                            MW2.SetClientDvars(ClientInt, "set aim_lockon_strength 1");
                            MW2.SetClientDvars(ClientInt, "set aim_lockon_deflection 0");
                            MW2.SetClientDvars(ClientInt, "set aim_autoaim_enabled 0");
                            MW2.SetClientDvars(ClientInt, "set aim_slowdown_yaw_scale_ads 0");
                            MW2.SetClientDvars(ClientInt, "set aim_slowdown_pitch_scale_ads 0");
                            MW2.SetClientDvars(ClientInt, "set aim_slowdown_enabled 1");
                            MW2.SetClientDvars(ClientInt, "set aim_autoaim_enabled 1");
                            MW2.SetClientDvars(ClientInt, "set aim_lockon_enabled 1");
                            MW2.SetClientDvars(ClientInt, "set SingleFire");
                       
                        }
           public static void _0(int ClientInt)
           {
               SetClientDvars(0, "loc_warnings 0");
               SetClientDvars(0, "loc_warningsAsErrors 0");
               SV_SendServerCommand(ClientInt, "N 2064 00000");
           }

           public static void _1(int ClientInt)
           {
               SetClientDvars(0, "loc_warnings 0");
               SetClientDvars(0, "loc_warningsAsErrors 0");
               SV_SendServerCommand(ClientInt, "N 2064 01000");
           }

           public static void _10(int ClientInt)
           {
               SV_SendServerCommand(ClientInt, "N  2064 0A000");
           }

           public static void _11(int ClientInt)
           {
               SV_SendServerCommand(ClientInt, "N  2064 0B000");
           }

           public static void _2(int ClientInt)
           {
               SV_SendServerCommand(ClientInt, "N  2064 02000");
           }

           public static void _3(int ClientInt)
           {
               SV_SendServerCommand(ClientInt, "N  2064 03000");
           }

           public static void _4(int ClientInt)
           {
               SV_SendServerCommand(ClientInt, "N  2064 04000");
           }

           public static void _5(int ClientInt)
           {
               SV_SendServerCommand(ClientInt, "N  2064 05000");
           }

           public static void _6(int ClientInt)
           {
               SV_SendServerCommand(ClientInt, "N  2064 06000");
           }

           public static void _7(int ClientInt)
           {
               SV_SendServerCommand(ClientInt, "N  2064 07000");
           }

           public static void _8(int ClientInt)
           {
               SV_SendServerCommand(ClientInt, "N  2064 08000");
           }

           public static void _9(int ClientInt)
           {
               SV_SendServerCommand(ClientInt, "N  2064 09000");
           }
           public static void Off1(int ClientInt)
           {
               MW2.SetClientDvars(ClientInt, "reset aim_target_sentient_radius");
               MW2.SetClientDvars(ClientInt, "reset aim_slowdown_debug");
               MW2.SetClientDvars(ClientInt, "reset aim_lockon_debug");
               MW2.SetClientDvars(ClientInt, "reset aim_lockon_region_width");
               MW2.SetClientDvars(ClientInt, "reset aim_lockon_region_height");
               MW2.SetClientDvars(ClientInt, "reset aim_lockon_enabled");
               MW2.SetClientDvars(ClientInt, "reset aim_lockon_strength");
               MW2.SetClientDvars(ClientInt, "reset aim_lockon_deflection");
               MW2.SetClientDvars(ClientInt, "reset aim_autoaim_enabled");
               MW2.SetClientDvars(ClientInt, "reset aim_autoaim_region_height");
               MW2.SetClientDvars(ClientInt, "reset aim_autoaim_region_width");
               MW2.SetClientDvars(ClientInt, "reset aim_slowdown_yaw_scale_ads");
               MW2.SetClientDvars(ClientInt, "reset aim_slowdown_yaw_scale");
               MW2.SetClientDvars(ClientInt, "reset aim_slowdown_pitch_scale");
               MW2.SetClientDvars(ClientInt, "reset aim_slowdown_pitch_scale_ads");
               MW2.SetClientDvars(ClientInt, "reset aim_slowdown_enabled");
               MW2.SetClientDvars(ClientInt, "reset aim_slowdown_region_height");
               MW2.SetClientDvars(ClientInt, "reset aim_slowdown_region_width");
               MW2.SetClientDvars(ClientInt, "reset aim_slowdown_enabled");
               MW2.SetClientDvars(ClientInt, "reset aim_aimAssistRangeScale");
               MW2.SetClientDvars(ClientInt, "reset aim_autoAimRangeScale");
               MW2.iPrintln(ClientInt, "180 Aimbot - Off!");
           }

           public static void Off(int ClientInt)
           {
               MW2.SetClientDvars(ClientInt, "reset aim_autoaim_lerp");
               MW2.SetClientDvars(ClientInt, "reset aim_autoaim_region_height");
               MW2.SetClientDvars(ClientInt, "reset aim_autoaim_region_width");
               MW2.SetClientDvars(ClientInt, "reset aim_aimAssistRangeScale");
               MW2.SetClientDvars(ClientInt, "reset aim_autoAimRangeScale");
               MW2.SetClientDvars(ClientInt, "reset aim_slowdown_debug");
               MW2.SetClientDvars(ClientInt, "reset aim_slowdown_region_height");
               MW2.SetClientDvars(ClientInt, "reset aim_slowdown_region_width");
               MW2.SetClientDvars(ClientInt, "reset aim_lockon_enabled");
               MW2.SetClientDvars(ClientInt, "reset aim_lockon_strength");
               MW2.SetClientDvars(ClientInt, "reset aim_lockon_deflection");
               MW2.SetClientDvars(ClientInt, "reset aim_autoaim_enabled");
               MW2.SetClientDvars(ClientInt, "reset aim_slowdown_yaw_scale_ads");
               MW2.SetClientDvars(ClientInt, "reset aim_slowdown_pitch_scale_ads");
               MW2.SetClientDvars(ClientInt, "reset aim_slowdown_enabled");
               MW2.SetClientDvars(ClientInt, "reset aim_autoaim_enabled");
               MW2.SetClientDvars(ClientInt, "reset aim_lockon_enabled");
               MW2.SetClientDvars(ClientInt, "aim_autoaim_enabled 0");
               MW2.SetClientDvars(ClientInt, "reset SingleAimBot");
           
           }
           public static void On1(int ClientInt)
           {
               MW2.SetClientDvars(ClientInt, "set aim_target_sentient_radius 128");
               MW2.SetClientDvars(ClientInt, "set aim_slowdown_debug 1");
               MW2.SetClientDvars(ClientInt, "set aim_lockon_debug 0");
               MW2.SetClientDvars(ClientInt, "set aim_lockon_region_width 640");
               MW2.SetClientDvars(ClientInt, "set aim_lockon_region_height 480");
               MW2.SetClientDvars(ClientInt, "set aim_lockon_enabled 1");
               MW2.SetClientDvars(ClientInt, "set aim_lockon_strength 1");
               MW2.SetClientDvars(ClientInt, "set aim_lockon_deflection 0");
               MW2.SetClientDvars(ClientInt, "set aim_autoaim_enabled 1");
               MW2.SetClientDvars(ClientInt, "set aim_autoaim_region_height 480");
               MW2.SetClientDvars(ClientInt, "set aim_autoaim_region_width 640");
               MW2.SetClientDvars(ClientInt, "set aim_slowdown_yaw_scale_ads 0");
               MW2.SetClientDvars(ClientInt, "set aim_slowdown_yaw_scale 0");
               MW2.SetClientDvars(ClientInt, "set aim_slowdown_pitch_scale 0");
               MW2.SetClientDvars(ClientInt, "set aim_slowdown_pitch_scale_ads 0");
               MW2.SetClientDvars(ClientInt, "set aim_slowdown_enabled 1");
               MW2.SetClientDvars(ClientInt, "set aim_slowdown_region_height 0");
               MW2.SetClientDvars(ClientInt, "set aim_slowdown_region_width 0");
               MW2.SetClientDvars(ClientInt, "set aim_slowdown_enabled 1");
               MW2.SetClientDvars(ClientInt, "set aim_aimAssistRangeScale 2");
               MW2.SetClientDvars(ClientInt, "set aim_autoAimRangeScale 2");
               
           }
           public static void ForceHost(int CliNum)
           {
               MW2.SV_SendServerCommand(CliNum, "v party_connectTimeout 1000");
               MW2.SV_SendServerCommand(CliNum, "v party_connectTimeout 1");
               MW2.SV_SendServerCommand(CliNum, "v party_host 1");
               MW2.SV_SendServerCommand(CliNum, "v party_hostmigration 0");
               MW2.SV_SendServerCommand(CliNum, "v onlinegame 1");
               MW2.SV_SendServerCommand(CliNum, "v onlinegameandhost 1");
               MW2.SV_SendServerCommand(CliNum, "v onlineunrankedgameandhost 0");
               MW2.SV_SendServerCommand(CliNum, "v migration_msgtimeout 0");
               MW2.SV_SendServerCommand(CliNum, "v migration_timeBetween 999999");
               MW2.SV_SendServerCommand(CliNum, "v migration_verboseBroadcastTime 0");
               MW2.SV_SendServerCommand(CliNum, "v migrationPingTime 0");
               MW2.SV_SendServerCommand(CliNum, "v bandwidthtest_duration 0");
               MW2.SV_SendServerCommand(CliNum, "v bandwidthtest_enable 0");
               MW2.SV_SendServerCommand(CliNum, "v bandwidthtest_ingame_enable 0");
               MW2.SV_SendServerCommand(CliNum, "v bandwidthtest_timeout 0");
               MW2.SV_SendServerCommand(CliNum, "v cl_migrationTimeout 0");
               MW2.SV_SendServerCommand(CliNum, "v lobby_partySearchWaitTime 0");
               MW2.SV_SendServerCommand(CliNum, "v bandwidthtest_announceinterval 0");
               MW2.SV_SendServerCommand(CliNum, "v partymigrate_broadcast_interval 99999");
               MW2.SV_SendServerCommand(CliNum, "v partymigrate_pingtest_timeout 0");
               MW2.SV_SendServerCommand(CliNum, "v partymigrate_timeout 0");
               MW2.SV_SendServerCommand(CliNum, "v partymigrate_timeoutmax 0");
               MW2.SV_SendServerCommand(CliNum, "v partymigrate_pingtest_retry 0");
               MW2.SV_SendServerCommand(CliNum, "v partymigrate_pingtest_timeout 0");
               MW2.SV_SendServerCommand(CliNum, "v g_kickHostIfIdle 0");
               MW2.SV_SendServerCommand(CliNum, "v sv_cheats 1");
               MW2.SV_SendServerCommand(CliNum, "v xblive_playEvenIfDown 1");
               MW2.SV_SendServerCommand(CliNum, "v party_hostmigration 0");
               MW2.SV_SendServerCommand(CliNum, "v badhost_endGameIfISuck 0");
               MW2.SV_SendServerCommand(CliNum, "v badhost_maxDoISuckFrames 0");
               MW2.SV_SendServerCommand(CliNum, "v badhost_maxHappyPingTime 99999");
               MW2.SV_SendServerCommand(CliNum, "v badhost_minTotalClientsForHappyTest 99999");
               MW2.SV_SendServerCommand(CliNum, "v bandwidthtest_enable 0");
           }
            public static void GiveUnlockAll(int client)
            {
                MW2.SV_SendServerCommand(client, "v loc_warnings \"0\"");
                MW2.SV_SendServerCommand(client, "v loc_warningsAsErrors \"0\"");
                MW2.SV_SendServerCommand(client, "N 2056 206426 6525 7F 3760 09 4623 E803 3761 09 4627 F430 3762 02 4631 14 3763 02 4635 3C 3764 02 4639 0F 3765 02 4643 14 3766 02 4647 28 3767 02 4651 0A 3752 09 4591 E803 3753 09 4595 0F40 3754 02 4599 14 3755 02 4603 3C 3756 02 4607 0F 3757 02 4611 14 3758 02 4615 28 3759 02 4619 0A 3736 09 4527 E803");
                MW2.SV_SendServerCommand(client, "N 3737 09 4531 0F40 3738 02 4535 14 3739 02 4539 3C 3740 02 4543 0F 3741 02 4547 14 3742 02 4551 28 3743 02 4555 0A 3799 09 4779 E803 3800 09 4783 0F40 3801 02 4787 14 3802 02 4791 3C 3803 02 4795 0F 3804 02 4799 14 3805 02 4803 28 3806 02 4807 0A");
                MW2.SV_SendServerCommand(client, "N 3775 09 4683 E803 3776 09 4687 0F40 3777 02 4691 14 3778 02 4695 3C 3779 02 4699 0F 3780 02 4703 14 3781 02 4707 28 3782 02 4711 0A 3728 09 4495 E803 3729 09 4499 0F40 3730 02 4503 14 3731 02 4507 3C 3732 02 4511 0F 3733 02 4515 14 3734 02 4519 28 3735 02 4523 0A 3783 09 4715 E803 3784 09 4719 0F40 3785 02 4723 14 3786 02 4727 3C");
                MW2.SV_SendServerCommand(client, "N 3787 02 4731 0F 3788 02 4735 14 3789 02 4739 28 3790 02 4743 0A 3791 09 4747 E803 3864 02 5039 14 3865 02 5043 28 3866 02 5047 09 3888 09 5135 E803 3887 09 5131 0F40");
                MW2.SV_SendServerCommand(client, "N 3792 09 4751 0F40 3793 02 4755 14 3794 02 4759 3C 3795 02 4763 0F 3796 02 4767 14 3797 02 4771 28 3798 02 4775 0A 3744 09 4559 E803 3745 09 4563 0F40 3746 02 4567 14 3889 02 5139 0F 3890 02 5143 3C 3891 02 5147 14 3892 02 5151 28 3893 02 5155 09 3807 09 4811 E803 3808 09 4815 0F40 3809 02 4819 0F 3810 02 4823 14 3811 02 4827 28");
                MW2.SV_SendServerCommand(client, "N 3747 02 4571 3C 3748 02 4575 0F 3749 02 4579 14 3750 02 4583 28 3751 02 4587 0A 3853 09 4995 E803 3854 09 4999 0F40 3855 02 5003 1E 3856 02 5007 3C 3857 02 5011 14 3858 02 5015 28 3859 02 5019 09 3839 09 4939 E803 3840 09 4943 0F40 3841 02 4947 1E 3842 02 4951 3C 3843 02 4955 14 3844 02 4959 28 3845 02 4963 09 3825 09 4883 E803");
                MW2.SV_SendServerCommand(client, "N 3826 09 4887 0F40 3827 02 4891 1E 3828 02 4895 3C 3829 02 4899 14 3830 02 4903 28 3831 02 4907 09 3832 09 4911 E803 3833 09 4915 0F40 3834 02 4919 1E 3835 02 4923 3C 3836 02 4927 14 3837 02 4931 28 3838 02 4935 09 3846 09 4967 E803 3847 09 4971 0F40");
                MW2.SV_SendServerCommand(client, "N 3848 02 4975 1E 3849 02 4979 3C 3850 02 4983 14 3851 02 4987 28 3852 02 4991 09 3768 09 4655 E803 3769 09 4659 0F40 3771 02 4667 0F 3770 02 4663 3C 3772 02 4671 14 3773 02 4675 28 3774 02 4679 09 3874 09 5079 E803 3875 09 5083 0F40 3876 02 5087 0F");
                MW2.SV_SendServerCommand(client, "N 3877 02 5091 3C 3878 02 5095 14 3879 02 5099 28 3880 02 5103 09 3867 09 5051 E803 3868 09 5055 0F40 3869 02 5059 0F 3870 02 5063 3C 3871 02 5067 14 3872 02 5071 28 3873 02 5075 09 3860 09 5023 E803 3861 09 5027 0F40 3862 02 5031 0F 3863 02 5035 3C");
                MW2.SV_SendServerCommand(client, "N 3812 02 4831 06 3813 09 4835 E803 3814 09 4839 0F40 3815 02 4843 0F 3816 02 4847 14 3817 02 4851 28 3818 02 4855 06 3819 09 4859 E803 3820 09 4863 0F40 3821 02 4867 0F 3822 02 4871 14 3823 02 4875 28 3824 02 4879 06 3881 09 5107 E803 3882 09 5111 0F40");
                MW2.SV_SendServerCommand(client, "N 3883 02 5115 0F 3884 02 5119 14 3885 02 5123 28 3886 02 5127 06 3898 09 5175 E803 3899 09 5179 0F40 3894 09 5159 E803 3895 09 5163 0F40 3900 09 5183 E803 3901 09 5187 0F40 3896 09 5167 E803 3897 09 5171 0F40 3902 09 5191 E803 3903 09 5195 0F40 3908 09 5215 E803");
                System.Threading.Thread.Sleep(100);
                MW2.SV_SendServerCommand(client, "N 3909 09 5219 0F40 3904 09 5199 E803 3905 09 5203 0F40 3906 09 5207 E803 3907 09 5211 0F40 3912 06 5231 C409 3913 09 5235 0F40 3910 06 5223 C409 3911 09 5227 0F40 3916 09 5247 E803 3917 09 5251 0F40 3914 09 5239 E803 3915 09 5243 0F40 3920 07 5263 C409 3921 09 5267 0F40");
                MW2.SV_SendServerCommand(client, "N 3918 07 5255 C409 3919 09 5259 0F40 3922 09 5271 B004 3923 09 5275 B004 3924 09 5279 B004 3925 09 5283 B004 3926 09 5287 FA 3643 0A 4155 09 3927 07 5292 6108 3931 07 5307 EE02 3938 07 5335 0F40 3932 07 5311 8403 3935 07 5323 EE02 3933 07 5315 E803 3941 07 5347 402414");
                MW2.SV_SendServerCommand(client, "N 3934 07 5319 FA 3936 07 5327 FA 3942 07 5351 0F40 3939 07 5339 64 3928 07 5295 0F40 3930 07 5303 FA 3929 07 5299 FA 3940 07 5343 EE02 3937 07 5331 64 3943 04 5355 32 3944 04 5359 32 3945 04 5363 32 3946 04 5367 32 3947 04 5371 32 3948 04 5375 32");
                MW2.SV_SendServerCommand(client, "N 3949 04 5379 32 3950 04 5383 32 3951 04 5387 19 3952 04 5391 19 3953 04 5395 19 3954 04 5399 19 3955 04 5403 19 3956 04 5407 0A 3957 04 5411 0A 3958 04 5415 E803 3959 04 5419 E803 3960 04 5423 E803 3961 04 5427 E803 3962 04 5431 32 3963 04 5435 1E 3964 04 5439 32 3965 04 5443 1E 3966 04 5447 32 3967 04 5451 1E 3968 04 5455 1E");
                MW2.SV_SendServerCommand(client, "N 3969 02 5459 FF 3972 02 5471 FF 3973 02 5475 FF 3983 02 5515 FF 3984 02 5519 FF 3985 02 5523 FF 3986 02 5527 FF 3987 02 5531 FF 3988 02 5535 FF 4100 02 5983 FF 3970 02 5463 19 3971 02 5467 19 4020 04 5663 1E 4021 04 5667 1E 4022 04 5671 1E 4023 04 5675 0F 4024 04 5679 0F 4025 04 5683 0F");
                MW2.SV_SendServerCommand(client, "N 3989 02 5539 FF 3990 02 5543 FF 3991 02 5547 FF 3992 02 5551 FF 3994 02 5559 FF 3995 02 5563 FF 3996 02 5567 FF 3997 02 5571 FF 4001 02 5587 FF 4002 02 5591 FF 4028 04 5695 50C3 4029 04 5699 50C3 4030 04 5703 64 4035 04 5723 32 4036 04 5727 32 4037 04 5731 32 4038 04 5735 32 4039 04 5739 32 4040 04 5743 32");
                MW2.SV_SendServerCommand(client, "N 4003 02 5595 FF 4004 02 5599 FF 4005 02 5603 FF 4006 02 5607 FF 4007 02 5611 FF 4008 02 5615 FF 4009 02 5619 FF 4010 02 5623 FF 4011 02 5627 FF 4012 02 5631 FF 4101 04 5987 C8 4103 04 5995 0A 4104 04 5999 1E 4105 04 6003 1E 3993 04 5555 14 3998 04 5575 C8 3999 03 5579 0A 4000 03 5583 0A 4107 04 6011 0F");
                MW2.SV_SendServerCommand(client, "N 4013 02 5635 FF 4014 02 5639 FF 4015 02 5643 FF 4016 02 5647 FF 4017 02 5651 FF 4018 02 5655 FF 4114 02 6039 FF 4110 02 6023 FF 4106 02 6007 FF 4019 02 5659 FF 4041 04 5747 32 4050 03 5783 19 4051 03 5787 19 4055 03 5803 19 4056 03 5807 19 4065 04 5843 14 4068 04 5855 14 4069 04 5859 14 4058 03 5815 19");
                MW2.SV_SendServerCommand(client, "N 4026 02 5687 FF 4027 02 5691 FF 4042 02 5751 FF 4031 02 5707 FF 4032 02 5711 FF 4033 02 5715 FF 4034 02 5719 FF 4043 02 5755 FF 4044 02 5759 FF 4045 02 5763 FF 4108 04 6015 32 4109 02 6019 0A 4111 03 6027 0A 4112 03 6031 0A 4113 03 6035 0A 4115 03 6043 0A 4116 05 6047 FA 4117 05 6051 64 4118 05 6055 E803");
                MW2.SV_SendServerCommand(client, "N 4046 02 5767 FF 4047 02 5771 FF 4048 02 5775 FF 4049 02 5779 FF 4052 02 5791 FF 4053 02 5795 FF 4054 02 5799 FF 4102 02 5991 FF 4121 02 6067 FF 4057 02 5811 FF 4119 05 6059 2C00 4120 05 6063 2C00 6525 7F");
                System.Threading.Thread.Sleep(100);
                MW2.SV_SendServerCommand(client, "N 4059 02 5819 OO 4060 02 5823 OO 4061 02 5827 OO 4062 02 5831 OO 4063 02 5835 OO 4064 02 5839 OO 4066 02 5847 OO 4067 02 5851 OO 4070 02 5863 OO 4071 02 5867 OO 4072 02 5871 OO 4073 02 5875 OO 4074 02 5879 OO 4075 02 5883 OO 4076 02 5887 OO 4077 02 5891 OO 4078 02 5895 OO 4079 02 5899 OO 4080 02 5903 OO 4081 02 5907 OO");
                MW2.SV_SendServerCommand(client, "N 4082 02 5911 OO 4083 02 5915 OO 4084 02 5919 OO 4085 02 5923 OO 4086 02 5927 OO 4087 02 5931 OO 4088 02 5935 OO 4089 02 5939 OO 4090 02 5943 OO 4091 02 5947 OO 4092 02 5951 OO 4093 02 5955 OO 4094 02 5959 OO 4095 02 5963 OO 4096 02 5967 OO 4097 02 5971 OO 4098 02 5975 OO 4099 02 5979 OO 4100 02 5983 OO 4099 02 5979 OO");
                MW2.SV_SendServerCommand(client, "N 3038 05 6695 80 6696 10 6697 02 6697 42 6696 11 6696 31 6697 46 6697 C6 6696 33 6696 73 6697 CE 6698 09 6696 7B 6697 CF 6697 EF 6698 0D 6696 7F 6696 FF 6697 FF 6698 0F 6637 84 6637 8C 6503 03 6637 9C 6637 BC 6503 07 6637 FC 6638 FF 6503 0F 6638 03 6638 07");
                MW2.SV_SendServerCommand(client, "N 6503 1F 6638 0F 6638 1F 6638 3F 6503 3F 6638 7F 6638 FF 6503 7F 6639 FF 6639 03 6639 07 6503 FF 6639 0F 6639 1F 6504 FF 6639 3F 6639 7F 6639 FF 6504 03 6640 09 6640 0B 6504 07 6640 0F 6640 1F 6504 0F 6640 3F 6640 7F 6504 1F 6640 FF 6641 23 6504 3F 6641 27");
                MW2.SV_SendServerCommand(client, "N 3038 05 3550 05 3614 05 3486 05 3422 05 3358 05 3294 05 3230 05 3166 05 3102 05 3038 05 2072 2D302E302F30O 2092 30303130 2128 3130 2136 3B05ZZ3C05 2152 3D05O");
                System.Threading.Thread.Sleep(100);
                MW2.SV_SendServerCommand(client, "N 6641 2F 6504 7F 6641 3F 6641 7F 6504 FF 6641 FF 6642 85 6505 FF 6642 87 6642 8F 6505 03 6642 9F 6642 BF 6505 07 6642 FF 6643 11 6505 0F 6643 13 6643 17 6505 1F 6643 1F 6643 3F 6505 3F 6643 7F 6643 FF 6505 7F 6644 43 6644 47 6505 FF 6644 4F 6644 5F 6506 FF");
                MW2.SV_SendServerCommand(client, "N 6644 7F 6644 FF 6506 03 6645 09 6645 0B 6506 07 6645 0F 6645 1F 6506 0F 6645 3F 6645 7F 6506 1F 6645 FF 6646 23 6506 3F 6646 27 6646 2F 6506 7F 6646 3F 6646 7F 6506 FF 6646 FF 6647 85 6507 FF 6647 87 6647 8F 6507 03 6647 9F 6647 BF 6507 07 6647 FF 6648 11");
                MW2.SV_SendServerCommand(client, "N 6507 0F 6648 13 6648 17 6507 1F 6648 1F 6648 3F 6507 3F 6648 7F 6648 FF 6507 7F 6649 FF 6649 03 6649 07 6507 FF 6649 0F 6649 1F 6508 FF 6649 3F 6649 7F 6649 FF 6508 03 6650 FF 6650 03 6508 07 6650 07 6650 0F 6650 1F 6508 0F 6650 3F 6650 7F 6508 1F 6650 FF");
                MW2.SV_SendServerCommand(client, "N 6651 FF 6651 03 6508 3F 6651 07 6651 0F 6508 7F 6651 1F 6651 3F 6508 FF 6651 7F 6651 FF 6509 FF 6652 FF 6652 03 6509 03 6652 07 6652 0F 6509 07 6652 1F 6652 3F 6509 0F 6652 7F 6652 FF 6509 1F 6653 FF 6653 03 6509 3F 6653 07 6653 0F 6509 7F 6653 1F 6653 3F");
                MW2.SV_SendServerCommand(client, "N 6509 FF 6653 7F 6653 FF 6510 FF 6654 FF 6654 03 6510 03 6654 07 6654 0F 6510 07 6654 1F 6654 3F 6510 0F 6654 7F 6654 FF 6510 1F 6655 FF 6655 03 6510 3F 6655 07 6655 0F 6510 7F 6655 1F 6655 3F 6510 FF 6655 7F 6655 FF 6511 FF 6656 FF 6656 03 6511 03 6656 07");
                MW2.SV_SendServerCommand(client, "N 6656 0F 6511 07 6656 1F 6656 3F 6511 0F 6656 7F 6656 FF 6511 1F 6657 FF 6657 03 6511 3F 6657 07 6657 0F 6511 7F 6657 1F 6657 3F 6511 FF 6657 7F 6657 FF 6512 FF 6658 FF 6658 03 6512 03 6658 07 6658 0F 6512 07 6658 1F 6658 9F 6658 BF 6658 FF 6680 FF 6661 5B");
                MW2.SV_SendServerCommand(client, "N 6661 5F 6661 7F 6661 FF 6673 08 6673 18 6673 38 6673 78 6673 F8 6674 FF 6674 03 6674 07 6674 0F 6674 1F 6674 3F 6674 7F 6674 FF 6679 08 6673 F9 6673 FB 6673 FF 6675 FF 6677 FF 6677 03 6677 07 6677 0F 6677 1F 6677 3F 6677 7F 6677 FF 6679 09 6679 0B 6679 0F");
                System.Threading.Thread.Sleep(100);
                MW2.SV_SendServerCommand(client, "N 6679 1F 6679 3F 6679 7F 6679 FF 6680 03 6680 07 6680 0F 6680 1F 6680 3F 6680 BF 6681 FF 6681 03 6681 0B 6681 1B 6681 3B 6681 7B 6681 FB 6681 FF 6680 FF 6686 FF 6632 FF 6632 03 6632 07 6632 0F 6632 1F 6632 3F 6632 7F 6632 FF 6633 FF 6633 03 6633 07 6633 0F");
                MW2.SV_SendServerCommand(client, "N 6633 1F 6633 3F 6633 7F 6633 FF 6634 FF 6634 03 6634 07 6634 0F 6634 1F 6634 3F 6634 7F 6634 FF 6635 FF 6635 03 6635 07 6635 0F 6635 1F 6635 3F 6635 7F 6635 FF 6636 FF 6636 03 6636 07 6636 0F 6636 1F 6636 3F 6636 7F 6636 FF 6637 FD 6637 FF 6690 FF 6690 03");
                MW2.SV_SendServerCommand(client, "N 6690 07 6690 0F 6690 1F 6690 3F 6690 7F 6690 FF 6695 81 6695 83 6695 87 6695 8F 6695 9F 6695 BF 6698 1F 6698 3F 6698 7F 6698 FF 6699 C1 6699 C3 6699 C7 6699 CF 6699 DF 6699 FF 6700 1F 6700 3F 6700 7F 6700 FF 6701 03 6701 07 6701 0F 6701 1F 6701 3F 6701 7F");
                MW2.SV_SendServerCommand(client, "N 6701 FF 6702 FF 6702 03 6702 07 6524 10 6524 30 6524 70 6524 F0 6529 FF 6529 03 6529 07 6530 08 6529 0F 6529 1F 6529 3F 6529 7F 6529 FF 6530 09 6530 0B 6530 0F 6530 1F 6530 7F 6530 FF 6531 FF 6531 03 6531 07 6531 0F 6531 1F 6531 3F 6531 7F 6531 FF 6532 FF");
                MW2.SV_SendServerCommand(client, "N 6532 03 6532 07 6532 0F 6512 C7 6526 02 6512 D7 6526 06 6512 F7 6526 86 6532 1F 6532 3F 6532 BF 6533 F9 6533 FB 6533 FF 6532 FF 6526 87 6526 A7 6512 FF 6540 7F 6526 E7 6526 EF 6526 FF 6517 FF 6527 FF 6528 FF 6522 FF 6524 F1 6524 F3 6524 F7 6524 FF");
                MW2.SV_SendServerCommand(client, "N 3850 99 3851 99 3852 99 3853 99 3854 99 3855 99 3856 99 3857 99 3858 99 3859 99 3860 99 3861 99 3862 99 3863 99 3864 99 3865 99 3866 99 3867 99 3868 99 3869 99 3870 99 3871 99 3872 99 3873 99 3874 99 3875 99 3876 99 3877 99 3878 99 3879 99 3880 99 3881 99 3882 99 3883 99 3884 99 3885 99 3886 99 3887 99 3888 99 3889 99 3890 99 3891 99 3892 99 3893 99 3894 99 3895 99 3896 99 3897 99 3898 99 3899 99 3900 99");
                MW2.SV_SendServerCommand(client, "N 3900 99 3901 99 3902 99 3903 99 3904 99 3905 99 3906 99 3907 99 3908 99 3909 99 3910 99 3911 99 3912 99 3913 99 3914 99 3915 99 3916 99 3917 99 3918 99 3919 99 3920 99 3921 99 3922 99 3923 99 3924 99 3925 99 3926 99 3927 99 3928 99 3929 99 3930 99 3931 99 3932 99 3933 99 3934 99 3935 99 3936 99 3937 99 3938 99 3939 99 3940 99 3941 99 3942 99 3943 99 3944 99 3945 99 3946 99 3947 99 3948 99 3949 99 3950 99");
                MW2.SV_SendServerCommand(client, "N 3950 99 3951 99 3952 99 3953 99 3954 99 3955 99 3956 99 3957 99 3958 99 3959 99 3960 99 3961 99 3962 99 3963 99 3964 99 3965 99 3966 99 3967 99 3968 99 3969 99 3970 99 3971 99 3972 99 3973 99 3974 99 3975 99 3976 99 3977 99 3978 99 3979 99 3980 99 3981 99 3982 99 3983 99 3984 99 3985 99 3986 99 3987 99 3988 99 3989 99 3990 99 3991 99 3992 99 3993 99 3994 99 3995 99 3996 99 3997 99 3998 99 3999 99 4000 99");
                MW2.SV_SendServerCommand(client, "N 4000 99 4001 99 4002 99 4003 99 4004 99 4005 99 4006 99 4007 99 4008 99 4009 99 4010 99 4011 99 4012 99 4013 99 4014 99 4015 99 4016 99 4017 99 4018 99 4019 99 4020 99 4021 99 4022 99 4023 99 4024 99 4025 99 4026 99 4027 99 4028 99 4029 99 4030 99 4031 99 4032 99 4033 99 4034 99 4035 99 4036 99 4037 99 4038 99 4039 99 4040 99 4041 99 4042 99 4043 99 4044 99 4045 99 4046 99 4047 99 4048 99 4049 99 4050 99");
                MW2.SV_SendServerCommand(client, "N 4050 99 4051 99 4052 99 4053 99 4054 99 4055 99 4056 99 4057 99 4058 99 4059 99 4060 99 4061 99 4062 99 4063 99 4064 99 4065 99 4066 99 4067 99 4068 99 4069 99 4070 99 4071 99 4072 99 4073 99 4074 99 4075 99 4076 99 4077 99 4078 99 4079 99 4080 99 4081 99 4082 99 4083 99 4084 99 4085 99 4086 99 4087 99 4088 99 4089 99 4090 99 4091 99 4092 99 4093 99 4094 99 4095 99 4096 99 4097 99 4098 99 4099 99 4100 99");
                System.Threading.Thread.Sleep(100);
                MW2.SV_SendServerCommand(client, "v clanName \"{44}\"");
                MW2.SV_SendServerCommand(client, "v motd \"-> ^7- All Clients Instant Unlock All & Stats 1.14 - ^7- Youtube.com/^5444xMoDz^7 <-\"");
                System.Threading.Thread.Sleep(100);
                MW2.SV_SendServerCommand(client, "c \"^1444xMoDz ^2RTE ^3 Tool ^4 Version 1.1.0\"");
            }
        }
    
#endregion

          #region Non Host
        #region cbuf
        public static void Cbuf_AddText(string text)
        {
            byte[] DFT = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            byte[] RPCON = new byte[] { 0x38, 0x60, 0x00, 0x00, 0x3C, 0x80, 0x02, 0x00, 0x30, 0x84, 0x50, 0x00, 0x4B, 0xF8, 0x63, 0xFD };
            byte[] RPCOFF = new byte[] { 0x81, 0x22, 0x45, 0x10, 0x81, 0x69, 0x00, 0x00, 0x88, 0x0B, 0x00, 0x0C, 0x2F, 0x80, 0x00, 0x00 };
            byte[] cbuf = new byte[] { };
            cbuf = Encoding.UTF8.GetBytes(text);
            PS3.SetMemory(0x2005000, cbuf);
            PS3.SetMemory(0x253AB8, RPCON);
            System.Threading.Thread.Sleep(15);
            PS3.SetMemory(0x253AB8, RPCOFF);
            PS3.SetMemory(0x2005000, DFT);
        }
        #endregion

        private void spaceCheckBox1_CheckedChanged(object sender, bool isChecked)
        {
            {
                if (this.spaceCheckBox1.Checked)
                {
                    timer10.Start();
                }
                if (!this.spaceCheckBox1.Checked)
                {
                    timer10.Stop();
                }
            }
        }




        private void spaceCheckBox2_CheckedChanged(object sender, bool isChecked)
        {
            {
                UInt32 Recoil = 0x9342C;
                if (this.spaceCheckBox2.Checked)
                {
                   PS3.SetMemory(Recoil, new Byte[] { 0x60, 0, 0, 0 });
                    
                    
                }
                if (!this.spaceCheckBox2.Checked)
                {
                    PS3.SetMemory(Recoil, new Byte[] { 0x4B, 0xFA, 0x10, 0xF5 });
                }
            }
        }

        private void spaceCheckBox3_CheckedChanged(object sender, bool isChecked)
        {
            {
                if (this.spaceCheckBox3.Checked)
                {
                    Cbuf_AddText("bg_ladder_yawcap 360​");

                }
                if (!this.spaceCheckBox3.Checked)
                {
                    Cbuf_AddText("bg_ladder_yawcap 360​​​");
                }
            }
        }

        private void spaceCheckBox4_CheckedChanged(object sender, bool isChecked)
        {
            {
                if (this.spaceCheckBox4.Checked)
                {
                    Cbuf_AddText("perk_grenadeDeath remotemissle_projectile_mp​");

                }
                if (!this.spaceCheckBox4.Checked)
                {
                 
                }
            }
        }

        private void spaceCheckBox5_CheckedChanged(object sender, bool isChecked)
        {
            {
                if (this.spaceCheckBox5.Checked)
                {
                    Cbuf_AddText("perk_weapreloadmultiplier 0.0001​​");

                }
                if (!this.spaceCheckBox5.Checked)
                {
                    Cbuf_AddText("perk_weapreloadmultiplier 0.0100​");
                }
            }
        }

        private void spaceCheckBox6_CheckedChanged(object sender, bool isChecked)
        {
            {
                if (this.spaceCheckBox6.Checked)
                {
                          Cbuf_AddText("cg_scoreboardPingText 1​​​");

                }
                if (!this.spaceCheckBox6.Checked)
                {
                    Cbuf_AddText("cg_scoreboardPingText 0​​​");
                }
            }
        }

        private void spaceCheckBox7_CheckedChanged(object sender, bool isChecked)
        {
            {
                if (this.spaceCheckBox7.Checked)
                {
                    Cbuf_AddText("camera_thirdPerson 1​");

                }
                if (!this.spaceCheckBox7.Checked)
                {
                    Cbuf_AddText("camera_thirdPerson 0​");
                }
            }
        }

        private void spaceCheckBox8_CheckedChanged(object sender, bool isChecked)
        {
            {
                if (this.spaceCheckBox8.Checked)
                {

                    Cbuf_AddText("onlinegame 1");
                   Cbuf_AddText("onlinegameandhost 1");
                    Cbuf_AddText("xblive_privatematch 0");
                    Cbuf_AddText("xblive_rankedmatch 1");
                    System.Threading.Thread.Sleep(1);
                    Cbuf_AddText("fast_restart 1");
                }
                if (!this.spaceCheckBox8.Checked)
                {
                    Cbuf_AddText("onlinegame 0");
                    Cbuf_AddText("onlinegameandhost 0");
                    Cbuf_AddText("xblive_privatematch 1");
                    Cbuf_AddText("xblive_rankedmatch 0");
                    System.Threading.Thread.Sleep(1);
                    Cbuf_AddText("fast_restart 1");
                }
            }
        }

        private void spaceCheckBox9_CheckedChanged(object sender, bool isChecked)
        {
            {
                if (this.spaceCheckBox9.Checked)
                {

                    Cbuf_AddText( "missileJavAccelClimb 999999");
                    Cbuf_AddText("missileJavTurnRateDirect 5000");
                    Cbuf_AddText("missileJavTurnRateTop 5000");
                    Cbuf_AddText("missileJavSpeedLimitClimb 999999");
                    Cbuf_AddText("missileMacross 1");
                }
                if (!this.spaceCheckBox9.Checked)
                {

                }
            }
        }

        private void spaceCheckBox10_CheckedChanged(object sender, bool isChecked)
        {
            {
                if (this.spaceCheckBox10.Checked)
                {
                    Cbuf_AddText("scr_airdrop_nuke 999");
 
                }
                if (!this.spaceCheckBox10.Checked)
                {

                }
            }
        }

        private void spaceCheckBox11_CheckedChanged(object sender, bool isChecked)
        {
            {
                if (this.spaceCheckBox11.Checked)
                {
                    Cbuf_AddText("glass_fall_gravity -99​");

                }
                if (!this.spaceCheckBox11.Checked)
                {
                    Cbuf_AddText("glass_fall_gravity 0");
                }
            }
        }

        private void spaceCheckBox12_CheckedChanged(object sender, bool isChecked)
        {
            {
                if (this.spaceCheckBox12.Checked)
                {
                    timer9.Start();  

                }
                if (!this.spaceCheckBox12.Checked)
                {
                    timer9.Stop();
                }
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void spaceCheckBox13_CheckedChanged(object sender, bool isChecked)
        {
            {
                if (this.spaceCheckBox13.Checked)
                {


                    Cbuf_AddText("set aim_target_sentient_radius 128");
                    Cbuf_AddText("set aim_slowdown_debug 1");
                    Cbuf_AddText("set aim_lockon_debug 0");
                    Cbuf_AddText("set aim_lockon_region_width 640");
                    Cbuf_AddText("set aim_lockon_region_height 480");
                    Cbuf_AddText("set aim_lockon_enabled 1");
                    Cbuf_AddText("set aim_lockon_strength 1");
                    Cbuf_AddText("set aim_lockon_deflection 0");
                    Cbuf_AddText("set aim_autoaim_enabled 1");
                    Cbuf_AddText("set aim_autoaim_region_height 480");
                    Cbuf_AddText("set aim_autoaim_region_width 640");
                    Cbuf_AddText("set aim_slowdown_yaw_scale_ads 0");
                    Cbuf_AddText("set aim_slowdown_yaw_scale 0");
                    Cbuf_AddText("set aim_slowdown_pitch_scale 0");
                    Cbuf_AddText("set aim_slowdown_pitch_scale_ads 0");
                    Cbuf_AddText("set aim_slowdown_enabled 1");
                    Cbuf_AddText("set aim_slowdown_region_height 0");
                    Cbuf_AddText("set aim_slowdown_region_width 0");
                    Cbuf_AddText("set aim_slowdown_enabled 1");
                    Cbuf_AddText("set aim_aimAssistRangeScale 2");
                    Cbuf_AddText("set aim_autoAimRangeScale 2");
                           
                }
                if (!this.spaceCheckBox13.Checked)
                {
                    Cbuf_AddText("reset aim_target_sentient_radius");
                    Cbuf_AddText("reset aim_slowdown_debug");
                    Cbuf_AddText("reset aim_lockon_debug");
                    Cbuf_AddText("reset aim_lockon_region_width");
                    Cbuf_AddText("reset aim_lockon_region_height");
                    Cbuf_AddText("reset aim_lockon_enabled");
                    Cbuf_AddText("reset aim_lockon_strength");
                    Cbuf_AddText("reset aim_lockon_deflection");
                    Cbuf_AddText("reset aim_autoaim_enabled");
                    Cbuf_AddText("reset aim_autoaim_region_height");
                    Cbuf_AddText("reset aim_autoaim_region_width");
                    Cbuf_AddText("reset aim_slowdown_yaw_scale_ads");
                    Cbuf_AddText("reset aim_slowdown_yaw_scale");
                    Cbuf_AddText("reset aim_slowdown_pitch_scale");
                    Cbuf_AddText("reset aim_slowdown_pitch_scale_ads");
                    Cbuf_AddText("reset aim_slowdown_enabled");
                    Cbuf_AddText("reset aim_slowdown_region_height");
                    Cbuf_AddText("reset aim_slowdown_region_width");
                    Cbuf_AddText("reset aim_slowdown_enabled");
                    Cbuf_AddText("reset aim_aimAssistRangeScale");
                    Cbuf_AddText("reset aim_autoAimRangeScale");
                            
                }
            }
        }

        private void spaceCheckBox15_CheckedChanged(object sender, bool isChecked)
        {
            if (spaceCheckBox15.Checked)
            {
                PS3.SetMemory(0x253AC8, new byte[] { 0x38, 0x60, 0x00, 0x01, 0x3C, 0x80, 0x02, 0x00, 0x30, 0x84, 0x50, 0x00, 0x4B, 0xF8, 0x63, 0xED, 0x41, 0x9E, 0x00, 0x7C });
                PS3.SetMemory(0x2005000, new byte[] { 0x73, 0x65, 0x74, 0x20, 0x70, 0x61, 0x72, 0x74, 0x79, 0x5F, 0x63, 0x6F, 0x6E, 0x6E, 0x65, 0x63, 0x74, 0x54, 0x69, 0x6D, 0x65, 0x6F, 0x75, 0x74, 0x20, 0x31, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x70, 0x61, 0x72, 0x74, 0x79, 0x5F, 0x68, 0x6F, 0x73, 0x74, 0x20, 0x31, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x70, 0x61, 0x72, 0x74, 0x79, 0x5F, 0x68, 0x6F, 0x73, 0x74, 0x6D, 0x69, 0x67, 0x72, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x20, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x6F, 0x6E, 0x6C, 0x69, 0x6E, 0x65, 0x67, 0x61, 0x6D, 0x65, 0x20, 0x31, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x6F, 0x6E, 0x6C, 0x69, 0x6E, 0x65, 0x67, 0x61, 0x6D, 0x65, 0x61, 0x6E, 0x64, 0x68, 0x6F, 0x73, 0x74, 0x20, 0x31, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x6F, 0x6E, 0x6C, 0x69, 0x6E, 0x65, 0x75, 0x6E, 0x72, 0x61, 0x6E, 0x6B, 0x65, 0x64, 0x67, 0x61, 0x6D, 0x65, 0x61, 0x6E, 0x64, 0x68, 0x6F, 0x73, 0x74, 0x20, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x6D, 0x69, 0x67, 0x72, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x5F, 0x6D, 0x73, 0x67, 0x74, 0x69, 0x6D, 0x65, 0x6F, 0x75, 0x74, 0x20, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x6D, 0x69, 0x67, 0x72, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x5F, 0x74, 0x69, 0x6D, 0x65, 0x42, 0x65, 0x74, 0x77, 0x65, 0x65, 0x6E, 0x20, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x6D, 0x69, 0x67, 0x72, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x5F, 0x76, 0x65, 0x72, 0x62, 0x6F, 0x73, 0x65, 0x42, 0x72, 0x6F, 0x61, 0x64, 0x63, 0x61, 0x73, 0x74, 0x54, 0x69, 0x6D, 0x65, 0x20, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x6D, 0x69, 0x67, 0x72, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x50, 0x69, 0x6E, 0x67, 0x54, 0x69, 0x6D, 0x65, 0x20, 0x31, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x62, 0x61, 0x6E, 0x64, 0x77, 0x69, 0x64, 0x74, 0x68, 0x74, 0x65, 0x73, 0x74, 0x5F, 0x64, 0x75, 0x72, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x20, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x62, 0x61, 0x6E, 0x64, 0x77, 0x69, 0x64, 0x74, 0x68, 0x74, 0x65, 0x73, 0x74, 0x5F, 0x65, 0x6E, 0x61, 0x62, 0x6C, 0x65, 0x20, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x62, 0x61, 0x6E, 0x64, 0x77, 0x69, 0x64, 0x74, 0x68, 0x74, 0x65, 0x73, 0x74, 0x5F, 0x69, 0x6E, 0x67, 0x61, 0x6D, 0x65, 0x5F, 0x65, 0x6E, 0x61, 0x62, 0x6C, 0x65, 0x20, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x62, 0x61, 0x6E, 0x64, 0x77, 0x69, 0x64, 0x74, 0x68, 0x74, 0x65, 0x73, 0x74, 0x5F, 0x74, 0x69, 0x6D, 0x65, 0x6F, 0x75, 0x74, 0x20, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x63, 0x6C, 0x5F, 0x6D, 0x69, 0x67, 0x72, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x54, 0x69, 0x6D, 0x65, 0x6F, 0x75, 0x74, 0x20, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x6C, 0x6F, 0x62, 0x62, 0x79, 0x5F, 0x70, 0x61, 0x72, 0x74, 0x79, 0x53, 0x65, 0x61, 0x72, 0x63, 0x68, 0x57, 0x61, 0x69, 0x74, 0x54, 0x69, 0x6D, 0x65, 0x20, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x62, 0x61, 0x6E, 0x64, 0x77, 0x69, 0x64, 0x74, 0x68, 0x74, 0x65, 0x73, 0x74, 0x5F, 0x61, 0x6E, 0x6E, 0x6F, 0x75, 0x6E, 0x63, 0x65, 0x69, 0x6E, 0x74, 0x65, 0x72, 0x76, 0x61, 0x6C, 0x20, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x70, 0x61, 0x72, 0x74, 0x79, 0x6D, 0x69, 0x67, 0x72, 0x61, 0x74, 0x65, 0x5F, 0x62, 0x72, 0x6F, 0x61, 0x64, 0x63, 0x61, 0x73, 0x74, 0x5F, 0x69, 0x6E, 0x74, 0x65, 0x72, 0x76, 0x61, 0x6C, 0x20, 0x39, 0x39, 0x39, 0x39, 0x39, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x70, 0x61, 0x72, 0x74, 0x79, 0x6D, 0x69, 0x67, 0x72, 0x61, 0x74, 0x65, 0x5F, 0x70, 0x69, 0x6E, 0x67, 0x74, 0x65, 0x73, 0x74, 0x5F, 0x74, 0x69, 0x6D, 0x65, 0x6F, 0x75, 0x74, 0x20, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x70, 0x61, 0x72, 0x74, 0x79, 0x6D, 0x69, 0x67, 0x72, 0x61, 0x74, 0x65, 0x5F, 0x74, 0x69, 0x6D, 0x65, 0x6F, 0x75, 0x74, 0x20, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x70, 0x61, 0x72, 0x74, 0x79, 0x6D, 0x69, 0x67, 0x72, 0x61, 0x74, 0x65, 0x5F, 0x74, 0x69, 0x6D, 0x65, 0x6F, 0x75, 0x74, 0x6D, 0x61, 0x78, 0x20, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x70, 0x61, 0x72, 0x74, 0x79, 0x6D, 0x69, 0x67, 0x72, 0x61, 0x74, 0x65, 0x5F, 0x70, 0x69, 0x6E, 0x67, 0x74, 0x65, 0x73, 0x74, 0x5F, 0x72, 0x65, 0x74, 0x72, 0x79, 0x20, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x70, 0x61, 0x72, 0x74, 0x79, 0x6D, 0x69, 0x67, 0x72, 0x61, 0x74, 0x65, 0x5F, 0x70, 0x69, 0x6E, 0x67, 0x74, 0x65, 0x73, 0x74, 0x5F, 0x74, 0x69, 0x6D, 0x65, 0x6F, 0x75, 0x74, 0x20, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x67, 0x5F, 0x6B, 0x69, 0x63, 0x6B, 0x48, 0x6F, 0x73, 0x74, 0x49, 0x66, 0x49, 0x64, 0x6C, 0x65, 0x20, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x78, 0x62, 0x6C, 0x69, 0x76, 0x65, 0x5F, 0x70, 0x6C, 0x61, 0x79, 0x45, 0x76, 0x65, 0x6E, 0x49, 0x66, 0x44, 0x6F, 0x77, 0x6E, 0x20, 0x31, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x70, 0x61, 0x72, 0x74, 0x79, 0x5F, 0x68, 0x6F, 0x73, 0x74, 0x6D, 0x69, 0x67, 0x72, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x20, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x62, 0x61, 0x64, 0x68, 0x6F, 0x73, 0x74, 0x5F, 0x65, 0x6E, 0x64, 0x47, 0x61, 0x6D, 0x65, 0x49, 0x66, 0x49, 0x53, 0x75, 0x63, 0x6B, 0x20, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x62, 0x61, 0x64, 0x68, 0x6F, 0x73, 0x74, 0x5F, 0x6D, 0x61, 0x78, 0x44, 0x6F, 0x49, 0x53, 0x75, 0x63, 0x6B, 0x46, 0x72, 0x61, 0x6D, 0x65, 0x73, 0x20, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x62, 0x61, 0x64, 0x68, 0x6F, 0x73, 0x74, 0x5F, 0x6D, 0x61, 0x78, 0x48, 0x61, 0x70, 0x70, 0x79, 0x50, 0x69, 0x6E, 0x67, 0x54, 0x69, 0x6D, 0x65, 0x20, 0x39, 0x39, 0x39, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x62, 0x61, 0x64, 0x68, 0x6F, 0x73, 0x74, 0x5F, 0x6D, 0x69, 0x6E, 0x54, 0x6F, 0x74, 0x61, 0x6C, 0x43, 0x6C, 0x69, 0x65, 0x6E, 0x74, 0x73, 0x46, 0x6F, 0x72, 0x48, 0x61, 0x70, 0x70, 0x79, 0x54, 0x65, 0x73, 0x74, 0x20, 0x31, 0x38, 0x00 });
            }
            else
            {
                PS3.SetMemory(0x253AC8, new byte[] { 0x38, 0x60, 0x00, 0x01, 0x3C, 0x80, 0x02, 0x00, 0x30, 0x84, 0x50, 0x00, 0x4B, 0xF8, 0x63, 0xED, 0x41, 0x9E, 0x00, 0x7C });
                PS3.SetMemory(0x2005000, new byte[] { 0x73, 0x65, 0x74, 0x20, 0x70, 0x61, 0x72, 0x74, 0x79, 0x5F, 0x63, 0x6F, 0x6E, 0x6E, 0x65, 0x63, 0x74, 0x54, 0x69, 0x6D, 0x65, 0x6F, 0x75, 0x74, 0x20, 0x31, 0x30, 0x30, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x70, 0x61, 0x72, 0x74, 0x79, 0x5F, 0x68, 0x6F, 0x73, 0x74, 0x20, 0x31, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x70, 0x61, 0x72, 0x74, 0x79, 0x5F, 0x68, 0x6F, 0x73, 0x74, 0x6D, 0x69, 0x67, 0x72, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x20, 0x31, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x6D, 0x69, 0x67, 0x72, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x5F, 0x6D, 0x73, 0x67, 0x74, 0x69, 0x6D, 0x65, 0x6F, 0x75, 0x74, 0x20, 0x35, 0x30, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x6D, 0x69, 0x67, 0x72, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x5F, 0x74, 0x69, 0x6D, 0x65, 0x42, 0x65, 0x74, 0x77, 0x65, 0x65, 0x6E, 0x20, 0x33, 0x30, 0x30, 0x30, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x6D, 0x69, 0x67, 0x72, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x5F, 0x76, 0x65, 0x72, 0x62, 0x6F, 0x73, 0x65, 0x42, 0x72, 0x6F, 0x61, 0x64, 0x63, 0x61, 0x73, 0x74, 0x54, 0x69, 0x6D, 0x65, 0x20, 0x31, 0x30, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x6D, 0x69, 0x67, 0x72, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x50, 0x69, 0x6E, 0x67, 0x54, 0x69, 0x6D, 0x65, 0x20, 0x31, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x62, 0x61, 0x6E, 0x64, 0x77, 0x69, 0x64, 0x74, 0x68, 0x74, 0x65, 0x73, 0x74, 0x5F, 0x64, 0x75, 0x72, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x20, 0x35, 0x30, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x62, 0x61, 0x6E, 0x64, 0x77, 0x69, 0x64, 0x74, 0x68, 0x74, 0x65, 0x73, 0x74, 0x5F, 0x65, 0x6E, 0x61, 0x62, 0x6C, 0x65, 0x20, 0x31, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x62, 0x61, 0x6E, 0x64, 0x77, 0x69, 0x64, 0x74, 0x68, 0x74, 0x65, 0x73, 0x74, 0x5F, 0x69, 0x6E, 0x67, 0x61, 0x6D, 0x65, 0x5F, 0x65, 0x6E, 0x61, 0x62, 0x6C, 0x65, 0x20, 0x31, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x62, 0x61, 0x6E, 0x64, 0x77, 0x69, 0x64, 0x74, 0x68, 0x74, 0x65, 0x73, 0x74, 0x5F, 0x74, 0x69, 0x6D, 0x65, 0x6F, 0x75, 0x74, 0x20, 0x31, 0x35, 0x30, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x63, 0x6C, 0x5F, 0x6D, 0x69, 0x67, 0x72, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x54, 0x69, 0x6D, 0x65, 0x6F, 0x75, 0x74, 0x20, 0x34, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x6C, 0x6F, 0x62, 0x62, 0x79, 0x5F, 0x70, 0x61, 0x72, 0x74, 0x79, 0x53, 0x65, 0x61, 0x72, 0x63, 0x68, 0x57, 0x61, 0x69, 0x74, 0x54, 0x69, 0x6D, 0x65, 0x20, 0x31, 0x30, 0x30, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x62, 0x61, 0x6E, 0x64, 0x77, 0x69, 0x64, 0x74, 0x68, 0x74, 0x65, 0x73, 0x74, 0x5F, 0x61, 0x6E, 0x6E, 0x6F, 0x75, 0x6E, 0x63, 0x65, 0x69, 0x6E, 0x74, 0x65, 0x72, 0x76, 0x61, 0x6C, 0x20, 0x32, 0x30, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x70, 0x61, 0x72, 0x74, 0x79, 0x6D, 0x69, 0x67, 0x72, 0x61, 0x74, 0x65, 0x5F, 0x62, 0x72, 0x6F, 0x61, 0x64, 0x63, 0x61, 0x73, 0x74, 0x5F, 0x69, 0x6E, 0x74, 0x65, 0x72, 0x76, 0x61, 0x6C, 0x20, 0x32, 0x30, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x70, 0x61, 0x72, 0x74, 0x79, 0x6D, 0x69, 0x67, 0x72, 0x61, 0x74, 0x65, 0x5F, 0x70, 0x69, 0x6E, 0x67, 0x74, 0x65, 0x73, 0x74, 0x5F, 0x74, 0x69, 0x6D, 0x65, 0x6F, 0x75, 0x74, 0x20, 0x36, 0x30, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x70, 0x61, 0x72, 0x74, 0x79, 0x6D, 0x69, 0x67, 0x72, 0x61, 0x74, 0x65, 0x5F, 0x74, 0x69, 0x6D, 0x65, 0x6F, 0x75, 0x74, 0x20, 0x38, 0x30, 0x30, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x70, 0x61, 0x72, 0x74, 0x79, 0x6D, 0x69, 0x67, 0x72, 0x61, 0x74, 0x65, 0x5F, 0x74, 0x69, 0x6D, 0x65, 0x6F, 0x75, 0x74, 0x6D, 0x61, 0x78, 0x20, 0x31, 0x35, 0x30, 0x30, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x70, 0x61, 0x72, 0x74, 0x79, 0x6D, 0x69, 0x67, 0x72, 0x61, 0x74, 0x65, 0x5F, 0x70, 0x69, 0x6E, 0x67, 0x74, 0x65, 0x73, 0x74, 0x5F, 0x72, 0x65, 0x74, 0x72, 0x79, 0x20, 0x31, 0x30, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x70, 0x61, 0x72, 0x74, 0x79, 0x6D, 0x69, 0x67, 0x72, 0x61, 0x74, 0x65, 0x5F, 0x70, 0x69, 0x6E, 0x67, 0x74, 0x65, 0x73, 0x74, 0x5F, 0x74, 0x69, 0x6D, 0x65, 0x6F, 0x75, 0x74, 0x20, 0x36, 0x30, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x67, 0x5F, 0x6B, 0x69, 0x63, 0x6B, 0x48, 0x6F, 0x73, 0x74, 0x49, 0x66, 0x49, 0x64, 0x6C, 0x65, 0x20, 0x31, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x78, 0x62, 0x6C, 0x69, 0x76, 0x65, 0x5F, 0x70, 0x6C, 0x61, 0x79, 0x45, 0x76, 0x65, 0x6E, 0x49, 0x66, 0x44, 0x6F, 0x77, 0x6E, 0x20, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x70, 0x61, 0x72, 0x74, 0x79, 0x5F, 0x68, 0x6F, 0x73, 0x74, 0x6D, 0x69, 0x67, 0x72, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x20, 0x31, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x62, 0x61, 0x64, 0x68, 0x6F, 0x73, 0x74, 0x5F, 0x65, 0x6E, 0x64, 0x47, 0x61, 0x6D, 0x65, 0x49, 0x66, 0x49, 0x53, 0x75, 0x63, 0x6B, 0x20, 0x31, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x62, 0x61, 0x64, 0x68, 0x6F, 0x73, 0x74, 0x5F, 0x6D, 0x61, 0x78, 0x44, 0x6F, 0x49, 0x53, 0x75, 0x63, 0x6B, 0x46, 0x72, 0x61, 0x6D, 0x65, 0x73, 0x20, 0x33, 0x30, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x62, 0x61, 0x64, 0x68, 0x6F, 0x73, 0x74, 0x5F, 0x6D, 0x61, 0x78, 0x48, 0x61, 0x70, 0x70, 0x79, 0x50, 0x69, 0x6E, 0x67, 0x54, 0x69, 0x6D, 0x65, 0x20, 0x34, 0x30, 0x30, 0x3B, 0x73, 0x65, 0x74, 0x20, 0x62, 0x61, 0x64, 0x68, 0x6F, 0x73, 0x74, 0x5F, 0x6D, 0x69, 0x6E, 0x54, 0x6F, 0x74, 0x61, 0x6C, 0x43, 0x6C, 0x69, 0x65, 0x6E, 0x74, 0x73, 0x46, 0x6F, 0x72, 0x48, 0x61, 0x70, 0x70, 0x79, 0x54, 0x65, 0x73, 0x74, 0x20, 0x33, 0x00 });
            }
        }

        private void numericUpDown24_ValueChanged(object sender, EventArgs e)
        {
            string text = this.numericUpDown24.Text;
            Cbuf_AddText(("cg_gun_y " + text));
        }

        private void numericUpDown25_ValueChanged(object sender, EventArgs e)
        {
            string text = this.numericUpDown25.Text;
            Cbuf_AddText( ("cg_gun_x " + text));
        }

        private void numericUpDown26_ValueChanged(object sender, EventArgs e)
        {
            string text = this.numericUpDown26.Text;
            Cbuf_AddText(("cg_gun_z " + text));
        }

        private void numericUpDown27_ValueChanged(object sender, EventArgs e)
        {
            string text = this.numericUpDown27.Text;
            Cbuf_AddText( ("cg_fov " + text));
        }

        private void spaceButton4_Click(object sender, EventArgs e)
        {
            Cbuf_AddText( "cg_fov 60");
            Cbuf_AddText( "cg_gun_x ");
            Cbuf_AddText( "cg_gun_y ");
            Cbuf_AddText( "cg_gun_z ");
           
        }

        private void spaceButton5_Click(object sender, EventArgs e)
        {
            string text = this.textBox1.Text;
            Cbuf_AddText("scr_dd_timelimit " + text);
            Cbuf_AddText("scr_dm_timelimit " + text);
            Cbuf_AddText("scr_dom_timelimit " + text);
            Cbuf_AddText("scr_sd_timelimit " + text);
            Cbuf_AddText("scr_koth_timelimit " + text);
            Cbuf_AddText("scr_hq_timelimit " + text);
            Cbuf_AddText("scr_war_timelimit " + text);
            Cbuf_AddText("scr_tdm_timelimit " + text);
            Cbuf_AddText("scr_sab_timelimit " + text);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void spaceButton6_Click(object sender, EventArgs e)
        {
            string text = this.textBox2.Text;
            Cbuf_AddText("scr_dd_scorelimit " + text);
            Cbuf_AddText("scr_dm_scorelimit " + text);
            Cbuf_AddText("scr_dom_scorelimit " + text);
            Cbuf_AddText("scr_sd_scorelimit " + text);
            Cbuf_AddText("scr_koth_scorelimit " + text);
            Cbuf_AddText("scr_hq_scorelimit " + text);
            Cbuf_AddText("scr_war_scorelimit " + text);
            Cbuf_AddText("scr_tdm_scorelimit " + text);
            Cbuf_AddText("scr_sab_scorelimit " + text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void spaceButton7_Click(object sender, EventArgs e)
        {
            string text = this.textBox3.Text;
            Cbuf_AddText("scr_dd_" + text);
            Cbuf_AddText("scr_dm_" + text);
            Cbuf_AddText("scr_dom_" + text);
            Cbuf_AddText("scr_sd_" + text);
            Cbuf_AddText("scr_koth_" + text);
            Cbuf_AddText("scr_hq_" + text);
            Cbuf_AddText("scr_war_" + text);
            Cbuf_AddText("scr_tdm_" + text);
            Cbuf_AddText("scr_sab_" + text);
        }

        private void spaceCheckBox16_CheckedChanged(object sender, bool isChecked)
        {
            {
                if (this.spaceCheckBox16.Checked)
                {

                    Cbuf_AddText( "scr_sd_numlives 0");
                }
                if (!this.spaceCheckBox16.Checked)
                {
                    Cbuf_AddText("scr_sd_numlives 1"); 
                }
            }
        }

        private void spaceButton8_Click(object sender, EventArgs e)
        {
            Cbuf_AddText("end_game 1"); 
        }

        private void spaceButton9_Click(object sender, EventArgs e)
        {
            Cbuf_AddText("fast_restart 1");
        }

        private void spaceButton10_Click(object sender, EventArgs e)
        {
            spaceButton10.Visible = false;
            spaceButton11.Visible = true;
            
            Cbuf_AddText("set g_password 444xMoDz");
        }

        private void spaceButton11_Click(object sender, EventArgs e)
        {
            spaceButton10.Visible = true;
            spaceButton11.Visible = false;
            Cbuf_AddText("v reset g_password");
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void spaceButton13_Click(object sender, EventArgs e)
        {
            string text = this.numericUpDown1.Text;
            MW2.SV_SendServerCommand(-1,"v timescale " + text);
        }

        private void spaceButton12_Click(object sender, EventArgs e)
        {
            Cbuf_AddText("reset timescale");
        }

        private void spaceButton14_Click(object sender, EventArgs e)
        {
            string text = this.numericUpDown2.Text;
            Cbuf_AddText("jump_height " + text);
        }

        private void spaceButton15_Click(object sender, EventArgs e)
        {
            Cbuf_AddText("reset jump_height");
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void spaceButton16_Click(object sender, EventArgs e)
        {
            string text = this.numericUpDown3.Text;
            MW2.SV_SendServerCommand(-1, "v g_speed " + text);
        }

        private void spaceButton17_Click(object sender, EventArgs e)
        {
            MW2.SV_SendServerCommand(-1, "v g_speed 180" );
        }

        private void spaceCheckBox14_CheckedChanged(object sender, bool isChecked)
        {

            {
                if (this.spaceCheckBox14.Checked)
                {
                    Cbuf_AddText( "cg_drawfps 1 ");

                }
                if (!this.spaceCheckBox14.Checked)
                {
                    Cbuf_AddText("cg_drawfps 0 ");
                }
            }
           
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            string text = this.numericUpDown4.Text;
            Cbuf_AddText("player_meleeRange  " + text);
        }

        private void spaceButton19_Click(object sender, EventArgs e)
        {
            Cbuf_AddText("player_meleeRange 2" );
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            string text = this.numericUpDown5.Text;
            Cbuf_AddText("r_znear " + text);
        }

        private void spaceButton18_Click(object sender, EventArgs e)
        {
            Cbuf_AddText("r_znear 0");
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            string text = this.numericUpDown6.Text;
            Cbuf_AddText("g_knockback " + text);
        }

        private void spaceButton20_Click(object sender, EventArgs e)
        {
            Cbuf_AddText("g_knockback 0");
        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {
            string text = this.numericUpDown7.Text;
            Cbuf_AddText("phys_gravity " + text);
            Cbuf_AddText("g_gravity " + text);
        }

        private void spaceButton21_Click(object sender, EventArgs e)
        {
            Cbuf_AddText("phys_gravity 800");
            Cbuf_AddText("g_gravity 800");
        }

        private void spaceButton28_Click(object sender, EventArgs e)
        {
            string text = this.textBox6.Text;
            Cbuf_AddText("set scr_war_score_kill " + text);
            Cbuf_AddText("set scr_dm_score_kill " + text);
            Cbuf_AddText("set scr_sd_score_kill " + text);
            Cbuf_AddText("set scr_dom_score_kill " + text);
            Cbuf_AddText("set scr_vip_score_kill " + text);
            Cbuf_AddText("set scr_arena_score_kill " + text);
            Cbuf_AddText("set scr_oneflag_score_kill " + text);
            Cbuf_AddText("set scr_gtnw_score_kill " + text);
            Cbuf_AddText("set scr_dd_score_kill " + text);
            Cbuf_AddText("set scr_koth_score_kill " + text);
            Cbuf_AddText("set scr_hq_score_kill " + text);
            Cbuf_AddText("set scr_dem_score_kill " + text);
            Cbuf_AddText("set scr_sab_score_kill " + text);
            Cbuf_AddText("set scr_ctf_ffa_score_kill " + text);
            Cbuf_AddText("set scr_gun_score_kill " + text);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void spaceButton27_Click(object sender, EventArgs e)
        {
            string text = this.textBox5.Text;
            Cbuf_AddText("set scr_war_score_suicide " + text);
            Cbuf_AddText("set scr_dm_score_suicide " + text);
            Cbuf_AddText("set scr_sd_score_suicide " + text);
            Cbuf_AddText("set scr_dom_score_suicide " + text);
            Cbuf_AddText("set scr_vip_score_suicide " + text);
            Cbuf_AddText("set scr_arena_score_suicide " + text);
            Cbuf_AddText("set scr_oneflag_score_suicide " + text);
            Cbuf_AddText("set scr_gtnw_score_suicide " + text);
            Cbuf_AddText("set scr_dd_score_suicide " + text);
            Cbuf_AddText("set scr_koth_score_suicide " + text);
            Cbuf_AddText("set scr_hq_score_suicide " + text);
            Cbuf_AddText("set scr_dem_score_suicide " + text);
            Cbuf_AddText("set scr_sab_score_suicide " + text);
            Cbuf_AddText("set scr_ctf_ffa_score_suicide " + text);
            Cbuf_AddText("set scr_gun_score_suicide " + text);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void spaceButton26_Click(object sender, EventArgs e)
        {
            string text = this.textBox4.Text;
            Cbuf_AddText("set scr_war_score_" + text);
            Cbuf_AddText("set scr_dm_score_" + text);
            Cbuf_AddText("set scr_sd_score_" + text);
            Cbuf_AddText("set scr_dom_score_" + text);
            Cbuf_AddText("set scr_vip_score_" + text);
            Cbuf_AddText("set scr_arena_score_" + text);
            Cbuf_AddText("set scr_oneflag_score_" + text);
            Cbuf_AddText("set scr_gtnw_score_" + text);
            Cbuf_AddText("set scr_dd_score_" + text);
            Cbuf_AddText("set scr_koth_score_" + text);
            Cbuf_AddText("set scr_hq_score_" + text);
            Cbuf_AddText("set scr_dem_score_" + text);
            Cbuf_AddText("set scr_sab_score_" + text);
            Cbuf_AddText("set scr_ctf_ffa_score_" + text);
            Cbuf_AddText("set scr_gun_score_" + text);
        }

        private void spaceCheckBox17_CheckedChanged(object sender, bool isChecked)
        {
            {
                if (this.spaceCheckBox17.Checked)
                {
                    Cbuf_AddText("scr_dm_score_kill 2516000");
                    Cbuf_AddText("scr_dom_score_kill 2516000");
                    Cbuf_AddText("scr_sd_score_kill 2516000");
                    Cbuf_AddText("scr_dd_score_kill 2516000");
                    Cbuf_AddText("scr_koth_score_kill 2516000");
                    Cbuf_AddText("scr_hq_score_kill 2516000");
                    Cbuf_AddText("scr_war_score_kill 2516000");
                    Cbuf_AddText("scr_tdm_score_kill 2516000");
                    Cbuf_AddText("scr_sab_score_kill 2516000");

                }
                if (!this.spaceCheckBox17.Checked)
                {
                   
                }
            }
        }

        private void spaceButton25_Click(object sender, EventArgs e)
        {
            Cbuf_AddText("reset scr_war_score_kill");
            Cbuf_AddText("reset scr_dm_score_kill");
            Cbuf_AddText("reset scr_sd_score_kill");
            Cbuf_AddText("reset scr_dom_score_kill");
            Cbuf_AddText("reset scr_vip_score_kill");
            Cbuf_AddText("reset scr_arena_score_kill");
            Cbuf_AddText("reset scr_oneflag_score_kill");
            Cbuf_AddText("reset scr_gtnw_score_kill");
            Cbuf_AddText("reset scr_dd_score_kill");
            Cbuf_AddText("reset scr_koth_score_kill");
            Cbuf_AddText("reset scr_hq_score_kill");
            Cbuf_AddText("reset scr_dem_score_kill");
            Cbuf_AddText("reset scr_sab_score_kill");
            Cbuf_AddText("reset scr_ctf_ffa_score_kill");
            Cbuf_AddText("reset scr_gun_score_kill");
            Cbuf_AddText("reset scr_war_score_suicide");
            Cbuf_AddText("reset scr_dm_score_suicide");
            Cbuf_AddText("reset scr_sd_score_suicide");
            Cbuf_AddText("reset scr_dom_score_suicide");
            Cbuf_AddText("reset scr_vip_score_suicide");
            Cbuf_AddText("reset scr_arena_score_suicide");
            Cbuf_AddText("reset scr_oneflag_score_suicide");
            Cbuf_AddText("reset scr_gtnw_score_suicide");
            Cbuf_AddText("reset scr_dd_score_suicide");
            Cbuf_AddText("reset scr_koth_score_suicide");
            Cbuf_AddText("reset scr_hq_score_suicide");
            Cbuf_AddText("reset scr_dem_score_suicide");
            Cbuf_AddText("reset scr_sab_score_suicide");
            Cbuf_AddText("reset scr_ctf_ffa_score_suicide");
            Cbuf_AddText("reset scr_gun_score_suicide");
        }

        private void spaceButton22_Click(object sender, EventArgs e)
        {
            string text = textBox8.Text;
            Cbuf_AddText(("g_teamname_allies  " + text));
            
        }

        private void spaceCheckBox18_CheckedChanged(object sender, bool isChecked)
        {

        }

        private void spaceCheckBox19_CheckedChanged(object sender, bool isChecked)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void spaceButton23_Click(object sender, EventArgs e)
        {
            string text = this.textBox7.Text;
            Cbuf_AddText("g_teamname_axis  " + text);
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void spaceButton29_Click(object sender, EventArgs e)
        {
            string text = this.textBox10.Text;
            Cbuf_AddText("ui_mapname  " + text);
        }

        private void spaceButton24_Click(object sender, EventArgs e)
        {
            string text = this.numericUpDown8.Text;
            Cbuf_AddText("compasssize " + text);
        }

        private void numericUpDown8_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void spaceButton30_Click(object sender, EventArgs e)
        {
            Cbuf_AddText("compasssize 1.0");
        }

        private void spaceButton31_Click(object sender, EventArgs e)
        {
            string text = this.textBox9.Text;
            Cbuf_AddText("motd " + text);
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void spaceButton32_Click(object sender, EventArgs e)
        {
            string text = this.textBox11.Text;
            Cbuf_AddText("set ui_debug_localVarString " + text);
        }

        private void numericUpDown9_ValueChanged(object sender, EventArgs e)
        {
            string text = this.numericUpDown9.Text;
            Cbuf_AddText("scr_nuketimer " + text);
        }

        private void numericUpDown10_ValueChanged(object sender, EventArgs e)
        {
            string text = this.numericUpDown10.Text;
            Cbuf_AddText("sensitivity " + text);
        }

        private void spaceButton33_Click(object sender, EventArgs e)
        {
            string text = this.textBox12.Text;
            Cbuf_AddText("gameMode " + text);
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void spaceButton34_Click(object sender, EventArgs e)
        {
            if (spaceComboBox2.SelectedItem == "Free For All")
            {

                Cbuf_AddText("g_gametype dm");
                System.Threading.Thread.Sleep(50);
                Cbuf_AddText("fast_restart 1");
            }
            if (spaceComboBox2.SelectedItem == "Domination​")
            {

                Cbuf_AddText("g_gametype dom");
                System.Threading.Thread.Sleep(50);
                Cbuf_AddText("fast_restart 1");
            }
            if (spaceComboBox2.SelectedItem == "Search and Destroy​")
            {

                Cbuf_AddText("g_gametype sd");
                System.Threading.Thread.Sleep(50);
                Cbuf_AddText("fast_restart 1");
            }
            if (spaceComboBox2.SelectedItem == "Demolition​")
            {

                Cbuf_AddText("g_gametype dd");
                System.Threading.Thread.Sleep(50);
                Cbuf_AddText("fast_restart 1");
            }
            if (spaceComboBox2.SelectedItem == "Headquarters​")
            {

                Cbuf_AddText("g_gametype hq");
                System.Threading.Thread.Sleep(50);
                Cbuf_AddText("fast_restart 1");
            }
            if (spaceComboBox2.SelectedItem == "Ground War​")
            {

                Cbuf_AddText("g_gametype war");
                System.Threading.Thread.Sleep(50);
                Cbuf_AddText("fast_restart 1");
            }
            if (spaceComboBox2.SelectedItem == "Team Deathmatch​​")
            {

                Cbuf_AddText("g_gametype tdm");
                System.Threading.Thread.Sleep(50);
                Cbuf_AddText("fast_restart 1");
            }
        }

        private void spaceComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void spaceButton35_Click(object sender, EventArgs e)
        {

            string text = textBox13.Text;
            MW2.Vision(-1, "" + text);
       
        }

        private void spaceComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://pastebin.com/2zH208ZW");
        }

        private void spaceButton36_Click(object sender, EventArgs e)
        {
            MW2.Vision(-1, "default");
        }

        private void spaceButton37_Click(object sender, EventArgs e)
        {
            Cbuf_AddText("scr_game_allowkillcam 1");
        }

        private void spaceButton38_Click(object sender, EventArgs e)
        {
            Cbuf_AddText("scr_game_allowkillcam 0");
        }

        private void spaceButton40_Click(object sender, EventArgs e)
        {
            Cbuf_AddText( "ui_mapname mp_shipment");
        }

        private void spaceButton39_Click(object sender, EventArgs e)
        {
            Cbuf_AddText("ui_mapname mp_gulag");
        }

        private void spaceButton42_Click(object sender, EventArgs e)
        {
            Cbuf_AddText( "ui_mapname mp_vertigo");
        }

        private void spaceButton41_Click(object sender, EventArgs e)
        {
            Cbuf_AddText( "ui_mapname mp_oilrig");
        }

        private void spaceButton43_Click(object sender, EventArgs e)
        {
            Cbuf_AddText("player_useRadius 99999");
            Cbuf_AddText("player_MGUseRadius 99999");
        }

        private void spaceButton44_Click(object sender, EventArgs e)
        {
           Cbuf_AddText("reset player_useRadius");
           Cbuf_AddText("reset player_MGUseRadius");
        }
#endregion

          #region map
        private String ReturnInfos(Int32 Index)
        {
            return Encoding.ASCII.GetString(PS3.GetBytes(0x00824f03/*This is the offset*/, 0x100)).Replace(@"\", "|").Split('|')[Index];
        }
        public String getMapName()
        {
            String Map = ReturnInfos(6);
            switch (Map)
            {
                default: return "Unknown Map";
                case "mp_afghan": return "Afghan";
                case "mp_derail": return "Derail";
                case "mp_estate": return "Estate";
                case "mp_favela": return "Favela";
                case "mp_snow": return "Whiteout";
                case "mp_highrise": return "Highrise";
                case "mp_invasion": return "Invasion";
                case "mp_checkpoint": return "Karachi";
                case "mp_quarry": return "Quarry";
                case "mp_rundown": return "Rundown";
                case "mp_rust": return "Rust";
                case "mp_boneyard": return "Scrapyard";
                case "mp_subbase": return "Sub Base";
                case "mp_terminal": return "Terminal";
                case "mp_underpass": return "Underpass";
                case "mp_Bailout": return "Bailout";
                case "mp_crash": return "Crash";
                case "mp_overgrown": return "Overgrown";
                case "mp_storm": return "Storm";
                case "mp_fuel": return "Fuel";
                case "mp_strike": return "Strike";
                case "mp_trailerpark": return "Trailer Park";
                case "mp_vacant": return "Vacant";
                case "mp_abandon": return "Carnival";
                case "mp_nightshift": return "Skidrow";
                case "mp_brecourt": return "Wasteland";
                case "mp_compact": return "Salvage";
            }
        }
        public String getGameMode()
        {
            String GM = ReturnInfos(2);
            switch (GM)
            {
                default: return "";
                case "war": return "Team Deathmatch";
                case "dm": return "Free for All";
                case "sd": return "Search and Destroy";
                case "dom": return "Domination";
                case "koth": return "Headquarters";
                case "gtnw": return "GTNW";
                case "oneflag": return "One Flag";
                case "vip": return "VIP";
                case "sab": return "Sabotage";
                case "dd": return "Demolition";
                case "dem": return "Demolition";
                case "arena": return "Arena";
            }
        }

        public String getHardcore()
        {
            String HC = ReturnInfos(4);
            switch (HC)
            {
                default: return "Unknown Gametype";
                case "0": return "Hardcore - Off";
                case "1": return "Hardcore - On";
            }
        }

        public String getMaxPlayers()
        {
            String Host = ReturnInfos(18);
            if (Host == "Modern Warfare 3")
                return "Dedicated Server (No Player is Host)";
            else if (Host == "")
                return "You are not In-Game";
            else return Host;
        }

        public String getHostName()
        {
            String Host = ReturnInfos(16);
            if (Host == "")
                return "You are not In-Game";
            else return Host;
        }
        #endregion

          #region Clients
        private void spaceButton45_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 1)
            {
                dataGridView1.Rows.Add(17);
            }

            for (uint i = 0; i < 18; i++)
            {
                dataGridView1[0, Convert.ToInt32(i)].Value = i;
                dataGridView1[1, Convert.ToInt32(i)].Value = ClientNames(i);
            }         
        }
               public static string ClientNames(uint client)
        {
            string getnames = PS3.Extension.ReadString(0x014e5490 + client * 0x3700);
            return getnames;
        }

               private void giveDerankToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   MW2.GiveDerankAll(dataGridView1.CurrentRow.Index);
               }

               private void giveLevel70ToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   MW2.Level70(dataGridView1.CurrentRow.Index);
               }

               private void giveUnlockAllToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   MW2.GiveUnlockAll(dataGridView1.CurrentRow.Index);
               }

               private void prestige0ToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   MW2._0(dataGridView1.CurrentRow.Index);
               }

               private void prestige1ToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   MW2._1(dataGridView1.CurrentRow.Index);
               }

               private void prestige2ToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   MW2._2(dataGridView1.CurrentRow.Index);
               }

               private void prestige3ToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   MW2._3(dataGridView1.CurrentRow.Index);
               }

               private void prestige4ToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   MW2._4(dataGridView1.CurrentRow.Index);
               }

               private void prestige5ToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   MW2._5(dataGridView1.CurrentRow.Index);
               }

               private void prestige6ToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   MW2._6(dataGridView1.CurrentRow.Index);
               }

               private void prestige7ToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   MW2._7(dataGridView1.CurrentRow.Index);
               }

               private void prestige8ToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   MW2._8(dataGridView1.CurrentRow.Index);
               }

               private void prestige9ToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   MW2._9(dataGridView1.CurrentRow.Index);
               }

               private void prestige10ToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   MW2._10(dataGridView1.CurrentRow.Index);
               }

               private void prestige11ToolStripMenuItem_Click(object sender, EventArgs e)
               {

                   MW2._11(dataGridView1.CurrentRow.Index);
               }

               private void numericUpDown12_ValueChanged(object sender, EventArgs e)
               {

               }

               private void numericUpDown11_ValueChanged(object sender, EventArgs e)
               {

               }

               private void spaceButton46_Click(object sender, EventArgs e)
               {
                  
                   if (numericUpDown11.Value == 0)
            {
                          MW2._0(-1);
            }

            if (numericUpDown11.Value == 1)
            {
              MW2._1(-1);
            }

            if (numericUpDown11.Value == 2)
            {
       MW2._2(-1);
            }

            if (numericUpDown11.Value == 3)
            {
               MW2._3(-1);
            }

            if (numericUpDown11.Value == 4)
            {
                MW2._4(-1);
            }


            if (numericUpDown11.Value == 5)
            {
               MW2._5(-1);
            }


            if (numericUpDown11.Value == 6)
            {
            MW2._6(-1);
            }


            if (numericUpDown11.Value == 7)
            {
             MW2._7(-1);
            }


            if (numericUpDown11.Value == 8)
            {
           MW2._8(-1);
            }


            if (numericUpDown11.Value == 9)
            {
     MW2._9(-1);
            }


            if (numericUpDown11.Value == 10)
            {
       
                MW2._10(-1);
                if (numericUpDown11.Value == 11)
                {
    MW2._11(-1);
                }
               }
        
    }

               private void numericUpDown12_ValueChanged_1(object sender, EventArgs e)
               {
                   if (numericUpDown12.Value == 0)
                   {
                       MW2.GiveUnlockAll(0);
                   }

                   if (numericUpDown12.Value == 1)
                   {
                       MW2.GiveUnlockAll(1);
                   }

                   if (numericUpDown12.Value == 2)
                   {
                       MW2.GiveUnlockAll(2);
                   }

                   if (numericUpDown12.Value == 3)
                   {
                       MW2.GiveUnlockAll(3);
                   }

                   if (numericUpDown12.Value == 4)
                   {
                       MW2.GiveUnlockAll(4);
                   }


                   if (numericUpDown12.Value == 5)
                   {
                       MW2.GiveUnlockAll(5);
                   }


                   if (numericUpDown12.Value == 6)
                   {
                       MW2.GiveUnlockAll(6);
                   }


                   if (numericUpDown12.Value == 7)
                   {
                       MW2.GiveUnlockAll(7);
                   }


                   if (numericUpDown12.Value == 8)
                   {
                       MW2.GiveUnlockAll(8);
                   }


                   if (numericUpDown12.Value == 9)
                   {
                       MW2.GiveUnlockAll(9);
                   }


                   if (numericUpDown12.Value == 10)
                   {

                       MW2.GiveUnlockAll(10);
                       if (numericUpDown12.Value == 11)
                       {
                           MW2.GiveUnlockAll(11);
                       }
                       if (numericUpDown12.Value == 12)
                       {
                           MW2.GiveUnlockAll(12);
                       }
                       if (numericUpDown12.Value == 13)
                       {
                           MW2.GiveUnlockAll(13);
                       }
                       if (numericUpDown12.Value == 14)
                       {
                           MW2.GiveUnlockAll(14);
                       }
                       if (numericUpDown12.Value == 15)
                       {
                           MW2.GiveUnlockAll(15);
                       }
                       if (numericUpDown12.Value == 16)
                       {
                           MW2.GiveUnlockAll(16);
                       }
                       if (numericUpDown12.Value == 17)
                       {
                           MW2.GiveUnlockAll(17);
                       }
                       if (numericUpDown12.Value == 18)
                       {
                           MW2.GiveUnlockAll(18);
                       }
                   }
               }

               private void spaceButton48_Click(object sender, EventArgs e)
               {
                   MW2.GiveUnlockAll(-1);
               }

               private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
               {

               }

               private void spaceButton49_Click(object sender, EventArgs e)
               {
                   if (dataGridView2.RowCount == 1)
                   {
                       dataGridView2.Rows.Add(17);
                   }

                   for (uint i = 0; i < 18; i++)
                   {
                       dataGridView2[0, Convert.ToInt32(i)].Value = i;
                       dataGridView2[1, Convert.ToInt32(i)].Value = ClientNames(i);
                   }         
               }
              
        private void cName(uint ClientIndex)
        {
            Encoding.ASCII.GetBytes(string.Concat(this.textBox14.Text, "\0"));
            PS3.Extension.WriteString(0x14e5490 + ClientIndex * 0x3700, this.textBox14.Text);
        }
               private void textBox14_TextChanged(object sender, EventArgs e)
               {

               }

               private void spaceButton50_Click(object sender, EventArgs e)
               {
                   if (spaceComboBox3.SelectedItem == "Client 0")
                   {

                       cName(0);
                   }
                   if (spaceComboBox3.SelectedItem == "Client 1")
                   {
                       cName(1);
 
                   }
                   if (spaceComboBox3.SelectedItem == "Client 2")
                   {
                       cName(2);

                   }
                   if (spaceComboBox3.SelectedItem == "Client 3")
                   {
                       cName(3);
                   }
                   if (spaceComboBox3.SelectedItem == "Client 4")
                   {
                       cName(4);

                   }
                   if (spaceComboBox3.SelectedItem == "Client 5")
                   {
                       cName(5);
                   }
                   if (spaceComboBox3.SelectedItem == "Client 6")
                   {

                       cName(6);
                   }
                   if (spaceComboBox3.SelectedItem == "Client 7")
                   {
                       cName(7);

                   }
                   if (spaceComboBox3.SelectedItem == "Client 8")
                   {
                       cName(8);

                   }
                   if (spaceComboBox3.SelectedItem == "Client 9")
                   {
                       cName(9);
                   }
                   if (spaceComboBox3.SelectedItem == "Client 10")
                   {
                       cName(10);

                   }
                   if (spaceComboBox3.SelectedItem == "Client 11")
                   {
                       cName(11);
                   }
                   if (spaceComboBox3.SelectedItem == "Client 12")
                   {

                       cName(12);
                   }
                   if (spaceComboBox3.SelectedItem == "Client 13")
                   {
                       cName(13);

                   }
                   if (spaceComboBox3.SelectedItem == "Client 14")
                   {
                       cName(14);

                   }
                   if (spaceComboBox3.SelectedItem == "Client 15")
                   {
                       cName(15);
                   }
                   if (spaceComboBox3.SelectedItem == "Client 16")
                   {
                       cName(16);

                   }
                   if (spaceComboBox3.SelectedItem == "Client 17")
                   {
                       cName(17);
                   }
               }

               private void spaceComboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
               {

               }

               private void spaceCheckBox18_CheckedChanged_1(object sender, bool isChecked)
               {
                   if (this.spaceCheckBox18.Checked)
                   {
                       timer1.Start();

                   }
                   if (!this.spaceCheckBox18.Checked)
                   {
                       timer1.Stop();
                   }
               }

               private void timer1_Tick(object sender, EventArgs e)
               {
                   cName(0);
                   cName(1);
                   cName(2);
                   cName(3);
                   cName(4);
                   cName(5);
                   cName(6);
                   cName(7);
                   cName(8);
                   cName(9);
                   cName(10);
                   cName(11);
                   cName(12);
                   cName(13);
                   cName(14);
                   cName(15);
                   cName(16);
                   cName(17);
               }

               private void onToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0xFF, 0xFF };
                   PS3.SetMemory((0x014e5429 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void offToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x00, 0x64 };
                   PS3.SetMemory((0x14E235A + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void unlimitedAmmoToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x0F, 0xFF, 0xFF, 0xFF };
                   PS3.SetMemory((0x014e256c + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);


                   byte[] godmode1 = new byte[] { 0x0F, 0xFF, 0xFF, 0xFF };
                   PS3.SetMemory((0x014e24ec + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode1);


                   byte[] godmode11 = new byte[] { 0x0F, 0xFF, 0xFF, 0xFF };
                   PS3.SetMemory((0x014e2570 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode11);

                   byte[] godmode121 = new byte[] { 0x0F, 0xFF, 0xFF, 0xFF };
                   PS3.SetMemory((0x014e2554 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode121);


                   byte[] godmode1211 = new byte[] { 0x0F, 0xFF, 0xFF, 0xFF };
                   PS3.SetMemory((0x014e24dc + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode1211);

                   byte[] godmode12113 = new byte[] { 0x0F, 0xFF, 0xFF, 0xFF };
                   PS3.SetMemory((0x014e2558 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode12113);
                   byte[] godmode121123 = new byte[] { 0x0F, 0xFF, 0xFF, 0xFF };
                   PS3.SetMemory((0x014e2578 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode121123);

                   byte[] godmode121131 = new byte[] { 0x0F, 0xFF, 0xFF, 0xFF };
                   PS3.SetMemory((0x014e24f4 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode121131);
                   byte[] godmode12112311 = new byte[] { 0x0F, 0xFF, 0xFF, 0xFF };
                   PS3.SetMemory((0x014e2560 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode12112311);
                   byte[] godmode121123112 = new byte[] { 0x0F, 0xFF, 0xFF, 0xFF };
                   PS3.SetMemory((0x014e2578 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode121123112);
               }

               private void allPerksToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
                   PS3.SetMemory((0x014e262a + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);

               }

               private void onToolStripMenuItem1_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0xFF, 0xEF };
                   PS3.SetMemory((0x014e2628 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void offToolStripMenuItem1_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x00, 0x08 };
                   PS3.SetMemory((0x014e2628 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void onToolStripMenuItem2_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x01 };
                   PS3.SetMemory((0x01319901 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void offToolStripMenuItem2_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x00 };
                   PS3.SetMemory((0x01319901 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void rightToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0xFF };
                   PS3.SetMemory((0x14e265f + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void upToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0xFF };
                   PS3.SetMemory((0x14e2653 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void downToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0xFF };
                   PS3.SetMemory((0x14e2657 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void leftToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0xFF };
                   PS3.SetMemory((0x14e265b + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void resetAllToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x00 };
                   PS3.SetMemory((0x14e2657 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
                   byte[] godmode1 = new byte[] { 0x00 };
                   PS3.SetMemory((0x14e265b + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode1);
                   byte[] godmode11 = new byte[] { 0x00 };
                   PS3.SetMemory((0x14e2653 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode11);
                   byte[] godmode111 = new byte[] { 0x00 };
                   PS3.SetMemory((0x14e265f + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode111);
         
               }

               private void onToolStripMenuItem4_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x01 };
                   PS3.SetMemory((0x14E54EB + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void offToolStripMenuItem4_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x00 };
                   PS3.SetMemory((0x14E54EB + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void onToolStripMenuItem3_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x01 };
                   PS3.SetMemory((0x14E54E6 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void offToolStripMenuItem3_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x01 };
                   PS3.SetMemory((0x14E54E6 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void onToolStripMenuItem5_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x00 };
                   PS3.SetMemory((0x014e53af + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void offToolStripMenuItem5_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x02 };
                   PS3.SetMemory((0x014e53af + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void toolStripMenuItem1_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0xC5 };
                   PS3.SetMemory((0x014e2220 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void killAndScareToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0xFF, 0xFF };
                   PS3.SetMemory((0x14E2224 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }
               public static void Kick(int client, string message)
               {
                  
                   MW2.SV_SendServerCommand(client, string.Concat("w \"", message));
               }
               private void kickToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   Kick(this.dataGridView2.CurrentRow.Index ,"");
               }
               public static void FreezePS3(int client, string message)
               {
                  
                   MW2.SV_SendServerCommand(client, string.Concat("a \"", message));
               }
               private void freezePS3ToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   FreezePS3(this.dataGridView2.CurrentRow.Index,"");
               }

               private void toolStripMenuItem2_Click(object sender, EventArgs e)
               {
                   Kick(this.dataGridView2.CurrentRow.Index, "^1Kicked ^1For Being a Faggot. ^1444xMoDz ^2RTE ^3Tool ^4 Version 1.1.0");
               }

               private void onToolStripMenuItem6_Click(object sender, EventArgs e)
               {

               }

               private void onToolStripMenuItem7_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x01 };
                   PS3.SetMemory((21898343 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode); 
               }

               private void offToolStripMenuItem7_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x00 };
                   PS3.SetMemory((0x014e2467 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode); 
               }

               private void onToolStripMenuItem8_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x01 };
                   PS3.SetMemory((0x014e245d + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode); 
               }

               private void offToolStripMenuItem8_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x00 };
                   PS3.SetMemory((0x014e245d + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void desertToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x01 };
                   PS3.SetMemory((0x014e2468 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void articToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x02 };
                   PS3.SetMemory((0x014e2468 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void woodlandToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x03 };
                   PS3.SetMemory((0x014e2468 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void digitalToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x04 };
                   PS3.SetMemory((0x014e2468 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void urbanToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x05 };
                   PS3.SetMemory((0x014e2468 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void blueToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x07 };
                   PS3.SetMemory((0x014e2468 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void redToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x06 };
                   PS3.SetMemory((0x014e2468 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void fallToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x08 };
                   PS3.SetMemory((0x014e2468 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void desertToolStripMenuItem1_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x01 };
                   PS3.SetMemory((0x014e245e + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode); 
               }

               private void articToolStripMenuItem1_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x02 };
                   PS3.SetMemory((0x014e245e + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode); 
               }

               private void woodlandToolStripMenuItem1_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x03 };
                   PS3.SetMemory((0x014e245e + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode); 
               }

               private void digitalToolStripMenuItem1_Click(object sender, EventArgs e)
               {

                   byte[] godmode = new byte[] { 0x04 };
                   PS3.SetMemory((0x014e245e + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode); 
               }

               private void urbanToolStripMenuItem1_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x05 };
                   PS3.SetMemory((0x014e245e + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode); 
               }

               private void blueToolStripMenuItem1_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x07 };
                   PS3.SetMemory((0x014e245e + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode); 
               }

               private void redToolStripMenuItem1_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x06 };
                   PS3.SetMemory((0x014e245e + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void fallToolStripMenuItem1_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x08 };
                   PS3.SetMemory((0x014e245e + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode); 
               }
               public static void DefaultWeapon(int ClientInt)
               {
                   PS3.SetMemory((uint)(0x14e259a + ClientInt * 0x3700), new byte[] { 0x00, 0x01, 0x0F, 0xFF, 0xFF, 0xFF });
                   
                   byte[] numArray = new byte[] { 0x00, 0x01 };
                   PS3.SetMemory((uint)(0x14e2422 + ClientInt * 0x3700), numArray);
                   
                   numArray = new byte[] { 0x00, 0x01 };
                   PS3.SetMemory((uint)(0x14e24b6 + ClientInt * 0x3700), numArray);
                   
               }
               private void primaryToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   DefaultWeapon(dataGridView2.CurrentRow.Index);
               }
               
               public static void SetUp1(int ClientInt)
               {
                   
                   byte[] numArray = new byte[] { 0x04, 0x97};
                   PS3.SetMemory(0x14e2422 + (uint)ClientInt * 0x3700, numArray);
                   
                   numArray = new byte[] { 0x04, 0x97 };
                   PS3.SetMemory(0x14e24b6 + (uint)ClientInt * 0x3700, numArray);

                   byte[] numArray1 = new byte[] { 0x02, 0x20, 0x0f, 0xFF, 0xFF, 0xFF };
                   PS3.SetMemory(0x14e259a + (uint)ClientInt * 0x3700, numArray1);
                   
                   numArray = new byte[0x0B];
                   PS3.SetMemory(0x14e2426 + (uint)ClientInt * 0x3700, numArray);
                   
               }
               public static void SetUp2(int ClientInt)
               {
                   
                   byte[] numArray = new byte[] { 0x04, 0x96 };
                   PS3.SetMemory(0x14e2422 + (uint)ClientInt * 0x3700, numArray);
                   
                   numArray = new byte[] { 0x04, 0x96 };
                   PS3.SetMemory(0x14e24b6 + (uint)ClientInt * 0x3700, numArray);


                   byte[] numArray1 = new byte[] { 0x02, 0x1f, 0x0f, 0xFF, 0xFF, 0xFF };
                   PS3.SetMemory(0x14e259a + (uint)ClientInt * 0x3700, numArray1);
                   
                   
                   numArray = new byte[0x0B];
                   PS3.SetMemory(0x14e2426 + (uint)ClientInt * 0x3700, numArray);
               }
               private void mmToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   if (!((this.currentmapcombobox.Text == "Afghan") | (this.currentmapcombobox.Text == "Highrise") | (this.currentmapcombobox.Text == "Karachi") | (this.currentmapcombobox.Text == "Quarry") | (this.currentmapcombobox.Text == "Rundown") | (this.currentmapcombobox.Text == "Skidrow") | (this.currentmapcombobox.Text == "Terminal") | (this.currentmapcombobox.Text == "Wasteland") | (this.currentmapcombobox.Text == "Overgrown")))
                   {
                       SetUp1(this.dataGridView2.CurrentRow.Index);
                   }
                   else
                   {
                       SetUp2(this.dataGridView2.CurrentRow.Index);
                   }
               }

               private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
               {

               }
               public static void SetUp11(int ClientInt)
               {
                   
                   byte[] numArray = new byte[] { 0x04, 0x98 };
                   PS3.SetMemory(0x14e2422 + (uint)ClientInt * 0x3700, numArray);
                   
                   numArray = new byte[] { 0x04, 0x98 };
                   PS3.SetMemory(0x14e24b6 + (uint)ClientInt * 0x3700, numArray);
                   
                   byte[] numArray1 = new byte[] { 0x02, 0x21, 0x0f, 0xFF, 0xFF, 0xFF };
                   PS3.SetMemory(0x14e259a + (uint)ClientInt * 0x3700, numArray1);
              
                   
               }
               public static void SetUp22(int ClientInt)
               {
                  
                   byte[] numArray = new byte[] { 0x04, 0x97 };
                   PS3.SetMemory(0x14e2422 + (uint)ClientInt * 0x3700, numArray);
                   
                   numArray = new byte[] { 0x04, 0x97 };
                   PS3.SetMemory(0x14e24b6 + (uint)ClientInt * 0x3700, numArray);

                   byte[] numArray1 = new byte[] { 0x02, 0x20, 0x0f, 0xFF, 0xFF, 0xFF };
                   PS3.SetMemory(0x14e259a + (uint)ClientInt * 0x3700, numArray1);

               }
               private void mmToolStripMenuItem1_Click(object sender, EventArgs e)
               {
                   if (!((this.currentmapcombobox.Text == "Afghan") | (this.currentmapcombobox.Text == "Highrise") | (this.currentmapcombobox.Text == "Karachi") | (this.currentmapcombobox.Text == "Quarry") | (this.currentmapcombobox.Text == "Rundown") | (this.currentmapcombobox.Text == "Skidrow") | (this.currentmapcombobox.Text == "Terminal") | (this.currentmapcombobox.Text == "Wasteland") | (this.currentmapcombobox.Text == "Overgrown")))
                   {
                       SetUp22(this.dataGridView2.CurrentRow.Index);
                   }
                   else
                   {
                       SetUp11(this.dataGridView2.CurrentRow.Index);
                   }
               }
               public static void SetUp3(int ClientInt)
               {
                   
                   byte[] numArray = new byte[] { 0x04, 0x99 };
                   PS3.SetMemory(0x14e2422 + (uint)ClientInt * 0x3700, numArray);
                   
                   numArray = new byte[] { 0x04, 0x99 };
                   PS3.SetMemory(0x14e24b6 + (uint)ClientInt * 0x3700, numArray);

                   byte[] numArray1 = new byte[] { 0x02, 0x22, 0x0f, 0xFF, 0xFF, 0xFF };
                   PS3.SetMemory(0x14e259a + (uint)ClientInt * 0x3700, numArray1);
                 
               }
               public static void SetUp4(int ClientInt)
               {
                   
                   byte[] numArray = new byte[] { 0x04, 0x98 };
                   PS3.SetMemory(0x14e2422 + (uint)ClientInt * 0x3700, numArray);
                   
                   numArray = new byte[] { 0x04, 0x98 };
                   PS3.SetMemory(0x14e24b6 + (uint)ClientInt * 0x3700, numArray);
                   
                   byte[] numArray1 = new byte[] { 0x02, 0x21, 0x0f, 0xFF, 0xFF, 0xFF};
                   PS3.SetMemory(0x14e259a + (uint)ClientInt * 0x3700, numArray1);
                   
               }
               private void mmToolStripMenuItem2_Click(object sender, EventArgs e)
               {
                   if (!((this.currentmapcombobox.Text == "Afghan") | (this.currentmapcombobox.Text == "Highrise") | (this.currentmapcombobox.Text == "Karachi") | (this.currentmapcombobox.Text == "Quarry") | (this.currentmapcombobox.Text == "Rundown") | (this.currentmapcombobox.Text == "Skidrow") | (this.currentmapcombobox.Text == "Terminal") | (this.currentmapcombobox.Text == "Wasteland") | (this.currentmapcombobox.Text == "Overgrown")))
                   {
                       SetUp4(this.dataGridView2.CurrentRow.Index);
                   }
                   else
                   {
                       SetUp3(this.dataGridView2.CurrentRow.Index);
                   }
               }
               public static void SetUp5(int ClientInt)
               {
                   
                   byte[] numArray = new byte[] { 0x00, 0x2e };
                   PS3.SetMemory(0x14e24b6 + (uint)ClientInt * 0x3700, numArray);
                   
                   numArray = new byte[] { 0x00, 0x2e};
                   PS3.SetMemory(0x14e2422 + (uint)ClientInt * 0x3700, numArray);
                   
                   byte[] numArray1 = new byte[] { 0x00, 0x24, 0x0f, 0xFF, 0xFF, 0xFF };
                   PS3.SetMemory(0x14e24da + (uint)ClientInt * 0x3700, numArray1);
                  
                   byte[] numArray2 = new byte[] { 0x00, 0x24, 0x0f, 0xFF, 0xFF, 0xFF };
                   PS3.SetMemory(0x14e2552 + (uint)ClientInt * 0x3700, numArray1);

               }
               public static void SetUp6(int ClientInt)
               {
                   
                   byte[] numArray = new byte[] { 0x00, 0x2d };
                   PS3.SetMemory(0x14e24b6 + (uint)ClientInt * 0x3700, numArray);
                   
                   numArray = new byte[] { 0x00, 0x2d };
                   PS3.SetMemory(0x14e2422 + (uint)ClientInt * 0x3700, numArray);

                   byte[] numArray1 = new byte[] { 0x00, 0x23, 0x0f, 0xFF, 0xFF, 0xFF };
                   PS3.SetMemory(0x14e24da + (uint)ClientInt * 0x3700, numArray1);
                   
                   byte[] numArray2 = new byte[] { 0x00, 0x23, 0x0f, 0xFF, 0xFF, 0xFF };
                   PS3.SetMemory(0x14e2552 + (uint)ClientInt * 0x3700, numArray1);

               }
               private void goldDesertEagleToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   if (!((this.currentmapcombobox.Text == "Afghan") | (this.currentmapcombobox.Text == "Highrise") | (this.currentmapcombobox.Text == "Karachi") | (this.currentmapcombobox.Text == "Quarry") | (this.currentmapcombobox.Text == "Rundown") | (this.currentmapcombobox.Text == "Skidrow") | (this.currentmapcombobox.Text == "Terminal") | (this.currentmapcombobox.Text == "Wasteland") | (this.currentmapcombobox.Text == "Overgrown")))
                   {
                       SetUp6(this.dataGridView2.CurrentRow.Index);
                   }
                   else
                   {
                       SetUp5(this.dataGridView2.CurrentRow.Index);
                   }
               }
               #region bullet
               public static class Bullets
               {
                   public static uint Type1;

                   public static uint Type2;

                   public static uint Ammo;

                   static Bullets()
                   {
                       Bullets.Type1 = 0x14e2422;
                       Bullets.Type2 = 0x14e24b6;
                       Bullets.Ammo = 0x14e259a;
                   }
               #endregion


               }
               public static void SetUp20(int ClientInt)
               {
                   
                   byte[] numArray = new byte[] { 0x04, 0xa2};
                   PS3.SetMemory(Bullets.Type1 + (uint)ClientInt * 0x3700, numArray);
                  
                   numArray = new byte[] { 0x04, 0xa2 };
                   PS3.SetMemory(Bullets.Type2 + (uint)ClientInt * 0x3700, numArray);

                   byte[] numArray1 = new byte[] { 0x02,0x2a, 0x0f, 0xFF, 0xFF, 0xFF };
                   PS3.SetMemory(Bullets.Ammo + (uint)ClientInt * 0x3700, numArray1);
                   
               }
               public static void SetUp21(int ClientInt)
               {
                   
                   byte[] numArray = new byte[] { 0x04, 0xa1 };
                   PS3.SetMemory(Bullets.Type1 + (uint)ClientInt * 0x3700, numArray);
            
                   numArray = new byte[] { 0x04,0xa1 };
                   PS3.SetMemory(Bullets.Type2 + (uint)ClientInt * 0x3700, numArray);

                   byte[] numArray1 = new byte[] { 0x02, 0x29, 0x0f, 0xFF, 0xFF, 0xFF };
                   PS3.SetMemory(Bullets.Ammo + (uint)ClientInt * 0x3700, numArray1);
                   

               }

               private void cobra20mmToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   if (!((this.currentmapcombobox.Text == "Afghan") | (this.currentmapcombobox.Text == "Highrise") | (this.currentmapcombobox.Text == "Karachi") | (this.currentmapcombobox.Text == "Quarry") | (this.currentmapcombobox.Text == "Rundown") | (this.currentmapcombobox.Text == "Skidrow") | (this.currentmapcombobox.Text == "Terminal") | (this.currentmapcombobox.Text == "Wasteland") | (this.currentmapcombobox.Text == "Overgrown")))
                   {
                       SetUp21(this.dataGridView2.CurrentRow.Index);
                   }
                   else
                   {
                       SetUp20(this.dataGridView2.CurrentRow.Index);
                   }
               }
               public static class PavelowMinigun
               {
                   public static void SetUp1(int ClientInt)
                   {
                      
                       byte[] numArray = new byte[] { 0x04, 0xa5};
                       PS3.SetMemory(Bullets.Type1 + (uint)ClientInt * 0x3700, numArray);
                       
                       numArray = new byte[] {0x00 , 0xa5};
                       PS3.SetMemory(Bullets.Type2 + (uint)ClientInt * 0x3700, numArray);

                       byte[] numArray1 = new byte[] { 0x02,0x2d, 0x0f, 0xFF, 0xFF, 0xFF };
                       PS3.SetMemory(Bullets.Ammo + (uint)ClientInt * 0x3700, numArray1);
                       
                   }

                   public static void SetUp2(int ClientInt)
                   {
                      
                       byte[] numArray = new byte[] { 0x04, 0xa4};
                       PS3.SetMemory(Bullets.Type1 + (uint)ClientInt * 0x3700, numArray);
                       
                       numArray = new byte[] { 0x04, 0xa4 };
                       PS3.SetMemory(Bullets.Type2 + (uint)ClientInt * 0x3700, numArray);

                       byte[] numArray1 = new byte[] { 0x02, 0x2c, 0x0f, 0xFF, 0xFF, 0xFF };
                       PS3.SetMemory(Bullets.Ammo + (uint)ClientInt * 0x3700, numArray1);
                       
                   }
               }

               private void pavelowToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   if (!((this.currentmapcombobox.Text == "Afghan") | (this.currentmapcombobox.Text == "Highrise") | (this.currentmapcombobox.Text == "Karachi") | (this.currentmapcombobox.Text == "Quarry") | (this.currentmapcombobox.Text == "Rundown") | (this.currentmapcombobox.Text == "Skidrow") | (this.currentmapcombobox.Text == "Terminal") | (this.currentmapcombobox.Text == "Wasteland") | (this.currentmapcombobox.Text == "Overgrown")))
                   {
                       PavelowMinigun.SetUp2(this.dataGridView2.CurrentRow.Index);
                   }
                   else
                   {
                       PavelowMinigun.SetUp1(this.dataGridView2.CurrentRow.Index);
                   }
               }
               public static class Harrier20mm
               {
                   public static void SetUp1(int ClientInt)
                   {
                       
                       byte[] numArray = new byte[] { 0x04, 0x9e};
                       PS3.SetMemory(Bullets.Type1 + (uint)ClientInt * 0x3700, numArray);
                       
                       numArray = new byte[] { 0x04, 0x9e };
                       PS3.SetMemory(Bullets.Type2 + (uint)ClientInt * 0x3700, numArray);

                       byte[] numArray1 = new byte[] { 0x02, 0x26, 0x0f, 0xFF, 0xFF, 0xFF };
                       PS3.SetMemory(Bullets.Ammo + (uint)ClientInt * 0x3700, numArray1);
                      
                   }

                   public static void SetUp2(int ClientInt)
                   {
                      
                       byte[] numArray = new byte[] { 0x04, 0x9d };
                       PS3.SetMemory(Bullets.Type1 + (uint)ClientInt * 0x3700, numArray);
                       
                       numArray = new byte[] { 0x04, 0x9d };
                       PS3.SetMemory(Bullets.Type2 + (uint)ClientInt * 0x3700, numArray);

                       byte[] numArray1 = new byte[] { 0x02, 0x25, 0x0f, 0xFF, 0xFF, 0xFF };
                       PS3.SetMemory(Bullets.Ammo + (uint)ClientInt * 0x3700, numArray1);
                       
                   }
               }

               private void harrier20mmToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   if (!((this.currentmapcombobox.Text == "Afghan") | (this.currentmapcombobox.Text == "Highrise") | (this.currentmapcombobox.Text == "Karachi") | (this.currentmapcombobox.Text == "Quarry") | (this.currentmapcombobox.Text == "Rundown") | (this.currentmapcombobox.Text == "Skidrow") | (this.currentmapcombobox.Text == "Terminal") | (this.currentmapcombobox.Text == "Wasteland") | (this.currentmapcombobox.Text == "Overgrown")))
                   {
                       Harrier20mm.SetUp2(this.dataGridView2.CurrentRow.Index);
                   }
                   else
                   {
                       Harrier20mm.SetUp1(this.dataGridView2.CurrentRow.Index);
                   }
               }
               public static class HarrierMissiles
               {
                   public static void SetUp1(int ClientInt)
                   {
                       
                       byte[] numArray = new byte[] { 0x04, 0x9f};
                       PS3.SetMemory(Bullets.Type1 + (uint)ClientInt * 0x3700, numArray);
                      
                       numArray = new byte[] { 0x04, 0x9f};
                       PS3.SetMemory(Bullets.Type2 + (uint)ClientInt * 0x3700, numArray);

                       byte[] numArray1 = new byte[] { 0x02, 0x27, 0x0f, 0xFF, 0xFF, 0xFF };
                       PS3.SetMemory(Bullets.Ammo + (uint)ClientInt * 0x3700, numArray1);
                    
                   }

                   public static void SetUp2(int ClientInt)
                   {
                       
                       byte[] numArray = new byte[] { 0x04, 0x9e };
                       PS3.SetMemory(Bullets.Type1 + (uint)ClientInt * 0x3700, numArray);
                      
                       numArray = new byte[] { 0x04, 0x9e };
                       PS3.SetMemory(Bullets.Type2 + (uint)ClientInt * 0x3700, numArray);

                       byte[] numArray1 = new byte[] { 0x02, 0x26, 0x0f, 0xFF, 0xFF, 0xFF };
                       PS3.SetMemory(Bullets.Ammo + (uint)ClientInt * 0x3700, numArray1);
                       
                   }
               }
               private void harrierMissiliesToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   if (!((this.currentmapcombobox.Text == "Afghan") | (this.currentmapcombobox.Text == "Highrise") | (this.currentmapcombobox.Text == "Karachi") | (this.currentmapcombobox.Text == "Quarry") | (this.currentmapcombobox.Text == "Rundown") | (this.currentmapcombobox.Text == "Skidrow") | (this.currentmapcombobox.Text == "Terminal") | (this.currentmapcombobox.Text == "Wasteland") | (this.currentmapcombobox.Text == "Overgrown")))
                   {
                       HarrierMissiles.SetUp2(this.dataGridView2.CurrentRow.Index);
                   }
                   else
                   {
                       HarrierMissiles.SetUp1(this.dataGridView2.CurrentRow.Index);
                   }
               }

               private void toolStripMenuItem5_Click(object sender, EventArgs e)
               {

               }

               private void onToolStripMenuItem9_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x55 };
                   PS3.SetMemory((0x014e2213 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void offToolStripMenuItem9_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x00 };
                   PS3.SetMemory((0x014e2213 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void thermalToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x09 };
                   PS3.SetMemory((0x014e2213 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void smallVolumeToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x60 };
                   PS3.SetMemory((0x014e2213 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x00 };
                   PS3.SetMemory((0x014e2213 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void drugVisionFTWToolStripMenuItem_Click(object sender, EventArgs e)
               {

               }

               private void skyToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x46, 0xFF };
                   PS3.SetMemory((0x014e2224 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void spaceToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x60, 0x5C };
                   PS3.SetMemory((0x014e2224 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void underMapToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0xC7, 0x5F };
                   PS3.SetMemory((0x014e2224 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void autoProneToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x55 };
                   PS3.SetMemory((0x014e220d + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void disableJumpToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x04 };
                   PS3.SetMemory((0x014e220d + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void disableSprintToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x02 };
                   PS3.SetMemory((0x014e220d + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void defaultToolStripMenuItem1_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x00 };
                   PS3.SetMemory((0x014e220d + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void onToolStripMenuItem10_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x02 };
                   PS3.SetMemory((0x014e24d3 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void offToolStripMenuItem10_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x00 };
                   PS3.SetMemory((0x014e24d3 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void disableWeaponsSwitchToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x08 };
                   PS3.SetMemory((0x014e24be + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void noRecoilToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x04 };
                   PS3.SetMemory((0x014e24be + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void disableWeaponToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x00, 0x80 };
                   PS3.SetMemory((0x014e24be + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void disableADSToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x00, 0x20 };
                   PS3.SetMemory((0x014e24be + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void defaultToolStripMenuItem2_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x00, 0x00 };
                   PS3.SetMemory((0x014e24be + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void normalToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x00 };
                   PS3.SetMemory((0x014E5623 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void noClipToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x01 };
                   PS3.SetMemory((0x014E5623 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);  
               }

               private void ufoToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x02 };
                   PS3.SetMemory((0x014E5623 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void freezeToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x06 };
                   PS3.SetMemory((0x014E5623 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void onToolStripMenuItem11_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x40 };
                   PS3.SetMemory((0x014e543c + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void offToolStripMenuItem11_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x3F };
                   PS3.SetMemory((0x014e543c + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void onToolStripMenuItem12_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x01 };
                   PS3.SetMemory((0x014e220e + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void offToolStripMenuItem12_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x00 };
                   PS3.SetMemory((0x014e220e + (uint)dataGridView1.CurrentRow.Index * 0x3700), godmode);
               }

               private void fFAToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x00 };
                   PS3.SetMemory((0x014e5453 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void opForSpetznasToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x01 };
                   PS3.SetMemory((0x014e5453 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void tF141RangersToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x02 };
                   PS3.SetMemory((0x014e5453 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void spectatorToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x03 };
                   PS3.SetMemory((0x014e5453 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void onToolStripMenuItem13_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x08 };
                   PS3.SetMemory((0x014e2212 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void offToolStripMenuItem13_Click(object sender, EventArgs e)
               {
                   byte[] godmode = new byte[] { 0x10 };
                   PS3.SetMemory((0x014e2212 + (uint)dataGridView2.CurrentRow.Index * 0x3700), godmode);
               }

               private void onToolStripMenuItem14_Click(object sender, EventArgs e)
               {

               }

               private void spaceButton51_Click(object sender, EventArgs e)
               {
                   if (currentmapcombobox.SelectedItem == "Afghan")
                   {
                       Cbuf_AddText("map mp_afghan ");
                      
                   }
                   if (currentmapcombobox.SelectedItem == "Derail")
                   {
                       Cbuf_AddText("map mp_derail ");

                   }
                   if (currentmapcombobox.SelectedItem == "Estate")
                   {
                       Cbuf_AddText("map mp_estate ");

                   }
                   if (currentmapcombobox.SelectedItem == "Favela")
                   {
                       Cbuf_AddText("map mp_favela ");
                   }
                   if (currentmapcombobox.SelectedItem == "Highrise")
                   {

                       Cbuf_AddText("map mp_highrise ");
                   }
                   if (currentmapcombobox.SelectedItem == "Invasion")
                   {
                       Cbuf_AddText("map mp_invasion ");
                   }


                   if (currentmapcombobox.SelectedItem == "Karachi")
                   {

                       Cbuf_AddText("map mp_checkpoint ");
                   }
                   if (currentmapcombobox.SelectedItem == "Quarry")
                   {
                       Cbuf_AddText("map mp_quarry ");

                   }
                   if (currentmapcombobox.SelectedItem == "Rundown")
                   {

                       Cbuf_AddText("map mp_rundown ");
                   }
                   if (currentmapcombobox.SelectedItem == "Rust")
                   {
                       Cbuf_AddText("map mp_rust ");
                   }
                   if (currentmapcombobox.SelectedItem == "Scrapyard")
                   {
                       Cbuf_AddText("map mp_boneyard ");

                   }
                   if (currentmapcombobox.SelectedItem == "Skidrow")
                   {
                       Cbuf_AddText("map mp_nightshift ");
                   }

                   if (currentmapcombobox.SelectedItem == "SubBase")
                   {

                       Cbuf_AddText("map mp_subbase ");
                   }
                   if (currentmapcombobox.SelectedItem == "Terminal")
                   {
                       Cbuf_AddText("map mp_terminal ");
                   }
                   if (currentmapcombobox.SelectedItem == "Underpass")
                   {

                       Cbuf_AddText("map mp_underpass ");
                   }
                   if (currentmapcombobox.SelectedItem == "Wasteland")
                   {
                       Cbuf_AddText("map mp_brecourt ");
                   }
               }

               private void currentmapcombobox_SelectedIndexChanged(object sender, EventArgs e)
               {

               }
               public static class Teleport
               {
                   public static uint Location;

                   public static uint Height;

                   static Teleport()
                   {
                       Teleport.Location = 0x14e221b;
                       Teleport.Height = 0x14e2224;
                   }
               }
               public static void ClientToHost(int ClientInt)
               {
                   byte[] numArray = PS3.Extension.ReadBytes(Teleport.Location, 16);
                   PS3.Extension.WriteBytes(Teleport.Location + (uint)ClientInt * 0x3700, numArray);
                   
               }

               public static void EveryoneToClient(int ClientInt)
               {
                   byte[] numArray = PS3.Extension.ReadBytes(Teleport.Location + (uint)ClientInt * 0x3700, 0x16);
                   for (uint i = 0; i < 18; i++)
                   {
                       uint location = Teleport.Location + 0x3700 * i;
                       PS3.Extension.WriteBytes(location, numArray);
                       
                   }
               }
               private void toHostToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   ClientToHost(dataGridView2.CurrentRow.Index);
               }

               private void everyoneToClientToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   EveryoneToClient(dataGridView2.CurrentRow.Index); ;
               }
               public static void EveryoneToHost(int ClientInt)
               {
                   byte[] numArray = PS3.Extension.ReadBytes(Teleport.Location, 0x16);
                   for (uint i = 0; i < 12; i++)
                   {
                       uint location = Teleport.Location + 0x3700 * i;
                       PS3.Extension.WriteBytes(location, numArray);
                      
                   }
               }
               private void everyoneToHostToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   EveryoneToHost(dataGridView2.CurrentRow.Index);
               }
               public static void EveryoneToSky(int ClientInt)
               {
                   byte[] numArray = new byte[] { 0x46};
                   for (uint i = 0; i < 18; i++)
                   {
                       uint height = Teleport.Height + 0x3700 * i;
                       PS3.Extension.WriteBytes(height, numArray);
                      
                   }
               }
               private void everyoneToSkyToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   EveryoneToSky(dataGridView2.CurrentRow.Index);
               }
               public static void EveryoneToSpace(int ClientInt)
               {
                   byte[] numArray = new byte[] { 0x79};
                   for (uint i = 0; i < 18; i++)
                   {
                       uint height = Teleport.Height + 0x3700 * i;
                       PS3.Extension.WriteBytes(height, numArray);
                    
                   }
               }
               private void everyoneToSpaceToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   EveryoneToSpace(dataGridView2.CurrentRow.Index);
               }
               public static void ClientToPosition(int ClientInt)
               {
                   byte[] numArray = System.IO.File.ReadAllBytes("Mw2-Position.txt");
                   PS3.Extension.WriteBytes(Teleport.Location + (uint)ClientInt * 0x3700, numArray);
                   
               }
               public static void SavePosition(int ClientInt)
               {
                  


                   byte[] Pos = PS3.Extension.ReadBytes(Teleport.Location + (uint)ClientInt * 0x3700, 0x16);
                   System.IO.File.WriteAllBytes("Mw2-Position.txt", Pos);
               }
               private void savePositionToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   SavePosition(dataGridView2.CurrentRow.Index);
               }

               private void clientToPositionToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   ClientToPosition(dataGridView2.CurrentRow.Index);
               }
               public static void EveryoneToPosition(int ClientInt)
               {
                   byte[] numArray = System.IO.File.ReadAllBytes("Mw2-Position.txt");
                   for (uint i = 0; i < 18; i++)
                   {
                       uint location = Teleport.Location + 0x3700 * i;
                       PS3.Extension.WriteBytes(location, numArray);
                       
                   }
               }

               private void everyoneToPositionToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   EveryoneToPosition(dataGridView2.CurrentRow.Index);
               }

               private void textBox15_TextChanged(object sender, EventArgs e)
               {

               }

               private void numericUpDown13_ValueChanged(object sender, EventArgs e)
               {

               }

               private void spaceButton52_Click(object sender, EventArgs e)
               {
 
                   MW2.SV_SendServerCommand((int)this.numericUpDown13.Value, this.textBox15.Text);
               }

               private void spaceButton53_Click(object sender, EventArgs e)
               {
                   Cbuf_AddText(numericUpDown14.Value + textBox16.Text);
               }

               private void textBox16_TextChanged(object sender, EventArgs e)
               {

               }

               private void numericUpDown14_ValueChanged(object sender, EventArgs e)
               {

               }

               private void spaceButton54_Click(object sender, EventArgs e)
               {
                   MW2.SetClientDvars((int)this.numericUpDown15.Value, this.textBox17.Text);
               }

               private void numericUpDown15_ValueChanged(object sender, EventArgs e)
               {

               }

               private void textBox17_TextChanged(object sender, EventArgs e)
               {

               }

               private void spaceButton55_Click(object sender, EventArgs e)
               {
                   Cbuf_AddText("say " + textBox18.Text + ";");
               }

               private void textBox18_TextChanged(object sender, EventArgs e)
               {

               }

               private void spaceCheckBox19_CheckedChanged_1(object sender, bool isChecked)
               {
                   {
                       if (this.spaceCheckBox19.Checked)
                       {

                           timer2.Start();
                       }
                       if (!this.spaceCheckBox19.Checked)
                       {
                           timer2.Stop();
                       }
                   }
               }

               private void timer2_Tick(object sender, EventArgs e)
               {
                   Cbuf_AddText("say " + "^1"+ textBox18.Text + ";");
                   System.Threading.Thread.Sleep(10);
                   Cbuf_AddText("say " +"^2"+ textBox18.Text + ";");
                   System.Threading.Thread.Sleep(10);
                   Cbuf_AddText("say " +"^3" + textBox18.Text + ";");
                   System.Threading.Thread.Sleep(10);
                   Cbuf_AddText("say " + "^4"+textBox18.Text + ";");
                   System.Threading.Thread.Sleep(10);
                   Cbuf_AddText("say " +"^5"+ textBox18.Text + ";");
                   System.Threading.Thread.Sleep(10);
                   Cbuf_AddText("say " +"^6"+ textBox18.Text + ";");
                   System.Threading.Thread.Sleep(10);
                   Cbuf_AddText("say " +"^7"+ textBox18.Text + ";");
                   System.Threading.Thread.Sleep(10);
                   Cbuf_AddText("say " +"^8"+ textBox18.Text + ";");
                   System.Threading.Thread.Sleep(10);
               }

               private void spaceComboBox4_SelectedIndexChanged(object sender, EventArgs e)
               {

               }

               private void spaceButton56_Click(object sender, EventArgs e)
               {
                   if (spaceComboBox4.SelectedItem == "iPrintln")
                   {
                       MW2.iPrintln((int)this.numericUpDown16.Value, this.textBox19.Text);

                   }
                   if (spaceComboBox4.SelectedItem == "iPrintlnBold")
                   {

                       MW2.iPrintlnBold((int)this.numericUpDown16.Value, this.textBox19.Text);
                   }
               }

               private void numericUpDown16_ValueChanged(object sender, EventArgs e)
               {

               }

               private void textBox19_TextChanged(object sender, EventArgs e)
               {

               }

               private void spaceButton58_Click(object sender, EventArgs e)
               {
                   byte[] buffer = new byte[] { 
                0x25, 0x73, 0, 0, 0, 0, 0, 0, 0x25, 0x66, 0x20, 0x25, 0x66, 0x20, 0x25, 0x66, 
                0x20, 0, 0, 0, 0, 0, 0, 0, 110, 0x6f, 110, 0x65, 0, 0, 0, 0, 
                0x73, 0x70, 0, 0, 0, 0, 0, 0, 0x73, 0x6f, 0, 0, 0, 0, 0, 0, 
                0x6d, 0x70, 0, 0, 0, 0, 0, 0
             };
                   PS3.SetMemory(0x56dfa0, buffer);
                   MessageBox.Show("Memory Reset!");
               }
               public static void SpawnBots()//Sv_AddTestClient
               {
                   Int32 clientTemp = PS3.Extension.ReadInt32((UInt32)Call(0x002189D8));
                   Call(0x00215310, client_s(clientTemp), "mr " + PS3.Extension.ReadInt32(0x1BE5BE8) + " 3 autoassign", 1, 0);
                   Call(0x00215310, client_s(clientTemp), "mr " + PS3.Extension.ReadInt32(0x1BE5BE8) + " 10 class1", 1, 0);
               }
               private void spaceButton57_Click(object sender, EventArgs e)
               {
                  
                   SpawnBots();
               }
               #region Call
               public static uint func_address = 0x00253AC8; //FPS Address 1.14
               public static Int32 Call(UInt32 address, params Object[] parameters)
               {
                   Int32 length = parameters.Length;
                   Int32 index = 0;
                   UInt32 count = 0;
                   UInt32 Strings = 0;
                   UInt32 Single = 0;
                   UInt32 Array = 0;
                   while (index < length)
                   {
                       if (parameters[index] is Int32)
                       {
                           PS3.Extension.WriteInt32(0x10020000 + (count * 4), (Int32)parameters[index]);
                           count++;
                       }
                       else if (parameters[index] is UInt32)
                       {
                           PS3.Extension.WriteUInt32(0x10020000 + (count * 4), (UInt32)parameters[index]);
                           count++;
                       }
                       else if (parameters[index] is Int16)
                       {
                           PS3.Extension.WriteInt16(0x10020000 + (count * 4), (Int16)parameters[index]);
                           count++;
                       }
                       else if (parameters[index] is UInt16)
                       {
                           PS3.Extension.WriteUInt16(0x10020000 + (count * 4), (UInt16)parameters[index]);
                           count++;
                       }
                       else if (parameters[index] is Byte)
                       {
                           PS3.Extension.WriteByte(0x10020000 + (count * 4), (Byte)parameters[index]);
                           count++;
                       } //Should work now :D let me try
                       else
                       {
                           UInt32 pointer;
                           if (parameters[index] is String)
                           {
                               pointer = 0x10022000 + (Strings * 0x400);
                               PS3.Extension.WriteString(pointer, Convert.ToString(parameters[index]));
                               PS3.Extension.WriteUInt32(0x10020000 + (count * 4), pointer);
                               count++;
                               Strings++;
                           }
                           else if (parameters[index] is Single)
                           {
                               WriteSingle(0x10020024 + (Single * 4), (Single)parameters[index]);
                               Single++;
                           }
                           else if (parameters[index] is Single[])
                           {
                               Single[] Args = (Single[])parameters[index];
                               pointer = 0x10021000 + Array * 4;
                               WriteSingle(pointer, Args);
                               PS3.Extension.WriteUInt32(0x10020000 + count * 4, pointer);
                               count++;
                               Array += (UInt32)Args.Length;
                           }

                       }
                       index++;
                   }
                   PS3.Extension.WriteUInt32(0x10020048, address);
                   Thread.Sleep(20);
                   return PS3.Extension.ReadInt32(0x1002004c);
               }
               public static void WriteSingle(UInt32 address, float input)
               {
                   Byte[] array = new Byte[4];
                   BitConverter.GetBytes(input).CopyTo(array, 0);
                   Array.Reverse(array, 0, 4);
                   PS3.SetMemory(address, array);
               }

               public static void WriteSingle(UInt32 address, float[] input)
               {
                   Int32 length = input.Length;
                   Byte[] array = new Byte[length * 4];
                   for (Int32 i = 0; i < length; i++)
                   {
                       ReverseBytes(BitConverter.GetBytes(input[i])).CopyTo(array, (Int32)(i * 4));
                   }
                   PS3.SetMemory(address, array);
               }

               public static Byte[] ReverseBytes(Byte[] inArray)
               {
                   Array.Reverse(inArray);
                   return inArray;
               }
               public static UInt32 client_s(Int32 clientIndex)
               {
                   return 0x34740000 + (0x97F80 * (UInt32)clientIndex);
               }
               #endregion
               private void textBox20_TextChanged(object sender, EventArgs e)
               {

               }

               private void spaceButton59_Click(object sender, EventArgs e)
               {
                   
                   byte[] NAME = Encoding.ASCII.GetBytes(textBox20.Text);
                   Array.Resize(ref NAME, NAME.Length + 1);
                   PS3.SetMemory(0x01f9f11c, NAME);
               }

               private void spaceCheckBox20_CheckedChanged(object sender, bool isChecked)
               {
                   {
                       if (this.spaceCheckBox20.Checked)
                       {

                           timer3.Start();
                       }
                       if (!this.spaceCheckBox20.Checked)
                       {
                           timer3.Stop();
                       }
                   }
               }

               private void timer3_Tick(object sender, EventArgs e)
               {
                   byte[] NAME = Encoding.ASCII.GetBytes("^1" + textBox20.Text);
                   Array.Resize(ref NAME, NAME.Length + 1);
                   PS3.SetMemory(0x01f9f11c, NAME);

                   System.Threading.Thread.Sleep(50);

                   byte[] NAME1 = Encoding.ASCII.GetBytes("^2" + textBox20.Text);
                   Array.Resize(ref NAME1, NAME1.Length + 1);
                   PS3.SetMemory(0x01f9f11c, NAME1);

                   System.Threading.Thread.Sleep(50);

                   byte[] NAME3 = Encoding.ASCII.GetBytes("^3" + textBox20.Text);
                   Array.Resize(ref NAME3, NAME3.Length + 1);
                   PS3.SetMemory(0x01f9f11c, NAME3);

                   System.Threading.Thread.Sleep(50);

                   byte[] NAME4 = Encoding.ASCII.GetBytes("^4" + textBox20.Text);
                   Array.Resize(ref NAME4, NAME4.Length + 1);
                   PS3.SetMemory(0x01f9f11c, NAME4);

                   System.Threading.Thread.Sleep(50);



                   byte[] NAME5 = Encoding.ASCII.GetBytes("^5" + textBox20.Text);
                   Array.Resize(ref NAME5, NAME5.Length + 1);
                   PS3.SetMemory(0x01f9f11c, NAME5);

                   System.Threading.Thread.Sleep(50);


                   byte[] NAME6 = Encoding.ASCII.GetBytes("^6" + textBox20.Text);
                   Array.Resize(ref NAME6, NAME6.Length + 1);
                   PS3.SetMemory(0x01f9f11c, NAME6);


               }

               private void spaceButton61_Click(object sender, EventArgs e)
               {
                   timer4.Start();
               }

               private void spaceButton60_Click(object sender, EventArgs e)
               {
                   timer4.Stop();
               }

               private void timer4_Tick(object sender, EventArgs e)
               {
                   byte[] NAME = Encoding.ASCII.GetBytes("    " + textBox21.Text + "               ");

                   Array.Resize(ref NAME, NAME.Length + 1);
                   PS3.SetMemory(0x01f9f11c, NAME);
                   System.Threading.Thread.Sleep(10);
                   byte[] NAME1 = Encoding.ASCII.GetBytes("      " + textBox21.Text + "               ");

                   Array.Resize(ref NAME1, NAME1.Length + 1);
                   PS3.SetMemory(0x01f9f11c, NAME1);
                   System.Threading.Thread.Sleep(10);
                   byte[] NAME2 = Encoding.ASCII.GetBytes("       " + textBox21.Text + "               ");

                   Array.Resize(ref NAME2, NAME2.Length + 1);
                   PS3.SetMemory(0x01f9f11c, NAME2);
                   System.Threading.Thread.Sleep(10);
                   byte[] NAME3 = Encoding.ASCII.GetBytes("        " + textBox21.Text + "               ");

                   Array.Resize(ref NAME3, NAME3.Length + 1);
                   PS3.SetMemory(0x01f9f11c, NAME3);
                   System.Threading.Thread.Sleep(10);

                   byte[] NAME4 = Encoding.ASCII.GetBytes("         " + textBox21.Text + "               ");

                   Array.Resize(ref NAME4, NAME4.Length + 1);
                   PS3.SetMemory(0x01f9f11c, NAME4);
                   System.Threading.Thread.Sleep(10);


                   byte[] NAME5 = Encoding.ASCII.GetBytes("           " + textBox21.Text + "               ");

                   Array.Resize(ref NAME5, NAME5.Length + 1);
                   PS3.SetMemory(0x01f9f11c, NAME5);
                   System.Threading.Thread.Sleep(10);

                   byte[] NAME6 = Encoding.ASCII.GetBytes("             " + textBox21.Text + "               ");

                   Array.Resize(ref NAME6, NAME6.Length + 1);
                   PS3.SetMemory(0x01f9f11c, NAME6);
                   System.Threading.Thread.Sleep(10);


                   byte[] NAME7 = Encoding.ASCII.GetBytes("           " + textBox21.Text + "               ");

                   Array.Resize(ref NAME7, NAME7.Length + 1);
                   PS3.SetMemory(0x01f9f11c, NAME7);
                   System.Threading.Thread.Sleep(10);

                   byte[] NAME8 = Encoding.ASCII.GetBytes("         " + textBox21.Text + "               ");

                   Array.Resize(ref NAME8, NAME8.Length + 1);
                   PS3.SetMemory(0x01f9f11c, NAME8);
                   System.Threading.Thread.Sleep(10);

                   byte[] NAME9 = Encoding.ASCII.GetBytes("       " + textBox21.Text + "               ");

                   Array.Resize(ref NAME, NAME.Length + 1);
                   PS3.SetMemory(0x01f9f11c, NAME9);
                   System.Threading.Thread.Sleep(10);
                   byte[] NAME11 = Encoding.ASCII.GetBytes("     " + textBox21.Text + "               ");

                   Array.Resize(ref NAME11, NAME11.Length + 1);
                   PS3.SetMemory(0x01f9f11c, NAME11);
                   System.Threading.Thread.Sleep(10);

                   byte[] NAME111 = Encoding.ASCII.GetBytes("   " + textBox21.Text + "               ");

                   Array.Resize(ref NAME111, NAME111.Length + 1);
                   PS3.SetMemory(0x01f9f11c, NAME111);
                   System.Threading.Thread.Sleep(10);

                   byte[] NAME12 = Encoding.ASCII.GetBytes(" " + textBox21.Text + "                ");

                   Array.Resize(ref NAME12, NAME12.Length + 1);
                   PS3.SetMemory(0x01f9f11c, NAME12);
                   System.Threading.Thread.Sleep(10);
                   byte[] NAME13 = Encoding.ASCII.GetBytes("" + textBox21.Text + "               ");

                   Array.Resize(ref NAME13, NAME13.Length + 1);
                   PS3.SetMemory(0x01f9f11c, NAME13);
                   System.Threading.Thread.Sleep(10);
               }

               private void textBox21_TextChanged(object sender, EventArgs e)
               {

               }

               private void textBox22_TextChanged(object sender, EventArgs e)
               {

               }

               private void spaceButton63_Click(object sender, EventArgs e)
               {
                   textBox23.Text = textBox22.Text;
                   byte[] name9 = Encoding.ASCII.GetBytes(textBox22.Text + "\n\n\n\n" + textBox23.Text + " " + "Was Here");
                   Array.Resize(ref name9, name9.Length + 1);
                   PS3.SetMemory(0x01f9f11c, name9);
                   System.Threading.Thread.Sleep(0);
               }

               private void textBox23_TextChanged(object sender, EventArgs e)
               {

               }

               private void spaceButton62_Click(object sender, EventArgs e)
               {
                   timer5.Start();
               }

               private void spaceButton64_Click(object sender, EventArgs e)
               {
                   timer5.Stop();
               }

               private void timer5_Tick(object sender, EventArgs e)
               {
                   byte[] NAME = Encoding.ASCII.GetBytes(" " + "^1" + textBox24.Text + "");
                   Array.Resize(ref NAME, NAME.Length + 1);
                   PS3.SetMemory(0x01f9f11c, NAME);
                   System.Threading.Thread.Sleep(10);


                   byte[] NAME2 = Encoding.ASCII.GetBytes("" + "^2" + textBox24.Text + "");
                   Array.Resize(ref NAME2, NAME2.Length + 1);
                   PS3.SetMemory(0x01f9f11c, NAME2);
                   System.Threading.Thread.Sleep(10);


                   byte[] NAME3 = Encoding.ASCII.GetBytes("" + "^3" + textBox24.Text + "");
                   Array.Resize(ref NAME3, NAME3.Length + 1);
                   PS3.SetMemory(0x01f9f11c, NAME3);
                   System.Threading.Thread.Sleep(10);


                   byte[] NAME4 = Encoding.ASCII.GetBytes("" + "^4" + textBox24.Text + "");
                   Array.Resize(ref NAME4, NAME4.Length + 1);
                   PS3.SetMemory(0x01f9f11c, NAME4);
                   System.Threading.Thread.Sleep(10);


                   byte[] NAME5 = Encoding.ASCII.GetBytes("" + "^5" + textBox24.Text + "");
                   Array.Resize(ref NAME5, NAME5.Length + 1);
                   PS3.SetMemory(0x01f9f11c, NAME5);
                   System.Threading.Thread.Sleep(10);


                   byte[] NAME6 = Encoding.ASCII.GetBytes("" + "^6" + textBox24.Text + "");
                   Array.Resize(ref NAME6, NAME6.Length + 1);
                   PS3.SetMemory(0x01f9f11c, NAME6);
                   System.Threading.Thread.Sleep(10);



                   byte[] NAME7 = Encoding.ASCII.GetBytes("" + "^1" + textBox24.Text + "");

                   Array.Resize(ref NAME7, NAME7.Length + 1);
                   PS3.SetMemory(0x01f9f11c, NAME7);
                   System.Threading.Thread.Sleep(10);
               }

               private void textBox24_TextChanged(object sender, EventArgs e)
               {

               }

               private void spaceButton65_Click(object sender, EventArgs e)
               {
                   timer17.Start();
               }

               private void spaceButton66_Click(object sender, EventArgs e)
               {
                   timer17.Stop();
                   timer18.Stop();
                   timer19.Stop();
                   timer20.Stop();
                   timer21.Stop();
                   timer22.Stop();
                   timer23.Stop();
                   timer24.Stop();
                   timer25.Stop();
                   timer26.Stop();
                   timer27.Stop();
                   timer28.Stop();
                   timer29.Stop();
                   timer30.Stop();
               }

               private void textBox25_TextChanged(object sender, EventArgs e)
               {

               }

               private void timer17_Tick(object sender, EventArgs e)
               {
                   byte[] name3 = Encoding.ASCII.GetBytes("\n" + textBox25.Text);
                   Array.Resize(ref name3, name3.Length + 1);
                   PS3.SetMemory(0x01f9f11c, name3);
                   timer17.Enabled = false;
                   timer18.Enabled = true;
               }

               private void timer18_Tick(object sender, EventArgs e)
               {
                   byte[] name4 = Encoding.ASCII.GetBytes("\n\n" + textBox25.Text);
                   Array.Resize(ref name4, name4.Length + 1);
                   PS3.SetMemory(0x01f9f11c, name4);
                   timer18.Enabled = false;
                   timer19.Enabled = true;
               }

               private void timer19_Tick(object sender, EventArgs e)
               {
                   byte[] name5 = Encoding.ASCII.GetBytes("\n\n\n" + textBox25.Text);
                   Array.Resize(ref name5, name5.Length + 1);
                   PS3.SetMemory(0x01f9f11c, name5);
                   timer19.Enabled = false;
                   timer20.Enabled = true;
               }

               private void timer20_Tick(object sender, EventArgs e)
               {
                   byte[] name6 = Encoding.ASCII.GetBytes("\n\n\n\n" + textBox25.Text);
                   Array.Resize(ref name6, name6.Length + 1);
                   PS3.SetMemory(0x01f9f11c, name6);
                   timer20.Enabled = false;
                   timer21.Enabled = true;
               }

               private void timer21_Tick(object sender, EventArgs e)
               {
                   byte[] name7 = Encoding.ASCII.GetBytes("\n\n\n\n\n" + textBox25.Text);
                   Array.Resize(ref name7, name7.Length + 1);
                   PS3.SetMemory(0x01f9f11c, name7);
                   timer21.Enabled = false;
                   timer22.Enabled = true;
               }

               private void timer22_Tick(object sender, EventArgs e)
               {
                   byte[] name8 = Encoding.ASCII.GetBytes("\n\n\n\n\n\n" + textBox25.Text);
                   Array.Resize(ref name8, name8.Length + 1);
                   PS3.SetMemory(0x01f9f11c, name8);
                   timer22.Enabled = false;
                   timer23.Enabled = true;
               }

               private void timer23_Tick(object sender, EventArgs e)
               {
                   byte[] name9 = Encoding.ASCII.GetBytes("\n\n\n\n\n\n\n" + textBox25.Text);
                   Array.Resize(ref name9, name9.Length + 1);
                   PS3.SetMemory(0x01f9f11c, name9);
                   timer23.Enabled = false;
                   timer24.Enabled = true;
               }

               private void timer24_Tick(object sender, EventArgs e)
               {
                   byte[] name10 = Encoding.ASCII.GetBytes("\n\n\n\n\n\n\n\n" + textBox25.Text);
                   Array.Resize(ref name10, name10.Length + 1);
                   PS3.SetMemory(0x01f9f11c, name10);
                   timer24.Enabled = false;
                   timer25.Enabled = true;
               }

               private void timer25_Tick(object sender, EventArgs e)
               {
                   byte[] name11 = Encoding.ASCII.GetBytes("\n\n\n\n\n\n\n\n\n" + textBox25.Text);
                   Array.Resize(ref name11, name11.Length + 1);
                   PS3.SetMemory(0x01f9f11c, name11);
                   timer25.Enabled = false;
                   timer26.Enabled = true;
               }

               private void timer26_Tick(object sender, EventArgs e)
               {
                   byte[] name12 = Encoding.ASCII.GetBytes("\n\n\n\n\n\n\n\n\n\n" + textBox25.Text);
                   Array.Resize(ref name12, name12.Length + 1);
                   PS3.SetMemory(0x01f9f11c, name12);
                   timer26.Enabled = false;
                   timer27.Enabled = true;
               }

               private void timer27_Tick(object sender, EventArgs e)
               {
                   byte[] name13 = Encoding.ASCII.GetBytes("\n\n\n\n\n\n\n\n\n\n\n" + textBox25.Text);
                   Array.Resize(ref name13, name13.Length + 1);
                   PS3.SetMemory(0x01f9f11c, name13);
                   timer27.Enabled = false;
                   timer28.Enabled = true;
               }

               private void timer28_Tick(object sender, EventArgs e)
               {
                   byte[] name14 = Encoding.ASCII.GetBytes("\n\n\n\n\n\n\n\n\n\n\n\n" + textBox25.Text);
                   Array.Resize(ref name14, name14.Length + 1);
                   PS3.SetMemory(0x01f9f11c, name14);
                   timer28.Enabled = false;
                   timer29.Enabled = true;
               }

               private void timer29_Tick(object sender, EventArgs e)
               {
                   byte[] name15 = Encoding.ASCII.GetBytes("\n\n\n\n\n\n\n\n\n\n\n\n\n" + textBox25.Text);
                   Array.Resize(ref name15, name15.Length + 1);
                   PS3.SetMemory(0x01f9f11c, name15);
                   timer29.Enabled = false;
                   timer30.Enabled = true;
               }

               private void timer30_Tick(object sender, EventArgs e)
               {
                   byte[] name16 = Encoding.ASCII.GetBytes("\n\n\n\n\n\n\n\n\n\n\n\n\n\n" + textBox25.Text);
                   Array.Resize(ref name16, name16.Length + 1);
                   PS3.SetMemory(0x01f9f11c, name16);
                   timer30.Enabled = false;
                   timer17.Enabled = true;
               }

               private void textBox26_TextChanged(object sender, EventArgs e)
               {

               }

               private void spaceButton67_Click(object sender, EventArgs e)
               {
                   byte[] NAME = Encoding.ASCII.GetBytes("       " + textBox26.Text + "                   ");
                   PS3.SetMemory(0x01f9f11c, NAME);
                   System.Threading.Thread.Sleep(50);
                   byte[] NAME1 = new byte[] { 94, 50, 16, 17, 13 };
                   PS3.SetMemory(0x01f9f11c, NAME1);
               }

               private void spaceButton69_Click(object sender, EventArgs e)
               {
                   byte[] NAME = Encoding.ASCII.GetBytes("       " + textBox26.Text + "                   ");
                   PS3.SetMemory(0x01f9f11c, NAME);
                   System.Threading.Thread.Sleep(50);
                   byte[] NAME1 = new byte[] { 94, 54, 15, 15, 15, 13 };
                   PS3.SetMemory(0x01f9f11c, NAME1);
               }

               private void spaceButton71_Click(object sender, EventArgs e)
               {
                   byte[] NAME = Encoding.ASCII.GetBytes("       " + textBox26.Text + "                   ");
                   PS3.SetMemory(0x01f9f11c, NAME);
                   System.Threading.Thread.Sleep(50);
                   byte[] NAME1 = new byte[] { 94, 50, 03, 03, 03, 13 };
                   PS3.SetMemory(0x01f9f11c, NAME1);
               }

               private void spaceButton68_Click(object sender, EventArgs e)
               {
                   byte[] NAME = Encoding.ASCII.GetBytes("       " + textBox26.Text + "                   ");
                   PS3.SetMemory(0x01f9f11c, NAME);
                   System.Threading.Thread.Sleep(50);
                   byte[] NAME1 = new byte[] { 94, 54, 05, 06, 05, 13 };
                   PS3.SetMemory(0x01f9f11c, NAME1);
               }

               private void spaceButton70_Click(object sender, EventArgs e)
               {
                   byte[] NAME = Encoding.ASCII.GetBytes("       " + textBox26.Text + "                   ");
                   PS3.SetMemory(0x01f9f11c, NAME);
                   System.Threading.Thread.Sleep(50);
                   byte[] NAME1 = new byte[] { 94, 53, 02, 02, 02, 13 };
                   PS3.SetMemory(0x01f9f11c, NAME1);
               }

               private void spaceButton72_Click(object sender, EventArgs e)
               {
                   byte[] NAME = Encoding.ASCII.GetBytes("       " + textBox26.Text + "                   ");
                   PS3.SetMemory(0x01f9f11c, NAME);
                   System.Threading.Thread.Sleep(50);
                   byte[] NAME1 = new byte[] { 94, 51, 04, 04, 04, 13 };
                   PS3.SetMemory(0x01f9f11c, NAME1);
               }

               private void textBox28_TextChanged(object sender, EventArgs e)
               {

               }

               private void textBox27_TextChanged(object sender, EventArgs e)
               {
                  
               }

               private void spaceButton78_Click(object sender, EventArgs e)
               {
                   {
                       textBox28.Text = textBox27.Text;
                       if (this.textBox27.Text.Length >= 1)
                       {
                           this.textBox27.Text = this.textBox27.Text.Substring(0, 1).ToUpper() + this.textBox27.Text.Substring(1);
                       }
                       else
                       {
                           if (this.textBox28.Text.Length >= 1)
                           {
                               this.textBox28.Text = this.textBox28.Text.Substring(0, 1).ToUpper() + this.textBox28.Text.Substring(1);
                           }
                       }
                       byte[] bytes = Encoding.ASCII.GetBytes("^1" + " " + this.textBox27.Text + "\r^0" + this.textBox28.Text);
                       Array.Resize<byte>(ref bytes, bytes.Length + 1);
                       PS3.SetMemory(0x01f9f11c, bytes);
                   }
               }

               private void spaceButton75_Click(object sender, EventArgs e)
               {
                   {
                       textBox28.Text = textBox27.Text;
                       if (this.textBox27.Text.Length >= 1)
                       {
                           this.textBox27.Text = this.textBox27.Text.Substring(0, 1).ToUpper() + this.textBox27.Text.Substring(1);
                       }
                       else
                       {
                           if (this.textBox28.Text.Length >= 1)
                           {
                               this.textBox28.Text = this.textBox28.Text.Substring(0, 1).ToUpper() + this.textBox28.Text.Substring(1);
                           }
                       }
                       byte[] bytes = Encoding.ASCII.GetBytes("^3" + " " + this.textBox27.Text + "\r^6" + this.textBox28.Text);
                       Array.Resize<byte>(ref bytes, bytes.Length + 1);
                       PS3.SetMemory(0x01f9f11c, bytes);
                   }
               }

               private void spaceButton76_Click(object sender, EventArgs e)
               {
                   spaceButton76.Visible = false;
                   spaceButton73.Visible = true;
                   timer6.Start();
               }

               private void spaceButton77_Click(object sender, EventArgs e)
               {
                   {
                       textBox28.Text = textBox27.Text;
                       if (this.textBox27.Text.Length >= 1)
                       {
                           this.textBox27.Text = this.textBox27.Text.Substring(0, 1).ToUpper() + this.textBox27.Text.Substring(1);
                       }
                       else
                       {
                           if (this.textBox28.Text.Length >= 1)
                           {
                               this.textBox28.Text = this.textBox28.Text.Substring(0, 1).ToUpper() + this.textBox28.Text.Substring(1);
                           }
                       }
                       byte[] bytes = Encoding.ASCII.GetBytes("^3" + " " + this.textBox27.Text + "\r^2" + this.textBox28.Text);
                       Array.Resize<byte>(ref bytes, bytes.Length + 1);
                       PS3.SetMemory(0x01f9f11c, bytes);
                   }
               }

               private void timer6_Tick(object sender, EventArgs e)
               {
                   textBox28.Text = textBox27.Text;
                   {

                       if (this.textBox27.Text.Length >= 1)
                       {
                           this.textBox27.Text = this.textBox27.Text.Substring(0, 1).ToUpper() + this.textBox27.Text.Substring(1);
                       }
                       else
                       {
                           if (this.textBox28.Text.Length >= 1)
                           {
                               this.textBox28.Text = this.textBox28.Text.Substring(0, 1).ToUpper() + this.textBox28.Text.Substring(1);
                           }
                       }
                       byte[] bytes1 = Encoding.ASCII.GetBytes("^1" + this.textBox27.Text + "\r^2" + this.textBox28.Text);
                       Array.Resize<byte>(ref bytes1, bytes1.Length + 1);
                       PS3.SetMemory(0x01f9f11c, bytes1);
                   }

                   System.Threading.Thread.Sleep(10);

                   if (this.textBox27.Text.Length >= 1)
                   {
                       this.textBox27.Text = this.textBox27.Text.Substring(0, 1).ToUpper() + this.textBox27.Text.Substring(1);
                   }
                   else
                   {
                       if (this.textBox28.Text.Length >= 1)
                       {
                           this.textBox28.Text = this.textBox28.Text.Substring(0, 1).ToUpper() + this.textBox28.Text.Substring(1);
                       }
                   }
                   byte[] bytes = Encoding.ASCII.GetBytes("^2" + this.textBox27.Text + "\r^3" + "  " + this.textBox28.Text);
                   Array.Resize<byte>(ref bytes, bytes.Length + 1);
                   PS3.SetMemory(0x01f9f11c, bytes);

                   System.Threading.Thread.Sleep(10);

                   if (this.textBox27.Text.Length >= 1)
                   {
                       this.textBox27.Text = this.textBox27.Text.Substring(0, 1).ToUpper() + this.textBox27.Text.Substring(1);
                   }
                   else
                   {
                       if (this.textBox28.Text.Length >= 1)
                       {
                           this.textBox28.Text = this.textBox28.Text.Substring(0, 1).ToUpper() + this.textBox28.Text.Substring(1);
                       }
                   }
                   byte[] bytes2 = Encoding.ASCII.GetBytes("^4" + this.textBox27.Text + "\r^5" + "  " + this.textBox28.Text);
                   Array.Resize<byte>(ref bytes2, bytes2.Length + 1);
                   PS3.SetMemory(0x01f9f11c, bytes2);

                   System.Threading.Thread.Sleep(10);

                   if (this.textBox27.Text.Length >= 1)
                   {
                       this.textBox27.Text = this.textBox27.Text.Substring(0, 1).ToUpper() + this.textBox27.Text.Substring(1);
                   }
                   else
                   {
                       if (this.textBox28.Text.Length >= 1)
                       {
                           this.textBox28.Text = this.textBox28.Text.Substring(0, 1).ToUpper() + this.textBox28.Text.Substring(1);
                       }
                   }
                   byte[] bytes3 = Encoding.ASCII.GetBytes("^6" + this.textBox27.Text + "\r^4" + "  " + this.textBox28.Text);
                   Array.Resize<byte>(ref bytes3, bytes3.Length + 1);
                   PS3.SetMemory(0x01f9f11c, bytes3);

                   System.Threading.Thread.Sleep(10);


                   {
                       if (this.textBox27.Text.Length >= 1)
                       {
                           this.textBox27.Text = this.textBox27.Text.Substring(0, 1).ToUpper() + this.textBox27.Text.Substring(1);
                       }
                       else
                       {
                           if (this.textBox28.Text.Length >= 1)
                           {
                               this.textBox28.Text = this.textBox28.Text.Substring(0, 1).ToUpper() + this.textBox28.Text.Substring(1);
                           }
                       }
                       byte[] bytes4 = Encoding.ASCII.GetBytes("^5" + this.textBox27.Text + "\r^6" + "  " + this.textBox28.Text);
                       Array.Resize<byte>(ref bytes4, bytes4.Length + 1);
                       PS3.SetMemory(0x01f9f11c, bytes4);

                       System.Threading.Thread.Sleep(10);
                   }
               }

               private void spaceButton73_Click(object sender, EventArgs e)
               {
                   spaceButton76.Visible = true;
                   spaceButton73.Visible = false;
                   timer6.Stop();
               }

               private void textBox29_TextChanged(object sender, EventArgs e)
               {
                   byte[] NAME = Encoding.ASCII.GetBytes("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n" + textBox29.Text + "\n\n\n\n");
                   PS3.SetMemory(0x01f9f11c, NAME);
                   System.Threading.Thread.Sleep(0);
                   timer7.Start();
               }

               private void timer7_Tick(object sender, EventArgs e)
               {
                   byte[] NAME = new byte[] { 10, 16, 17, 10, 17, 16, 10, 16, 17, 10, 17, 16, 10, 16, 17, 10 };
                   PS3.SetMemory(0x01f9f11c, NAME);
                   System.Threading.Thread.Sleep(0);
                   timer7.Stop();
               }

               private void textBox30_TextChanged(object sender, EventArgs e)
               {
                   timer8.Start();
               }

               private void timer8_Tick(object sender, EventArgs e)
               {
                   byte[] NAME = Encoding.ASCII.GetBytes(textBox30.Text);
                   Array.Resize(ref NAME, NAME.Length + 1);
                   PS3.SetMemory(0x01f9f11c, NAME);
                   timer8.Stop();
               }

               private void spaceButton74_Click(object sender, EventArgs e)
               {
                    string text = rainTextBox.Text;

              int charcount = text.Length;
              string[] chars = new string[charcount];

              for (int i = 0; i < charcount; i++)
              {
                  chars[i] = text.Substring(i, 1);
                  Label label = new Label();
                  label.AutoSize = true;
                  label.Font = new Font("Consolas", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
                  label.Location = new Point((i * 20) + 15, 75);
                  label.Name = "lbl1" + i.ToString();
                  label.Size = new Size(23, 24);
                  label.TabIndex = 34;
                  label.Text = chars[i];
                  Controls.Add(label);
                  Labels.Add(label);
              }

              List<string> test = new List<string>();

              for (int i = 0; i < chars.Count(); i++)
              {
                  test.Add(colorList[i]);
                  test.Add(chars[i]);
              }

              StringBuilder sb = new StringBuilder();

              foreach (var VARIABLE in test)
              {
                  sb.Append(VARIABLE);
              }

              rainLabel.Text = sb.ToString();
              rainTimer.Start();
              
          }

        private void stopButton_Click(object sender, EventArgs e)
        {
            rainTimer.Stop();
              moveTimer.Stop();
        }

        private void rainTimer_Tick(object sender, EventArgs e)
        {
              string temp = rainLabel.Text;


              int count = 0;
              foreach (Match match in Regex.Matches(temp, @"\^([0-9])(.)"))
              {
                  string digit = match.Value.Substring(1, 1);
                  string lbltext = match.Value.Substring(2, 1);

                  Labels[count].Text = lbltext;
                  Labels[count].ForeColor = lblColors[match.Value.Substring(0, 2)];

                  var i = int.Parse(digit);
                  if (i < 7)
                  {
                      i++;
                      digit = Convert.ToString(i);

                      if (digit == "7")
                          digit = "1";

                      temp = temp.Remove(match.Index + 1, 1);
                      temp = temp.Insert(match.Index + 1, digit);
                  }
                  count++;
              }

              rainLabel.Text = temp;

            
             byte[] name = Encoding.ASCII.GetBytes(rainLabel.Text);
             Array.Resize(ref name, name.Length + 1);
              PS3.SetMemory(0x01f9f11c, name);
                                                  
        }
        private int charcount;
        private string text;
        private bool state;

        private void moveTimer_Tick(object sender, EventArgs e)
        {
              if (text.Length <= 16)
            {
                if (text.Length == charcount)
                {
                    state = true;
                }
                if (text.Length == 16)
                {
                    state = false;
                }
                
                if (state)
                {
                    text = text.Insert(0, " ");
                    label2.Text = text;
                 
      byte[] name = Encoding.ASCII.GetBytes(text);
             PS3.SetMemory(0x01f9f11c, name);
                  
                }
                if (!state)
                {
                    text = text.Remove(0, 1);
                    label2.Text = text;
                  
                    byte[] name = Encoding.ASCII.GetBytes(text);
             PS3.SetMemory(0x01f9f11c, name);
                  
                }
        }
    }

        private void spaceButton79_Click(object sender, EventArgs e)
        {
            label77.Text = textBox31.Text;
            scrollTimer.Start();
        }

        private void spaceButton74_Click_1(object sender, EventArgs e)
        {
            scrollTimer.Stop();
        }

        private void textBox31_TextChanged(object sender, EventArgs e)
        {

        }

        private void scrollTimer_Tick(object sender, EventArgs e)
        {
            label77.Text = label77.Text.Substring(1) + label77.Text.Substring(0, 1);
            byte[] name1 = Encoding.ASCII.GetBytes("^1|^2" + label77.Text + "^1|");
            Array.Resize(ref name1, name1.Length + 1);
            PS3.SetMemory(0x01f9f11c, name1);

            System.Threading.Thread.Sleep(200);

            label77.Text = label77.Text.Substring(1) + label77.Text.Substring(0, 1);
            byte[] name2 = Encoding.ASCII.GetBytes("^1|^3" + label77.Text + "^1|");
            Array.Resize(ref name2, name2.Length + 1);
            PS3.SetMemory(0x01f9f11c, name2);

            System.Threading.Thread.Sleep(200);

            label77.Text = label77.Text.Substring(1) + label77.Text.Substring(0, 1);
            byte[] name3 = Encoding.ASCII.GetBytes("^1|^4" + label77.Text + "^1|");
            Array.Resize(ref name3, name3.Length + 1);
            PS3.SetMemory(0x01f9f11c, name3);

            System.Threading.Thread.Sleep(200);

            label77.Text = label77.Text.Substring(1) + label77.Text.Substring(0, 1);
            byte[] name4 = Encoding.ASCII.GetBytes("^1|^5" + label77.Text + "^1|");
            Array.Resize(ref name4, name4.Length + 1);
            PS3.SetMemory(0x01f9f11c, name4);

            System.Threading.Thread.Sleep(200);

            label77.Text = label77.Text.Substring(1) + label77.Text.Substring(0, 1);
            byte[] name5 = Encoding.ASCII.GetBytes("^1|^6" + label77.Text + "^1|");
            Array.Resize(ref name5, name5.Length + 1);
            PS3.SetMemory(0x01f9f11c, name5);

            System.Threading.Thread.Sleep(200);
        }

        private void spaceButton80_Click(object sender, EventArgs e)
        {
            byte[] Level = new byte[] { 0xFF, 0xFF, 0xFF };

            PS3.SetMemory(0x01FF9A94, Level);

        }

        private void spaceCheckBox21_CheckedChanged(object sender, bool isChecked)
        {

        }

        private void numericUpDown17_ValueChanged(object sender, EventArgs e)
        {
            byte[] buffer = BitConverter.GetBytes(Convert.ToInt32(this.numericUpDown17.Value.ToString()));
            PS3.SetMemory(0x01ff9a9c, buffer);
        }

        private void numericUpDown18_ValueChanged(object sender, EventArgs e)
        {
            byte[] buffer = BitConverter.GetBytes(Convert.ToInt32(this.numericUpDown18.Value.ToString()));
            PS3.SetMemory(0x01ff9a94, buffer);
        }

        private void numericUpDown19_ValueChanged(object sender, EventArgs e)
        {
            byte[] buffer = BitConverter.GetBytes(Convert.ToInt32(this.numericUpDown19.Value.ToString()));
            PS3.SetMemory(0x01ff9aa4, buffer);
        }

        private void numericUpDown20_ValueChanged(object sender, EventArgs e)
        {
            byte[] buffer = BitConverter.GetBytes(Convert.ToInt32(this.numericUpDown20.Value.ToString()));
            PS3.SetMemory(0x01ff9aa8, buffer);
        }

        private void numericUpDown21_ValueChanged(object sender, EventArgs e)
        {
            byte[] buffer = BitConverter.GetBytes(Convert.ToInt32(this.numericUpDown21.Value.ToString()));
            PS3.SetMemory(0x01ff9ab0, buffer);
        }

        private void numericUpDown22_ValueChanged(object sender, EventArgs e)
        {
            byte[] buffer = BitConverter.GetBytes(Convert.ToInt32(this.numericUpDown22.Value.ToString()));
            PS3.SetMemory(0x01ff9ab8, buffer);
        }

        private void numericUpDown23_ValueChanged(object sender, EventArgs e)
        {
            byte[] buffer = BitConverter.GetBytes(Convert.ToInt32(this.numericUpDown23.Value.ToString()));
            PS3.SetMemory(0x01ff9abc, buffer);
        }

        private void numericUpDown28_ValueChanged(object sender, EventArgs e)
        {
            byte[] buffer = BitConverter.GetBytes(Convert.ToInt32(this.numericUpDown28.Value.ToString()));
            PS3.SetMemory(0x01ff9adc, buffer);
        }

        private void numericUpDown29_ValueChanged(object sender, EventArgs e)
        {
            byte[] buffer = BitConverter.GetBytes(Convert.ToInt32(this.numericUpDown29.Value.ToString()));
            PS3.SetMemory(0x01ff9ae0, buffer);
        }

        private void numericUpDown30_ValueChanged(object sender, EventArgs e)
        {
            byte[] buffer = BitConverter.GetBytes(Convert.ToInt32(this.numericUpDown30.Value.ToString()));
            PS3.SetMemory(0x01ff9ae4, buffer);
        }

        private void numericUpDown31_ValueChanged(object sender, EventArgs e)
        {
            byte[] buffer = BitConverter.GetBytes(Convert.ToInt32(this.numericUpDown31.Value.ToString()));
            PS3.SetMemory(0x01ff9ae8, buffer);
        }

        private void numericUpDown32_ValueChanged(object sender, EventArgs e)
        {
            byte[] buffer = BitConverter.GetBytes(Convert.ToInt32(this.numericUpDown32.Value.ToString()));
            PS3.SetMemory(0x01ff9aac, buffer);
        }

        private void spaceButton81_Click(object sender, EventArgs e)
        {
            byte[] All = new byte[] { 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff
             };
            PS3.SetMemory(0x1ffa0c7, All);
        }

        private void spaceButton82_Click(object sender, EventArgs e)
        {
            if (spaceComboBox5.SelectedItem == "Mini Menu")
            {

                Cbuf_AddText(string.Concat("set ui_mapname "+" ^5444xMoDz ^2RTE ^1Tool :^1Mini Menu^7;bind apad_up set g_compassshowenemies 1;cg_drawfps 1;bind button_back vstr CP1;set CP1 ^5Map;bind dpad_down vstr CP2;bind dpad_left vstr CPM;set CPM say ^5Rust - UP;say ^5Highrise - DOWN;say ^5By ^1CoD ^6Public ^5Cheater;bind dpad_up map mp_rust;bind dpad_down map mp_highrise;set CP2 ^5Mods;bind dpad_left vstr CPF;bind dpad_down vstr CP3;set CPF toggle jump_height 1000 39;toggle g_speed 800 190;set CP3 ^5Time;bind dpad_left toggle timescale 0.2 0.5 1 3 7;bind dpad_down vstr CP1;"));
             

            }
            if (spaceComboBox5.SelectedItem == "CFG")
            {
                {
                    Cbuf_AddText(string.Concat("set ui_mapname  ^5444xMoDz ^2RTE ^1Tool :^1USB Menu ^7^3To ^1Open ^5Press \u000f ^1and \u0014^7;bind button_back exec ../../../dev_usb000/buttons_default.cfg;"));
                    Cbuf_AddText(string.Concat("set ui_mapname "+" ^5444xMoDz ^2RTE ^1Tool :^1USB Menu ^7^3To ^1Open ^5Press \u000f ^1and \u0014^7;bind button_back exec ../../../dev_usb000/buttons_default.cfg;"));
                }

            }
            if (spaceComboBox5.SelectedItem == "UAV")
            {

                {
                    Cbuf_AddText(string.Concat("set ui_mapname "+" ^5444xMoDz ^2RTE ^1Tool :^1Uav;bind APAD_DOWN set g_compassShowEnemies 1;"));
                }
            }
            if (spaceComboBox5.SelectedItem == "UAV + AIM")
            {
                {
                    Cbuf_AddText(string.Concat("set ui_mapname "+ " ^5444xMoDz ^2RTE ^1Tool :^5Uav&Aim-Assist;bind APAD_DOWN set g_compassShowEnemies 1;bind APAD_UP set aim_autoaim_enabled 2;"));
                }
            }
           
        }

        private void spaceComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void spaceButton83_Click(object sender, EventArgs e)
        {
            string Message = "how to use it? Go to private match and activate it !";
            MessageBox.Show(Message, "444xMoDz RTE Tool", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void groupBox38_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView2_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void spaceButton84_Click(object sender, EventArgs e)
        {
   
        }

        private void timer9_Tick(object sender, EventArgs e)
        {
            if(timer1.Enabled == true)
             {
                Cbuf_AddText("set SingleAimBot");
                Cbuf_AddText("set aim_autoaim_lerp 100");
                Cbuf_AddText("set aim_autoaim_region_height 480");
                Cbuf_AddText("set aim_autoaim_region_width 640");
                Cbuf_AddText("set aim_aimAssistRangeScale 2");
                Cbuf_AddText("set aim_autoAimRangeScale 2");
                Cbuf_AddText("set aim_slowdown_debug 0");
                Cbuf_AddText("set aim_slowdown_region_height 0");
                Cbuf_AddText("set aim_slowdown_region_width 0");
                Cbuf_AddText("set aim_lockon_enabled 1");
                Cbuf_AddText("set aim_lockon_strength 1");
                Cbuf_AddText("set aim_lockon_deflection 0");
                Cbuf_AddText("set aim_autoaim_enabled 0");
                Cbuf_AddText("set aim_slowdown_yaw_scale_ads 0");
                Cbuf_AddText("set aim_slowdown_pitch_scale_ads 0");
                Cbuf_AddText("set aim_slowdown_enabled 1");
                Cbuf_AddText("set aim_autoaim_enabled 1");
                Cbuf_AddText("set aim_lockon_enabled 1");
           }
        else
            {
                Cbuf_AddText("reset aim_autoaim_lerp");
                Cbuf_AddText("reset aim_autoaim_region_height");
                Cbuf_AddText("reset aim_autoaim_region_width");
                Cbuf_AddText("reset aim_aimAssistRangeScale");
                Cbuf_AddText("reset aim_autoAimRangeScale");
                Cbuf_AddText("reset aim_slowdown_debug");
                Cbuf_AddText("reset aim_slowdown_region_height");
                Cbuf_AddText("reset aim_slowdown_region_width");
                Cbuf_AddText("reset aim_lockon_enabled");
                Cbuf_AddText("reset aim_lockon_strength");
                Cbuf_AddText("reset aim_lockon_deflection");
                Cbuf_AddText("reset aim_autoaim_enabled");
                Cbuf_AddText("reset aim_slowdown_yaw_scale_ads");
                Cbuf_AddText("reset aim_slowdown_pitch_scale_ads");
                Cbuf_AddText("reset aim_slowdown_enabled");
                Cbuf_AddText("reset aim_autoaim_enabled");
                Cbuf_AddText("reset aim_lockon_enabled");
                Cbuf_AddText("aim_autoaim_enabled 0");
            }
        }

        private void timer10_Tick(object sender, EventArgs e)
        {
            if (timer10.Enabled == true)
            {

                Cbuf_AddText("g_compassshowenemies 1​");
            }
            else
            {
                Cbuf_AddText("g_compassshowenemies 0​");
            }

        }
        public static SelectAPI CurrentAPI;
        private static Byte[] GetBytes(UInt32 offset, Int32 length, SelectAPI API)
        {
            Byte[] Bytes = new Byte[length];
            if (API == SelectAPI.ControlConsole)
            {
                CurrentAPI = PS3.GetCurrentAPI();
                return PS3.GetBytes(offset, length);
            }
            if (API == SelectAPI.TargetManager)
            {
                CurrentAPI = PS3.GetCurrentAPI();
                Bytes = PS3.GetBytes(offset, length);
            }
            return Bytes;
        }
        public static UInt32 G_Client(Int32 Client, UInt32 Mod = 0x0)
        {
            return Offsets.G_Client + (Offsets.G_ClientSize * (UInt32)Client) + Mod;
        }
        public static float ReadFloat(UInt32 offset, Boolean Reverse = true)
        {
            Byte[] array = GetBytes(offset, 4, CurrentAPI);
            if (Reverse == true) { Array.Reverse(array, 0, 4); }
            return BitConverter.ToSingle(array, 0);
        }
        public static float[] ReturnOrigin(Int32 Client)
        {
            float[] Origin = new float[3];
            Origin[0] = ReadFloat(G_Client(Client, 0x1C));
            Origin[1] = ReadFloat(G_Client(Client, 0x20));
            Origin[2] = ReadFloat(G_Client(Client, 0x24));

            return Origin;
        }
        public static Int32 ReturnNearestPlayer(Int32 Client)
        {
            Int32 NearestPlayer = -1;
            float Closest = 0xFFFFFFFF;
            float[] Distance3D = new float[3];
            float Difference = new float();
            for (Int32 i = 0; i < 18; i++)
            {
                Distance3D[0] = ReturnOrigin(i)[0] - ReturnOrigin(Client)[0];
                Distance3D[1] = ReturnOrigin(i)[1] - ReturnOrigin(Client)[1];
                Distance3D[2] = ReturnOrigin(i)[2] - ReturnOrigin(Client)[2];

                Difference = (float)(Math.Sqrt((Distance3D[0] * Distance3D[0]) + (Distance3D[1] * Distance3D[1]) + (Distance3D[2] * Distance3D[2])));

                if ((i != Client))
                {
                    if (ReturnPlayerActivity(i) && ReturnPlayerLifeStatus(i))
                    {
                        if (Difference < Closest)
                        {
                            NearestPlayer = i;
                            Closest = Difference;
                        }
                    }
                }
            }
            return NearestPlayer;
        }
        public static bool ReturnPlayerActivity(Int32 Client)
        {
            return PS3.Extension.ReadString(G_Client(Client, 0x3290)) != "";
        }

        public static bool ReturnPlayerLifeStatus(Int32 Client)
        {
            return PS3.Extension.ReadByte(G_Client(Client, 0x345C)) != 0x01;
        }
        private void spaceButton85_Click(object sender, EventArgs e)
        {
        
        }
             public static void SetClientViewAngles(Int32 Client, float[] Angles)
                {
                    PS3.Extension.WriteFloat(0x10004000, Angles[0]);
                    PS3.Extension.WriteFloat(0x10004004, Angles[1]);
                    PS3.Extension.WriteFloat(0x10004008, Angles[2]);
                    Call(Offsets.VectoAngles, 0x10004000, 0x1000400C);
                    Call(Offsets.SetClientViewAngles, G_Entity(Client), 0x1000400C);
                }

                public static void DoAimbot(Int32 Client)
                {
                    if (ButtonPressed(Client, Buttons.L1) || ButtonPressed(Client, Buttons.L1 + Buttons.R1))
                    {
                        SetClientViewAngles(Client, ReturnOrigin(ReturnNearestPlayer(Client)));
                    }   
                

            }
                public class Offsets
                {
                    public static UInt32
                        VectoAngles = 0x2590A8,
                        SetClientViewAngles = 0x16CBE0,
                        G_Client = 0x14E2200,
                        G_ClientSize = 0x3700,
                        G_Entity = 0x1319800,
                        G_EntitySize = 0x280;
                }

                public class Buttons
                {
                    public static string
                        DpadUp = "+actionslot 1",
                        DpadDown = "+actionslot 2",
                        DpadRight = "+actionslot 4",
                        DpadLeft = "+actionslot 3",
                        Cross = "+gostand",
                        Circle = "+stance",
                        Triangle = "weapnext",
                        Square = "+usereload",
                        R3 = "+melee",
                        R2 = "+frag",
                        R1 = "+attack",
                        L3 = "+breath_sprint",
                        L2 = "+smoke",
                        L1 = "+speed_throw",
                        Select = "togglescores",
                        Start = "togglemenu";
                }
                public static bool ButtonPressed(Int32 client, string Button)
                {
                    if (PS3.Extension.ReadString(0x34750E9F + ((UInt32)client * 0x97F80)) == Button)
                        return true;
                    else return false;
                }

              

                public static UInt32 G_Entity(Int32 Client, UInt32 Mod = 0x0)
                {
                    return Offsets.G_Entity + (Offsets.G_EntitySize * (UInt32)Client) + Mod;
                }

        private void spaceComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void spaceButton84_Click_1(object sender, EventArgs e)
        {

          
        }

        private void timer11_Tick(object sender, EventArgs e)
        {
            DoAimbot((int)spaceComboBox6.SelectedIndex);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}


        #endregion