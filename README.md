# 2D-Top-Down-Shooter

## Controlls:

- **Movement** - 'WASD'
- **Shoot** - 'Left mouse button' or 'Ctrl'
- **Restart** - 'R'

## Weapons

- **Simple gun** - initial gun; slow; low damage.
- **Dual gun** - high fire rate.
- **Shotgun** - short range; multiple shots; high damage.
- **Sniper** - highest damage; low fire rate.

## IA (State Machine)

- **Wander** - the AI will pick a random path to follow.
- **Awareness** - when a player enters the detection zone, the AI will search for them until they get a line of sight, or until they get far enough.
- **Chase** - when a player is in the line of sight, the AI will chase them until it gets in range for shooting.
- **Shoot** - while the player is in range, the AI will aim and shoot.
