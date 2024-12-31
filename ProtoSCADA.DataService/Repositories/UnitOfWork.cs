using ProtoSCADA.Data.Context;
using ProtoSCADA.Data.Interfaces;
using ProtoSCADA.Entities.Entities;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private bool _disposed = false;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    private IGenericRepository<User> _users;
    public IGenericRepository<User> Users => _users ??= new GenericRepository<User>(_context);

    private IGenericRepository<Machine> _machines;
    public IGenericRepository<Machine> Machines => _machines ??= new GenericRepository<Machine>(_context);

    private IGenericRepository<Metric> _metrics;
    public IGenericRepository<Metric> Metrics => _metrics ??= new GenericRepository<Metric>(_context);

    private IGenericRepository<Factory> _factories;
    public IGenericRepository<Factory> Factories => _factories ??= new GenericRepository<Factory>(_context);

    private IGenericRepository<Alert> _alerts;
    public IGenericRepository<Alert> Alerts => _alerts ??= new GenericRepository<Alert>(_context);

    private IGenericRepository<Event> _events;
    public IGenericRepository<Event> Events => _events ??= new GenericRepository<Event>(_context);

    private IGenericRepository<Report> _reports;
    public IGenericRepository<Report> Reports => _reports ??= new GenericRepository<Report>(_context);

    private IGenericRepository<Line> _lines;
    public IGenericRepository<Line> Lines => _lines ??= new GenericRepository<Line>(_context);

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}