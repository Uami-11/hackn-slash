using Godot;
using System;
using System.Collections.Generic;

public partial class BatEnemy : CharacterBody2D
{
	[ExportCategory("Bat vars")]
	[Export] public Timer timer;
	[Export] public AnimatedSprite2D sprite;
	public const float SPEED = 30.0f;
	public Vector2 DIR;	
	
	public CharacterBody2D player;
	
	public bool IsBatChasing;
	private RandomNumberGenerator _rng = new RandomNumberGenerator();

	public override void _Ready()
	{
		IsBatChasing = true;
		timer.Timeout += OnTimerTimeout;
		_rng.Randomize();

   	}

	public override void _PhysicsProcess(double delta)
	{
		Move(delta);
		HandleAnimation();
   }

	public void HandleAnimation()
	{
		sprite.Play("fly");
		if (Velocity.X > 0)
			sprite.FlipH = false;
		else if (Velocity.X < 0)
			sprite.FlipH = true;
	}
	public void Move(double delta)
	{
		Vector2 velocity = Velocity;
		if (IsBatChasing)
		{
			player = Globals.playerBody;
			velocity = Position.DirectionTo(player.Position) * SPEED * (float) delta;
		}
		else if (!IsBatChasing)
		{
			velocity += DIR * SPEED * (float) delta;
		}
		Velocity = velocity;
		MoveAndCollide(Velocity);
	}
	
	public void OnTimerTimeout()
	{
		float[] waitTimes = {1.0f, 1.5f, 2.0f, 2.5f};
		var randomWaitTime = _rng.RandiRange(0, waitTimes.Length - 1);
		timer.WaitTime = waitTimes[randomWaitTime];
		
		if (!IsBatChasing)
		{
			Vector2[] directions = {Vector2.Right, Vector2.Left, Vector2.Up, Vector2.Down};
			var randomDirection = _rng.RandiRange(0, directions.Length - 1);
			DIR = directions[randomDirection];
		}
	}
	


}
