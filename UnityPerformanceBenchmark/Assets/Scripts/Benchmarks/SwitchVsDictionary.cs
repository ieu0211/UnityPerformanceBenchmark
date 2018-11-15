using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = System.Random;

namespace BenchMarks
{
	public class SwitchVsDictionary : MonoBehaviour
	{
		private Random _random = new Random();
		private String _logText = "";
		
		private void Start()
		{
			_logText += "||Switch|Dictionary|\n";
			_logText += "|---|--:|--:|\n";

			_logText += "|1000";
			Benchmark(1000);
			
			_logText += "|10000";
			Benchmark(10000);
			
			_logText += "|100000";
			Benchmark(100000);
			
			_logText += "|1000000";
			Benchmark(1000000);
			
			Debug.Log(_logText);
		}
		
		private void Benchmark (int benchmarkCount)
		{
			var randomNumbers = new List<Number>();
			for (var i = 0; i < benchmarkCount; i++)
			{
				var randomNumber = Enum.GetValues(typeof(Number))
					.Cast<Number>()
					.OrderBy(c => _random.Next())
					.FirstOrDefault();
				randomNumbers.Add(randomNumber);
			}
			
			var stopWatch = new Stopwatch();
			stopWatch.Start();

			foreach (var number in randomNumbers)
			{
				GetBySwitch(number);
			}
			
			stopWatch.Stop();
			
			//Debug.Log($"switch: {stopWatch.Elapsed}");
			_logText += $"{stopWatch.Elapsed}|";
			
			stopWatch.Restart();

			foreach (var number in randomNumbers)
			{
				GetByDictionary(number);
			}
			
			stopWatch.Stop();
			
			//Debug.Log($"dictionary: {stopWatch.Elapsed}");
			_logText += $"{stopWatch.Elapsed}|\n";
		}
		
		enum Number
		{
			One,
			Two,
			Three,
			Four,
			Five,
			Six,
			Seven,
			Eight,
			Nine,
			Ten
		}

		private int GetBySwitch(Number number)
		{
			switch (number)
			{
				case Number.One:
					return 1;
				case Number.Two:
					return 2;
				case Number.Three:
					return 3;
				case Number.Four:
					return 4;
				case Number.Five:
					return 5;
				case Number.Six:
					return 6;
				case Number.Seven:
					return 7;
				case Number.Eight:
					return 8;
				case Number.Nine:
					return 9;
				case Number.Ten:
					return 10;
				default:
					return default(int);
			}
		}

		private readonly Dictionary<Number, int> _numberTable = new Dictionary<Number, int>
		{
			{ Number.One,    1 },
			{ Number.Two,    2 },
			{ Number.Three,  3 },
			{ Number.Four,   4 },
			{ Number.Five,   5 },
			{ Number.Six,    6 },
			{ Number.Seven,  7 },
			{ Number.Eight,  8 },
			{ Number.Nine,   9 },
			{ Number.Ten,   10 }
		};

		private int GetByDictionary(Number number)
		{
			return _numberTable[number];
		}
	}	
}
