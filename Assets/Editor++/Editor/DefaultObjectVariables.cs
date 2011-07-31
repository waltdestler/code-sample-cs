using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// Contains static methods that return object variables for given types.
/// </summary>
public static class DefaultObjectVariables
{
	[ObjectVariables(typeof(MonoBehaviour))]
	[ObjectVariables(typeof(ScriptableObject))]
	public static IEnumerable<ObjectVariableBase> GetMonoBehaviourVariables(Type type)
	{
		// Return any public fields with a NonSerialized attribute.
		foreach(FieldInfo fi in type.GetFields(BindingFlags.Instance | BindingFlags.Public))
		{
			if(Attribute.IsDefined(fi, typeof(NonSerializedAttribute)) || Attribute.IsDefined(fi, typeof(HideInInspector)))
				continue;
			yield return new ObjectFieldVariable(fi);
		}
	}

	[ObjectVariables(typeof(Behaviour))]
	public static IEnumerable<ObjectVariableBase> GetBehaviorVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("enabled"));
	}

	[ObjectVariables(typeof(GameObject))]
	public static IEnumerable<ObjectVariableBase> GetGameObjectVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("active"));
		yield return new ObjectPropertyVariable(type.GetProperty("name"));
		yield return new ObjectPropertyVariable(type.GetProperty("isStatic"));
		yield return new ObjectPropertyVariable(type.GetProperty("tag"));
		yield return new ObjectPropertyVariable(type.GetProperty("layer"));
	}

	[ObjectVariables(typeof(Transform))]
	public static IEnumerable<ObjectVariableBase> GetTransformVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("parent"));
		yield return new ObjectPropertyVariable(type.GetProperty("localPosition"));
		yield return new ObjectPropertyVariable(type.GetProperty("localEulerAngles"));
		yield return new ObjectPropertyVariable(type.GetProperty("localScale"));
	}

	[ObjectVariables(typeof(Animation))]
	public static IEnumerable<ObjectVariableBase> GetAnimationVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("clip"));
		yield return new ObjectPropertyVariable(type.GetProperty("playAutomatically"));
		yield return new ObjectPropertyVariable(type.GetProperty("animatePhysics"));
		yield return new ObjectPropertyVariable(type.GetProperty("animateOnlyIfVisible"));
		yield return new AnimationsVariable();
	}

	[ObjectVariables(typeof(AudioChorusFilter))]
	public static IEnumerable<ObjectVariableBase> GetAudioChorusFilterVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("dryMix"));
		yield return new ObjectPropertyVariable(type.GetProperty("wetMix1"));
		yield return new ObjectPropertyVariable(type.GetProperty("wetMix2"));
		yield return new ObjectPropertyVariable(type.GetProperty("wetMix3"));
		yield return new ObjectPropertyVariable(type.GetProperty("delay"));
		yield return new ObjectPropertyVariable(type.GetProperty("rate"));
		yield return new ObjectPropertyVariable(type.GetProperty("depth"));
		yield return new ObjectPropertyVariable(type.GetProperty("feedback"));
	}

	[ObjectVariables(typeof(AudioDistortionFilter))]
	public static IEnumerable<ObjectVariableBase> GetAudioDistortionFilterVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("distortionLevel"));
	}

	[ObjectVariables(typeof(AudioEchoFilter))]
	public static IEnumerable<ObjectVariableBase> GetAudioEchoFilterVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("delay"));
		yield return new ObjectPropertyVariable(type.GetProperty("decayRatio"));
		yield return new ObjectPropertyVariable(type.GetProperty("dryMix"));
		yield return new ObjectPropertyVariable(type.GetProperty("wetMix"));
	}

	[ObjectVariables(typeof(AudioHighPassFilter))]
	public static IEnumerable<ObjectVariableBase> GetAudioHighPassFilterVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("cutoffFrequency"));
		yield return new ObjectPropertyVariable(type.GetProperty("highpassResonanceQ"));
	}

	[ObjectVariables(typeof(AudioLowPassFilter))]
	public static IEnumerable<ObjectVariableBase> GeAudioLowPassFilterVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("cutoffFrequency"));
		yield return new ObjectPropertyVariable(type.GetProperty("lowpassResonanceQ"));
	}

	[ObjectVariables(typeof(AudioReverbFilter))]
	public static IEnumerable<ObjectVariableBase> GetAudioReverbFilterVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("reverbPreset"));
		yield return new ObjectPropertyVariable(type.GetProperty("dryLevel"));
		yield return new ObjectPropertyVariable(type.GetProperty("room"));
		yield return new ObjectPropertyVariable(type.GetProperty("roomHF"));
		yield return new ObjectPropertyVariable(type.GetProperty("decayTime"));
		yield return new ObjectPropertyVariable(type.GetProperty("decayHFRatio"));
		yield return new ObjectPropertyVariable(type.GetProperty("reflectionsLevel"));
		yield return new ObjectPropertyVariable(type.GetProperty("reflectionsDelay"));
		yield return new ObjectPropertyVariable(type.GetProperty("reverbLevel"));
		yield return new ObjectPropertyVariable(type.GetProperty("reverbDelay"));
		yield return new ObjectPropertyVariable(type.GetProperty("diffusion"));
		yield return new ObjectPropertyVariable(type.GetProperty("density"));
		yield return new ObjectPropertyVariable(type.GetProperty("hfReference"));
		yield return new ObjectPropertyVariable(type.GetProperty("roomLF"));
		yield return new ObjectPropertyVariable(type.GetProperty("LFReference"));
	}

	[ObjectVariables(typeof(AudioReverbZone))]
	public static IEnumerable<ObjectVariableBase> GetAudioReverbZoneVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("minDistance"));
		yield return new ObjectPropertyVariable(type.GetProperty("maxDistance"));
		yield return new ObjectPropertyVariable(type.GetProperty("reverbPreset"));
		yield return new ObjectPropertyVariable(type.GetProperty("room"));
		yield return new ObjectPropertyVariable(type.GetProperty("roomHF"));
		yield return new ObjectPropertyVariable(type.GetProperty("roomLF"));
		yield return new ObjectPropertyVariable(type.GetProperty("decayTime"));
		yield return new ObjectPropertyVariable(type.GetProperty("decayHFRatio"));
		yield return new ObjectPropertyVariable(type.GetProperty("reflections"));
		yield return new ObjectPropertyVariable(type.GetProperty("reflectionsDelay"));
		yield return new ObjectPropertyVariable(type.GetProperty("reverb"));
		yield return new ObjectPropertyVariable(type.GetProperty("reverbDelay"));
		yield return new ObjectPropertyVariable(type.GetProperty("HFReference"));
		yield return new ObjectPropertyVariable(type.GetProperty("LFReference"));
		yield return new ObjectPropertyVariable(type.GetProperty("roomRolloffFactor"));
		yield return new ObjectPropertyVariable(type.GetProperty("diffusion"));
		yield return new ObjectPropertyVariable(type.GetProperty("density"));
	}

	[ObjectVariables(typeof(AudioSource))]
	public static IEnumerable<ObjectVariableBase> GetAudioSourceVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("volume"));
		yield return new ObjectPropertyVariable(type.GetProperty("pitch"));
		yield return new ObjectPropertyVariable(type.GetProperty("clip"));
		yield return new ObjectPropertyVariable(type.GetProperty("loop"));
		yield return new ObjectPropertyVariable(type.GetProperty("playOnAwake"));
		yield return new ObjectPropertyVariable(type.GetProperty("panLevel"));
		yield return new ObjectPropertyVariable(type.GetProperty("bypassEffects"));
		yield return new ObjectPropertyVariable(type.GetProperty("dopplerLevel"));
		yield return new ObjectPropertyVariable(type.GetProperty("spread"));
		yield return new ObjectPropertyVariable(type.GetProperty("priority"));
		yield return new ObjectPropertyVariable(type.GetProperty("mute"));
		yield return new ObjectPropertyVariable(type.GetProperty("minDistance"));
		yield return new ObjectPropertyVariable(type.GetProperty("maxDistance"));
		yield return new ObjectPropertyVariable(type.GetProperty("pan"));
		yield return new ObjectPropertyVariable(type.GetProperty("rolloffMode"));
	}

	[ObjectVariables(typeof(Camera))]
	public static IEnumerable<ObjectVariableBase> GetCameraVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("fieldOfView"));
		yield return new ObjectPropertyVariable(type.GetProperty("nearClipPlane"));
		yield return new ObjectPropertyVariable(type.GetProperty("farClipPlane"));
		yield return new ObjectPropertyVariable(type.GetProperty("renderingPath"));
		yield return new ObjectPropertyVariable(type.GetProperty("orthographicSize"));
		yield return new ObjectPropertyVariable(type.GetProperty("orthographic"));
		yield return new ObjectPropertyVariable(type.GetProperty("depth"));
		yield return new ObjectPropertyVariable(type.GetProperty("cullingMask"));
		yield return new ObjectPropertyVariable(type.GetProperty("backgroundColor"));
		yield return new ObjectPropertyVariable(type.GetProperty("rect"));
		yield return new ObjectPropertyVariable(type.GetProperty("targetTexture"));
		yield return new ObjectPropertyVariable(type.GetProperty("clearFlags"));
	}

	[ObjectVariables(typeof(ConstantForce))]
	public static IEnumerable<ObjectVariableBase> GetConstantForceVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("force"));
		yield return new ObjectPropertyVariable(type.GetProperty("relativeForce"));
		yield return new ObjectPropertyVariable(type.GetProperty("torque"));
		yield return new ObjectPropertyVariable(type.GetProperty("relativeTorque"));
	}

	[ObjectVariables(typeof(GUIText))]
	public static IEnumerable<ObjectVariableBase> GetGUITextVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("text"));
		yield return new ObjectPropertyVariable(type.GetProperty("material"));
		yield return new ObjectPropertyVariable(type.GetProperty("pixelOffset"));
		yield return new ObjectPropertyVariable(type.GetProperty("font"));
		yield return new ObjectPropertyVariable(type.GetProperty("alignment"));
		yield return new ObjectPropertyVariable(type.GetProperty("anchor"));
		yield return new ObjectPropertyVariable(type.GetProperty("lineSpacing"));
		yield return new ObjectPropertyVariable(type.GetProperty("tabSize"));
		yield return new ObjectPropertyVariable(type.GetProperty("fontSize"));
		yield return new ObjectPropertyVariable(type.GetProperty("fontStyle"));
	}

	[ObjectVariables(typeof(GUITexture))]
	public static IEnumerable<ObjectVariableBase> GetGUITextureVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("color"));
		yield return new ObjectPropertyVariable(type.GetProperty("texture"));
		yield return new ObjectPropertyVariable(type.GetProperty("pixelInset"));
	}

	[ObjectVariables(typeof(LensFlare))]
	public static IEnumerable<ObjectVariableBase> GetLensFlareVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("flare"));
		yield return new ObjectPropertyVariable(type.GetProperty("brightness"));
		yield return new ObjectPropertyVariable(type.GetProperty("color"));
	}

	[ObjectVariables(typeof(Light))]
	public static IEnumerable<ObjectVariableBase> GetLightVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("type"));
		yield return new ObjectPropertyVariable(type.GetProperty("color"));
		yield return new ObjectPropertyVariable(type.GetProperty("intensity"));
		yield return new ObjectPropertyVariable(type.GetProperty("shadows"));
		yield return new ObjectPropertyVariable(type.GetProperty("shadowStrength"));
		yield return new ObjectPropertyVariable(type.GetProperty("range"));
		yield return new ObjectPropertyVariable(type.GetProperty("spotAngle"));
		yield return new ObjectPropertyVariable(type.GetProperty("cookie"));
		yield return new ObjectPropertyVariable(type.GetProperty("flare"));
		yield return new ObjectPropertyVariable(type.GetProperty("renderMode"));
		yield return new ObjectPropertyVariable(type.GetProperty("cullingMask"));
	}

	[ObjectVariables(typeof(Terrain))]
	public static IEnumerable<ObjectVariableBase> GetTerrainVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("treeDistance"));
		yield return new ObjectPropertyVariable(type.GetProperty("treeBillboardDistance"));
		yield return new ObjectPropertyVariable(type.GetProperty("treeCrossFadeLength"));
		yield return new ObjectPropertyVariable(type.GetProperty("treeMaximumFullLODCount"));
		yield return new ObjectPropertyVariable(type.GetProperty("detailObjectDistance"));
		yield return new ObjectPropertyVariable(type.GetProperty("detailObjectDensity"));
		yield return new ObjectPropertyVariable(type.GetProperty("heightmapPixelError"));
		yield return new ObjectPropertyVariable(type.GetProperty("basemapDistance"));
		yield return new ObjectPropertyVariable(type.GetProperty("castShadows"));
	}

	[ObjectVariables(typeof(NetworkView))]
	public static IEnumerable<ObjectVariableBase> GetNetworkViewVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("observed"));
		yield return new ObjectPropertyVariable(type.GetProperty("stateSynchronization"));
	}

	[ObjectVariables(typeof(Projector))]
	public static IEnumerable<ObjectVariableBase> GetProjectorVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("nearClipPlane"));
		yield return new ObjectPropertyVariable(type.GetProperty("farClipPlane"));
		yield return new ObjectPropertyVariable(type.GetProperty("fieldOfView"));
		yield return new ObjectPropertyVariable(type.GetProperty("aspectRatio"));
		yield return new ObjectPropertyVariable(type.GetProperty("orthographic"));
		yield return new ObjectPropertyVariable(type.GetProperty("orthographicSize"));
		yield return new ObjectPropertyVariable(type.GetProperty("ignoreLayers"));
		yield return new ObjectPropertyVariable(type.GetProperty("material"));
	}

	[ObjectVariables(typeof(Skybox))]
	public static IEnumerable<ObjectVariableBase> GetSkyboxVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("material"));
	}

	[ObjectVariables(typeof(Cloth))]
	public static IEnumerable<ObjectVariableBase> GetClothVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("bendingStiffness"));
		yield return new ObjectPropertyVariable(type.GetProperty("stretchingStiffness"));
		yield return new ObjectPropertyVariable(type.GetProperty("damping"));
		yield return new ObjectPropertyVariable(type.GetProperty("thickness"));
		yield return new ObjectPropertyVariable(type.GetProperty("externalAcceleration"));
		yield return new ObjectPropertyVariable(type.GetProperty("randomAcceleration"));
		yield return new ObjectPropertyVariable(type.GetProperty("useGravity"));
		yield return new ObjectPropertyVariable(type.GetProperty("selfCollision"));
		yield return new ObjectPropertyVariable(type.GetProperty("enabled"));
	}

	[ObjectVariables(typeof(InteractiveCloth))]
	public static IEnumerable<ObjectVariableBase> GetInteractiveClothVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("mesh"));
		yield return new ObjectPropertyVariable(type.GetProperty("friction"));
		yield return new ObjectPropertyVariable(type.GetProperty("density"));
		yield return new ObjectPropertyVariable(type.GetProperty("pressure"));
		yield return new ObjectPropertyVariable(type.GetProperty("collisionResponse"));
		yield return new ObjectPropertyVariable(type.GetProperty("tearFactor"));
		yield return new ObjectPropertyVariable(type.GetProperty("attachmentTearFactor"));
		yield return new ObjectPropertyVariable(type.GetProperty("attachmentResponse"));
	}

	[ObjectVariables(typeof(SkinnedCloth))]
	public static IEnumerable<ObjectVariableBase> GetSkinnedClothVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("coefficients"));
		yield return new ObjectPropertyVariable(type.GetProperty("worldVelocityScale"));
		yield return new ObjectPropertyVariable(type.GetProperty("worldAccelerationScale"));
	}

	[ObjectVariables(typeof(Collider))]
	public static IEnumerable<ObjectVariableBase> GetColliderVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("isTrigger"));
		yield return new ObjectPropertyVariable(type.GetProperty("sharedMaterial"));
	}

	[ObjectVariables(typeof(BoxCollider))]
	public static IEnumerable<ObjectVariableBase> GetBoxColliderVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("center"));
		yield return new ObjectPropertyVariable(type.GetProperty("size"));
	}

	[ObjectVariables(typeof(CapsuleCollider))]
	public static IEnumerable<ObjectVariableBase> GetCapsuleColliderVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("center"));
		yield return new ObjectPropertyVariable(type.GetProperty("radius"));
		yield return new ObjectPropertyVariable(type.GetProperty("height"));
		yield return new ObjectPropertyVariable(type.GetProperty("direction"));
	}

	[ObjectVariables(typeof(CharacterController))]
	public static IEnumerable<ObjectVariableBase> GetCharacterControllerVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("radius"));
		yield return new ObjectPropertyVariable(type.GetProperty("height"));
		yield return new ObjectPropertyVariable(type.GetProperty("center"));
		yield return new ObjectPropertyVariable(type.GetProperty("slopeLimit"));
		yield return new ObjectPropertyVariable(type.GetProperty("stepOffset"));
	}

	[ObjectVariables(typeof(MeshCollider))]
	public static IEnumerable<ObjectVariableBase> GetMeshColliderVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("convex"));
		yield return new ObjectPropertyVariable(type.GetProperty("smoothSphereCollisions"));
		yield return new ObjectPropertyVariable(type.GetProperty("sharedMesh"));
	}

	[ObjectVariables(typeof(SphereCollider))]
	public static IEnumerable<ObjectVariableBase> GetSphereColliderVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("center"));
		yield return new ObjectPropertyVariable(type.GetProperty("radius"));
	}

	[ObjectVariables(typeof(TerrainCollider))]
	public static IEnumerable<ObjectVariableBase> GetTerrainColliderVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("terrainData"));
	}

	[ObjectVariables(typeof(WheelCollider))]
	public static IEnumerable<ObjectVariableBase> GetWheelColliderVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("center"));
		yield return new ObjectPropertyVariable(type.GetProperty("radius"));
		yield return new ObjectPropertyVariable(type.GetProperty("suspensionDistance"));
		yield return new ObjectPropertyVariable(type.GetProperty("suspensionSpring"));
		yield return new ObjectPropertyVariable(type.GetProperty("mass"));
		yield return new ObjectPropertyVariable(type.GetProperty("forwardFriction"));
		yield return new ObjectPropertyVariable(type.GetProperty("sidewaysFriction"));
	}

	[ObjectVariables(typeof(Joint))]
	public static IEnumerable<ObjectVariableBase> GetJointVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("axis"));
		yield return new ObjectPropertyVariable(type.GetProperty("anchor"));
		yield return new ObjectPropertyVariable(type.GetProperty("breakForce"));
		yield return new ObjectPropertyVariable(type.GetProperty("breakTorque"));
	}

	[ObjectVariables(typeof(CharacterJoint))]
	public static IEnumerable<ObjectVariableBase> GetCharacterJointVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("swingAxis"));
		yield return new ObjectPropertyVariable(type.GetProperty("lowTwistLimit"));
		yield return new ObjectPropertyVariable(type.GetProperty("highTwistLimit"));
		yield return new ObjectPropertyVariable(type.GetProperty("swing1Limit"));
		yield return new ObjectPropertyVariable(type.GetProperty("swing2Limit"));
	}

	[ObjectVariables(typeof(ConfigurableJoint))]
	public static IEnumerable<ObjectVariableBase> GetConfigurableJointVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("secondaryAxis"));
		yield return new ObjectPropertyVariable(type.GetProperty("xMotion"));
		yield return new ObjectPropertyVariable(type.GetProperty("yMotion"));
		yield return new ObjectPropertyVariable(type.GetProperty("zMotion"));
		yield return new ObjectPropertyVariable(type.GetProperty("angularXMotion"));
		yield return new ObjectPropertyVariable(type.GetProperty("angularYMotion"));
		yield return new ObjectPropertyVariable(type.GetProperty("angularZMotion"));
		yield return new ObjectPropertyVariable(type.GetProperty("linearLimit"));
		yield return new ObjectPropertyVariable(type.GetProperty("lowAngularXLimit"));
		yield return new ObjectPropertyVariable(type.GetProperty("highAngularXLimit"));
		yield return new ObjectPropertyVariable(type.GetProperty("angularYLimit"));
		yield return new ObjectPropertyVariable(type.GetProperty("angularZLimit"));
		yield return new ObjectPropertyVariable(type.GetProperty("targetPosition"));
		yield return new ObjectPropertyVariable(type.GetProperty("targetVelocity"));
		yield return new ObjectPropertyVariable(type.GetProperty("xDrive"));
		yield return new ObjectPropertyVariable(type.GetProperty("yDrive"));
		yield return new ObjectPropertyVariable(type.GetProperty("zDrive"));
		yield return new ObjectPropertyVariable(type.GetProperty("targetRotation"));
		yield return new ObjectPropertyVariable(type.GetProperty("targetAngularVelocity"));
		yield return new ObjectPropertyVariable(type.GetProperty("rotationDriveMode"));
		yield return new ObjectPropertyVariable(type.GetProperty("angularXDrive"));
		yield return new ObjectPropertyVariable(type.GetProperty("angularYZDrive"));
		yield return new ObjectPropertyVariable(type.GetProperty("slerpDrive"));
		yield return new ObjectPropertyVariable(type.GetProperty("projectionMode"));
		yield return new ObjectPropertyVariable(type.GetProperty("projectionDistance"));
		yield return new ObjectPropertyVariable(type.GetProperty("projectionAngle"));
		yield return new ObjectPropertyVariable(type.GetProperty("configuredInWorldSpace"));
	}

	[ObjectVariables(typeof(HingeJoint))]
	public static IEnumerable<ObjectVariableBase> GetHingeJointVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("motor"));
		yield return new ObjectPropertyVariable(type.GetProperty("limits"));
		yield return new ObjectPropertyVariable(type.GetProperty("spring"));
		yield return new ObjectPropertyVariable(type.GetProperty("useMotor"));
		yield return new ObjectPropertyVariable(type.GetProperty("useLimits"));
		yield return new ObjectPropertyVariable(type.GetProperty("useSpring"));
	}

	[ObjectVariables(typeof(SpringJoint))]
	public static IEnumerable<ObjectVariableBase> GetSpringJointVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("spring"));
		yield return new ObjectPropertyVariable(type.GetProperty("damper"));
		yield return new ObjectPropertyVariable(type.GetProperty("minDistance"));
		yield return new ObjectPropertyVariable(type.GetProperty("maxDistance"));
		yield return new ObjectPropertyVariable(type.GetProperty("connectedBody"));
	}

	[ObjectVariables(typeof(MeshFilter))]
	public static IEnumerable<ObjectVariableBase> GetMeshFilterVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("sharedMesh"));
	}

	[ObjectVariables(typeof(OcclusionArea))]
	public static IEnumerable<ObjectVariableBase> GetOcclusionAreaVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("center"));
		yield return new ObjectPropertyVariable(type.GetProperty("size"));
	}

	[ObjectVariables(typeof(ParticleAnimator))]
	public static IEnumerable<ObjectVariableBase> GetParticleAnimatorVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("doesAnimateColor"));
		yield return new ObjectPropertyVariable(type.GetProperty("worldRotationAxis"));
		yield return new ObjectPropertyVariable(type.GetProperty("localRotationAxis"));
		yield return new ObjectPropertyVariable(type.GetProperty("sizeGrow"));
		yield return new ObjectPropertyVariable(type.GetProperty("rndForce"));
		yield return new ObjectPropertyVariable(type.GetProperty("force"));
		yield return new ObjectPropertyVariable(type.GetProperty("damping"));
		yield return new ObjectPropertyVariable(type.GetProperty("autodestruct"));
		yield return new ObjectPropertyVariable(type.GetProperty("colorAnimation"));
	}

	[ObjectVariables(typeof(ParticleEmitter))]
	public static IEnumerable<ObjectVariableBase> GetParticleEmitterVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("emit"));
		yield return new ObjectPropertyVariable(type.GetProperty("minSize"));
		yield return new ObjectPropertyVariable(type.GetProperty("maxSize"));
		yield return new ObjectPropertyVariable(type.GetProperty("minEnergy"));
		yield return new ObjectPropertyVariable(type.GetProperty("maxEnergy"));
		yield return new ObjectPropertyVariable(type.GetProperty("minEmission"));
		yield return new ObjectPropertyVariable(type.GetProperty("maxEmission"));
		yield return new ObjectPropertyVariable(type.GetProperty("emitterVelocityScale"));
		yield return new ObjectPropertyVariable(type.GetProperty("worldVelocity"));
		yield return new ObjectPropertyVariable(type.GetProperty("localVelocity"));
		yield return new ObjectPropertyVariable(type.GetProperty("rndVelocity"));
		yield return new ObjectPropertyVariable(type.GetProperty("useWorldSpace"));
		yield return new ObjectPropertyVariable(type.GetProperty("rndRotation"));
		yield return new ObjectPropertyVariable(type.GetProperty("angularVelocity"));
		yield return new ObjectPropertyVariable(type.GetProperty("rndAngularVelocity"));
		yield return new ObjectPropertyVariable(type.GetProperty("enabled"));
	}

	[ObjectVariables(typeof(Renderer))]
	public static IEnumerable<ObjectVariableBase> GetRendererVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("enabled"));
		yield return new ObjectPropertyVariable(type.GetProperty("castShadows"));
		yield return new ObjectPropertyVariable(type.GetProperty("receiveShadows"));
		yield return new ObjectPropertyVariable(type.GetProperty("sharedMaterials"));
	}

	[ObjectVariables(typeof(ClothRenderer))]
	public static IEnumerable<ObjectVariableBase> GetClothRendererVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("pauseWhenNotVisible"));
	}

	[ObjectVariables(typeof(LineRenderer))]
	public static IEnumerable<ObjectVariableBase> GetLineRendererVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("useWorldSpace"));
	}

	[ObjectVariables(typeof(ParticleRenderer))]
	public static IEnumerable<ObjectVariableBase> GetParticleRendererVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("lengthScale"));
		yield return new ObjectPropertyVariable(type.GetProperty("velocityScale"));
		yield return new ObjectPropertyVariable(type.GetProperty("cameraVelocityScale"));
		yield return new ObjectPropertyVariable(type.GetProperty("maxParticleSize"));
		yield return new ObjectPropertyVariable(type.GetProperty("uvAnimationXTile"));
		yield return new ObjectPropertyVariable(type.GetProperty("uvAnimationYTile"));
		yield return new ObjectPropertyVariable(type.GetProperty("uvAnimationCycles"));
	}

	[ObjectVariables(typeof(SkinnedMeshRenderer))]
	public static IEnumerable<ObjectVariableBase> GetSkinnedMeshRendererVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("quality"));
		yield return new ObjectPropertyVariable(type.GetProperty("sharedMesh"));
		yield return new ObjectPropertyVariable(type.GetProperty("skinNormals"));
		yield return new ObjectPropertyVariable(type.GetProperty("updateWhenOffscreen"));
	}

	[ObjectVariables(typeof(TrailRenderer))]
	public static IEnumerable<ObjectVariableBase> GetTrailRendererVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("time"));
		yield return new ObjectPropertyVariable(type.GetProperty("startWidth"));
		yield return new ObjectPropertyVariable(type.GetProperty("endWidth"));
		yield return new ObjectPropertyVariable(type.GetProperty("autodestruct"));
	}

	[ObjectVariables(typeof(Rigidbody))]
	public static IEnumerable<ObjectVariableBase> GetRigidbodyVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("drag"));
		yield return new ObjectPropertyVariable(type.GetProperty("angularDrag"));
		yield return new ObjectPropertyVariable(type.GetProperty("mass"));
		yield return new ObjectPropertyVariable(type.GetProperty("useGravity"));
		yield return new ObjectPropertyVariable(type.GetProperty("isKinematic"));
		yield return new ObjectPropertyVariable(type.GetProperty("freezeRotation"));
		yield return new ObjectPropertyVariable(type.GetProperty("collisionDetectionMode"));
		yield return new ObjectPropertyVariable(type.GetProperty("interpolation"));
#if !UNITY_2_6 && !UNITY_3_0 && !UNITY_3_1
		yield return new ObjectPropertyVariable(type.GetProperty("constraints"));
#endif
	}

	[ObjectVariables(typeof(TextMesh))]
	public static IEnumerable<ObjectVariableBase> GetTextMeshVariables(Type type)
	{
		yield return new ObjectPropertyVariable(type.GetProperty("text"));
		yield return new ObjectPropertyVariable(type.GetProperty("font"));
		yield return new ObjectPropertyVariable(type.GetProperty("fontSize"));
		yield return new ObjectPropertyVariable(type.GetProperty("fontStyle"));
	}

	public class AnimationsVariable : ObjectVariableBase
	{
		public override string VariableName
		{
			get { return "animations"; }
		}

		public override Type VariableType
		{
			get { return typeof(AnimationClip[]); }
		}

		public override object GetValue(object obj)
		{
			Animation a = (Animation)obj;
			List<AnimationClip> ret = new List<AnimationClip>();
			foreach(AnimationState state in a)
				ret.Add(state.clip);
			return ret.ToArray();
		}

		public override void SetValue(object obj, object value)
		{
			Animation a = (Animation)obj;
			List<AnimationClip> curClips = new List<AnimationClip>();
			foreach(AnimationState state in a)
				curClips.Add(state.clip);
			foreach(AnimationClip clip in curClips)
				a.RemoveClip(clip.name);
			foreach(AnimationClip clip in (AnimationClip[])value)
				a.AddClip(clip, clip.name);
		}
	}
}