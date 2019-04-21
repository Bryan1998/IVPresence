using DiscordRPC;
using GTA;
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

			Interval = 15000;
			Tick += new EventHandler(Main);
		}

		public void Main(object sender, EventArgs e) {
			dynamic CurVehicle;
			if (Exists(Player.Character.CurrentVehicle)) {
				CurVehicle = "In Vehicle: " + Player.Character.CurrentVehicle.ToString();
			}
			else {
				CurVehicle = "On Foot";
			}

			//I wish there was a better way...
			dynamic CurWeapon;
			dynamic CurWeaponIcon;
			if (Player.Character.Weapons.Current == Player.Character.Weapons.AssaultRifle_AK47) {
				CurWeapon = "Weapon: AK-47";
				CurWeaponIcon = "assaultrifle_ak47";
			}
			else if (Player.Character.Weapons.Current == Player.Character.Weapons.AssaultRifle_M4) {
				CurWeapon = "Weapon: M4";
				CurWeaponIcon = "assaultrifle_m4";
			}
			else if (Player.Character.Weapons.Current == Player.Character.Weapons.BarettaShotgun) {
				CurWeapon = "Weapon: Beretta 1201";
				CurWeaponIcon = "barettashotgun";
			}
			else if (Player.Character.Weapons.Current == Player.Character.Weapons.BaseballBat) {
				CurWeapon = "Weapon: Baseball Bat";
				CurWeaponIcon = "baseballbat";
			}
			else if (Player.Character.Weapons.Current == Player.Character.Weapons.BasicShotgun) {
				CurWeapon = "Weapon: Shotgun";
				CurWeaponIcon = "basicshotgun";
			}
			else if (Player.Character.Weapons.Current == Player.Character.Weapons.BasicSniperRifle) {
				CurWeapon = "Weapon: Sniper Rifle";
				CurWeaponIcon = "basicsniperrifle";
			}
			else if (Player.Character.Weapons.Current == Player.Character.Weapons.DesertEagle) {
				CurWeapon = "Weapon: Desert Eagle";
				CurWeaponIcon = "deserteagle";
			}
			else if (Player.Character.Weapons.Current == Player.Character.Weapons.Glock) {
				CurWeapon = "Weapon: Glock 17";
				CurWeaponIcon = "glock";
			}
			else if (Player.Character.Weapons.Current == Player.Character.Weapons.Grenades) {
				CurWeapon = "Weapon: Grenade";
				CurWeaponIcon = "grenades";
			}
			else if (Player.Character.Weapons.Current == Player.Character.Weapons.Knife) {
				CurWeapon = "Weapon: Knife";
				CurWeaponIcon = "knife";
			}
			else if (Player.Character.Weapons.Current == Player.Character.Weapons.MolotovCocktails) {
				CurWeapon = "Weapon: Molotov Cocktail";
				CurWeaponIcon = "molotovcocktails";
			}
			else if (Player.Character.Weapons.Current == Player.Character.Weapons.MP5) {
				CurWeapon = "Weapon: MP5";
				CurWeaponIcon = "mp5";
			}
			else if (Player.Character.Weapons.Current == Player.Character.Weapons.RocketLauncher) {
				CurWeapon = "Weapon: Rocket Launcher";
				CurWeaponIcon = "rocketlauncher";
			}
			else if (Player.Character.Weapons.Current == Player.Character.Weapons.SniperRifle_M40A1) {
				CurWeapon = "Weapon: M40A1";
				CurWeaponIcon = "sniperrifle_m40a1";
			}
			else if (Player.Character.Weapons.Current == Player.Character.Weapons.Uzi) {
				CurWeapon = "Weapon: Uzi";
				CurWeaponIcon = "uzi";
			}
			else {
				CurWeapon = "No Weapon";
				CurWeaponIcon = "unarmed";
			}

			client.SetPresence(new RichPresence() {
				Details = "Money: $" + Player.Money.ToString() + " | " + CurVehicle,
				State = "Wanted Level: " + Player.WantedLevel.ToString() + " Stars | " + CurWeapon,
				//Timestamps = Timestamps.FromTimeSpan(StartTime.Second),
				Assets = new Assets() {
					LargeImageKey = "game_icon",
					LargeImageText = "Location: Not Implemented Yet...",
					SmallImageKey = CurWeaponIcon,
					SmallImageText = CurWeapon
				}
			});
		}
	}
}
