using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

namespace Beat
{
   public class Clock : Singleton<Clock>
   {
      protected Clock() { }

      void Awake()
      {
         if (BPM.Equals(0.0)) Debug.LogWarning("BPM not set! Please set the BPM in the BeatClock");
      }

      public double BPM; //stable to pretty high BPMs, but when you past the low 200s things might break down

      public string MBT; //for display purposes in unity editor  
      public double StartDelay;
      public double LatencyCompensation = 0;
      
      public double _secondsPerMeasure;

      private bool[] _beatMask = new bool[(int)BeatValue.Max];

      private int _thirtySecondCount;
      private int _sixteenthCount;
      private int _eighthCount;
      private int _quarterCount;
      private int _halfCount;
      private int _measureCount;
      
      private double _thirtySecondLength;
      private double _sixteenthLength;
      private double _eighthLength;
      private double _quarterLength;
      private double _halfLength;
      private double _measureLength;
      
      private double _nextThirtySecond = System.Double.MaxValue;
      private double _nextMeasure;
      private double _nextSixteenth;
      private double _nextEighth;
      private double _nextQuarter;
      private double _nextHalf;

      private List<double> latency = new List<double>();

      public enum BeatValue
      {
         ThirtySecond,
         Sixteenth = 2,
         Eighth = 4,
         Quarter = 8,
         Half = 16,
         Measure = 32,
         Max = 33
      };

      public class BeatArgs
      {
         public BeatValue BeatVal;
         public int BeatCount;
         public double BeatTime;
         public double NextBeatTime;
         public bool[] BeatMask = new bool[(int)BeatValue.Max];

         public BeatArgs(BeatValue beatVal, int beatCount, double beatTime, double nextBeatTime, bool[] beatMask)
         {
            BeatVal = beatVal;
            BeatCount = beatCount;
            BeatTime = beatTime;
            NextBeatTime = nextBeatTime;
            BeatMask = beatMask;
         }
      }

      public delegate void BeatEvent(BeatArgs args);

      public event BeatEvent Tick;
      public event BeatEvent Beat;

      public void SetBPM(int newBPM)
      {
         double BPMdbl = (double)newBPM;
         InitializeBPM(BPMdbl);
      }

      public void SetBPM(float newBPM)
      {
         double BPMdbl = (double)newBPM;
         InitializeBPM(BPMdbl);
      }

      public void SetBPM(double NewBPM)
      {
         InitializeBPM((NewBPM));
      }

      void InitializeBPM(double NewBPM)
      {
         ResetBeatCounts();
         this.BPM = NewBPM;
         _secondsPerMeasure = 60 / this.BPM * 4;
         SetLengths();
         FirstBeat();
      }

      void SetLengths()
      {
         _measureLength = _secondsPerMeasure;
         _thirtySecondLength = _measureLength / 32;
         _sixteenthLength = _measureLength / 16;
         _eighthLength = _measureLength / 8;
         _quarterLength = _measureLength / 4;
         _halfLength = _measureLength / 2;
      }

      void FirstBeat()
      {
         double time = AudioSettings.dspTime + StartDelay;
         _nextThirtySecond = time + _thirtySecondLength;
         _nextSixteenth = time + _sixteenthLength;
         _nextEighth = time + _eighthLength;
         _nextQuarter = time + _quarterLength;
         _nextHalf = time + _halfLength;
         _nextMeasure = time + _measureLength;
      }

      void ResetBeatCounts()
      {
         _thirtySecondCount = 0;
         _sixteenthCount = 1;
         _eighthCount = 1;
         _quarterCount = 1;
         _halfCount = 1;
         _measureCount = 1;
      }

      void Start()
      {
         _beatMask = new bool[(int)BeatValue.Max];
         InitializeBPM(BPM);
      }

      void UpdateBeats()
      {
         _thirtySecondCount++;
         BuildBeatMask();
         if (Tick != null)
            Tick(new BeatArgs(BeatValue.ThirtySecond, _thirtySecondCount, _nextThirtySecond,
                  _nextThirtySecond + _thirtySecondLength, _beatMask));
         //latency.Add(AudioSettings.dspTime - _nextThirtySecond); //benchmarking stuff
         _nextThirtySecond += _thirtySecondLength;
         if (_beatMask[(int)BeatValue.Sixteenth])
         {
            _sixteenthCount++;
            _nextSixteenth += _sixteenthLength;
         }
         if (_beatMask[(int)BeatValue.Eighth])
         {
            _eighthCount++;
            _nextEighth += _eighthLength;
         }
         if (_beatMask[(int)BeatValue.Quarter])
         {
            _quarterCount++;
            if (Beat != null)
               Beat(new BeatArgs(BeatValue.Quarter, _quarterCount, _nextQuarter,
                  _nextQuarter + _quarterLength, _beatMask));
            _nextQuarter += _quarterLength;
         }
         if (_beatMask[(int)BeatValue.Half])
         {
            _halfCount++;
            _nextHalf += _halfLength;
         }
         if (_beatMask[(int)BeatValue.Measure])
         {
            _measureCount++;
            _nextMeasure += _measureLength;
         }
      }

      void BuildBeatMask()
      {
         _beatMask[(int)BeatValue.ThirtySecond] = true;
         if (_thirtySecondCount % 2 != 0) return;
         _beatMask[(int)BeatValue.Sixteenth] = true;
         if (_thirtySecondCount % 4 != 0) return;
         _beatMask[(int)BeatValue.Eighth] = true;
         if (_thirtySecondCount % 8 != 0) return;
         _beatMask[(int)BeatValue.Quarter] = true;
         if (_thirtySecondCount % 16 != 0) return;
         _beatMask[(int)BeatValue.Half] = true;
         if (_thirtySecondCount % 32 != 0) return;
         _beatMask[(int)BeatValue.Measure] = true;
      }

      void Update()
      {
         if (AudioSettings.dspTime >= _nextThirtySecond - LatencyCompensation)
         {
            UpdateBeats();
         }
         Array.Clear(_beatMask, 0, _beatMask.Length);
         int beats = 1 + ((_quarterCount - 1) % 4);
         MBT = _measureCount.ToString() + ":" + beats.ToString() + ":" + ((_thirtySecondCount % 8)+1).ToString();

         //double rollav = RollingAverage(latency);
         //if (rollav != 0.0)
         //Debug.Log("Average Drift of Last 25 Notes: " + (rollav * 1000).ToString().Remove(5) + "ms");
      }

      //CoRoutine to sync the execution of a particular callback to the next relevant beat of a particular value
      //Usage: Clock.Instance.SyncFunction(FunctionToCallback, BeatValue.Quarter)
      public void SyncFunction(System.Action callback, BeatValue beatValue = BeatValue.Measure)
      {
         StartCoroutine(YieldForSync(callback, beatValue));
      }

      IEnumerator YieldForSync(System.Action callback, BeatValue beatValue)
      {
         int startCount = _thirtySecondCount % 32;
         bool isStartNote = true;
         bool waiting = true;
         while (waiting)
         {
            isStartNote = (isStartNote && startCount == (_thirtySecondCount % 32));
            if (isStartNote)
               yield return false;
            isStartNote = false;
            if (beatValue == BeatValue.ThirtySecond || (_thirtySecondCount % 32) % (int)beatValue == 1)
               waiting = false;
            else
               yield return false;
         }
         callback();
      }

      //Helper functions for cueing things to play (usually audio) at the next available interval
      //For playing audio, use AudioSource.PlayScheduled(AtNextThirtySecond());
      public double AtNextThirtySecond()
      {
         return _nextThirtySecond;
      }

      public double AtNextSixteenth()
      {
         return _nextSixteenth;
      }

      public double AtNextEighth()
      {
         return _nextEighth;
      }

      public double AtNextQuarter()
      {
         return _nextQuarter;
      }

      public double AtNextBeat()
      {
         return _nextQuarter;
      }

      public double AtNextHalf()
      {
         return _nextHalf;
      }

      public double AtNextMeasure()
      {
         return _nextMeasure;
      }

      //Helper functions for timing things like animations, etc. to the beatclock
      //These are casted to floats as most animations, tweens, etc. take float values for time
      public float ThirtySecondLength()
      {
         return (float)_thirtySecondLength;
      }

      public float SixteenthLength()
      {
         return (float)_sixteenthLength;
      }

      public float EighthLength()
      {
         return (float)_eighthLength;
      }

      public float QuarterLength()
      {
         return (float)_quarterLength;
      }

      public float BeatLength()
      {
         return (float)_quarterLength;
      }

      public float HalfLength()
      {
         return (float)_halfLength;
      }

      public float MeasureLength()
      {
         return (float)_measureLength;
      }

      //benchmarking function for latency measurement
      double RollingAverage(List<double> values)
      {
         int periodLength = 25;
         if (values.Count - 1 < periodLength) return 0.0;
         var temp = Enumerable
             .Range(0, values.Count - periodLength)
             .Select(n => values.Skip(n).Take(periodLength).Average())
             .ToList();
         return temp[temp.Count - 1];
      }

   }
}
