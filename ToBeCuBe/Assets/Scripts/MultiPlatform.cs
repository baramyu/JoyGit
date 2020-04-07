using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPlatform : MonoBehaviour
{
#if UNITY_STANDALONE

#elif UNITY_IOS || UNITY_ANDROID

#endif
}

