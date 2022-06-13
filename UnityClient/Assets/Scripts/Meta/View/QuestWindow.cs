using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UnknownSpace.Meta.View {
	public sealed class QuestWindow : MonoBehaviour {
		[SerializeField] Button _startButton;

		void Awake() {
			_startButton.onClick.AddListener(OnStartButtonClick);
		}

		void OnStartButtonClick() {
			SceneManager.LoadScene(1);
		}
	}
}