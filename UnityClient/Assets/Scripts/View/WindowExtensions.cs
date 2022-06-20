namespace UnknownSpace.View {
	public static class WindowExtensions {
		public static void Show(this IWindow window) {
			window.Root.SetActive(true);
		}

		public static void Hide(this IWindow window) {
			window.Root.SetActive(false);
		}

		public static void MakeInactive(this IInterativeWindow window) {
			foreach ( var element in window.Elements ) {
				element.interactable = false;
			}
		}

		public static void MakeActive(this IInterativeWindow window) {
			foreach ( var element in window.Elements ) {
				element.interactable = true;
			}
		}
	}
}