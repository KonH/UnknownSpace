using System;

namespace UnknownSpace.Config {
	[Flags]
	public enum Direction {
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