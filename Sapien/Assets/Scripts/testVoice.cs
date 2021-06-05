using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
namespace FrostweepGames.Plugins.GoogleCloud.SpeechRecognition.Examples
{	public class testVoice : MonoBehaviour
	{

		public Text _resultText;

		private GCSpeechRecognition _speechRecognition;


		public Image _voiceLevelImage;


		public Dropdown _languageDropdown,
						 _microphoneDevicesDropdown;

		public Button _startRecordButton;

		public float MaxVol, Current;

		private bool isRecording;
		private void Start()
		{
			_speechRecognition = GCSpeechRecognition.Instance;
			_speechRecognition.RecognizeSuccessEvent += RecognizeSuccessEventHandler;
			_speechRecognition.RecognizeFailedEvent += RecognizeFailedEventHandler;
			
			_speechRecognition.FinishedRecordEvent += FinishedRecordEventHandler;

			_speechRecognition.RecordFailedEvent += RecordFailedEventHandler;

			_speechRecognition.BeginTalkigEvent += BeginTalkigEventHandler;
			_speechRecognition.EndTalkigEvent += EndTalkigEventHandler;

			_microphoneDevicesDropdown.onValueChanged.AddListener(MicrophoneDevicesDropdownOnValueChangedEventHandler);

			_languageDropdown.ClearOptions();

			for (int i = 0; i < Enum.GetNames(typeof(Enumerators.LanguageCode)).Length; i++)
			{
				_languageDropdown.options.Add(new Dropdown.OptionData(((Enumerators.LanguageCode)i).Parse()));
			}

			_languageDropdown.value = _languageDropdown.options.IndexOf(_languageDropdown.options.Find(x => x.text == Enumerators.LanguageCode.en_US.Parse()));
			RefreshMicsButtonOnClickHandler();
		}

		private void Update()
		{
			if (_speechRecognition.IsRecording)
			{
				if (_speechRecognition.GetMaxFrame() > 0)
				{
					MaxVol = (float)_speechRecognition.configs[_speechRecognition.currentConfigIndex].voiceDetectionThreshold;
					Current = _speechRecognition.GetLastFrame() / MaxVol;

					if (Current >= 1f)
					{
						_voiceLevelImage.fillAmount = Mathf.Lerp(_voiceLevelImage.fillAmount, Mathf.Clamp(Current / 2f, 0, 1f), 30 * Time.deltaTime);
					}
					else
					{
						_voiceLevelImage.fillAmount = Mathf.Lerp(_voiceLevelImage.fillAmount, Mathf.Clamp(Current / 2f, 0, 0.5f), 30 * Time.deltaTime);
					}

					_voiceLevelImage.color = Current >= 1f ? Color.green : Color.red;
				}
			}
			else
			{
				_voiceLevelImage.fillAmount = 0f;

			}
		}

		private void RefreshMicsButtonOnClickHandler()
		{
			_speechRecognition.RequestMicrophonePermission(null);

			_microphoneDevicesDropdown.ClearOptions();

			for (int i = 0; i < _speechRecognition.GetMicrophoneDevices().Length; i++)
			{
				_microphoneDevicesDropdown.options.Add(new Dropdown.OptionData(_speechRecognition.GetMicrophoneDevices()[i]));

			}

			//smart fix of dropdowns

			_microphoneDevicesDropdown.value = PlayerPrefs.GetInt("SavedMic");
		}

		private void MicrophoneDevicesDropdownOnValueChangedEventHandler(int value)
		{
			if (!_speechRecognition.HasConnectedMicrophoneDevices())
				return;
			_speechRecognition.SetMicrophoneDevice(_speechRecognition.GetMicrophoneDevices()[value]);
			PlayerPrefs.SetInt("SavedMic", value);
			//Debug.Log(value);
		}

		public void StartRecordButtonOnClickHandler()
		{
			isRecording = true;
			_startRecordButton.interactable = false;
			_resultText.text = string.Empty;
			StartCoroutine(StopRecordButtonOnClickHandler());
			_speechRecognition.StartRecord(true);
		}


		private IEnumerator StopRecordButtonOnClickHandler()
		{
			while (true)
			{
				yield return new WaitForSeconds(2f);
				if (Current < 0.01f)
				{
					yield return new WaitForSeconds(3f);
					if (Current < 0.01f)
					{
						_speechRecognition.StopRecord();
						Debug.Log("StopRecord");

						isRecording = false;
						_startRecordButton.interactable = true;
						yield break;
					}
				}
			}
		}



		private void RecordFailedEventHandler()
		{

			_resultText.text = "<color=red>Start record Failed. Please check microphone device and try again.</color>";

			_startRecordButton.interactable = true;
		}

		private void BeginTalkigEventHandler()
		{
			_resultText.text = "<color=blue>Talk Began.</color>";
		}

		private void EndTalkigEventHandler(AudioClip clip, float[] raw)
		{
			_resultText.text += "\n<color=blue>Talk Ended.</color>";

			FinishedRecordEventHandler(clip, raw);
		}

		private void FinishedRecordEventHandler(AudioClip clip, float[] raw)
		{
			string message = "";
			if (clip == null)
				return;

			RecognitionConfig config = RecognitionConfig.GetDefault();
			config.languageCode = (Enumerators.LanguageCode.en_US.Parse());
			config.speechContexts = new SpeechContext[]
			{
				new SpeechContext()
				{
					phrases = message.Replace(" ", string.Empty).Split(',')
				}
			};
			config.audioChannelCount = clip.channels;
			// configure other parameters of the config if need

			GeneralRecognitionRequest recognitionRequest = new GeneralRecognitionRequest()
			{
				audio = new RecognitionAudioContent()
				{
					content = raw.ToBase64()
				},
				//audio = new RecognitionAudioUri() // for Google Cloud Storage object
				//{
				//	uri = "gs://bucketName/object_name"
				//},
				config = config
			};
			_speechRecognition.Recognize(recognitionRequest);

		}



		private void RecognizeFailedEventHandler(string error)
		{
			_resultText.text = "Recognize Failed: " + error;
		}


		private void RecognizeSuccessEventHandler(RecognitionResponse recognitionResponse)
		{
			_resultText.text = "Recognize Success.";
			InsertRecognitionResponseInfo(recognitionResponse);
		}

		
		private void InsertRecognitionResponseInfo(RecognitionResponse recognitionResponse)
		{
			if (recognitionResponse == null || recognitionResponse.results.Length == 0)
			{
				_resultText.text = "\nWords not detected.";
				return;
			}

			_resultText.text += "\n" + recognitionResponse.results[0].alternatives[0].transcript;

			var words = recognitionResponse.results[0].alternatives[0].words;

			if (words != null)
			{
				string times = string.Empty;

				foreach (var item in recognitionResponse.results[0].alternatives[0].words)
				{
					times += "<color=green>" + item.word + "</color> -  start: " + item.startTime + "; end: " + item.endTime + "\n";
				}

				_resultText.text += "\n" + times;
			}
		}
	}

}

