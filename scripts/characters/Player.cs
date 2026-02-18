using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[ExportCategory("player nodes")]
	[Export] public AnimatedSprite2D sprite;
	public enum State
	{
		idle, run, fall, dead 
	}
	private bool weaponEquipped;
	public const float Speed = 270.0f;
	public const float JumpVelocity = -250.0f;
	public const float GRAVITY = 900.0f;

	public override void _Ready()
	{
		weaponEquipped = false;
   	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity.Y += GRAVITY * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		Vector2 direction = Input.GetVector("left", "right", "up", "down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
		HandleAnimation(direction);
	}
	
	public void HandleAnimation(Vector2 dir)
	{
		if (dir.X > 0) sprite.FlipH = false;
		if (dir.X < 0) sprite.FlipH = true;
		if (!weaponEquipped)
		{
			if (IsOnFloor())
			{
				if (dir == Vector2.Zero)
				{
					sprite.Play("idle");
				}
				else
				{
					sprite.Play("run");
				}
			}
			else
			{
				sprite.Play("fall");
			}
		}
		else
		{
			if (IsOnFloor())
			{
				if (dir == Vector2.Zero)
				{
					sprite.Play("weapon_idle");
				}
				else
				{
					sprite.Play("weapon_run");
				}
			}
			else
			{
				sprite.Play("weapon_fall");
			}
		}
	}
}
