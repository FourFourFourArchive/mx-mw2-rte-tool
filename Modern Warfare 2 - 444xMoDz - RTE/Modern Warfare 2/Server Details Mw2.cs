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
namespace Modern_Warfare_2
{
    public partial class Server_Details_Mw2 : Form
    {

        public static PS3API PS3 = new PS3API();
        public Server_Details_Mw2()
        {
            InitializeComponent();

        }




        public String ReturnInfos(Int32 Index)
        {
            return Encoding.ASCII.GetString(PS3.GetBytes(0x00824f03, 0x100)).Replace(@"\", "|").Split('|')[Index];
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
        private void chromeForm1_Click(object sender, EventArgs e)
        {
 
          
        }

        private void spaceButton1_Click(object sender, EventArgs e)
        {
            label1.Text = getHostName();
            label2.Text = getMapName();
            label3.Text = getGameMode();
            label4.Text = getHardcore();
            label5.Text = getMaxPlayers();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
