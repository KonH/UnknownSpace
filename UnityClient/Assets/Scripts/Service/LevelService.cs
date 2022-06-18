using UnityEngine.SceneManagement;

namespace UnknownSpace.Service {
	public sealed class LevelService {
		public void GoToMeta() {
			SceneManager.LoadScene("Meta");
		}

		public void StartLevel() {
			SceneManager.LoadScene("Gameplay");
		}
	}
}