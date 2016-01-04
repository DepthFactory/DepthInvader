using UnityEngine;
using System.Collections;

public class CachesManager : MonoBehaviour {

	public Transform m_tRightCache;
	public Transform m_tLeftCache;

	static private float m_tfPosZ = -5.0f;
	
//##################################################################################
// General functions
//##################################################################################
	private void Start () {
		float CacheWidth = m_tRightCache.gameObject.GetComponent<SpriteRenderer> ().sprite.textureRect.width / 100.0f;

		Transform RightCacheTransform = Instantiate(m_tRightCache) as Transform;
		Transform LeftCacheTransform = Instantiate(m_tLeftCache) as Transform;
		int iPanelWidth = LevelManager.GetPanelWidth ();
		RightCacheTransform.position = new Vector3 ((float)iPanelWidth * 0.5f + CacheWidth * 0.5f + 1.0f, 0.0f, m_tfPosZ);
		LeftCacheTransform.position = new Vector3 ((float)-iPanelWidth * 0.5f - CacheWidth * 0.5f - 1.0f, 0.0f, m_tfPosZ);
	}
	
// ----------------------------------------------------------------------------
// ----------------------------------------------------------------------------
	private void Update () {
	
	}
}
