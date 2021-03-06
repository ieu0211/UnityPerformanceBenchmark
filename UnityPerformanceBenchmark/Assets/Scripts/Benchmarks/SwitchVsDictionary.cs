﻿using System;
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
			_logText += "|回数|Switch|Dictionary|Dictionary with EqualityComparer|\n";
			_logText += "|---|--:|--:|--:|\n";

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
			
			_logText += $"{stopWatch.Elapsed.TotalMilliseconds}|";
			
			stopWatch.Restart();

			foreach (var number in randomNumbers)
			{
				GetByDictionary(number);
			}
			
			stopWatch.Stop();
			
			_logText += $"{stopWatch.Elapsed.TotalMilliseconds}|";
			
			stopWatch.Restart();

			foreach (var number in randomNumbers)
			{
				GetByDictionaryWithEqualityComparer(number);
			}
			
			stopWatch.Stop();
			
			_logText += $"{stopWatch.Elapsed.TotalMilliseconds}|\n";
		}
		
		// Switch
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

		// Dictionary
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

		// Dictionary with EqualityComparer
		private class NumberComparer : IEqualityComparer<Number>
		{
			public bool Equals(Number x, Number y)
			{
				return x == y;
			}

			public int GetHashCode(Number obj)
			{
				return (int)obj;
			}
		}
		
		private readonly Dictionary<Number, int> _numberTableWithEqualityComparer = new Dictionary<Number, int>(new NumberComparer())
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

		private int GetByDictionaryWithEqualityComparer(Number number)
		{
			return _numberTableWithEqualityComparer[number];
		}
	}
}
