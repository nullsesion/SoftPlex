using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPlex.DataAccess.Repositories
{
	public abstract class AbstractRepository
	{
		protected readonly SoftPlexDbContext _context;
		public AbstractRepository(SoftPlexDbContext context) => _context = context;

		public async Task SaveAsync() => await _context.SaveChangesAsync();
		

		private bool _disposed = false;
		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
				if (disposing)
					_context.Dispose();
			_disposed = true;
		}
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

	}
}
