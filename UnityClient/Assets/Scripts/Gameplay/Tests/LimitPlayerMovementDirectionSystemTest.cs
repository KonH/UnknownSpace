using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnknownSpace.Gameplay.Systems;

namespace UnknownSpace.Tests {
	/// <summary>
	/// Possibly too paranoid test, but it covers all input combinations, good for 100% cover testing
	/// </summary>
	public class LimitPlayerMovementDirectionSystemTest {
		[TestCaseSource(nameof(GetAllTestCases))]
		public void IsDirectionValidCorrect((PossibleDirection, Vector2, bool) input) {
			var (mask, vector, isValid) = input;
			Assert.AreEqual(LimitPlayerMovementDirectionSystem.IsDirectionValid(mask, vector), isValid);
		}

		static IEnumerable<(PossibleDirection, Vector2, bool)> GetAllTestCases() => new[] {
			GetTestCases(PossibleDirection.None),
			GetTestCases(PossibleDirection.Up, Vector2.up),
			GetTestCases(PossibleDirection.Down, Vector2.down),
			GetTestCases(PossibleDirection.Left, Vector2.left),
			GetTestCases(PossibleDirection.Right, Vector2.right),
			GetTestCases(PossibleDirection.Vertical, Vector2.up, Vector2.down),
			GetTestCases(PossibleDirection.Horizontal, Vector2.left, Vector2.right),
			GetTestCases(PossibleDirection.All, Vector2.up, Vector2.down, Vector2.left, Vector2.right),
		}.SelectMany(cases => cases);

		static IEnumerable<(PossibleDirection, Vector2, bool)> GetTestCases(PossibleDirection direction, params Vector2[] positive) =>
			new[] {
				Vector2.up, Vector2.down, Vector2.left, Vector2.right,
			}.Select(vec => (direction, vec, positive.Contains(vec)));
	}
}
