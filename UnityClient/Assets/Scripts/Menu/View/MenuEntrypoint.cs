using UnityEngine;
using UnityEngine.Analytics;

namespace UnknownSpace.Menu.View {
	public class MenuEntrypoint : MonoBehaviour {
		void Start() {
			Analytics.CustomEvent("load_menu");
		}
	}
}