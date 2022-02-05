using TMPro;
using UnityEngine;

namespace UnknownSpace.UnityClient {
	[RequireComponent(typeof(TMP_Text))]
	public sealed class ProjectVersionView : MonoBehaviour {
		[SerializeField] TMP_Text _text;

		void Reset() {
			_text = GetComponent<TMP_Text>();
		}

		void Start() {
			_text.text = Application.version;
		}
	}
}