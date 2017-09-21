using Data.Models.Entities;
using Data.Models.Gamestate;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Core.ResourceManagers
{
    internal class SceneResources
    {
        private static ConcurrentDictionary<string, Scene> _scenes = new ConcurrentDictionary<string, Scene>();

        internal static Scene Get(string id)
        {
            Scene scene;
            _scenes.TryGetValue(id, out scene);
            return scene;
        }

        internal static void Add(Scene scene)
        {
            //TODO: Handle when this doesnt work
            _scenes.TryAdd(scene.Id.Trunk, scene);
        }

        internal static void Remove(Scene scene)
        {
            _scenes.TryRemove(scene.Id.Trunk, out scene);
        }

        internal IEnumerable<Scene> GetScenesByLocation(Location location)
        {
            throw new NotImplementedException();
        }
    }
}