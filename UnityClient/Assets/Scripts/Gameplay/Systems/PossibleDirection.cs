using System;

namespace UnknownSpace.Gameplay.Systems {
	[Flags]
	public enum PossibleDirection {
		None = 0,
		Up = 1,
		Down = 2,
		Left = 4,
		Right = 8,
		Vertical = Up | Down,
		Horizontal = Left | Right,
		All = Vertical | Horizontal,
	}
}