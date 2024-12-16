using Microsoft.AspNetCore.Mvc.RazorPages;
using StackExchange.Redis;

namespace Service.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public long HitCount { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, IConnectionMultiplexer connectionMultiplexer)
        {
            _logger = logger;
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task OnGetAsync()
        {
            var redisDatabase = _connectionMultiplexer.GetDatabase();

            HitCount = await redisDatabase.StringIncrementAsync("HitCount");
        }

    }
}
