using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace SoftPlex.WebApp.Services
{
	public class ClientApiProductCacheService
	{
		private readonly IDistributedCache _distributedCache;
		private readonly DistributedCacheEntryOptions _options;
		private const string Prefix = "webapp_";

		public ClientApiProductCacheService(IDistributedCache distributedCache)
		{
			_distributedCache = distributedCache;
			_options = new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow =
					TimeSpan.FromSeconds(120),
				SlidingExpiration = TimeSpan.FromSeconds(60)
			};
		}

		public async Task<string> TryGetAsync(string key)
		{
			var keyRedis = Prefix + key;
			var cache = await _distributedCache.GetStringAsync(keyRedis);
			if (cache is null)
				return string.Empty;

			return cache;
		}

		public async Task TrySetAsync(string key, string content)
		{
			var keyRedis = Prefix + key;
			byte[] rawJsonBye = UTF8Encoding.UTF8.GetBytes(content);
			_distributedCache.SetAsync(keyRedis, rawJsonBye, _options);
		}
	}
}
