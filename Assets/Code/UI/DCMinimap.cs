using System.ComponentModel;
using UnityEngine;

public class DCMinimap : DCWidget
{
    RenderTexture m_RenderTexture;

    #region Getters
    public override gui.EWidgetType GetWidgetType() { return gui.EWidgetType.Minimap; }

    protected override string GetFileName() { return "Minimap"; }
    #endregion

    public override void Initialize(NoesisView panel)
    {
        base.Initialize(panel);
        BindMinimap();
    }

    public void SetMinimapTexture(RenderTexture texture) { m_RenderTexture = texture; }
    public void BindMinimap()
    {
        // Find the border where texture will be drawn
        var border = (Noesis.Border)m_DC.FindName("rtBorder");

        // Create Noesis texture
        var tex = Noesis.Texture.WrapTexture(m_RenderTexture);

        // Create brush to store render texture and assign it to the rectangle
        border.Background = new Noesis.ImageBrush()
        {
            ImageSource = new Noesis.TextureSource(tex),
            Stretch = Noesis.Stretch.UniformToFill,
        };
    }
}
