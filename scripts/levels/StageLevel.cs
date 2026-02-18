using Godot;
using System;


public partial class StageLevel : Node2D
{
	[Export] public Camera2D PlayerCamera;
	public override void _Ready()
	{
		PlayerCamera.Enabled = true;
		Globals.playerWeaponEquip = true;
		GD.Print("HEEEE");
		GD.Print(Globals.playerWeaponEquip);
	}

	public override void _Process(double delta)
	{
	}
}
