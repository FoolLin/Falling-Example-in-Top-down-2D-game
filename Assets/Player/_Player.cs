using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Rendering;

[SelectionBase]
public class _Player : MonoBehaviour
{
	[SerializeField] private float speed;
	[SerializeField] private Animator anim;
	[SerializeField] private Transform sprite;
	[SerializeField] private Tilemap holeTile;

	private SortingGroup sortingGroup;
	private Rigidbody2D rb;

	private Vector3 dir;
	private Vector3 spriteOriginalPos;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		sortingGroup = GetComponent<SortingGroup>();
		spriteOriginalPos = sprite.localPosition;
	}

	private void Update()
	{
		Vector3Int pos = holeTile.WorldToCell(transform.position);
		if (holeTile.HasTile(pos))
		{
			if (sprite.position.y < -10)
			{
				transform.position = Vector3.zero;
				sprite.localPosition = spriteOriginalPos;
				sortingGroup.sortingLayerName = "Defualt";
				return;
			}

			rb.velocity = Vector2.zero;
			sortingGroup.sortingLayerName = "Terrain";
			dir += Physics.gravity * 2 * Time.deltaTime;
			sprite.transform.position += dir * Time.deltaTime;
			return;
		}

		dir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

		rb.velocity = dir * speed;

		UpdateAnimation();
	}

	private void UpdateAnimation()
	{
		if (dir.x > 0)
			transform.localScale = new Vector3(1, 1, 1);

		else if (dir.x < 0)
			transform.localScale = new Vector3(-1, 1, 1);

		if (dir == Vector3.zero)
		{
			anim.SetBool("isMoving", false);
		}
		else
		{
			anim.SetBool("isMoving", true);
			anim.SetFloat("horizontal", dir.x);
			anim.SetFloat("vertical", dir.y);
		}
	}
}
