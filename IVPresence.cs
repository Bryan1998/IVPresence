﻿using DiscordRPC;
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
				CurVehicle = "In Vehicle: " + Player.Character.CurrentVehicle.Name;
			}
			else {
				CurVehicle = "On Foot";
			}

			string CurWeapon = Player.Character.Weapons.Current.Type.ToString();
			string CurWeaponIcon = Player.Character.Weapons.Current.Type.ToString().ToLower();
			if (CurWeapon == "Rifle_AK47") {
				CurWeapon = "AK-47";
			}
			else if (CurWeapon == "Rifle_M4") {
				CurWeapon = "M4";
			}
			else if (CurWeapon == "Shotgun_Baretta") {
				CurWeapon = "Beretta 1201";
			}
			else if (CurWeapon == "Melee_BaseballBat") {
				CurWeapon = "Baseball Bat";
			}
			else if (CurWeapon == "Shotgun_Basic") {
				CurWeapon = "Shotgun";
			}
			else if (CurWeapon == "SniperRifle_Basic") {
				CurWeapon = "Sniper Rifle";
			}
			else if (CurWeapon == "Handgun_DesertEagle") {
				CurWeapon = "Desert Eagle";
			}
			else if (CurWeapon == "Handgun_Glock") {
				CurWeapon = "Glock 17";
			}
			else if (CurWeapon == "Thrown_Grenade") {
				CurWeapon = "Grenade";
			}
			else if (CurWeapon == "Melee_Knife") {
				CurWeapon = "Knife";
			}
			else if (CurWeapon == "Thrown_Molotov") {
				CurWeapon = "Molotov Cocktail";
			}
			else if (CurWeapon == "SMG_MP5") {
				CurWeapon = "MP5";
			}
			else if (CurWeapon == "Heavy_RocketLauncher") {
				CurWeapon = "Rocket Launcher";
			}
			else if (CurWeapon == "SniperRifle_M40A1") {
				CurWeapon = "M40A1";
			}
			else if (CurWeapon == "SMG_Uzi") {
				CurWeapon = "Uzi";
			}
			else {
				CurWeapon = "Unarmed";
			}
			
			client.SetPresence(new RichPresence() {
				Details = "Money: $" + Player.Money.ToString() + " | " + CurVehicle,
				State = "Wanted Level: " + Player.WantedLevel.ToString() + " Stars | " + CurWeapon,
				Assets = new Assets() {
					LargeImageKey = "game_icon",
					LargeImageText = "On Street: " + World.GetStreetName(Player.Character.Position).ToString(),
					SmallImageKey = CurWeaponIcon,
					SmallImageText = CurWeapon
				}
			});
		}
	}
}