using DiscordRPC;
using GTA;
using GTA.Native;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace IVPresence {
	public class IVPresence : Script {

		public DiscordRpcClient client = new DiscordRpcClient("568611046995263508");

		public IVPresence() {
			client.Initialize();

			client.OnReady += (sender, e) => {
				Game.Console.Print("Received Ready from user " + e.User.Username);
			};

			client.OnPresenceUpdate += (sender, e) => {
				Game.Console.Print("Received Update! " + e.Presence);
			};

			Interval = 7500;
			Tick += new EventHandler(Main);
		}

		public void Main(object sender, EventArgs e) {
			dynamic CurVehicle;
			if (Exists(Player.Character.CurrentVehicle)) {
				CurVehicle = "In Vehicle";
			}
			else {
				CurVehicle = "On Foot";
			}

			dynamic CurPos = Player.Character.Position;
			string CurZone = Function.Call("GET_MAP_AREA_FROM_COORDS", CurPos.X, CurPos.Y, CurPos.Z).ToString(); // How the fuck do I assign this to a variable??

			// I wish there was a better way...
			string CurWeapon = Player.Character.Weapons.Current.Type.ToString();
			string CurWeaponIcon = Player.Character.Weapons.Current.Type.ToString().ToLower();
			if (CurWeapon == "AssaultRifle_AK47") {
				CurWeapon = "AK-47";
			}
			else if (CurWeapon == "AssaultRifle_M4") {
				CurWeapon = "M4";
			}
			else if (CurWeapon == "BarettaShotgun") {
				CurWeapon = "Beretta 1201";
			}
			else if (CurWeapon == "BaseballBat") {
				CurWeapon = "Baseball Bat";
			}
			else if (CurWeapon == "BasicShotgun") {
				CurWeapon = "Shotgun";
			}
			else if (CurWeapon == "BasicSniperRifle") {
				CurWeapon = "Sniper Rifle";
			}
			else if (CurWeapon == "DesertEagle") {
				CurWeapon = "Desert Eagle";
			}
			else if (CurWeapon == "Glock") {
				CurWeapon = "Glock 17";
			}
			else if (CurWeapon == "Grenades") {
				CurWeapon = "Grenade";
			}
			else if (CurWeapon == "Knife") {
				CurWeapon = "Knife";
			}
			else if (CurWeapon == "MolotovCocktails") {
				CurWeapon = "Molotov Cocktail";
			}
			else if (CurWeapon == "MP5") {
				CurWeapon = "MP5";
			}
			else if (CurWeapon == "RocketLauncher") {
				CurWeapon = "Rocket Launcher";
			}
			else if (CurWeapon == "SniperRifle_M40A1") {
				CurWeapon = "M40A1";
			}
			else if (CurWeapon == "Uzi") {
				CurWeapon = "Uzi";
			}
			else {
				CurWeapon = "Unarmed";
			}
			// As I said, I wish there was a better way...
			//CurWeapon = Player.Character.Weapons.Current.Type.ToString();
			//CurWeaponIcon = Player.Character.Weapons.Current.Type.ToString().ToLower();

			client.SetPresence(new RichPresence() {
				Details = "Money: $" + Player.Money.ToString() + " | " + CurVehicle,
				State = "Wanted Level: " + Player.WantedLevel.ToString() + " Stars | " + CurWeapon,
				//Timestamps = Timestamps.FromTimeSpan(StartTime.Second),
				Assets = new Assets() {
					LargeImageKey = "game_icon",
					LargeImageText = "Location: " + CurZone,
					SmallImageKey = CurWeaponIcon,
					SmallImageText = CurWeapon
				}
			});
		}
	}
}
