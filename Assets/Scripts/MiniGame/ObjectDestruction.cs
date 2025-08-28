using System.Collections.Generic;
using UnityEngine;

public class ObjectDestruction : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int rows = 2;
    public int cols = 2;

    public void Break()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Texture2D tex = GetTextureFromSprite(sr.sprite);
        float ppu = sr.sprite.pixelsPerUnit;
        List<Sprite> pieces = SliceSprite(tex, rows, cols, ppu);
        SpawnPieces(pieces, transform.position, 0.5f);
        Destroy(gameObject); // 원본 삭제
    }


    Texture2D GetTextureFromSprite(Sprite sprite)
    {
        if (sprite.rect.width != sprite.texture.width)
        {
            Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] newPixels = sprite.texture.GetPixels(
                (int)sprite.textureRect.x,
                (int)sprite.textureRect.y,
                (int)sprite.textureRect.width,
                (int)sprite.textureRect.height
            );
            newText.SetPixels(newPixels);
            newText.Apply();
            return newText;
        }
        else
            return sprite.texture;
    }

    List<Sprite> SliceSprite(Texture2D texture, int rows, int cols, float pixelsPerUnit)
    {
        int sliceWidth = texture.width / cols;
        int sliceHeight = texture.height / rows;

        List<Sprite> pieces = new List<Sprite>();

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                Rect rect = new Rect(x * sliceWidth, y * sliceHeight, sliceWidth, sliceHeight);
                Sprite piece = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f), pixelsPerUnit);
                pieces.Add(piece);
            }
        }

        return pieces;
    }

    void SpawnPieces(List<Sprite> pieces, Vector3 center, float spread)
    {
        foreach (var sprite in pieces)
        {
            GameObject piece = new GameObject("Piece");
            piece.transform.position = center + (Vector3)(Random.insideUnitCircle * spread);
            piece.transform.localScale = transform.localScale;

            var sr = piece.AddComponent<SpriteRenderer>();
            sr.sprite = sprite;

            var rb = piece.AddComponent<Rigidbody2D>();
            rb.gravityScale = 1f;
            rb.AddForce(Random.insideUnitCircle * 200f);

            var cl = piece.AddComponent<PolygonCollider2D>();

            piece.layer = gameObject.layer;
            piece.transform.parent = gameObject.transform.parent;

            // 필요시 일정 시간 뒤 삭제
            Destroy(piece, 30f);
        }
    }
}
