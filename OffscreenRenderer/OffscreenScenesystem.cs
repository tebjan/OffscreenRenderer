using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xenko.Core.Mathematics;
using Xenko.Input;
using Xenko.Engine;
using Xenko.Rendering;
using Xenko.Rendering.Lights;
using Xenko.Rendering.Skyboxes;
using Xenko.Rendering.Colors;
using Xenko.Rendering.Compositing;
using Xenko.Graphics;

namespace OffscreenRenderer
{
    public class OffscreenScenesystem : SyncScript
    {
        Entity dirlight;
        
        public override void Start()
        {
            // load a scene
            var scene = Content.Load<Scene>("OffscreenScene");

            // get the directional light for animation
            dirlight = scene.Entities.FirstOrDefault(e => e.Name == "Directional light");
            
            // OffscreenCompositor
            var offscreenCompositor = Content.Load<GraphicsCompositor>("OffscreenCompositor");

            // Create independent SceneSystem
            var sceneSystem = new SceneSystem(Services)
            {
                Name = "OffscreeSceneSystem",
                GraphicsCompositor = offscreenCompositor,
                SceneInstance = new SceneInstance(Services, scene)
            };

            // add scene system to Game, so it gets called
            Game.GameSystems.Add(sceneSystem);
        }

        public override void Update()
        {
            var deltaTime = (float)Game.UpdateTime.Elapsed.TotalSeconds;

            dirlight.Transform.Rotation *= Quaternion.RotationYawPitchRoll(0.8f * deltaTime, 0.3f * deltaTime, 0.6f * deltaTime);

        }
    }
}
