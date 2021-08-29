using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;
using Unity.Jobs;

namespace Ocs.Sensor
{
  public class Lidar : MonoBehaviour
  {
    [SerializeField, Tooltip("Target model")]
    private Transform _model;
    [SerializeField, Tooltip("Number of samples per layer.[1, inf]")]
    private int _samples = 360;
    [SerializeField, Tooltip("Number of layer.[2, inf]")]
    private int _layers = 16;
    [SerializeField, Tooltip("Minimum sampling angle in vertical direction.[-90, Angle Max]")]
    private float _angleMin = -10;
    [SerializeField, Tooltip("Maximum sampling angle in vertical direction.[Angle Min, 90]")]
    private float _angleMax = 10;
    [SerializeField, Tooltip("Minimum sampling Distance.[0, Range Max]")]
    private float _rangeMin = 0.12f;
    [SerializeField, Tooltip("Maximum sampling Distance.[Range Min, inf]")]
    private float _rangeMax = 100f;

    [HideInInspector]
    private NativeArray<RaycastCommand> _commands;
    private NativeArray<RaycastHit> _results;
    private JobHandle _handle;

    public NativeArray<float> Distance;
    public int Samples { get => _samples; }
    public int Layers { get => _layers; }


    void OnEnable()
    {
      Distance = new NativeArray<float>(_layers*_samples, Allocator.Persistent);
      for(int i=0; i<_layers*_samples; i++)
      {
        Distance[i] = _rangeMax;
      }

      // Increment ranges
      float vertIncrement;
      float azimuthIncrAngle;
      if(_layers == 1)
        vertIncrement = 1;
      else       
        vertIncrement = (float)(_angleMax - _angleMin) / (float)(_layers - 1);
      azimuthIncrAngle = (float)(360.0f / _samples);

      _commands = new NativeArray<RaycastCommand>(Distance.Length, Allocator.Persistent);
      _results = new NativeArray<RaycastHit>(Distance.Length, Allocator.Persistent);

      // create commands
      Vector3 fwd = new Vector3(0, 0, 1);
      Vector3 dir;
      int index = 0;
      float angle;
      float azimuth;
      for (int incr = 0; incr < _samples; incr++)
      {
        for (int layer = 0; layer < _layers; layer++)
        {
          index = layer + incr * _layers;
          angle = _angleMin + (float)layer * vertIncrement;
          azimuth = incr * azimuthIncrAngle;
          dir = _model.rotation * Quaternion.Euler(-angle, azimuth, 0)*fwd;
          var command = new RaycastCommand(_model.position, dir);
          _commands[index] = command;
        }
      }
    }

    void OnDisable()
    {
      _handle.Complete();
      Distance.Dispose();
      _commands.Dispose();
      _results.Dispose();
    }

    void Update()
    {
      _handle.Complete();
    
      UpdateDistance updateDistanceJob = new UpdateDistance()
      {
        raycastResults = _results,
        raycastDistance = Distance
      };

      var raycastJobHandle = RaycastCommand.ScheduleBatch(_commands, _results, 360);
      _handle = updateDistanceJob.Schedule(Distance.Length, 360, raycastJobHandle);
      JobHandle.ScheduleBatchedJobs();

    }

    [BurstCompile]
    struct UpdateDistance : IJobParallelFor
    {
      [ReadOnly] public NativeArray<RaycastHit> raycastResults;
      public NativeArray<float> raycastDistance;

      void IJobParallelFor.Execute(int index)
      {
        raycastDistance[index] = (float)raycastResults[index].distance;        
      }
    }
    
    public bool ParameterCheck()
    {
      if (_samples < 1)
      {
        Debug.LogError("[Samples] is invalid.");
        return false;
      }
      if (_layers < 1)
      {
        Debug.LogError("[Layers] is invalid.");
        return false;
      }
      if (_angleMin > _angleMax)
      {
        Debug.LogError("[Angle Min/Max] is invalid.");
        return false;
      }
      if (_rangeMin > _rangeMax)
      {
        Debug.LogError("[Range Min/Max] is invalid.");
        return false;
      }

      return true;
    }
  }
}
