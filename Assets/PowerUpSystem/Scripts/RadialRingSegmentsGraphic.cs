using UnityEngine;
using UnityEngine.UI;

namespace PowerUpSystem.Scripts
{
    /// <summary>
    /// Procedural donut menu: N ring segments, center hub, inner accent ring,
    /// selected wedge tint, cyan radial borders, and inward pointer (similar to radial HUD references).
    /// Add on a RectTransform (full stretch). Set raycastTarget off if you use buttons on top.
    /// </summary>
    [AddComponentMenu("UI/PowerUp/Radial Ring Segments Graphic")]
    [RequireComponent(typeof(RectTransform))]
    public class RadialRingSegmentsGraphic : MaskableGraphic
    {
        [SerializeField] [Min(2)] private int _segmentCount = 4;
        [Range(0.2f, 0.75f)] [SerializeField] private float _innerRadiusRatio = 0.42f;

        [Header("Segment fill")]
        [SerializeField] private Color _segmentColorNormal = new Color(0.16f, 0.17f, 0.19f, 0.96f);
        [SerializeField] private Color _segmentColorSelected = new Color(0.38f, 0.48f, 0.58f, 0.98f);

        [Header("Selected wedge accents")]
        [SerializeField] private Color _borderHighlightColor = new Color(0.35f, 0.88f, 1f, 1f);
        [SerializeField] [Min(0.5f)] private float _borderWidth = 2.2f;

        [Header("Center hub")]
        [SerializeField] private bool _drawCenterDisc = true;
        [SerializeField] private Color _centerFillColor = new Color(0.2f, 0.21f, 0.23f, 1f);
        [SerializeField] private Color _innerRingColor = new Color(0.35f, 0.82f, 0.95f, 0.85f);
        [SerializeField] [Min(0.5f)] private float _innerRingThickness = 2.5f;
        [SerializeField] [Range(16, 128)] private int _innerRingSegments = 48;

        [Header("Pointer (selected wedge, toward center)")]
        [SerializeField] private bool _drawPointer = true;
        [SerializeField] private Color _pointerColor = new Color(0.35f, 0.88f, 1f, 1f);
        [SerializeField] [Min(1f)] private float _pointerBaseWidth = 10f;
        [SerializeField] [Min(1f)] private float _pointerDepth = 14f;

        private int _selectedSegmentIndex = -1;

        protected override void Awake()
        {
            base.Awake();
            raycastTarget = false;
        }

        public int SelectedSegmentIndex
        {
            get => _selectedSegmentIndex;
            set
            {
                int v = value;
                if (v >= _segmentCount)
                {
                    v = -1;
                }

                if (_selectedSegmentIndex == v)
                {
                    return;
                }

                _selectedSegmentIndex = v;
                SetVerticesDirty();
            }
        }

        public int SegmentCount => _segmentCount;

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            _segmentCount = Mathf.Max(2, _segmentCount);
            SetVerticesDirty();
        }
#endif

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
            Rect r = rectTransform.rect;
            float cx = r.center.x;
            float cy = r.center.y;
            float half = Mathf.Min(r.width, r.height) * 0.5f;
            if (half <= 0.01f)
            {
                return;
            }

            float outerR = half;
            float innerR = outerR * _innerRadiusRatio;
            int n = _segmentCount;
            float step = (Mathf.PI * 2f) / n;

            for (int i = 0; i < n; i++)
            {
                float mid = Mathf.PI * 0.5f - i * step;
                float a0 = mid - step * 0.5f;
                float a1 = mid + step * 0.5f;
                bool selected = _selectedSegmentIndex == i;
                Color c = selected ? _segmentColorSelected : _segmentColorNormal;
                AddAnnulusSector(vh, cx, cy, innerR, outerR, a0, a1, c);
            }

            if (_drawCenterDisc)
            {
                AddDisc(vh, cx, cy, innerR, _centerFillColor);
                AddThinRing(vh, cx, cy, innerR - _innerRingThickness * 0.5f, innerR + _innerRingThickness * 0.5f, _innerRingColor, _innerRingSegments);
            }

            if (_selectedSegmentIndex >= 0 && _selectedSegmentIndex < n)
            {
                int i = _selectedSegmentIndex;
                float mid = Mathf.PI * 0.5f - i * step;
                float a0 = mid - step * 0.5f;
                float a1 = mid + step * 0.5f;
                AddRadialBorder(vh, cx, cy, innerR, outerR, a0, _borderWidth, _borderHighlightColor);
                AddRadialBorder(vh, cx, cy, innerR, outerR, a1, _borderWidth, _borderHighlightColor);

                if (_drawPointer)
                {
                    AddPointer(vh, cx, cy, innerR, mid, _pointerColor);
                }
            }
        }

        private static void AddDisc(VertexHelper vh, float cx, float cy, float radius, Color32 color)
        {
            const int segs = 48;
            int v0 = vh.currentVertCount;
            vh.AddVert(new Vector3(cx, cy, 0f), color, Vector2.zero);
            for (int s = 0; s <= segs; s++)
            {
                float t = (s / (float)segs) * Mathf.PI * 2f;
                vh.AddVert(new Vector3(cx + radius * Mathf.Cos(t), cy + radius * Mathf.Sin(t), 0f), color, Vector2.zero);
            }

            for (int s = 0; s < segs; s++)
            {
                vh.AddTriangle(v0, v0 + 1 + s, v0 + 2 + s);
            }
        }

        private static void AddThinRing(VertexHelper vh, float cx, float cy, float rInner, float rOuter, Color32 color, int segments)
        {
            for (int s = 0; s < segments; s++)
            {
                float t0 = (s / (float)segments) * Mathf.PI * 2f;
                float t1 = ((s + 1) / (float)segments) * Mathf.PI * 2f;
                Vector3 i0 = new Vector3(cx + rInner * Mathf.Cos(t0), cy + rInner * Mathf.Sin(t0), 0f);
                Vector3 i1 = new Vector3(cx + rInner * Mathf.Cos(t1), cy + rInner * Mathf.Sin(t1), 0f);
                Vector3 o0 = new Vector3(cx + rOuter * Mathf.Cos(t0), cy + rOuter * Mathf.Sin(t0), 0f);
                Vector3 o1 = new Vector3(cx + rOuter * Mathf.Cos(t1), cy + rOuter * Mathf.Sin(t1), 0f);
                int v = vh.currentVertCount;
                vh.AddVert(i0, color, Vector2.zero);
                vh.AddVert(o0, color, Vector2.zero);
                vh.AddVert(o1, color, Vector2.zero);
                vh.AddVert(i1, color, Vector2.zero);
                vh.AddTriangle(v, v + 1, v + 2);
                vh.AddTriangle(v, v + 2, v + 3);
            }
        }

        private static void AddAnnulusSector(VertexHelper vh, float cx, float cy, float rIn, float rOut, float a0, float a1, Color32 color)
        {
            Vector3 p0 = new Vector3(cx + rIn * Mathf.Cos(a0), cy + rIn * Mathf.Sin(a0), 0f);
            Vector3 p1 = new Vector3(cx + rIn * Mathf.Cos(a1), cy + rIn * Mathf.Sin(a1), 0f);
            Vector3 p2 = new Vector3(cx + rOut * Mathf.Cos(a1), cy + rOut * Mathf.Sin(a1), 0f);
            Vector3 p3 = new Vector3(cx + rOut * Mathf.Cos(a0), cy + rOut * Mathf.Sin(a0), 0f);
            int v = vh.currentVertCount;
            vh.AddVert(p0, color, Vector2.zero);
            vh.AddVert(p3, color, Vector2.zero);
            vh.AddVert(p2, color, Vector2.zero);
            vh.AddVert(p1, color, Vector2.zero);
            vh.AddTriangle(v, v + 1, v + 2);
            vh.AddTriangle(v, v + 2, v + 3);
        }

        private static void AddRadialBorder(VertexHelper vh, float cx, float cy, float rIn, float rOut, float angle, float halfWidthPx, Color32 color)
        {
            Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            Vector2 perp = new Vector2(-dir.y, dir.x) * halfWidthPx;

            Vector3 i0 = new Vector3(cx + rIn * dir.x - perp.x, cy + rIn * dir.y - perp.y, 0f);
            Vector3 i1 = new Vector3(cx + rIn * dir.x + perp.x, cy + rIn * dir.y + perp.y, 0f);
            Vector3 o0 = new Vector3(cx + rOut * dir.x + perp.x, cy + rOut * dir.y + perp.y, 0f);
            Vector3 o1 = new Vector3(cx + rOut * dir.x - perp.x, cy + rOut * dir.y - perp.y, 0f);
            int v = vh.currentVertCount;
            vh.AddVert(i0, color, Vector2.zero);
            vh.AddVert(i1, color, Vector2.zero);
            vh.AddVert(o0, color, Vector2.zero);
            vh.AddVert(o1, color, Vector2.zero);
            vh.AddTriangle(v, v + 1, v + 2);
            vh.AddTriangle(v, v + 2, v + 3);
        }

        private void AddPointer(VertexHelper vh, float cx, float cy, float innerR, float midAngle, Color32 color)
        {
            Vector2 outward = new Vector2(Mathf.Cos(midAngle), Mathf.Sin(midAngle));
            Vector2 perp = new Vector2(-outward.y, outward.x);
            float hw = _pointerBaseWidth * 0.5f;
            float tipR = Mathf.Max(innerR - _pointerDepth, 0f);
            Vector3 tip = new Vector3(cx + tipR * outward.x, cy + tipR * outward.y, 0f);
            Vector3 bL = new Vector3(cx + innerR * outward.x - perp.x * hw, cy + innerR * outward.y - perp.y * hw, 0f);
            Vector3 bR = new Vector3(cx + innerR * outward.x + perp.x * hw, cy + innerR * outward.y + perp.y * hw, 0f);
            int v = vh.currentVertCount;
            vh.AddVert(bL, color, Vector2.zero);
            vh.AddVert(bR, color, Vector2.zero);
            vh.AddVert(tip, color, Vector2.zero);
            vh.AddTriangle(v, v + 1, v + 2);
        }
    }
}
