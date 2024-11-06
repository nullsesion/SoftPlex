using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace SoftPlex.WebApp.Services
{
	public class ClientApiProductCacheService
	{
		private readonly IDistributedCache _distributedCache;
		private readonly DistributedCacheEntryOptions _options;
		private const string Prefix = "webapp_";
		private const string REDIS_ERROR = "Redis error";
		private bool _serverAviable { get; set; }

		public ClientApiProductCacheService(IDistributedCache distributedCache)
		{
			_distributedCache = distributedCache;
			_options = new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow =
					TimeSpan.FromSeconds(120),
				SlidingExpiration = TimeSpan.FromSeconds(60)
			};
			/*
			_serverAviable = true;
			try
			{
				//_distributedCache.
			}
			catch
			{
				_serverAviable = false;
			}
			*/
		}

		public async Task<Result<string>> TryGetAsync(string key)
		{
			try
			{
				string keyRedis = Prefix + key;
				string? cache = await _distributedCache.GetStringAsync(keyRedis);
				if (cache is null)
					return Result.Success(string.Empty);

				return cache;
			}
			catch
			{
				return Result.Failure<string>(REDIS_ERROR);
			}
		}

		public async Task<Result> TrySetAsync(string key, string content)
		{
			try
			{
				string keyRedis = Prefix + key;
				byte[] rawJsonBye = UTF8Encoding.UTF8.GetBytes(content);
				await _distributedCache.SetAsync(keyRedis, rawJsonBye, _options);
				return Result.Success();
			}
			catch
			{
				return Result.Failure(REDIS_ERROR);
			}
		}
	}
}
