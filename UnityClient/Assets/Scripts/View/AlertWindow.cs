using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UnknownSpace.View {
	public sealed class AlertWindow : MonoBehaviour, IWindow {
		[SerializeField] GameObject _root;
		[SerializeField] TMP_Text _messageText;
		[SerializeField] Button _confirmButton;

		public GameObject Root => _root;

		public void Init(string text) {
			_messageText.text = text;
		}

		void Awake() {
			_confirmButton.onClick.AddListener(OnConfirm);
		}

		void OnConfirm() {
			this.Hide();
		}
	}
}
