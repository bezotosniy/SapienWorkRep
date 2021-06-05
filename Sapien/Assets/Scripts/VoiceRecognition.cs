using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
namespace FrostweepGames.Plugins.GoogleCloud.SpeechRecognition.Examples
{
	public class VoiceRecognition : MonoBehaviour
	{
		public Text task, resp;
		public Text _resultText;

		private GCSpeechRecognition _speechRecognition;


		public Image _voiceLevelImage;

		public Button _startRecordButton;

		private float MaxVol, Current;

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

			_speechRecognition.SetMicrophoneDevice(_speechRecognition.GetMicrophoneDevices()[PlayerPrefs.GetInt("SavedMic")]);

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
				if (Current < 0.05f)
				{
					yield return new WaitForSeconds(1f);
					if (Current < 0.05f)
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

			string other = "\nDetected alternatives: ";

			foreach (var result in recognitionResponse.results) //дает возможные варианты текста
			{
				foreach (var alternative in result.alternatives)
				{
					if (recognitionResponse.results[0].alternatives[0] != alternative)
					{
						other += alternative.transcript + ", ";
					}
				}
			}
			resp.text = "";
			SravnTask(_resultText.text + other);
			//_resultText.text += other;
		}
		public void SravnTask(string other)
        {
			if (other.Contains(task.text))
            {
				resp.text = "‘раза произнесена правильно";
				GetComponent<VoicePlayback>().RecordResult = true;
            }
            else
            {
				resp.text = "‘раза произнесена неправильно";
			}
        }
	}
	
}