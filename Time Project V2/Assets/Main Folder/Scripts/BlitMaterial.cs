
using UnityEngine;
//using UnityEngine.Experimental.Rendering
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;




/*public class BlitMaterial : ScriptableRendererFeature
{

    /*[System.Serializable]

    public class Settings {
        public Material material;
        public int materialpassIndex = -1;
        public RenderPassEvent renderEvent = RenderPassEvent.AfterRenderingOpaques;
    }

    [SerializeField]
    private Settings settings = new Settings();

    private RenderPass RenderPass;

   public Material Material
    {
        get => settings.material;

    }

    public override void Create() 
    {
        this.RenderPass = new RenderPass(name, settings.material, settings.materialpassIndex);
        RenderPass.renderPassEvent = settings.renderEvent;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData) 
    {
        RenderPass.setSource(renderer.cameraColorTarget);
        renderer.EnqueuePass(RenderPass);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}*/
