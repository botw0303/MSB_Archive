using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SignalReceiverWithFloat : MonoBehaviour, INotificationReceiver
{
    public SignalAssetEventPair<float>[] signalAssetEventPairs;

    [Serializable]
    public class SignalAssetEventPair<T>
    {
        public SignalAsset signalAsset;
        public ParameterizedEvent events;

        [Serializable]
        public class ParameterizedEvent : UnityEvent<T> { }
    }

    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if (notification is ParameterizedEmitter<float> boolEmitter)
        {
            var matches = signalAssetEventPairs.Where(x => ReferenceEquals(x.signalAsset, boolEmitter.asset));
            foreach (var m in matches)
            {
                m.events.Invoke(boolEmitter.parameter);
            }
        }
    }
}