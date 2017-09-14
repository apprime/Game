namespace Web.Temp
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;
    using System;
    using System.Reflection;
    using Microsoft.AspNetCore.SignalR.Infrastructure;
    using Newtonsoft.Json.Serialization;

    public class PostsHub : Hub
    {
    }

    [Route("Api/Posts")]
    public class PostsController : Controller
    {
        private IPostRepository _postRepository { get; set; }
        private IConnectionManager _connectionManager { get; set; }

        public PostsController(IPostRepository postRepository, IConnectionManager connectionManager)
        {
            _postRepository = postRepository;
            _connectionManager = connectionManager;
        }

        [HttpGet]
        public List<Post> GetPosts()
        {
            return _postRepository.GetAll();
        }

        [HttpGet("{id}")]
        public Post GetPost(int id)
        {
            return _postRepository.GetPost(id);
        }

        [HttpPost]
        [Route("AddPost")]
        public void AddPost(Post post)
        {
            _postRepository.AddPost(post);
            _connectionManager.GetHubContext<PostsHub>().Clients.All.publishPost(post);
        }
    }

    public class Post
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }

        public Post(int id, string userName, string text)
        {
            Id = id;
            UserName = userName;
            Text = text;
        }

        public Post()
        {
        }
    }

    public interface IPostRepository
    {
        List<Post> GetAll();
        Post GetPost(int id);
        void AddPost(Post post);
    }

    public class PostRepository : IPostRepository
    {
        private List<Post> _posts = new List<Post>()
    {
        new Post(1, "Obi-Wan Kenobi","These are not the droids you're looking for"),
        new Post(2, "Darth Vader","I find your lack of faith disturbing")
    };
        public void AddPost(Post post)
        {
            _posts.Add(post);
        }

        public List<Post> GetAll()
        {
            return _posts;
        }

        public Post GetPost(int id)
        {
            return _posts.FirstOrDefault(p => p.Id == id);
        }
    }

    public class SignalRContractResolver : IContractResolver
    {
        private readonly Assembly _assembly;
        private readonly IContractResolver _camelCaseContractResolver;
        private readonly IContractResolver _defaultContractSerializer;

        public SignalRContractResolver()
        {
            _defaultContractSerializer = new DefaultContractResolver();
            _camelCaseContractResolver = new CamelCasePropertyNamesContractResolver();
            _assembly = typeof(Connection).GetTypeInfo().Assembly;
        }


        public JsonContract ResolveContract(Type type)
        {
            if (type.GetTypeInfo().Assembly.Equals(_assembly))
                return _defaultContractSerializer.ResolveContract(type);

            return _camelCaseContractResolver.ResolveContract(type);
        }

    }
}