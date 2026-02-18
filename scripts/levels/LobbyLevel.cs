using Godot;
using System;

public partial class LobbyLevel : Node2D
{
	[Export] public Camera2D PlayerCamera;
	public override void _Ready()
	{
		PlayerCamera.Enabled = false;
		Globals.playerWeaponEquip = false;
		GD.Print("HEEEE");
	}

	public override void _Process(double delta)
	{
	}
}
